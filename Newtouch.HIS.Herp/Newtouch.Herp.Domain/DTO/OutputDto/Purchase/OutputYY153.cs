using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class OutputYY153
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY153 MAIN { get; set; }
        public OutputDetailYY153 DETAIL { get; set; }
    }

    public class OutputMainYY153
    {
        public string SFWJ { get; set; }
        public string DCZHFPMXBH { get; set; }
        public int JLS { get; set; }
    }
    public class OutputDetailYY153
    {
        public List<OutputStructYY153> STRUCT { get; set; }
    }
    public class OutputStructYY153
    {
        public string PSDBM { get; set; }
        public string PSDBH { get; set; }
        public string PSMXBH { get; set; }
        public string PSDH { get; set; }
        public string QYBM { get; set; }
        public string PSMXTMLX { get; set; }
        public string PSMXTM { get; set; }
        public string DDMXBH { get; set; }
        public string YYJHDH { get; set; }
        public string SXH { get; set; }
        public string CWXX { get; set; }
        public string XSDDH { get; set; }
        public string HCTBDM { get; set; }
        public string HCXFDM { get; set; }
        public string YYBDDM { get; set; }
        public string PM { get; set; }
        public string GG { get; set; }
        public string XH { get; set; }
        public string GGXHSM { get; set; }
        public string DW { get; set; }
        public string SCQY { get; set; }
        public string SCPH { get; set; }
        public string SCRQ { get; set; }
        public string YXRQ { get; set; }
        public decimal PSL { get; set; }


    }
}
