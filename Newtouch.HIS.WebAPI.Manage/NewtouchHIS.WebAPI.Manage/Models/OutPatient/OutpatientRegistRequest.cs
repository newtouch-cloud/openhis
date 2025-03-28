namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    /// <summary>
    /// 门诊挂号
    /// </summary>
    public class OutpatientRegistRequest
    {
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// 预约号
        /// </summary>
        public string? BookId { get; set; }
        /// <summary>
        /// 排班Id
        /// </summary>
        public string? ScheduId { get; set; }
        /// <summary>
        /// 挂号性质 自费医保等 brxz 等同
        /// </summary>
        public string? ghxz { get; set; }
        /// <summary>
        /// 支付方式 xjzffs
        /// </summary>
        public string? PayWay { get; set; }
        /// <summary>
        /// 已支付费用
        /// </summary>
        public string? PayFee { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary>
        public string? PayLsh { get; set; }

    }
    /// <summary>
    /// 查询挂号记录
    /// </summary>
    public class OutpRegistQueryRequest
    {
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string? mzh { get; set; }
    }
}
