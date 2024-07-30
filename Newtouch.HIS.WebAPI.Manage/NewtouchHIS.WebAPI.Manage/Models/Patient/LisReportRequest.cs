namespace NewtouchHIS.WebAPI.Manage.Models
{
    public class LisReportRequestBase
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// 申请单状态 EnumReportStu
        /// </summary>
        public string? sqdzt { get; set; }
        /// <summary>
        /// 申请单号
        /// </summary>
        public string? sqdh { get; set; }
        /// <summary>
        /// 项目代码
        /// </summary>
        public string? xmdm { get; set; }
        /// <summary>
        /// 业务类型 1 门诊 2 住院
        /// </summary>
        public string? ywlx { get; set; }
    }
    /// <summary>
    /// 门诊报告
    /// </summary>
    public class LisReportMzRequest:LisReportRequestBase
    {
        /// <summary>
        /// 门诊号
        /// </summary>
        public string? mzh { get; set; }
    }
    /// <summary>
    /// 住院报告
    /// </summary>
    public class LisReportZyRequest: LisReportRequestBase
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string? zyh { get; set; }
    }
}
