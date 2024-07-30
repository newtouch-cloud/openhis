using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{

    [XmlRoot("XMLDATA")]
    public class OutputYY152
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY152 MAIN { get; set; }
        public OutputDetailYY152 DETAIL { get; set; }
    }

    public class OutputMainYY152
    {
        public string SFWJ { get; set; }
        public string DCZHTHMXBH { get; set; }
        public int JLS { get; set; }

    }
    public class OutputDetailYY152
    {
        public List<OutputStructYY152> STRUCT { get; set; }
    }
    public class OutputStructYY152
    {
        public string DJTXF { get; set; }
        public string QYBM { get; set; }
        public string PSDBM { get; set; }
        public string THDBH { get; set; }
        public string THMXBH { get; set; }
        public string SXH { get; set; }
        public string CGLX { get; set; }
        public string THLX { get; set; }
        public string HCTBDM { get; set; }
        public string HCXFDM { get; set; }
        public string YYBDDM { get; set; }
        public string PM { get; set; }
        public string GG { get; set; }
        public string XH { get; set; }
        public string GGXHSM { get; set; }
        public string DW { get; set; }
        public string SCQY { get; set; }
        public string CGGGXH { get; set; }
        public string SCPH { get; set; }
        public string SCRQ { get; set; }
        public string YXRQ { get; set; }
        public string PSMXTMLX { get; set; }
        public string PSMXTM { get; set; }
        public decimal THSL { get; set; }
        public decimal THDJ { get; set; }
        public string THYY { get; set; }
        public string CGFS { get; set; }
        public string XTBM { get; set; }
        public string SFHBSFW { get; set; }

    }
}
