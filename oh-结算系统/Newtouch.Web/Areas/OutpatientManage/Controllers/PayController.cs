using System;
using System.Threading;
using System.Web.Mvc;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using NewtouchPay;
using NewtouchPay.Models;


namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{   
    /// <summary>
    /// 
    /// </summary>
    public class PayController : ControllerBase
    {
        private readonly IOrderPayInfoRepo _OrderPayInfoRepo;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <param name="subject"></param>
        /// <param name="total_amount"></param>
        /// <param name="auth_code"></param>
        /// <returns></returns>
        public ActionResult MicroPaySubmit(string out_trade_no, string subject, string total_amount, string auth_code)
        {
            PayResultModel payresult = TradeHelper.Pay(out_trade_no,subject,"0.01",auth_code);
            Thread.Sleep(1000 * 10);

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
                    ety.PayType = Convert.ToInt32(payresult.PayType) ;
                    ety.Memo = payresult.TradeStatus;
                    
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
                    ety.Memo = payresult.TradeStatus+";"+payresult.sub_msg;
                    _OrderPayInfoRepo.SubmitInfo(ety);
                    return Error(payresult.msg+","+payresult.sub_msg);
                }
            

            }

            return Error("支付失败");

        }


    }
}