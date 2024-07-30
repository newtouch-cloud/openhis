
namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 上一次结算信息
    /// </summary>
    public class LastSettleInfoVO
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 应收款
        /// </summary>
        public decimal? ysk { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        public decimal xjzf { get; set; }
        /// <summary>
        /// 找零
        /// </summary>
        public decimal zl { get; set; }
        /// <summary>
        /// 前一张发票号
        /// </summary>
        public string fph { get; set; }
    }
}
