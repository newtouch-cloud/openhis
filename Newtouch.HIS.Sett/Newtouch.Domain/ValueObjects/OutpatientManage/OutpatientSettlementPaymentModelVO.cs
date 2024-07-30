
namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// 门诊结算支付方式
    /// </summary>
    public class OutpatientSettlementPaymentModelVO
    {
        /// <summary>
        /// 支付方式名称
        /// </summary>
        public string xjzffsmc { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal zfje { get; set; }
    }
}
