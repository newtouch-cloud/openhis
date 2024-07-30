namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    /// <summary>
    /// 预约申请请求参数
    /// </summary>
    public class BookApplyRequest
    {
        /// <summary>
        /// 挂号性质 自费医保等 brxz 等同
        /// </summary>
        public string ghxz { get; set; }
        /// <summary>
        /// mz_ghpb_schedule 预约既定日程Id
        /// </summary>
        public string ScheduId { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
    }
    /// <summary>
    /// 取消预约请求参数
    /// </summary>
    public class BookCancelRequest
    {
        /// <summary>
        /// 预约号
        /// </summary>
        public string BookId { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 预约取消原因
        /// </summary>
        public string? CancelReason { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string? lxdh { get; set; }
    }
}
