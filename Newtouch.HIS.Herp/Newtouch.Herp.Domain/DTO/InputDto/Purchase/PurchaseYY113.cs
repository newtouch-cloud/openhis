using Newtouch.Herp.Domain.DTO.OutputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.InputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class PurchaseYY113
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY113 MAIN { get; set; }
        public PurchaseDetailY113 DETAIL { get; set; }
    }
    public class PurchaseMainYY113
    {
        public string CZLX { get; set; }
        public string YYBM { get; set; }
        public string PSDBM { get; set; }
        public string SJTHRQ { get; set; }
        public string THDBH { get; set; }
        public string CGFS { get; set; }
        public string XTBM { get; set; }
        public string SFHBSFW { get; set; }
    public string DDBH { get; set; }
    public int JLS { get; set; }

}

public class PurchaseDetailY113
    {
        [XmlElement("STRUCT")]
        public List<PurchaseStructYY113> STRUCT { get; set; }
    }

    public class PurchaseStructYY113
    {
        public string SXH { get; set; }
        public string CGLX { get; set; }
        public string THLX { get; set; }
        public string HCTBDM { get; set; }
        public string HCXFDM { get; set; }
        public string YYBDDM { get; set; }
        public string CGGGXH { get; set; }
        public string SCPH { get; set; }
        public string SCRQ { get; set; }
        public string YXRQ { get; set; }
        public string PSMXTMLX { get; set; }
        public string PSMXTM { get; set; }
        public decimal THSL { get; set; }
    public decimal THDJ { get; set; }
    public string QYBM { get; set; }
    public string THYY { get; set; }


}
}
