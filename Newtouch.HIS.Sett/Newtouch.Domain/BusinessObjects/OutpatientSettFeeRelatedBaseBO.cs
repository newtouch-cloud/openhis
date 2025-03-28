namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// 门诊结算金额支付相关基类
    /// </summary>
    public class OutpatientSettFeeRelatedBaseBO
    {
        /// <summary>
        /// 原支付应收
        /// </summary>
        public decimal? orglxjzfys { get; set; }
        /// <summary>
        /// 折扣比例
        /// </summary>
        public decimal? zkbl { get; set; }
        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal? zkje { get; set; }
        /// <summary>
        /// 支付应收（原支付应收打折后）
        /// </summary>
        public decimal? xjzfys { get; set; }
        /// <summary>
        /// 实收款
        /// </summary>
        public decimal? ssk { get; set; }
        public string zffs1 { get; set; }
        public decimal? zfje1 { get; set; }
        public string zffs2 { get; set; }
        public decimal? zfje2 { get; set; }
        /// <summary>
        /// 找零
        /// </summary>
        public decimal? zhaoling { get; set; }

    }
}
