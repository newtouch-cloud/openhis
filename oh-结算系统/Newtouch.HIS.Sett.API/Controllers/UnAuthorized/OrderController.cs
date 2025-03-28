using Autofac;
using Newtonsoft.Json;
using Newtouch.Common.Web;
using Newtouch.Core.Redis;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.HIS.API.Common.Models;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Proxy.Log;
using Newtouch.HIS.Sett.Request;
using Newtouch.HIS.Sett.Request.OrderCenter;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Newtouch.HIS.Sett.API.Controllers
{

    [RoutePrefix("api/OrderCenter")]
    public class OrderController : ApiControllerBase<OrderController>
    {
        private readonly IOutPatChargeApp _outPatChargeApp;
        private readonly IOrderDmnService _orderDmn;
        private readonly IOutPatientDmnService _outPatientDmnService;
        public OrderController(IComponentContext com)
            : base(com)
        {
        }

        private string GetAuth(string orgId, string AppId = "API")
        {
            var identity = new UserIdentity
            {
                AppId = AppId,
                TokenType = "",
                UserId = GetBookTerminal(AppId),
                Account = GetBookTerminal(AppId),
                OrganizeId = orgId,
                TopOrganizeId = orgId,
            };
            var key = Guid.NewGuid().ToString();
            RedisHelper.StringSet(key, JsonConvert.SerializeObject((object)identity), new TimeSpan(0, 20, 0));
            HttpContext.Current.Items[(object)"API_UserIdentity_Account"] = (object)identity.Account;
            return identity.Account;
        }
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="apireq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OrderQuery")]
        [IgnoreTokenDecrypt]
        public ResponseBase OrderQuery(BookingReqBase apireq)
        {

            GetAuth(apireq.OrganizeId, apireq.AppId);
            OrderRequest request = new OrderRequest
            {
                AppId = GetBookTerminal(apireq.AppId),
                Timestamp = apireq.Timestamp,
                VisitNo = apireq.paradata.ContainsKey("VisitNo") == true ? apireq.paradata["VisitNo"] : null,
                CardNo = apireq.paradata.ContainsKey("CardNo") == true ? apireq.paradata["CardNo"] : null,
                OrderNo = apireq.paradata.ContainsKey("OrderNo") == true ? apireq.paradata["OrderNo"] : null,
                OrderStu = apireq.paradata.ContainsKey("OrderStu") == true && apireq.paradata["OrderStu"]!=null ? Convert.ToInt32(apireq.paradata["OrderStu"]) : -1,
                OrderType = apireq.paradata.ContainsKey("OrderType") == true && apireq.paradata["OrderType"] != null ? Convert.ToInt32(apireq.paradata["OrderType"]) : -1
            };
            Action<OrderRequest, DefaultResponse> ac = (req, resp) =>
            {
                OrderInfoBaseVO vo = new OrderInfoBaseVO
                {
                    OrderNo = request.OrderNo,
                    CardNo = request.CardNo,
                    VisitNo = request.VisitNo,
                    OrderType = request.OrderType,
                    OrderStu = request.OrderStu
                };
                var data = _orderDmn.OrderQuery(vo, apireq.OrganizeId);
                if (data != null && data.Count > 0)
                {
                    resp.data = data.Select(p=>new {
                        CardNo=p.CardNo,
                        VisitNo=p.VisitNo,
                        OrderNo=p.OrderNo,
                        OrderStu=p.OrderStu,
                        OrderType=p.OrderType,
                        OrderAmt=p.OrderAmt,
                        PayFee=p.PayFee,
                        PayLsh=p.PayLsh,
                        PayTime=p.PayTime,
                        SettTradeNo=p.SettTradeNo,
                        CreateTime=p.CreateTime,
                        CreatorCode=p.CreatorCode
                    });
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };
            return base.CommonExecute(ac, request);
        }
        /// <summary>
        /// 查询订单明细
        /// </summary>
        /// <param name="apireq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OrderDetailQuery")]
        [IgnoreTokenDecrypt]
        public ResponseBase OrderDetailQuery(BookingReqBase apireq)
        {

            GetAuth(apireq.OrganizeId, apireq.AppId);
            OrderRequest request = new OrderRequest
            {
                AppId = GetBookTerminal(apireq.AppId),
                Timestamp = apireq.Timestamp,
                OrderNo = apireq.paradata.ContainsKey("OrderNo") == true ? apireq.paradata["OrderNo"] : null,
            };
            Action<OrderRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _orderDmn.OrderDetailQuery(request.OrderNo, apireq.OrganizeId, request.AppId);
                if (data != null)
                {
                    resp.data = data;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };
            return base.CommonExecute(ac, request);
        }
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="apireq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OrderCreate")]
        [IgnoreTokenDecrypt]
        public ResponseBase OrderCreate(BookingReqBase apireq)
        {

            GetAuth(apireq.OrganizeId, apireq.AppId);
            OrderCreateRequest request = new OrderCreateRequest
            {
                AppId = GetBookTerminal(apireq.AppId),
                Timestamp = apireq.Timestamp,
                VisitNo = apireq.paradata.ContainsKey("VisitNo") == true ? apireq.paradata["VisitNo"] : null,
                CardNo = apireq.paradata.ContainsKey("CardNo") == true ? apireq.paradata["CardNo"] : null,
                OrderType = apireq.paradata.ContainsKey("OrderType") == true ? Convert.ToInt32(apireq.paradata["OrderType"]) : -1
            };
            Action<OrderCreateRequest, DefaultResponse> ac = (req, resp) =>
           {
               var data = _orderDmn.OrderCreateMz(request.CardNo, request.VisitNo, apireq.OrganizeId, request.AppId);
               if (data != null)
               {
                   resp.data = data;
                   resp.code = ResponseResultCode.SUCCESS;
               }
               else
               {
                   resp.code = ResponseResultCode.FAIL;
                   resp.msg = "未找到相关记录";
               }
           };

            return base.CommonExecute(ac, request);
        }
        /// <summary>
        /// 申请锁定订单
        /// </summary>
        /// <param name="apireq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LockerApply")]
        [IgnoreTokenDecrypt]
        public ResponseBase OrderLockerApply(BookingReqBase apireq)
        {

            GetAuth(apireq.OrganizeId, apireq.AppId);
            OrderRequest request = new OrderRequest
            {
                AppId = GetBookTerminal(apireq.AppId),
                Timestamp = apireq.Timestamp,
                CardNo = apireq.paradata.ContainsKey("CardNo") == true ? apireq.paradata["CardNo"] : null,
                OrderNo = apireq.paradata.ContainsKey("OrderNo") == true ? apireq.paradata["OrderNo"] : null,
                OrderAmt = apireq.paradata.ContainsKey("OrderAmt") == true ? Convert.ToDecimal(apireq.paradata["OrderAmt"]) : -1
            };
            Action<OrderRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _orderDmn.LockOrderApply(request.OrderNo, request.OrderAmt, request.CardNo, request.AppId, apireq.OrganizeId);
                if (data != null)
                {
                    resp.data = data;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };

            return base.CommonExecute(ac, request);
        }
        /// <summary>
        /// 申请延时锁定订单
        /// </summary>
        /// <param name="apireq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LockerDelayedApply")]
        [IgnoreTokenDecrypt]
        public ResponseBase LockOrderDelayedApply(BookingReqBase apireq)
        {

            GetAuth(apireq.OrganizeId, apireq.AppId);
            OrderRequest request = new OrderRequest
            {
                AppId = GetBookTerminal(apireq.AppId),
                Timestamp = apireq.Timestamp,
                OrderNo = apireq.paradata.ContainsKey("OrderNo") == true ? apireq.paradata["OrderNo"] : null,
            };
            Action<OrderRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _orderDmn.LockOrderDelayedApply(request.OrderNo, request.AppId, apireq.OrganizeId);
                if (data != null)
                {
                    resp.data = data;
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "未找到相关记录";
                }
            };

            return base.CommonExecute(ac, request);
        }

        /// <summary>
        /// 订单支付(自费)
        /// </summary>
        /// <param name="apireq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PayOrder")]
        [IgnoreTokenDecrypt]
        public ResponseBase PayOrder(BookingReqBase apireq)
        {
            GetAuth(apireq.OrganizeId, apireq.AppId);
            OrderRequest request = new OrderRequest
            {
                AppId = GetBookTerminal(apireq.AppId),
                Timestamp = apireq.Timestamp,
                CardNo = apireq.paradata.ContainsKey("CardNo") == true ? apireq.paradata["CardNo"] : null,
                VisitNo = apireq.paradata.ContainsKey("VisitNo") == true ? apireq.paradata["VisitNo"] : null,
                OrderNo = apireq.paradata.ContainsKey("OrderNo") == true ? apireq.paradata["OrderNo"] : null,
                PayWay = apireq.paradata.ContainsKey("PayWay") == true ? apireq.paradata["PayWay"] : null,
                PayFee = apireq.paradata.ContainsKey("PayFee") == true ? Convert.ToDecimal(apireq.paradata["PayFee"]) : -1,
                PayerId = apireq.paradata.ContainsKey("PayerId") == true ? apireq.paradata["PayerId"] : null,
                PayLsh = apireq.paradata.ContainsKey("PayLsh") == true ? apireq.paradata["PayLsh"] : null,
                PayTime = apireq.paradata.ContainsKey("PayTime") == true ? Convert.ToDateTime(apireq.paradata["PayTime"]) : DateTime.Now,
            };
            string msg = "";
            Action<OrderRequest, DefaultResponse> ac = (req, resp) =>
            {
                var orderInfoVo = new OrderInfoVO
                {
                    CardNo = request.CardNo,
                    VisitNo = request.VisitNo,
                    OrderNo = request.OrderNo,
                    PayFee = request.PayFee,
                    PayerId = request.PayerId,
                    PayLsh = request.PayLsh,
                    PayTime = request.PayTime,
                    PayWay = Convert.ToInt32(request.PayWay),
                    OrganizeId = apireq.OrganizeId
                };
                var ordermz = _orderDmn.PayForOutpBillOrderBefore(orderInfoVo, request.AppId);
                IList<int> jsnmList = null;
                if (ordermz != null && ordermz.info != null && ordermz.mx.Count() > 0)
                {
                    //获取虚拟发票号
                    //var fph = _outPatientDmnService.GetInvoiceListByEmpNo(request.AppId, apireq.OrganizeId);

                    IList<int> cfnmList = ordermz.mx.Select(p => Convert.ToInt32(p.cfxmmx)).Distinct().ToList();
                    //结算
                    var sett = _outPatChargeApp.submitOutpatCharge(new Domain.DTO.InputDto.BasicInfoDto2018
                    {
                        mzh = ordermz.info.VisitNo,
                        sfrq = request.PayTime,
                        isQfyj = false,
                        ys = request.AppId,
                        fph = null
                    }, new Domain.DTO.OutpatientSettFeeRelatedDTO
                    {
                        djjess = request.PayFee,
                        djjesszffs = request.PayWay,
                        ssk = request.PayFee,
                        xjzfys = ordermz.info.OrderAmt,
                        zje = request.PayFee
                    }, null, null, null, apireq.OrganizeId, cfnmList, out jsnmList, null, request.PayLsh);
                    orderInfoVo.SettTradeNo = jsnmList != null ? jsnmList[0].ToString() : "";
                    //更新订单状态
                    _orderDmn.OrderPaidSuccess(orderInfoVo, request.AppId, apireq.OrganizeId);

                    #region 结算成功推送
                    var cfList = ordermz.mx.Select(p => p.cfh).ToList();
                    msg = _orderDmn.PushPresInfo(cfnmList, cfList, ordermz.info.VisitNo, jsnmList[0], null, request.PayTime, request.AppId, apireq.OrganizeId, ordermz.cflx ?? 0);
                    #endregion
                }
                if (jsnmList != null)
                {
                    resp.data = jsnmList[0];
                    resp.code = ResponseResultCode.SUCCESS;
                    resp.msg = msg;
                }
                else
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "结算异常，请联系管理员：" + msg;
                }
            };

            return base.CommonExecute(ac, request);
        }

        /// <summary>
        /// 申请延时锁定订单
        /// </summary>
        /// <param name="apireq"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("test")]
        [IgnoreTokenDecrypt]
        public ResponseBase test(BookingReqBase apireq)
        {

            GetAuth(apireq.OrganizeId, apireq.AppId);
            OrderRequest request = new OrderRequest
            {
                AppId = GetBookTerminal(apireq.AppId),
                Timestamp = apireq.Timestamp,
                CardNo = apireq.paradata.ContainsKey("CardNo") == true ? apireq.paradata["CardNo"] : null,
                VisitNo = apireq.paradata.ContainsKey("VisitNo") == true ? apireq.paradata["VisitNo"] : null,
                OrderNo = apireq.paradata.ContainsKey("OrderNo") == true ? apireq.paradata["OrderNo"] : null,
                PayWay = apireq.paradata.ContainsKey("PayWay") == true ? apireq.paradata["PayWay"] : null,
                PayFee = apireq.paradata.ContainsKey("PayFee") == true ? Convert.ToDecimal(apireq.paradata["PayFee"]) : -1,
                PayerId = apireq.paradata.ContainsKey("PayerId") == true ? apireq.paradata["PayerId"] : null,
                PayLsh = apireq.paradata.ContainsKey("PayLsh") == true ? apireq.paradata["PayLsh"] : null,
                PayTime = apireq.paradata.ContainsKey("PayTime") == true ? Convert.ToDateTime(apireq.paradata["PayTime"]) : DateTime.Now,
            };
            Action<OrderRequest, DefaultResponse> ac = (req, resp) =>
            {
                var cfInfo = _orderDmn.OrderDetailQuery(request.OrderNo, apireq.OrganizeId, request.AppId);
                var cfnmList = cfInfo.Select(p => Convert.ToInt32(p.cfxmmx)).Distinct().ToList();
                var cfList = cfInfo.Select(p => p.cfh).Distinct().ToList();
                _orderDmn.PushPresInfo(cfnmList, cfList, cfInfo.FirstOrDefault().cfh, 88514, null, request.PayTime, request.AppId, apireq.OrganizeId);

            };

            return base.CommonExecute(ac, request);
        }

        #region privare method
        /// <summary>
        /// 获取请求来源
        /// </summary>
        /// <param name="AppID"></param>
        /// <returns></returns>
        private string GetBookTerminal(string AppID)
        {
            if (AppID.Contains("SelfTerminal"))
            {
                return EnumMzghly.SelfTerminal.ToString();
            }
            return EnumMzghly.His.ToString();
        }



        #endregion
    }
}
