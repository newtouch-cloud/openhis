using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 药品发票信息
    /// </summary>
    public class MedicineInvoiceInfoVO
    {
        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public string kprq { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string gysCode { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string gysmc { get; set; }
    }
}
