namespace NewtouchHIS.WebAPI.Manage.Models.InHosPatient
{
    /// <summary>
    /// 住院账单查询Request
    /// </summary>
    public class InHosPatBillRequest
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string? zyh { get; set; }
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string? OrderNo { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal? OrderAmt { get; set; }
    }
}
