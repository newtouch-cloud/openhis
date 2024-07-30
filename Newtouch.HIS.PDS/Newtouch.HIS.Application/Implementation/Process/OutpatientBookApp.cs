using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.VO;
using Newtouch.HIS.DomainServices;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Log;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 资源预定
    /// </summary>
    public class OutpatientBookApp : ProcessorFun<OutpatientBookRequestDTO>
    {
        private readonly IDispensingDmnService _dispensingDmnService;
        private readonly IPyDmnService _pyDmnService;
        private readonly IResourcesOperateApp _resourcesOperateApp;
        private readonly ISysMedicineRepo _sysMedicineRepo;
        private readonly FrameworkBase.MultiOrg.Domain.IDomainServices.IBaseDataDmnService _baseDataDmnService;
        private readonly IOutpatientPrescriptionDetailBatchNumberRepo _mxphRepo;

        /// <summary>
        /// book失败项目，返回给请求方
        /// </summary>
        private readonly List<FailItemDetail> _failList;
        /// <summary>
        /// book成功项目， 后期还原
        /// </summary>
        private readonly List<BookItemDo> _successList;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="request"></param>
        public OutpatientBookApp(OutpatientBookRequestDTO request) : base(request)
        {
            _failList = new List<FailItemDetail>();
            _successList = new List<BookItemDo>();
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request.Items == null || Request.Items.Count == 0) throw new FailedException("项目/药品不能为空");
            if (string.IsNullOrWhiteSpace(Request.OrganizeId)) throw new FailedException("OrganizeId(组织结构代码) 必传");
            try
            {
                Parallel.ForEach(Request.Items, p =>
                {
                    if (string.IsNullOrWhiteSpace(p.ItemCode)) throw new FailedException("ItemCode(项目编号) 必传。");
                    if (string.IsNullOrWhiteSpace(p.Cfh)) throw new FailedException("Cfh(处方号) 必填。");
                    if (string.IsNullOrWhiteSpace(p.CardNo)) throw new FailedException("CardNo(卡号) 必填。");
                    var rpCount = new fyDmnService(new DefaultDatabaseFactory(), false).ExistEffectiveRp(p.Cfh, p.Yfbm, Request.OrganizeId);
                    if (rpCount > 0) throw new Exception("已存在处方【" + p.Cfh + "】，不可重复提交。");
                    var ypxx = new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(Request.OrganizeId,
                        p.ItemCode);
                    if (ypxx == null) throw new Exception("未找到项目【" + p.ItemName + "】。");
                    if (p.Zhyz <= 0)
                    {
                        if (ypxx.mzcls == null) throw new Exception("项目【" + p.ItemName + "】门诊拆零数为空。");
                        p.Zhyz = (int)ypxx.mzcls;
                    }

                    p.ItemSpecifications = string.IsNullOrWhiteSpace(p.ItemSpecifications) ? ypxx.ypgg : p.ItemSpecifications;
                    p.ItemManufacturer = string.IsNullOrWhiteSpace(p.ItemManufacturer) ? ypxx.ycmc : p.ItemManufacturer;
                });
            }
            catch (Exception e)
            {
                throw new FailedException(e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
            }
            return new ActResult();
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected override void Action(ActResult actResult)
        {
            var errorMsg = new StringBuilder();
            Parallel.ForEach(Request.Items, p =>
            {
                var bookItem = new BookItemDo
                {
                    YpCode = p.ItemCode,
                    Sl = (int)p.ItemCount * p.Zhyz,
                    Yfbm = p.Yfbm ?? "",
                    OrganizeId = Request.OrganizeId ?? "",
                    Cfh = p.Cfh ?? "",
                    CreatorCode = Request.CreatorCode ?? "",
                    czh = p.GroupNum
                };
                var result = new DispensingDmnService(new DefaultDatabaseFactory(), false).OutPatientBook(bookItem);
                if (string.IsNullOrWhiteSpace(result))
                {
                    _successList.Add(bookItem);
                }
                else
                {
                    var failItem = p.ToJson().ToObject<FailItemDetail>();
                    if (failItem == null) return;
                    failItem.FailMsg = result;
                    errorMsg.Append(result + " ");
                    _failList.Add(failItem);
                }
            });

            if (_failList.Count <= 0) return;
            actResult.Data = _failList;
            actResult.IsSucceed = false;
            actResult.ResultMsg = errorMsg.ToString();
            if (_successList.Any())
            {
                //还原 解冻
                OutPatientBookCancel();
            }
        }

        /// <summary>
        /// 后处理
        /// </summary>
        /// <param name="actResult"></param>
        protected override void AfterAction(ActResult actResult)
        {
            try
            {
                //插入 mz_cf、mz_cfmx 表
                var mzcfxx = new List<MzCfEntity>();
                var mzcfmx = new List<MzCfmxEntity>();
                Parallel.For(0, 2, i => //组装数据
                {
                    switch (i)
                    {
                        case 0:
                            mzcfxx = AssembleMzCfEntity();
                            break;
                        case 1:
                            mzcfmx = AssembleMzCfmxEntity();
                            break;
                    }
                });
                _pyDmnService.InsertOutpatientRpInfo(mzcfxx, mzcfmx);
            }
            catch (Exception e)
            {
                //还原 解冻
                OutPatientBookCancel();
                LogCore.Error("OutpatientBookApp AfterAction InsertOutpatientRpInfo error", e);
            }
        }

        /// <summary>
        /// 取消门诊预定
        /// </summary>
        private void OutPatientBookCancel()
        {
            try
            {
                _successList.ForEach(p =>
                {
                    using (var db = new EFDbTransaction(new DefaultDatabaseFactory()).BeginTrans())
                    {
                        var sql1 = @"SELECT * FROM dbo.mz_cfmxph WHERE cfh=@cfh AND zt='1' AND gjzt='0' AND yp=@ypCode AND fyyf=@yfbmCode AND OrganizeId=@OrganizeId ";
                        var param1 = new DbParameter[] { new SqlParameter("@cfh", p.Cfh), new SqlParameter("@ypCode", p.YpCode), new SqlParameter("@yfbmCode", p.Yfbm), new SqlParameter("@OrganizeId", p.OrganizeId) };
                        var mxph = db.FindList<OutpatientPrescriptionDetailBatchNumberEntity>(sql1, param1);
                        if (mxph == null || mxph.Count == 0) return;
                        var dd = new List<MzcfphxxVO>();
                        foreach (var o in mxph)
                        {
                            o.zt = "0";
                            o.gjzt = "1";
                            db.Update(o);

                            var it = dd.Find(q => q.ypCode == o.yp && q.OrganizeId == o.OrganizeId && q.pc == o.pc && q.ph == o.ph && q.cfh == o.cfh && q.yfbmCode == o.fyyf);
                            if (it == null)
                            {
                                dd.Add(new MzcfphxxVO
                                {
                                    OrganizeId = o.OrganizeId,
                                    pc = o.pc,
                                    ph = o.ph,
                                    sl = (int)o.sl,
                                    yfbmCode = o.fyyf,
                                    ypCode = o.yp,
                                    cfh = o.cfh
                                });
                            }
                            else
                            {
                                it.sl += (int)o.sl;
                            }
                        }
                        if (dd.Count == 0) return;

                        foreach (var o in dd)
                        {
                            var sql2 = @"SELECT * FROM dbo.xt_yp_kcxx WHERE ypdm=@ypCode AND OrganizeId=@OrganizeId AND pc=@pc AND ph=@ph AND yfbmCode=@yfbmCode AND zt='1'";
                            var param2 = new DbParameter[]
                            {
                                new SqlParameter("@ypCode",o.ypCode),
                                new SqlParameter("@OrganizeId", o.OrganizeId),
                                new SqlParameter("@pc",o.pc),
                                new SqlParameter("@ph", o.ph),
                                new SqlParameter("@yfbmCode",o.yfbmCode)
                            };
                            var kcxx = db.FindList<SysMedicineStockInfoEntity>(sql2, param2);
                            if (kcxx == null || kcxx.Count == 0) continue;
                            if (kcxx.Sum(i => i.djsl) >= o.sl)
                            {
                                var sysl = o.sl;
                                foreach (var k in kcxx)
                                {
                                    if (sysl <= 0) break;

                                    if (k.djsl >= sysl)
                                    {
                                        k.djsl -= sysl;
                                        sysl = 0;
                                    }
                                    else
                                    {
                                        sysl -= k.djsl;
                                        k.djsl = 0;
                                    }
                                    db.Update(k);
                                }
                            }
                            else
                            {
                                throw new FailedException(string.Format("处方【{0}】的药品【{1}】已冻结数小于将要取消的冻结数，取消冻结失败", o.cfh, o.ypCode));
                            }
                        }

                        db.Commit();
                    }
                });
            }
            catch (Exception e)
            {
                LogCore.Error("OutPatientBookCancel error", e, string.Format("_successList:{0}", _successList.ToJson()));
            }
        }

        /// <summary>
        /// 组装门诊处方信息
        /// </summary>
        /// <returns></returns>
        private List<MzCfEntity> AssembleMzCfEntity()
        {
            var cfhs = Request.Items.Select(p => p.Cfh).Distinct();
            return (from cfh in cfhs
                    let cfxx = Request.Items.Find(p => p.Cfh == cfh)
                    select new MzCfEntity
                    {
                        cfh = cfh,
                        jsnm = 0,
                        CardNo = cfxx.CardNo ?? "",
                        xm = cfxx.xm ?? "",
                        Fph = "",
                        nl = cfxx.nl,
                        brxzmc = cfxx.brxzmc,
                        ysmc = cfxx.ysmc,
                        ksmc = cfxx.ksmc,
                        je = Request.Items.FindAll(p => p.Cfh == cfh).Sum(s => s.ItemCount * s.ItemUnitPrice),
                        OrganizeId = Request.OrganizeId,
                        lyyf = cfxx.Yfbm,
                        fybz = ((int)EnumFybz.Yp).ToString(),
                        zt = "1",
                        CreateTime = DateTime.Now,
                        CreatorCode = Request.CreatorCode
                    }).ToList();
        }

        /// <summary>
        /// 组装门诊处方明细信息
        /// </summary>
        /// <returns></returns>
        private List<MzCfmxEntity> AssembleMzCfmxEntity()
        {
            var result = new List<MzCfmxEntity>();
            Request.Items.ForEach(p =>
            {
                result.Add(new MzCfmxEntity
                {
                    cfh = p.Cfh,
                    ypCode = p.ItemCode,
                    ypmc = p.ItemName ?? "",
                    gg = p.ItemSpecifications,
                    sl = (int)p.ItemCount,
                    dw = p.ItemUnitName ?? "",
                    dj = p.ItemUnitPrice / p.Zhyz,
                    ycmc = p.ItemManufacturer,
                    je = p.ItemCount * p.ItemUnitPrice,
                    jl = p.Dosage,
                    jldw = p.DosageUnit,
                    yfmc = p.UsageName,
                    bz = p.Remark,
                    zhyz = p.Zhyz,
                    czh = p.GroupNum,
                    OrganizeId = Request.OrganizeId,
                    CreatorCode = Request.CreatorCode,
                    CreateTime = DateTime.Now,
                    zt = "1"
                });
            });
            return result;
        }
    }
}
