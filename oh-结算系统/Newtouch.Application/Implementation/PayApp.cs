using FrameworkBase.MultiOrg.Application;
using Newtouch.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using NewtouchPay;
using NewtouchPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Application
{
    public class PayApp : AppBase, IPayApp
    {
        private readonly IOrderPayInfoRepo _orderPayInfoRepo;
        private readonly IOrderRefundInfoRepo _orderRefundInfoRepo;

        /// <summary>
        /// 退款（原路退回）
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <param name="refundAmount">金额</param>
        /// <param name="refundReason">退款原因</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public int TradeRefund(string outTradeNo, decimal refundAmount, string refundReason, string outRequestNo
            , out string errorMsg)
        {
            errorMsg = null;

            var refundEntity = new OrderRefundInfoEntity();
            refundEntity.OutTradeNo = outTradeNo;
            refundEntity.Amount = (refundAmount = Math.Abs(refundAmount));
            refundEntity.RefundReason = refundReason;
            if (!string.IsNullOrWhiteSpace(outRequestNo))
            {
                refundEntity.OutRequestNo = outRequestNo;
            }
            else
            {
                refundEntity.OutRequestNo = DateTime.Now.ToString("yyyyMMddHHmmssfff") + DateTime.Now.Ticks.ToString();

            }


            try
            {
                //1、获取可退金额 及 tradeNo 、退款方式
                var payEntity = _orderPayInfoRepo.GetSuccessRecordByOutTradeNo(outTradeNo);
                if (payEntity != null)
                {
                    refundEntity.TradeNo = payEntity.TradeNo;

                    //2、验证是否可退
                    var refundedSum = _orderRefundInfoRepo.GetRefundedSumByOutTradeNo(outTradeNo);
                    if (refundedSum + refundAmount <= payEntity.Amount)
                    {
                        //13聚合支付退费
                        if (payEntity.PayType == 13)
                        {
                            //N|BH1234567890133|20240402|0.01
                            string orderid = string.Format("N|{0}|{1}|{2}", payEntity.OutTradeNo, payEntity.TradeNo.Substring(0, 8), payEntity.Amount);
                            var auth_codedata = new
                            {
                                refundreason = refundReason,
                                terminal = this.UserIdentity.UserCode,
                                hospitalid = "隆安县城厢卫生院",
                                orderid = orderid,
                                sign = ""
                            };
                            string datajson = Newtonsoft.Json.JsonConvert.SerializeObject(auth_codedata);

                            CommmHelper commm = new CommmHelper();
                            string nxresult = commm.NxPost(Newtouch.Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("NxPay") + "system/refund-refundfee", datajson);
                            if (nxresult != null)
                            {
                                var nxpaymodel = Newtonsoft.Json.JsonConvert.DeserializeObject<RefundFeeOut>(nxresult);
                                if (nxpaymodel.Result == "0")
                                {
                                    refundEntity.RefundDate = DateTime.Now;
                                    refundEntity.RefundStatus = (int)EnumRefundStatus.Success;
                                    return refundEntity.RefundStatus;
                                }
                                else
                                {
                                    refundEntity.Memo = errorMsg = "失败，" + nxpaymodel.Message;
                                    refundEntity.RefundStatus = (int)EnumRefundStatus.Failed;
                                    return refundEntity.RefundStatus;
                                }
                            }
                            else
                            {
                                refundEntity.Memo = errorMsg = "退款原路退回进度未知，请人工核查";
                                refundEntity.RefundStatus = (int)EnumRefundStatus.UnKnown;
                                return refundEntity.RefundStatus;
                            }

                        }
                        else
                        {
                            //3、退
                            PayResultModel payresult = TradeHelper.TradeRefund(refundEntity.OutTradeNo, refundEntity.TradeNo, refundAmount.ToString("0.00"), refundReason, payEntity.PayType, "", refundEntity.OutRequestNo);

                            if (payresult.code == ResponseResultCode.SUCCESS)
                            {
                                DateTime refundDate;
                                if (DateTime.TryParse(payresult.GmtTime, out refundDate))
                                {
                                    refundEntity.RefundDate = refundDate;
                                }

                                refundEntity.RefundStatus = (int)EnumRefundStatus.Success;
                                return refundEntity.RefundStatus;
                            }
                            else
                            {
                                if ("UNKNOWN".Equals(payresult.sub_code, StringComparison.OrdinalIgnoreCase))
                                {
                                    refundEntity.Memo = errorMsg = "退款原路退回进度未知，请人工核查";
                                    refundEntity.RefundStatus = (int)EnumRefundStatus.UnKnown;
                                    return refundEntity.RefundStatus;
                                }
                                else
                                {
                                    refundEntity.Memo = errorMsg = "失败，" + payresult.msg + "," + payresult.sub_msg;
                                    refundEntity.RefundStatus = (int)EnumRefundStatus.Failed;
                                    return refundEntity.RefundStatus;
                                }
                            }
                        }
                    }
                    else
                    {
                        refundEntity.Memo = errorMsg = "失败，可退金额不足";
                        refundEntity.RefundStatus = (int)EnumRefundStatus.Failed;
                        return refundEntity.RefundStatus;
                    }
                }
                else
                {
                    refundEntity.Memo = errorMsg = "失败，原始支付记录未找到";
                    refundEntity.RefundStatus = (int)EnumRefundStatus.Failed;
                    return refundEntity.RefundStatus;
                }
            }
            catch (Exception ex)
            {
                refundEntity.Memo = errorMsg = "发生异常，" + ex.Message;
                refundEntity.RefundStatus = (int)EnumRefundStatus.UnKnown;
                return refundEntity.RefundStatus;
            }
            finally
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(outRequestNo))
                    {
                        refundEntity.Create();
                        refundEntity.Id = Guid.NewGuid().ToString();
                        refundEntity.zt = "1";
                        _orderRefundInfoRepo.Insert(refundEntity);
                    }
                    else
                    {
                        if (refundEntity.RefundStatus == (int)EnumRefundStatus.Success)
                        {
                            var ety = _orderRefundInfoRepo.FindEntity(p => p.OutRequestNo == outRequestNo);

                            if (ety != null)
                            {
                                ety.RefundStatus = refundEntity.RefundStatus;
                                _orderRefundInfoRepo.Update(ety);
                            }
                        }


                    }


                }
                catch (Exception ex)
                {
                    //log
                    AppLogger.Instance.Error(ex.Message, ex);
                }
            }
        }

    }
}
