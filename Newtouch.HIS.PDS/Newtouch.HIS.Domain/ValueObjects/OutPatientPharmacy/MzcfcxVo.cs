using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy
{
    public class MzcfcxVo
    {
        public string organizeId { get; set; }
        public string ks { get; set; }
        public string cflx { get; set; }
        public string keyword { get; set; }
        public DateTime kssj { get; set; }
        public DateTime jssj { get; set; }
        public string cfh { get; set; }
    }

    public class MzcfcxList
    {
        public string mzh { get; set; }
        public string xm { get; set; }
        public DateTime kssj { get; set; }
        public decimal zje { get; set; }
        public string cftag { get; set; }
        public string cftagName { get; set; }
        public string xb { get; set; }
        public string cfh { get; set; }
        public string cfId { get; set; }
        public string yscode { get; set; }
        public string ysmc { get; set; }
        public int cflx { get; set; }
        public string fybz { get; set; }
        public string ks { get; set; }
        public string ksmc { get; set; }
        //补打退药单使用
        public string tydh { get; set; }
    }
    public class MzcfcxDetailList : MzcfcxList
    {
        public string zh { get; set; }
        public string ypcode { get; set; }
        public string ypmc { get; set; }
        public string gg { get; set; }
        public int sl { get; set; }
        public decimal dj { get; set; }
        public decimal je { get; set; }
        public string dw { get; set; }
        public string ypyfmc { get; set; }
        public string yfcode { get; set; }
        public decimal? mcjl { get; set; }
        public string pcCode { get; set; }
        public string pcmc { get; set; }
    }
}
