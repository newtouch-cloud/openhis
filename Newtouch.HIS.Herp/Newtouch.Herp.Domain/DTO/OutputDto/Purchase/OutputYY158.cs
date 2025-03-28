using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class OutputYY158
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY158 MAIN { get; set; }
        public OutputDetailYY158 DETAIL { get; set; }
    }

    public class OutputMainYY158
    {
        public string SFWJ { get; set; }
        public string DCZHFPMXBH { get; set; }
        public int JLS { get; set; }

    }
    public class OutputDetailYY158
    {
        public List<OutputStructYY158> STRUCT { get; set; }
    }
    public class OutputStructYY158
    {
        public string FPMXBH { get; set; }
        public string SFWPSFP { get; set; }
        public string WPSFPSM { get; set; }
        public string SFCH { get; set; }
        public string HCTBDM { get; set; }
        public string HCXFDM { get; set; }
        public string YYBDDM { get; set; }
        public string GGXHSM { get; set; }
        public string GLMXBH { get; set; }
        public string XSDDH { get; set; }
        public string SCPH { get; set; }
        public string SCRQ { get; set; }
        public string YXRQ { get; set; }
        public decimal SPSL { get; set; }
        public decimal WSDJ { get; set; }
        public decimal HSDJ { get; set; }
        public decimal SL { get; set; }
        public decimal SE { get; set; }
        public decimal HSJE { get; set; }
        public decimal PFJ { get; set; }
        public decimal LSJ { get; set; }
        public string ZCZH { get; set; }

    }
}
