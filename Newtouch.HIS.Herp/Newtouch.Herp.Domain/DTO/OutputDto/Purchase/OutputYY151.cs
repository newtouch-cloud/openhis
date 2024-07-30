using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{

    [XmlRoot("XMLDATA")]
    public class OutputYY151
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY151 MAIN { get; set; }
        public OutputDetailYY151 DETAIL { get; set; }
    }

    public class OutputMainYY151
    {
        public string SFWJ { get; set; }
        public string DCZHDDMXBH { get; set; }
        public int JLS { get; set; }

    }
    public class OutputDetailYY151
    {
        public List<OutputStructYY151> STRUCT { get; set; }
    }
    public class OutputStructYY151
    {
        public string DDLX { get; set; }
        public string DJTXF { get; set; }
        public string DDMXBH { get; set; }
        public string DDBH { get; set; }
        public string SXH { get; set; }
        public string YYJHDH { get; set; }
        public string QYBM { get; set; }
        public string PSDBM { get; set; }
        public string CGLX { get; set; }
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
        public string PSSM { get; set; }
        public decimal CGSL { get; set; }
        public decimal CGDJ { get; set; }
        public string SFJJ { get; set; }
        public string PSYQ { get; set; }
        public string CWXX { get; set; }
        public string DCPSBS { get; set; }
        public string BZSM { get; set; }
        public string CGFS { get; set; }
        public string XTBM { get; set; }
        public string SFHBSFW { get; set; }

    }
}
