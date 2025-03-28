using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class GzybybItemCodeVO
    {
        public string ypmc { get; set; }
        public string ybdm { get; set; }
        public string gg { get; set; }
        public string ycmc { get; set; }
        public string ypxz { get; set; }
        public string ybxz { get; set; }
        public string py { get; set; }
        public decimal? ybdj { get; set; }
        public string gjybdm { get; set; }
        public string jxmc { get; set; }
        public string pzwh { get; set; }
    }
    public class GzybNameCodeVO
    {
        public int id { get; set; }
        public string xmmc { get; set; }
        public string pym { get; set; }
        public decimal ybxj { get; set; }
        public string dfybdm { get; set; }
        public string gjybdm { get; set; }
        public string lx { get; set; }
        public string gg { get; set; }
        public string dw { get; set; }
        public string dj { get; set; }
        public string pzwh { get; set; }
        public string sccj { get; set; }
        public string dfxmmc { get; set; }

    }
    public class GzxnhybItemCodeVO
    {
        public string name { get; set; }
        public string code { get; set; }
        public string dosageForm { get; set; }
        public string isBase { get; set; }
    }
}
