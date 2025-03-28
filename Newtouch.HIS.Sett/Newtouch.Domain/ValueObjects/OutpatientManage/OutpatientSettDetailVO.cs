using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 结算明细
    /// </summary>
    public class OutpatientSettDetailVO
    {
        /// <summary>
        /// 结算明细内码
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string mc { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal je { get; set; }
    }
}
