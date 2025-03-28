using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.HIS.Sett.Request
{
    public class OrderRequest : RequestBase
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 本次就诊标识  门诊：mzh；住院：zyh
        /// </summary>
        public string VisitNo { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmt { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayWay { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal? PayFee { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary>
        public string PayLsh { get; set; }
        /// <summary>
        /// 支付者身份信息
        /// </summary>
        public string PayerId { get; set; }
        /// <summary>
        /// HIS内部结算号
        /// </summary>
        public string SettTradeNo { get; set; }
        /// <summary>
        /// 订单支付时间
        /// </summary>
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// 订单类型 EnumOrderType
        /// </summary>
        public int? OrderType { get; set; }
        /// <summary>
        /// 订单状态 EnumOrderStatus
        /// </summary>
        public int? OrderStu { get; set; }
    }

    
}
