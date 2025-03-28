namespace Newtouch.HIS.Domain.ValueObjects.ReportManage
{
    /// <summary>
    /// 站点收入统计
    /// </summary>
    public class SiteIncomeStatisticsVo
    {
        
        public string itemName { get; set; }

        /// <summary>
        /// 项目code
        /// </summary>
        public string dlcode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 门诊收费金额
        /// </summary>
        public double opsfje { get; set; }

        /// <summary>
        /// 门诊治疗执行金额
        /// </summary>
        public double opzlzxje { get; set; }

        /// <summary>
        /// 住院收费金额
        /// </summary>
        public double hpsfje { get; set; }

        /// <summary>
        /// 住院治疗执行金额
        /// </summary>
        public double hpzlzxje { get; set; }

        public double hssr { get; set; }
        public double ce { get; set; }
        public string tzsm { get; set; }
    }
}
