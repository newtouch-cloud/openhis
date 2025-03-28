using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class OutputYY159
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY159 MAIN { get; set; }
        public OutputDetailYY159 DETAIL { get; set; }
    }

    public class OutputMainYY159
    {
        public string SFWJ { get; set; }
        public string DCZHDDMXBH { get; set; }
        public int JLS { get; set; }

    }
    public class OutputDetailYY159
    {
        public List<OutputStructYY159> STRUCT { get; set; }
    }
    public class OutputStructYY159
    {
        public string DDMXBH { get; set; }
        public string CGMXZT { get; set; }
        public string QYKC { get; set; }
        public string CGMXSHYJ { get; set; }
        public string CGDCLSM { get; set; }

    }
}
