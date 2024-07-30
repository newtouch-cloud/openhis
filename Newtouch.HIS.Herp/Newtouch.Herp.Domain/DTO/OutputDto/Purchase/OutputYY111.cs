using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class OutputYY111
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY111 MAIN { get; set; }
        public List<OutputDetailYY111> DETAIL { get; set; }
    }

    public class OutputMainYY111
    {
        public string DDBH { get; set; }
    }

    public class OutputDetailYY111
    {
        public string DDMXBH { get; set; }
        public string SXH { get; set; }
        public string HCTBDM { get; set; }
        public string HCXFDM { get; set; }
        public string YYBDDM { get; set; }
        public string QYKC { get; set; }
        public string CLJG { get; set; }
        public string CLQKMS { get; set; }
    }
}
