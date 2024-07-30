namespace NewtouchHIS.WebAPI.Manage.Models.Patient
{
    public class SearchPatientRequest
    {
        /// <summary>
        /// 证件号
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string? xm { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? csrq { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string? mzh { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string? zyh { get; set; }
        /// <summary>
        /// 业务类型 EnumBusType
        /// </summary>
        public string? ywlx { get; set; }
    }

    public class SearchPatientResponse
    {
    }
}
