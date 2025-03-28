using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.PayDto
{
    public class MicroPayTradeInfoDTO
    {
        /// <summary>
        /// 支付来源 支付宝1 微信2
        /// </summary>
        public int? PayType { get; set; }

        /// <summary>
        /// 请求支付订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 第三方交易号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal? PayAmount { get; set; }


        /// <summary>
        /// 是否支付成功 0 否 1 是
        /// </summary>
        public int? PayStatus { get; set; }

        public string PayMemo { get; set; }

        /// <summary>
        /// 支付账户
        /// </summary>
        public string PayAccount { get; set; }

        /// <summary>
        /// 交易描述
        /// </summary>
        public string TradeDesc { get; set; }

        /// <summary>
        /// 请求支付时间
        /// </summary>
        public DateTime? OutTradeTime { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal? RefundAmount { get; set; }

        /// <summary>
        /// 退款平台交易时间
        /// </summary>
        public DateTime? RefundDate { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        public string RefundReason { get; set; }

        public int? RefundStatus { get; set; }
        public string RefundMemo { get; set; }

        /// <summary>
        /// 请求退款时间
        /// </summary>
        public DateTime? OutRefundTime { get; set; }

        /// <summary>
        /// 退款请求号
        /// </summary>
        public string OutRequestNo { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 门诊号/住院号
        /// </summary>
        public string patno { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        public string TradeStatus { get; set; }
    }
}
