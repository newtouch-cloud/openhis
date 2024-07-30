namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 医嘱次|处方药品次
    /// </summary>
    public class FyCountBydlVO
    {
        /// <summary>
        /// 药品发药次汇总
        /// </summary>
        public long ypCount { get; set; }

        /// <summary>
        /// 大类代码
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 大类名称
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 门诊住院标志
        /// </summary>
        public string mzzybz { get; set; }
    }

    /// <summary>
    /// 根据大类统计发药数  首页统计highcharts专用
    /// </summary>
    public class FyClassificationStatistics
    {
        /// <summary>
        /// 大类名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public long y { get; set; }
    }
}
