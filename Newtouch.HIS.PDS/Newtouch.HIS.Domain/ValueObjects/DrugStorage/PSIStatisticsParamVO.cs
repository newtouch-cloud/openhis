namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 进销存统计查询条件
    /// </summary>
    public class PsiStatisticsParam
    {
        /// <summary>
        /// 开始结转时间
        /// </summary>
        public string ksjzsj { get; set; }

        /// <summary>
        /// 结束结转时间
        /// </summary>
        public string jsjzsj { get; set; }

        /// <summary>
        /// 药品状态
        /// </summary>
        public string ypzt { get; set; }

        /// <summary>
        /// 输入码/药品代码
        /// </summary>
        public string srm { get; set; }

        /// <summary>
        /// 大类
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        public string jx { get; set; }

        /// <summary>
        /// 零差率
        /// </summary>
        public string rate { get; set; }

        /// <summary>
        /// 显示无进销存药品
        /// </summary>
        public string noPSI { get; set; }
    }

}
