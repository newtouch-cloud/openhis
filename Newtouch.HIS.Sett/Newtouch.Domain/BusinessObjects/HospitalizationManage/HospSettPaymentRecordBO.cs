namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// （出院结算）新增 收支记录
    /// </summary>
    public class HospSettPaymentRecordBO
    {
        /// <summary>
        /// 支付方式编号
        /// </summary>
        public int? xjzffsbh { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// 支付方式名称
        /// </summary>
        public string zffsmc { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal zfje { get; set; }

        /// <summary>
        /// 支付账户
        /// </summary>
        public int? zfzh { get; set; }

    }
}
