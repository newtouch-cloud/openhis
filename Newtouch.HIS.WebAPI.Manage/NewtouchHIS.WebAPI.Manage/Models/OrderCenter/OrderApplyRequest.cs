namespace NewtouchHIS.WebAPI.Manage.Models.OrderCenter
{
    public class OrderApplyRequest
    {
        /// <summary>
        /// 订单类型 EnumOrderType 1 门诊，2 住院
        /// </summary>
        public int? OrderType { get; set; }

        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 本次就诊标识  门诊：mzh；住院：zyh
        /// </summary>
        public string VisitNo { get; set; }
        /// <summary>
        /// 患者信息
        /// </summary>
        public int patId { get; set; }
        /// <summary>
        /// 地址Id
        /// </summary>
        public string dzId { get; set; }
    }


    public class OrderInfoBaseVO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单类型 EnumOrderType
        /// </summary>
        public int? OrderType { get; set; }
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string? CardNo { get; set; }
        /// <summary>
        /// 本次就诊标识  门诊：mzh；住院：zyh
        /// </summary>
        public string? VisitNo { get; set; }
        /// <summary>
        /// 订单状态 EnumOrderStatus
        /// </summary>
        public int? OrderStu { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal? OrderAmt { get; set; }
        /// <summary>
        /// 锁定超时时间
        /// </summary>
        public DateTime? LockExpTime { get; set; }
    }
}
