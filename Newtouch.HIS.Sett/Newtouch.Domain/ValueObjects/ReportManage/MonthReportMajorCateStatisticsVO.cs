namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 月报表 按大类 统计 VO
    /// </summary>
    public class MonthReportMajorCateStatisticsVO
    {
        /// <summary>
        /// 病人类型 inpatient/outpatient
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 住院号/门诊号
        /// </summary>
        public string zyhmzh { get; set; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string brxm { get; set; }

        /// <summary>
        /// 报表大类Code（收费大类）
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 大类名称
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 大类 金额合计
        /// </summary>
        public decimal dlje { get; set; }

        /// <summary>
        /// 备注（补差说明）
        /// </summary>
        public string bz { get; set; }

    }
}
