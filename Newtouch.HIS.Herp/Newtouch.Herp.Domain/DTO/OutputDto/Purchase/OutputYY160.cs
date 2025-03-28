using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class OutputYY160
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY160 MAIN { get; set; }
        public OutputDetailYY160 DETAIL { get; set; }
    }

    public class OutputMainYY160
    {
        public string SFWJ { get; set; }
        public string DCZHFPBH { get; set; }
        public int JLS { get; set; }

    }
    public class OutputDetailYY160
    {
        public List<OutputStructYY160> STRUCT { get; set; }
    }
    public class OutputStructYY160
    {
        public string FPBH { get; set; }
        public string FPDM { get; set; }
        public string FPH { get; set; }
        public string FPZT { get; set; }

    }
}
