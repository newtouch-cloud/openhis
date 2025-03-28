using System;
using System.Threading;
using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices.PayManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using NewtouchPay;
using NewtouchPay.Models;


namespace Newtouch.HIS.Web.Areas.PayManage.Controllers
{   
    /// <summary>
    /// 
    /// </summary>
    public class MicroPayController : ControllerBase
    {
        private readonly IOrderPayInfoRepo _OrderPayInfoRepo;
        private readonly IPayDmnService _PayDmnService;
        private readonly IPayApp _PayApp;

        /// <summary>
        /// 付款码支付
        /// </summary>
        /// <returns></returns>
        public ActionResult MicroPay()
        {
            return View();
        }

        public ActionResult MicroPayQuery()
        {
            return View();
        }

        public ActionResult TradeInfo()
        {
            return View();
        }

        public ActionResult TradeRefund()
        {
            return View();
        }

        /// <summary>
        /// 条码支付（扫码付）
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="subject"></param>
        /// <param name="total_amount"></param>
        /// <param name="auth_code"></param>
        /// <returns></returns>
        public ActionResult MicroPaySubmit(string out_trade_no, string subject, string total_amount, string auth_code, string djjesszffs, string patid)
        {
            out_trade_no = this.UserIdentity.UserCode + "_" + out_trade_no;
            if (djjesszffs == "13")
            {
                var auth_codedata = new
                {
                    OutTradeNo = out_trade_no,
                    BarCode = auth_code,
                    TotalAmount = total_amount,
                    Subject = subject,
                    Body = subject,
                    Terminal = this.UserIdentity.UserCode,
                    PatientName = patid,
                    IDCardNo = "",
                    PatientID = patid,
                    Operation = subject,
                    PayType = 5,
                    StoreId = "",
                    SellerName = "隆安县城厢卫生院",
                    FacePay = false,
                    TxnTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                string datajson = Newtonsoft.Json.JsonConvert.SerializeObject(auth_codedata);

                CommmHelper commm = new CommmHelper();
                string nxresult = commm.NxPost(Newtouch.Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("NxPay") + "qrpay/qr-barcode-pay-request", datajson);


                if (!string.IsNullOrEmpty(nxresult))
                {
                    var nxpaymodel = Newtonsoft.Json.JsonConvert.DeserializeObject<QRBarPayOutput>(nxresult);

                    if (nxpaymodel.Result == "0")
                    {
                        PayResultModel payresult = new PayResultModel();
                        payresult.code = ResponseResultCode.SUCCESS;
                        payresult.TradeNo = nxpaymodel.tradeNo;
                        payresult.GmtTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        payresult.PayType = "2";
                        payresult.TradeStatus = "0";
                        payresult.PayAccount = total_amount;

                        OrderPayInfoEntity ety = new OrderPayInfoEntity();
                        ety.OutTradeNo = out_trade_no;
                        ety.Amount = Convert.ToDecimal(total_amount);
                        ety.TradeDesc = subject;

                        if (payresult != null)
                        {
                            if (payresult.code == ResponseResultCode.SUCCESS)
                            {
                                ety.TradeNo = payresult.TradeNo;
                                ety.PayStatus = (int)EnumPayStatus.Success;
                                ety.OrderDate = Convert.ToDateTime(payresult.GmtTime);
                                ety.PayType = Convert.ToInt32(djjesszffs);
                                ety.Memo = payresult.TradeStatus;
                                ety.PayAccount = payresult.PayAccount;
                                ety.OrganizeId = this.OrganizeId;

                                _OrderPayInfoRepo.SubmitInfo(ety);

                                var data = new
                                {
                                    outTradeNo = out_trade_no,    //商户订单号
                                    tradeNo = payresult.TradeNo,   //第三方交易号
                                    payType = payresult.PayType,    //支付方式
                                };

                                return Success(null, data);
                            }
                            else
                            {
                                ety.PayStatus = (int)EnumPayStatus.Failed;
                                ety.Memo = payresult.TradeStatus + ";" + payresult.sub_msg;
                                _OrderPayInfoRepo.SubmitInfo(ety);
                                return Error(payresult.msg + "," + payresult.sub_msg);
                            }
                        }
                    }
                }

            }
            else
            {
                PayResultModel payresult = TradeHelper.Pay(out_trade_no, subject, total_amount, auth_code);

                OrderPayInfoEntity ety = new OrderPayInfoEntity();
                ety.OutTradeNo = out_trade_no;
                ety.Amount = Convert.ToDecimal(total_amount);
                ety.TradeDesc = subject;

                if (payresult != null)
                {
                    if (payresult.code == ResponseResultCode.SUCCESS)
                    {
                        ety.TradeNo = payresult.TradeNo;
                        ety.PayStatus = (int)EnumPayStatus.Success;
                        ety.OrderDate = Convert.ToDateTime(payresult.GmtTime);
                        ety.PayType = Convert.ToInt32(payresult.PayType);
                        ety.Memo = payresult.TradeStatus;
                        ety.PayAccount = payresult.PayAccount;
                        ety.OrganizeId = this.OrganizeId;

                        _OrderPayInfoRepo.SubmitInfo(ety);

                        var data = new
                        {
                            outTradeNo = out_trade_no,    //商户订单号
                            tradeNo = DateTime.Now.Ticks,   //第三方交易号
                            payType = payresult.PayType,    //支付方式
                        };

                        return Success(null, data);
                    }
                    else
                    {
                        ety.PayStatus = (int)EnumPayStatus.Failed;
                        ety.Memo = payresult.TradeStatus + ";" + payresult.sub_msg;
                        _OrderPayInfoRepo.SubmitInfo(ety);
                        return Error(payresult.msg + "," + payresult.sub_msg);
                    }


                }

            }

            return Error("支付失败");

        }

        public ActionResult MicroTradeQuery(Pagination pagination, string ksrq, string jsrq, int? payType, int? payStatus, string keywords)
        {

            var data = new
            {
                rows = _PayDmnService.TradePayLsit(pagination,ksrq, jsrq, payType, payStatus, keywords, this.OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public ActionResult TradeRefundList(string outTradeNo,string tradeNo)
        {

            var data = new
            {
                rows = _PayDmnService.TradeRefundList(outTradeNo, tradeNo)
            };
            return Content(data.ToJson());
        }

        public ActionResult Opr_TradeRefund(string outTradeNo, decimal refundAmount,string Reason,string OutRequestNo=null)
        {
            bool? isTradeRefundError = null;
            if (!string.IsNullOrWhiteSpace(outTradeNo) && refundAmount > 0)   //需要原路退回
            {
                string errorMsg;
                var refundReuslt = _PayApp.TradeRefund(outTradeNo, refundAmount, Reason, OutRequestNo, out errorMsg);
                isTradeRefundError = refundReuslt == (int)EnumRefundStatus.Failed || refundReuslt == (int)EnumRefundStatus.UnKnown;    //失败 或 未知
                if (isTradeRefundError==true)
                {
                    return Error("退款失败，请重试");
                }
                else
                {
                    return Success("退款成功");
                }
            }
            return Error("请求失败，请检查数据");
        }

        public ActionResult GetTradeInfo(string outTradeNo)
        {
            var tradeEty = _PayDmnService.TradePayInfobyNo(outTradeNo, "");
            if (tradeEty != null)
            {
                TradeInfoResultModel qureyResult = new TradeInfoResultModel();
                if (tradeEty.PayType == (int)Infrastructure.EnumTradeType.Alipay_Bar_Code )
                {                  
                    qureyResult = TradeHelper.TradeQuery(outTradeNo, tradeEty.TradeNo, (int)Infrastructure.EnumTradeType.Alipay_Bar_Code);
                }
                else if(tradeEty.PayType== (int)Infrastructure.EnumTradeType.Wechat_MICROPAY)
                {
                    qureyResult = TradeHelper.TradeQuery(outTradeNo, tradeEty.TradeNo, (int)Infrastructure.EnumTradeType.Wechat_MICROPAY);
                }

                if (qureyResult != null)
                {
                    tradeEty.TradeStatus = qureyResult.TradeStatus;
                }
            }

            return Content(tradeEty.ToJson());
        }
    }
}