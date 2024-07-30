using System;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// 门诊发药主表显示
    /// </summary>
    public class fyMainListRequest
    {
        public string fph { get; set; }
        public string xm { get; set; }
        public int nl { get; set; }
        public string kh { get; set; }
        public string cfh { get; set; }
        public string brlx { get; set; }
        public DateTime sfsj { get; set; }
        public decimal je { get; set; }
        public int cflx { get; set; }
        public string CardNo { get; set; }
        public string xb { get; set; }
        public string ks { get; set; }
        public string ys { get; set; }
        public string pyy { get; set; }
    }
}
