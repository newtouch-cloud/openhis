using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 欠费预结的结算记录
    /// </summary>
    public class OutpatientSettArrearageVO
    {
        /// <summary>
        /// 结算内码
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// 欠费预结的操作日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal zje { get; set; }
    }
}
