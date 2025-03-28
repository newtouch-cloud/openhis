using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Redis;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.HospitalizationManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices
{
    public class OrderDmnService : DmnServiceBase, IOrderDmnService
    {
        private readonly IOrderInfoRepo _orderInfoRepo;
        private readonly IOrderMzRepo _orderMzRepo;
        private readonly IOrderZyRepo _orderZyRepo;
        private readonly IOutPatChargeDmnService _outChargeDmnService;
        private readonly ISysCashPaymentModelRepo _sysForCashPayRepository;
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly IOutpatientPrescriptionRepo _outpatientPrescriptionRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IDischargeSettleDmnService _dischargeSettleDmnService;
        private readonly IPatientCenterDmnService _patientCenterDmnService;

        private readonly string _orderCache = "sett_orderno_";
        private readonly string _orderCacheKey = $"{DateTime.Now.ToString("yyyyMMdd")}";
        private readonly string _LocdedOrderExpiredMinute = ConfigurationManager.AppSettings["LocdedOrderExpiredMinute"];
        public OrderDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            _orderCacheKey = $"{_orderCache}{DateTime.Now.ToString("yyyyMMdd")}";
        }
        private static int cnt = 0;
        //获取当天最大订单号
        public int GetOrderNoToday()
        {
            DateTime today = Convert.ToDateTime(DateTime.Now.ToDateString());
            var orderno = _orderInfoRepo.IQueryable(p => p.CreateTime >= today && p.zt == "1").OrderByDescending(p => p.OrderNo).FirstOrDefault();
            if (orderno != null)
            {
                return Convert.ToInt32(orderno.OrderNo.Substring(orderno.OrderNo.Length - 5, 5));
            }
            return 1;
        }
        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public string OrderNoGen(int orderType)
        {
            string valueCache = RedisHelper.StringGet(_orderCacheKey);
            if (!string.IsNullOrWhiteSpace(valueCache))
            {
                cnt = Convert.ToInt32(valueCache);
            }
            else
            {
                cnt = GetOrderNoToday(); //获取数据库当天最大订单号
            }
            cnt++;
            RedisHelper.StringSet(_orderCacheKey, cnt.ToString(), new TimeSpan(2, 0, 0, 0));//两天有效期

            return $"{orderType}{DateTime.Now.ToString("yyyyMMddHHmm")}{cnt.ToString().PadLeft(5).Replace(' ', '0')}";
        }
        /// <summary>
        /// 作废订单
        /// </summary>
        /// <param name="order"></param>
        /// <param name="appId"></param>
        public void OrderCancel(OrderInfoEntity order, string appId, string msg)
        {
            //作废订单
            order.OrderStu = (int)EnumOrderStatus.zf;
            order.Memo = order.Memo + $"|([{appId}]{msg}({DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}))";
            order.Modify();
            order.LastModifierCode = appId;
            _orderInfoRepo.Update(order);
            //throw new FailedException($"订单信息已过期，请重新创建订单");
        }
        public void OrderCancel(List<OrderInfoEntity> orderList, string appId, string msg)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                foreach (var order in orderList)
                {
                    //作废订单
                    order.OrderStu = (int)EnumOrderStatus.zf;
                    order.Memo = order.Memo + $"|([{appId}]{msg}({DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}))";
                    order.Modify();
                    order.LastModifierCode = appId;
                    //_orderInfoRepo.Update(order);
                    db.Update(order);
                }

                db.Commit();
            }
        }
        #region 订单查询&操作
        /// <summary>
        /// 通过VisitNo作废超时锁定订单
        /// </summary>
        /// <param name="visitNo"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <param name="expiredList"></param>
        /// <returns></returns>
        public int ExpOrder(string visitNo, string orgId, string appId, List<OrderInfoEntity> expiredList = null)
        {
            //过期作废 5分钟超时
            var expConfig = string.IsNullOrWhiteSpace(_LocdedOrderExpiredMinute) ? 5 : Convert.ToInt32(_LocdedOrderExpiredMinute);
            DateTime sj = DateTime.Now.AddMinutes(-expConfig);
            if (expiredList == null || expiredList.Count == 0)
            {
                expiredList = _orderInfoRepo.IQueryable(p => p.VisitNo == visitNo && p.OrderStu == (int)EnumOrderStatus.zfz && p.LastModifyTime < sj && p.OrganizeId == orgId && p.zt == "1").ToList();
            }
            if (expiredList != null && expiredList.Count() > 0)
            {
                var expIds = expiredList.Select(p => p.Id).ToArray();
                string zfsql = $"update order_info set OrderStu={(int)EnumOrderStatus.zf},memo=isnull(memo,'')+'|过期取消订单锁定({appId}:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")})',LastModifierCode='{appId}',LastModifyTime=getdate() " +
                    $" where id in('{string.Join("','", expIds)}')";
                return this.ExecuteSqlCommand(zfsql);
            }
            return 0;
        }
        /// <summary>
        /// 门诊订单
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="cfmxData"></param>
        /// <param name="orderType"></param>
        private void OrderCreateExec(OrderInfoVO vo, List<PresSettleInfoVO> cfmxData, int orderType, string appId)
        {
            if (vo == null || cfmxData.Count == 0 || string.IsNullOrWhiteSpace(vo.OrderNo))
            {
                throw new FailedException($"订单创建失败！订单及明细信息不可为空。");
            }
            OrderInfoEntity infoEntity = new OrderInfoEntity();
            infoEntity.OrderNo = vo.OrderNo;
            infoEntity.CardNo = vo.CardNo;
            infoEntity.OrderType = (int)EnumOrderType.mz;
            infoEntity.OrderAmt = vo.OrderAmt.Value;
            infoEntity.OrderStu = (int)EnumOrderStatus.dzf;
            infoEntity.VisitNo = vo.VisitNo;
            infoEntity.OrganizeId = vo.OrganizeId;

            infoEntity.patid = vo.patId;
            infoEntity.dzId = vo.dzId;
            infoEntity.Create(true);
            infoEntity.CreatorCode = appId;
            _orderInfoRepo.Insert(infoEntity);

            List<OrderMzEntity> mxList = new List<OrderMzEntity>();
            foreach (var item in cfmxData)
            {
                var mxEntity = new OrderMzEntity();
                mxEntity.cfh = item.cfh;
                mxEntity.cfje = item.zje;
                mxEntity.OrderNo = vo.OrderNo;
                mxEntity.sfxm = item.sfxmCode;
                mxEntity.sfxmmc = item.sfxmmc;
                mxEntity.cfxmmx = item.cfnm.ToString();
                mxEntity.OrganizeId = vo.OrganizeId;
                mxEntity.mzh = vo.VisitNo;
                mxEntity.cflx = item.cflx.ToString();
                mxEntity.isyp = item.yzlx != "1" ? "0" : item.yzlx;
                mxEntity.Create(true);
                mxEntity.CreatorCode = appId;
                mxList.Add(mxEntity);
            }
            _orderMzRepo.Insert(mxList);
        }
        /// <summary>
        /// 住院医嘱订单
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="zymxData"></param>
        /// <param name="orderType"></param>
        private void OrderCreateExec(OrderInfoVO vo, List<HospFeeChargeCategoryGroupDetailVO> zymxData, int orderType, string appId)
        {
            if (vo == null || zymxData.Count == 0 || string.IsNullOrWhiteSpace(vo.OrderNo))
            {
                throw new FailedException($"订单创建失败！订单及明细信息不可为空。");
            }
            OrderInfoEntity infoEntity = new OrderInfoEntity();
            infoEntity.OrderNo = vo.OrderNo;
            infoEntity.CardNo = vo.CardNo;
            infoEntity.OrderType = (int)EnumOrderType.zy;
            infoEntity.OrderAmt = vo.OrderAmt.Value;
            infoEntity.OrderStu = (int)EnumOrderStatus.dzf;
            infoEntity.VisitNo = vo.VisitNo;
            infoEntity.OrganizeId = vo.OrganizeId;
            infoEntity.Create(true);
            infoEntity.CreatorCode = appId;
            _orderInfoRepo.Insert(infoEntity);

            List<OrderZyEntity> mxList = new List<OrderZyEntity>();
            foreach (var item in zymxData)
            {
                var mxEntity = new OrderZyEntity();
                mxEntity.zyh = vo.VisitNo;
                mxEntity.OrderNo = vo.OrderNo;
                mxEntity.dlcode = item.dlCode;
                mxEntity.dlmc = item.dlmc;
                mxEntity.sfxm = item.sfxm;
                mxEntity.sl = item.sl;
                mxEntity.dj = item.dj;
                mxEntity.je = item.je;
                mxEntity.jfdw = item.jfdw;
                mxEntity.sfxmmx = item.sfxmmc;
                mxEntity.zfxz = item.zfxzcode;
                mxEntity.zzfbz = item.zzfbzcode;
                mxEntity.OrganizeId = vo.OrganizeId;
                mxEntity.Create(true);
                mxEntity.CreatorCode = appId;
                mxList.Add(mxEntity);
            }
            _orderZyRepo.Insert(mxList);
        }
        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public List<OrderInfoVO> OrderQuery(OrderInfoBaseVO vo, string orgId)
        {
            if (vo == null || (string.IsNullOrWhiteSpace(vo.CardNo) && string.IsNullOrWhiteSpace(vo.CardNo)))
            {
                throw new FailedException("就诊信息不可为空");
            }
            List<SqlParameter> para = new List<SqlParameter>();
            string sql = @"select * from order_info with(nolock) where organizeid=@orgId and zt='1' ";
            para.Add(new SqlParameter("orgId", orgId));
            if (!string.IsNullOrWhiteSpace(vo.OrderNo))
            {
                sql += " and OrderNo=@OrderNo";
                para.Add(new SqlParameter("OrderNo", vo.OrderNo));
            }
            if (!string.IsNullOrWhiteSpace(vo.CardNo))
            {
                sql += " and cardno=@kh";
                para.Add(new SqlParameter("kh", vo.CardNo));
            }
            if (!string.IsNullOrWhiteSpace(vo.VisitNo))
            {
                sql += " and VisitNo=@VisitNo";
                para.Add(new SqlParameter("VisitNo", vo.VisitNo));
            }
            if (vo.OrderStu >= 0)
            {
                sql += " and OrderStu=@OrderStu";
                para.Add(new SqlParameter("OrderStu", vo.OrderStu));
            }
            if (vo.OrderType >= 0)
            {
                sql += " and OrderType=@OrderType";
                para.Add(new SqlParameter("OrderType", vo.OrderType));
            }
            sql += " order by OrderNo desc";
            return FindList<OrderInfoVO>(sql, para.ToArray());
        }
        //获取订单明细
        public OrderDetailVO OrderDetailQuery(string orderNo, string orgId, string appId)
        {
            OrderDetailVO detail = new OrderDetailVO();
            var orderInfo = _orderInfoRepo.FindEntity(p => p.OrderNo == orderNo && p.OrganizeId == orgId && p.zt == "1");
            if (orderInfo == null)
            {
                throw new FailedException("未找到有效订单信息");
            }
            if (orderInfo.OrderType == (int)EnumOrderType.mz)
            {
                detail.mzList = OrderDetailMzQuery(orderNo, orgId, appId);
            }
            if (orderInfo.OrderType == (int)EnumOrderType.zy)
            {
                detail.zyList = OrderDetailZyQuery(orderNo, orgId, appId);
            }
            return detail;
            //List<SqlParameter> para = new List<SqlParameter> {
            //    new SqlParameter("orgId",orgId),
            //    new SqlParameter("orderNo",orderNo)
            //};
            //string sql = @"select * from order_mz with(nolock) where orderno=@orderNo and organizeId=@orgId and zt='1'";
            //if (!string.IsNullOrWhiteSpace(appId))
            //{
            //    sql += " and CreatorCode=@appId ";
            //    para.Add(new SqlParameter("appId", appId));
            //}
            //return FindList<OrderMzVO>(sql, para.ToArray());
        }

        /// <summary>
        /// 获取门诊订单详情
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public List<OrderMzVO> OrderDetailMzQuery(string orderNo, string orgId, string appId)
        {
            List<SqlParameter> para = new List<SqlParameter> {
                new SqlParameter("orgId",orgId),
                new SqlParameter("orderNo",orderNo)
            };
            string sql = @"select a.OrderNo,a.mzh,a.cfh,a.cfje,b.cflxmc cfmc,a.cflx,b.ksmc,b.ysmc,b.brxz,a.sfxmmc,a.sfxm,a.cfxmmx
from order_mz a with(nolock) 
left join mz_cf b with(nolock) on a.cfh=b.cfh and a.OrganizeId=b.OrganizeId and b.zt='1' 
where a.orderno=@orderNo and a.organizeId=@orgId and a.zt='1'";
            if (!string.IsNullOrWhiteSpace(appId))
            {
                sql += " and a.CreatorCode=@appId ";
                para.Add(new SqlParameter("appId", appId));
            }
            return FindList<OrderMzVO>(sql, para.ToArray());
        }
        public List<OrderZyBaseVO> OrderDetailZyQuery(string orderNo, string orgId, string appId)
        {
            List<SqlParameter> para = new List<SqlParameter> {
                new SqlParameter("orgId",orgId),
                new SqlParameter("orderNo",orderNo)
            };
            string sql = @"select a.OrderNo,a.zyh,a.dlcode,a.dlmc,sum(a.je)je 
from order_zy a with(nolock)
where a.orderno=@orderNo and a.organizeId=@orgId and a.zt='1'
";
            if (!string.IsNullOrWhiteSpace(appId))
            {
                sql += " and a.CreatorCode=@appId ";
                para.Add(new SqlParameter("appId", appId));
            }
            sql += " group by a.OrderNo,a.zyh,a.dlcode,a.dlmc ";
            return FindList<OrderZyBaseVO>(sql, para.ToArray());
        }
        /// <summary>
        /// 创建门诊订单
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public OrderInfoBaseVO OrderCreateMz(string kh, string mzh, string orgId, string appId, int patId, string dzId)
        {
            string msg = string.Empty;
            //查询账单
            var cfmxData = _outChargeDmnService.GetNewAllUnSettedList(mzh, null, orgId);
            if (cfmxData == null || cfmxData.Count == 0)
            {
                throw new FailedException("未找到待支付账单");
            }
            else if (cfmxData.FirstOrDefault().kh != kh)
            {
                throw new FailedException("账单信息与就诊卡信息不一致");
            }
            decimal cfje = cfmxData.Sum(p => p.zje);
            string orderno = OrderNoGen((int)EnumOrderType.mz);
            //验证订单
            var extEty = _orderInfoRepo.FindEntity(p => p.OrderNo == orderno && p.OrganizeId == orgId);
            if (extEty != null)
            {
                throw new FailedException($"订单号生成异常（已存在：{orderno}），请重试");
            }
            //超时订单处理
            ExpOrder(mzh, orgId, appId);
            //查询已有有效订单：待支付、已支付、已锁定
            var unpayMxArr = cfmxData.Select(p => p.cfh).ToArray();
            var orderMz = _orderInfoRepo.IQueryable(p => p.VisitNo == mzh && p.CardNo == kh && p.OrderStu != (int)EnumOrderStatus.zf && p.OrganizeId == orgId && p.zt == "1");
            if (orderMz != null && orderMz.Count() > 0)
            {
                //订单明细准备
                var orderNoArr = orderMz.Select(p => p.OrderNo).ToArray();
                var orderMzMx = _orderMzRepo.IQueryable(p => orderNoArr.Contains(p.OrderNo) && p.OrganizeId == orgId && p.zt == "1");
                //已支付订单===>防止重复支付
                var paidOrderList = orderMz.Where(p => p.OrderStu == (int)EnumOrderStatus.yzf).Select(p => p.OrderNo).ToArray();
                if (paidOrderList.Count() > 0)
                {
                    var paidMxList = orderMzMx.Where(p => paidOrderList.Contains(p.OrderNo) && unpayMxArr.Contains(p.cfh));  //_orderMzRepo.IQueryable(p => paidOrderList.Contains(p.OrderNo) && unpayMxArr.Contains(p.cfh) && p.OrganizeId == orgId && p.zt == "1");
                    if (paidMxList.Count() > 0)
                    {
                        throw new FailedException($"部分处方已支付，请刷新待支付账单");
                    }
                }
                var lockOrderList = orderMz.Where(p => p.OrderStu == (int)EnumOrderStatus.zfz);
                if (lockOrderList.Count() > 0)
                {
                    var lockOrderNos = lockOrderList.Select(p => p.OrderNo).ToArray();
                    var lockMxList = orderMzMx.Where(p => lockOrderNos.Contains(p.OrderNo)).ToList();
                    var left = cfmxData.Select(p => new OrderMzVO { cfh = p.cfh, cfje = p.zje, sfxm = p.sfxmCode }).OrderBy(p => p.cfh).OrderBy(p => p.sfxm).ToList();
                    var right = lockMxList.Select(p => new OrderMzVO { cfh = p.cfh, cfje = p.cfje, sfxm = p.sfxm }).OrderBy(p => p.cfh).OrderBy(p => p.sfxm).ToList();
                    if (left.All(t => right.Any(b => b.cfh == t.cfh && b.cfje == t.cfje && b.sfxm == t.sfxm)) && left.Count() == right.Count())
                    {
                        //包含即一致
                        if (lockOrderList.FirstOrDefault().LastModifierCode == appId)
                        {
                            return lockOrderList.FirstOrDefault().MapperTo<OrderInfoEntity, OrderInfoBaseVO>();
                        }
                        //此处可加入窗口最高权限 可取消其他渠道锁定订单
                        throw new FailedException($"订单已在其他渠道支付中，请稍后重试。");
                    }
                    else if (!left.Any(t => right.Any(b => b.cfh == t.cfh && b.cfje == t.cfje && b.cfxmmx == t.cfxmmx)))
                    {

                    }
                    else
                    {
                        //不包含
                        if (lockOrderList.FirstOrDefault().LastModifierCode == appId)
                        {
                            OrderCancel(lockOrderList.ToList(), appId, "处方息更新，作废原订单");
                            //throw new FailedException($"处方信息已失效，请作废锁定订单后重新申请支付。", lockOrderNos.FirstOrDefault());
                        }
                        throw new FailedException($"订单已在其他渠道支付中，请稍后重试。");
                    }
                }
                var unpayOrderList = orderMz.Where(p => p.OrderStu == (int)EnumOrderStatus.dzf);
                if (unpayOrderList.Count() > 0)
                {
                    var unpayOrderNos = unpayOrderList.Select(p => p.OrderNo).ToArray();
                    var unpayMxList = orderMzMx.Where(p => unpayOrderNos.Contains(p.OrderNo)).Select(p => new OrderMzVO { cfh = p.cfh, cfje = p.cfje, sfxm = p.sfxm }).OrderBy(p => p.cfh).OrderBy(p => p.sfxm).ToList();
                    var unpayCf = cfmxData.Select(p => new OrderMzVO { cfh = p.cfh, cfje = p.zje, sfxm = p.sfxmCode }).OrderBy(p => p.cfh).OrderBy(p => p.sfxm).ToList();
                    if (unpayCf.All(t => unpayMxList.Any(b => b.cfh == t.cfh && b.cfje == t.cfje && b.sfxm == t.sfxm)) && unpayMxList.All(t => unpayCf.Any(b => b.cfh == t.cfh && b.cfje == t.cfje && b.sfxm == t.sfxm)))
                    {
                        return unpayOrderList.FirstOrDefault().MapperTo<OrderInfoEntity, OrderInfoBaseVO>();
                    }
                    else if (!unpayCf.Any(t => unpayMxList.Any(b => b.cfh == t.cfh && b.cfje == t.cfje && b.cfxmmx == t.cfxmmx)))
                    {

                    }
                    else
                    {
                        OrderCancel(unpayOrderList.ToList(), appId, "处方息更新，作废原订单");
                        //throw new FailedException($"处方信息已失效，请作废订单后重新申请支付。", unpayOrderNos.FirstOrDefault());
                    }
                }

            }
            var orderVo = new OrderInfoVO
            {
                CardNo = kh,
                VisitNo = mzh,
                OrganizeId = orgId,
                OrderNo = orderno,
                OrderAmt = cfmxData.Sum(p => p.zje),
                patId = patId,
                dzId = dzId

            };
            OrderCreateExec(orderVo, cfmxData.ToList(), (int)EnumOrderType.mz, appId);
            return orderVo.MapperTo<OrderInfoVO, OrderInfoBaseVO>();
        }
        #region 锁定
        public OrderInfoBaseVO LockOrderApply(string orderNo, decimal? orderAmt, string kh, string appId, string orgId)
        {
            var order = _orderInfoRepo.FindEntity(p => p.OrderNo == orderNo && p.OrderStu == (int)EnumOrderStatus.dzf && p.OrganizeId == orgId && p.zt == "1");
            if (order != null && !string.IsNullOrWhiteSpace(order.OrderNo))
            {
                if (order.CardNo == kh && order.OrderAmt == orderAmt)
                {
                    order.OrderStu = (int)EnumOrderStatus.zfz;
                    order.Memo = order.Memo + $"|(appid:{appId}锁定订单({DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}))";
                    order.Modify();
                    order.LastModifierCode = appId;
                    _orderInfoRepo.Update(order);
                    var expConfig = string.IsNullOrWhiteSpace(_LocdedOrderExpiredMinute) ? 5 : Convert.ToInt32(_LocdedOrderExpiredMinute);
                    return new OrderInfoBaseVO
                    {
                        OrderNo = orderNo,
                        OrderAmt = order.OrderAmt,
                        OrderStu = order.OrderStu,
                        LockExpTime = DateTime.Now.AddMinutes(-expConfig)
                    };
                }
                throw new FailedException($"订单信息异常或与就诊卡信息不符");
            }
            throw new FailedException($"未找到该订单，请重新确认订单状态");
        }
        /// <summary>
        /// 申请锁定延时
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="appId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public OrderInfoBaseVO LockOrderDelayedApply(string orderNo, string appId, string orgId)
        {
            var order = _orderInfoRepo.FindEntity(p => p.OrderNo == orderNo && p.OrderStu == (int)EnumOrderStatus.zfz && p.LastModifierCode == appId && p.OrganizeId == orgId && p.zt == "1");
            if (order != null && !string.IsNullOrWhiteSpace(order.OrderNo))
            {
                order.Modify();
                order.LastModifierCode = appId;
                _orderInfoRepo.Update(order);
                var expConfig = string.IsNullOrWhiteSpace(_LocdedOrderExpiredMinute) ? 5 : Convert.ToInt32(_LocdedOrderExpiredMinute);
                return new OrderInfoBaseVO
                {
                    OrderNo = orderNo,
                    OrderAmt = order.OrderAmt,
                    OrderStu = order.OrderStu,
                    LockExpTime = DateTime.Now.AddMinutes(-expConfig)
                };
            }
            throw new FailedException($"锁定延时仅限本终端锁定订单，请重新确认订单状态");
        }
        /// <summary>
        /// 支付成功
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="appId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public OrderInfoBaseVO OrderPaidSuccess(OrderInfoVO vo, string appId, string orgId)
        {
            var order = _orderInfoRepo.FindEntity(p => p.OrderNo == vo.OrderNo && p.OrderStu == (int)EnumOrderStatus.zfz && p.LastModifierCode == appId && p.OrganizeId == orgId && p.zt == "1");
            if (order != null && !string.IsNullOrWhiteSpace(order.OrderNo))
            {
                order.OrderStu = (int)EnumOrderStatus.yzf;
                order.PayFee = vo.PayFee;
                order.PayLsh = vo.PayLsh;
                order.PayTime = vo.PayTime;
                order.PayWay = vo.PayWay;
                order.PayerId = vo.PayerId;
                order.SettTradeNo = vo.SettTradeNo;
                order.Memo = order.Memo + $"|(appid:{appId}支付订单({DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}))";
                order.Modify();
                order.LastModifierCode = appId;
                _orderInfoRepo.Update(order);
                return new OrderInfoBaseVO
                {
                    OrderNo = vo.OrderNo,
                    OrderAmt = order.OrderAmt,
                    OrderStu = order.OrderStu,
                };
            }
            throw new FailedException($"支付失败，请重新确认订单状态");
        }
        #endregion

        #endregion
        /// <summary>
        /// 支付前订单信息校验
        /// </summary>
        public OrderInfoEntity PayOrderBefore(OrderInfoVO vo, string appId)
        {
            SysCashPaymentModelEntity zffsEty = new SysCashPaymentModelEntity();
            zffsEty = _sysForCashPayRepository.FindEntity(p => p.xjzffs == vo.PayWay.ToString() && p.zt == "1");
            if (zffsEty == null)
            {
                throw new FailedException("暂不支持该支付方式！");
            }
            var order = _orderInfoRepo.FindEntity(p => p.OrderNo == vo.OrderNo && p.OrderStu == (int)EnumOrderStatus.zfz && p.OrganizeId == vo.OrganizeId && p.zt == "1");
            if (order == null)
            {
                throw new FailedException($"支付失败，订单信息已过期，请刷新订单状态");
            }
            else if (order.OrderAmt != vo.PayFee)
            {
                throw new FailedException($"订单金额与支付金额不符");
            }
            if (order == null || order.CardNo != vo.CardNo || (!string.IsNullOrWhiteSpace(vo.VisitNo) && order.VisitNo != vo.VisitNo))
            {
                throw new FailedException($"订单信息与就诊信息不符");
            }
            return order;
        }

        #region 门诊结算
        public OutpOrderVO PayForOutpBillOrderBefore(OrderInfoVO vo, string appId)
        {
            var order = PayOrderBefore(vo, appId);
            if (order.OrderType != (int)EnumOrderType.mz)
            {
                throw new FailedException($"非门诊订单");
            }
            //获取订单明细
            var orderMz = _orderMzRepo.IQueryable(p => p.OrderNo == order.OrderNo && p.OrganizeId == vo.OrganizeId && p.zt == "1").ToList();
            if (orderMz == null || orderMz.FirstOrDefault().mzh != order.VisitNo)
            {
                throw new FailedException($"订单明细异常，取消订单重试");
            }
            var orderMxh = orderMz.Select(p => p.cfxmmx).ToList();
            string cfSql = $"select * from mz_cf cf with(nolock) where cfnm in({string.Join(",", orderMxh)}) and (cf.zt is null or (cf.zt= '1' and cf.cfzt = '0'))  and exists(select 1 from order_mz b with(nolock) where cf.cfnm=b.cfxmmx and cf.zje=b.cfje) and cf.OrganizeId='{vo.OrganizeId}'";
            var cfList = FindList<PresSettleInfoVO>(cfSql);
            if (cfList.Count == 0 || cfList.Count != orderMz.Count)
            {
                //作废订单
                OrderCancel(order, appId, "支付中处方信息变更，遂作废订单");
                throw new FailedException($"订单信息已过期，请重新创建订单");
            }
            var cflxList = cfList.Select(p => p.cflx).Distinct().ToList();
            return new OutpOrderVO
            {
                info = order.MapperTo<OrderInfoEntity, OrderInfoVO>(),
                mx = orderMz.Select(p => new OrderMzVO { cfh = p.cfh, cfxmmx = p.cfxmmx, cfje = p.cfje }).ToList(),
                cflx = cflxList.Count > 1 ? 0 : cflxList.FirstOrDefault()
            };

        }

        /// <summary>
        /// 处方状态推送
        /// </summary>
        /// <param name="cfnmList"></param>
        /// <param name="cfList"></param>
        /// <param name="mzh"></param>
        /// <param name="jsnm"></param>
        /// <param name="fph"></param>
        /// <param name="sfrq"></param>
        /// <param name="appId"></param>
        /// <param name="orgId"></param>
        public string PushPresInfo(IList<int> cfnmList, List<string> cfList, string mzh, int jsnm, string fph, DateTime? sfrq, string appId, string orgId, int cflx = 0)
        {
            if (cfnmList != null && cfnmList.Count > 0 && cfList != null && cfList.Count > 0 && !string.IsNullOrWhiteSpace(mzh))
            {
                try
                {
                    var toCIS = _sysConfigRepo.GetBoolValueByCode("Outpatient_ChargeFee_AutoSyncPrescriptionStatus", orgId) ?? false;
                    if (toCIS)
                    {
                        if (cfList.Count > 0)
                        {

                            var reqObj = new
                            {
                                cfList = cfList.Select(p => new { cfh = p }).ToList(),
                                sfbz = true,
                                OrganizeId = orgId,
                                Appid = appId,
                                CreatorCode = appId,
                                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                            };
                            var apiResp = SiteCISAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                                "api/Prescription/UpdateChargeStatusApi", reqObj);
                            if (apiResp != null)
                            {
                                AppLogger.Instance.Error(string.Format("处方收费状态更新API同步至CIS，处方号：{0}，结果：{1}、{2}", string.Join(",", cfnmList), apiResp.code, apiResp.sub_code), null);
                            }
                            else
                            {
                                AppLogger.Instance.Error(string.Format("处方收费状态更新API同步至CIS，处方号：{0}，结果：未获取到响应，同步失败", string.Join(",", cfnmList)), null);
                            }
                        }
                        #region 接口版 暂不用
                        //string cfStr = string.Join(",", cfList);
                        //string cfmxStr = string.Join(",", cfnmList);
                        ////                    string ciscfSql = @" update Newtouch_CIS.dbo.xt_cf
                        //// set sfbz='1',LastModifierCode=@appId,LastModifyTime=getdate()
                        //// where OrganizeId=@orgId and zt='1' and cfh in(@cfList);
                        //// select @@ROWCOUNT result
                        ////";
                        //string ciscfSql = "exec sp_internal_cfsettInfo_sync @orgId=@orgId,@appId=@appId,@mzh=@mzh,@cfh=@cfh,@cfmx=@cfmx,@sfrq=@sfrq,@fph=@fph";
                        //var cfresult = FindList<int>(ciscfSql, new SqlParameter[] {
                        //    new SqlParameter("appId",appId),
                        //    new SqlParameter("orgId",orgId),
                        //    new SqlParameter("mzh",mzh),
                        //    new SqlParameter("cfh",cfStr),
                        //    new SqlParameter("cfmx",cfmxStr),
                        //    new SqlParameter("sfrq",sfrq),
                        //    new SqlParameter("fph",fph??""),
                        //});

                        #endregion

                        //异步推送处方收费成功的通知给药房药库
                        if (cflx == (int)EnumYiZhuXZ.YP || cflx == 0)
                        {
                            AppLogger.Info("");
                            notifyPDS(jsnm, sfrq ?? DateTime.Now, cfnmList, fph, orgId, appId);
                        }
                        _outpatientRegistRepo.RecordOutpId(mzh, "", appId, orgId);
                    }
                    return "";
                }
                catch (Exception ex)
                {
                    AppLogger.Instance.Error(string.Format("处方收费状态更新API同步至CIS，处方号：{0}，结果异常：{1}", string.Join(",", cfnmList), ex.Message + ";" + ex.InnerException.ToString()), null);
                    return "推送处方状态失败：" + ex.Message + ";" + ex.InnerException.ToString();
                }
            }
            return "推送处方状态失败：关键信息不可为空";
        }

        /// <summary>
        /// 异步推送处方收费成功的通知给药房药库
        /// </summary>
        /// <param name="cfnmList"></param>
        private void notifyPDS(int jsnm, DateTime sfrq, IList<int> cfnmList, string Fph, string orgId, string appId)
        {
            if (cfnmList == null || cfnmList.Count == 0)
            {
                return;
            }
            try
            {
                //cfnm cfh
                var ypCflx = (int)EnumPrescriptionType.Medicine;
                var ypcfList = _outpatientPrescriptionRepo.IQueryable(p => cfnmList.Contains(p.cfnm) && p.cflx == ypCflx).ToList();

                if (ypcfList.Count > 0)
                {
                    var creatorCode = appId;
                    var context = System.Web.HttpContext.Current;
                    var message = "";
                    foreach (var cf in ypcfList)
                    {
                        Task t=Task.Run(() =>
                        {
                            var reqObj = new
                            {
                                Jsnm = jsnm,
                                Sfsj = sfrq,
                                Cfh = cf.cfh,
                                Cfnm = cf.cfnm,
                                Fph = Fph,
                                OrganizeId = orgId,
                                CreatorCode = creatorCode,
                                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")//,
                                //Token = token
                            };
                            var apiResp = SitePDSAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/ResourcesOperate/OutpatientCommitApi", reqObj);
                            if (apiResp.code == APIRequestHelper.ResponseResultCode.SUCCESS)
                            {
                                message += "OutpatientCommit response:{ " + apiResp.code + ";" + apiResp.msg + "}\n\r";
                                message += apiResp != null
                                ? string.Format("HIS收费后药品推送至药房，处方号：{0}，结果：{1}、{2}", reqObj.Cfh, apiResp.code,
                                    apiResp.sub_code)
                                : string.Format("HIS收费后药品推送至药房，处方号：{0}，结果：未获取到响应，同步失败", reqObj.Cfh);
                                return;
                            }
                            else
                            {
                                message = apiResp.ToJson();
                            }

                        });
                        try
                        {
                            t.Wait();  //异常抛出到此处
                            AppLogger.Instance.Error(message, null);
                        }
                        catch (AggregateException aex)
                        {
                            AppLogger.Instance.Error(aex.Message, null);
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.Instance.Error($"日志记录异常：业务返回:" + ex.Message + ex.InnerException, null);
            }
        }
        #endregion

        #region 住院订单
        /// <summary>
        /// 生成住院患者待支付订单
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public OrderInfoBaseVO OrderCreateZy(string kh, string zyh, string orgId, string appId)
        {
            var patInfo = _patientCenterDmnService.InHosPatientBasic(zyh, null, null, orgId, kh);
            if (patInfo.Count == 0 || (patInfo.FirstOrDefault().zybz != ((int)EnumZYBZ.Djz).ToString() && patInfo.FirstOrDefault().zybz != ((int)EnumZYBZ.Ycy).ToString()))
            {
                throw new FailedException("请确认患者身份信息及在院状态，请办理出区后再进行结算");
            }
            else if (patInfo.FirstOrDefault().brxz != ((int)EnumBrxz.zf).ToString())
            {
                throw new FailedException("医保患者请至窗口办理出院结算");
            }
            var yzmxData = _dischargeSettleDmnService.GetHospGroupFeeDetailVOList(zyh, orgId, null);
            if (yzmxData == null || yzmxData.Count == 0)
            {
                throw new FailedException("未找到待支付账单");
            }
            decimal zje = yzmxData.Sum(p => p.je);
            //超时订单处理
            ExpOrder(zyh, orgId, appId);
            //查询已有有效订单：待支付、已支付、已锁定
            var orderInfo = _orderInfoRepo.IQueryable(p => p.VisitNo == zyh && p.CardNo == kh && p.OrderStu != (int)EnumOrderStatus.zf && p.OrganizeId == orgId && p.zt == "1");
            if (orderInfo != null && orderInfo.Count() > 0)
            {
                //订单明细准备
                var orderNoArr = orderInfo.Select(p => p.OrderNo).ToArray();
                var orderMx = _orderZyRepo.IQueryable(p => orderNoArr.Contains(p.OrderNo) && p.zyh == zyh && p.OrganizeId == orgId && p.zt == "1");
                var mxzje = orderMx.Sum(p => p.je);
                //已支付订单===>防止重复支付
                var paidOrderList = orderInfo.Where(p => p.OrderStu == (int)EnumOrderStatus.yzf).Select(p => p.OrderNo).ToArray();
                if (paidOrderList.Count() > 0)
                {
                    throw new FailedException($"部分账单已支付，请至收费窗口办理。");
                }
                var lockOrderList = orderInfo.Where(p => p.OrderStu == (int)EnumOrderStatus.zfz);
                if (lockOrderList.Count() > 0)
                {
                    var lockedAppInfo = lockOrderList.Where(p => p.LastModifierCode == appId).ToList().FirstOrDefault();
                    if (lockedAppInfo == null)
                    {
                        throw new FailedException($"订单已在其他渠道支付中，请稍后重试。");
                    }
                    if (zje == lockedAppInfo.OrderAmt && lockedAppInfo.OrderAmt == mxzje)
                    {
                        return lockedAppInfo.MapperTo<OrderInfoEntity, OrderInfoBaseVO>();
                    }
                    else
                    {
                        //待支付金额不一致，作废原订单
                        OrderCancel(lockedAppInfo, appId, "账单信息更新，作废原订单");
                    }
                }
                var unpayOrderList = orderInfo.Where(p => p.OrderStu == (int)EnumOrderStatus.dzf);
                if (unpayOrderList.Count() == 1)
                {
                    if (zje == unpayOrderList.FirstOrDefault().OrderAmt && mxzje == zje)
                    {
                        return unpayOrderList.FirstOrDefault().MapperTo<OrderInfoEntity, OrderInfoBaseVO>();
                    }
                    //待支付金额不一致，作废原订单
                    OrderCancel(unpayOrderList.FirstOrDefault(), appId, "账单信息更新，作废原订单");
                }
                else if (unpayOrderList.Count() > 1)
                {
                    //待支付金额不一致，作废原订单
                    OrderCancel(unpayOrderList.ToList(), appId, "账单信息更新，作废原订单");
                }
            }
            string orderno = OrderNoGen((int)EnumOrderType.zy);
            //验证订单
            var extEty = _orderInfoRepo.FindEntity(p => p.OrderNo == orderno && p.OrganizeId == orgId);
            if (extEty != null)
            {
                throw new FailedException($"订单号生成异常（已存在：{orderno}），请重试");
            }
            var orderVo = new OrderInfoVO
            {
                CardNo = kh,
                VisitNo = zyh,
                OrganizeId = orgId,
                OrderNo = orderno,
                OrderAmt = zje
            };
            //var yzmx = yzmxData.ToList().MapperTo<List<HospFeeChargeCategoryGroupDetailVO>, List<ZyPresSettleInfoVO>>();
            OrderCreateExec(orderVo, yzmxData.ToList(), (int)EnumOrderType.zy, appId);
            return orderVo.MapperTo<OrderInfoVO, OrderInfoBaseVO>();
        }

        public InHosOrderVO PayForInHosBillOrderBefore(OrderInfoVO vo, string appId)
        {
            var order = PayOrderBefore(vo, appId);
            if (order.OrderType != (int)EnumOrderType.zy)
            {
                throw new FailedException($"非住院订单");
            }
            var patInfo = _patientCenterDmnService.InHosPatientBasic(order.VisitNo, null, null, vo.OrganizeId);
            if (patInfo.Count == 0)
            {
                throw new FailedException($"患者信息异常，请联系管理员");
            }
            return new InHosOrderVO
            {
                info = order.MapperTo<OrderInfoEntity, OrderInfoVO>(),
                patient = patInfo.FirstOrDefault()
            };
        }
        #endregion

    }
}
