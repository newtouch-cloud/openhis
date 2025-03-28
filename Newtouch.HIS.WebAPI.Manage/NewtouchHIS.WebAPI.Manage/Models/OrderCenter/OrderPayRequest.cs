using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.WebAPI.Manage.Models.OrderCenter
{
    public class OrderPayRequest
    {
        /// <summary>
        /// 就诊卡号
        /// </summary>
        [Required(ErrorMessage = "就诊卡号不可为空")]
        public string CardNo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "订单号不可为空")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        [Required(ErrorMessage = "支付方式不可为空")]
        public string PayWay { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        [Required(ErrorMessage = "支付金额不可为空")]
        public decimal PayFee { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary>
        [Required(ErrorMessage = "支付流水号不可为空")]
        public string PayLsh { get; set; }
        /// <summary>
        /// 支付者身份信息
        /// </summary>
        public string? PayerId { get; set; }
        /// <summary>
        /// 订单支付时间
        /// </summary>
        [Required(ErrorMessage = "订单支付时间不可为空")]
        public DateTime? PayTime { get; set; }
        /// <summary>
        /// HIS内部结算号
        /// </summary>
        public string? SettTradeNo { get; set; }
        /// <summary>
        /// 订单类型 EnumOrderType 
        /// 1 门诊订单
        /// 2 住院订单
        /// </summary>
        public int? OrderType { get; set; }
    }
}
