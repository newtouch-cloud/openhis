namespace Newtouch.HIS.Domain.ValueObjects.ReportManage
{
    /// <summary>
    /// 
    /// </summary>
    public class PerformanceIndicatorVO
    {
        /// <summary>
        /// 日期 例：201701
        /// </summary>
        public string col { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public int zrs { get; set; }
        /// <summary>
        /// 总人次
        /// </summary>
        public int zrc { get; set; }
        /// <summary>
        /// 平均人次
        /// </summary>
        public decimal pjrc { get; set; }
    }
}
