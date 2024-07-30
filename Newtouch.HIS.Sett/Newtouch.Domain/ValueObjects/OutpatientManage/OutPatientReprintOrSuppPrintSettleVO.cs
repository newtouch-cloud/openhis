
using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// 重打/补打发票（mz_js 对象）
    /// </summary>
    public class OutPatientReprintOrSuppPrintSettleVO
    {
        public int jsnm { get; set; }
        public string xm { get; set; }
        public string kh { get; set; }
        public string xfph { get; set; }
        public string rymc { get; set; }
        public DateTime CreateTime { get; set; }  //结算日期
        public decimal zje { get; set; }

    }
}
