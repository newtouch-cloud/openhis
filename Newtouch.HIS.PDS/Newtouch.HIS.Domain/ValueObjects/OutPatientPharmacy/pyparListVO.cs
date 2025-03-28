namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 排药参数列表
    /// </summary>
    public class PyparListVo
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 处方明细ID
        /// </summary>
        public string cfmxId { get; set; }

        /// <summary>
        /// 药品
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 提档日期
        /// </summary>
        public string tybz { get; set; }

        /// <summary>
        /// 拆零数
        /// </summary>
        public int cls { get; set; }
    }
}
