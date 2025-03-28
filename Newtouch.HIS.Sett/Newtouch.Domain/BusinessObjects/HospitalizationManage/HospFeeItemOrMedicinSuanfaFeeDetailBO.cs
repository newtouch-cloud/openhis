namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// 出院结算 费用（项目/药品）详情 算法 费用 明细
    /// </summary>
    public class HospFeeItemOrMedicinSuanfaFeeDetailBO
    {
        /// <summary>
        /// 减免金额
        /// </summary>
        public decimal jmje { get; set; }
        /// <summary>
        /// 费用合计（自费病人需要要支付的现金）
        /// </summary>
        public decimal total { get; set; }

        /// <summary>
        /// 分类自负
        /// </summary>
        public decimal flzf { get; set; }

        /// <summary>
        /// 记账费用
        /// </summary>
        public decimal jzfy { get; set; }

        /// <summary>
        /// 自理费用
        /// </summary>
        public decimal zl { get; set; }

        /// <summary>
        /// 现金
        /// </summary>
        public decimal xj { get; set; }

        /// <summary>
        /// 算法自负（通过算法计算出的自负金额）
        /// </summary>
        public decimal sfzf { get; set; }

        /// <summary>
        /// 算法自理（通过算法计算出的自负金额）
        /// </summary>
        public decimal sfzl { get; set; }

    }
}
