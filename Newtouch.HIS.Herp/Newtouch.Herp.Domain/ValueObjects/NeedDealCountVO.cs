namespace Newtouch.Herp.Domain.ValueObjects
{
    /// <summary>
    /// 待处理事件汇总
    /// </summary>
    public class NeedDealCountVO
    {
        /// <summary>
        /// 调价待审核
        /// </summary>
        public long? tjshCount { get; set; }

        /// <summary>
        /// 调价物资
        /// </summary>
        public long? tjwzCount { get; set; }

        /// <summary>
        /// 申领待审核
        /// </summary>
        public long? sldshCount { get; set; }

        /// <summary>
        /// 出库待审核
        /// </summary>
        public long? ckdshCount { get; set; }

        /// <summary>
        /// 外部入库待审核
        /// </summary>
        public long? wbrkCount { get; set; }

        /// <summary>
        /// 内部退货待审核
        /// </summary>
        public long? nbthCount { get; set; }

        /// <summary>
        /// 入库待审核
        /// </summary>
        public long? rkdshCount { get; set; }

        /// <summary>
        /// 过期药品数量
        /// </summary>
        public long? expriedWzCount { get; set; }
    }
}
