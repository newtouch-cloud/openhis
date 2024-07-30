namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 进销存统计
    /// </summary>
    public class VPsiStatisticsByDateEntity
    {
        private string _statisticsDate = "";
        /// <summary>
        /// 统计时间
        /// </summary>
        public string statisticsDate
        {
            get { return _statisticsDate; }
            set { _statisticsDate = value; }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public long sl { get; set; }

        private string _itemName = "";
        /// <summary>
        /// 项目名称
        /// </summary>
        public string itemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
    }

    /// <summary>
    /// 首页统计highcharts专用
    /// </summary>
    public class ClassificationStatisticsEntity
    {
        private string _name = "";
        /// <summary>
        /// 项目名称
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public long y { get; set; }
    }
}
