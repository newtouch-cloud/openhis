using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain
{
    public class QRBarPayOutput
    {
        /// <summary>
        ///     结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        ///     消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     返回值
        /// </summary>
        public string ReturnValue { get; set; }

        /// <summary>
        /// 外部订单号
        /// </summary>
        public string outTradeNo { get; set; }

        /// <summary>
        /// 订单号 
        /// </summary>
        public string tradeNo { get; set; }

        /// <summary>
        /// 买家ID
        /// </summary> 
        public string OpenId { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public string TotalAmount { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public string PaymentAmount { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string TradeStatus { get; set; }

        /// <summary>
        /// 交易成功，交易失败
        /// </summary>
        public bool TradeSuccess { get; set; }
    }

    public class RefundFeeOut
    {
        /// <summary>
        ///     结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        ///     消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     返回值
        /// </summary>
        public string ReturnValue { get; set; }
    }

}
