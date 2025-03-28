using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class PurchaseYY009
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY009 MAIN { get; set; }
        public List<STRUCT> DETAIL { get; set; }
    }

    //public class PurchaseHeadYY009 {
    //    public string IP { get; set; }
    //    public string MAC { get; set; }
    //    public string BZXX { get; set; }
    //}

    public class PurchaseMainYY009 {
        public string CZLX { get; set; }
        public string YYBM { get; set; }
        public string PSDBM { get; set; }
        public string DDLX { get; set; }
        public string DDBH { get; set; }
        public string YYJHDH { get; set; }
        public string ZWDHRQ { get; set; }
        public int JLS { get; set; }
    }

    public class STRUCT
    {
        public int SXH { get; set; }
        public string CGLX{ get; set; }
        public string SPLX { get; set; }
        public string ZXSPBM { get; set; }
        public string GGBZ { get; set; }
        public string CGJLDW { get; set; }
        public decimal CGSL { get; set; }
        public decimal CGDJ { get; set; }
        public string YQBM { get; set; }
        public string DCPSBS { get; set; }
        public string BZSM { get; set; }
    }
}
