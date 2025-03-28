using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class OutputYY113
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY113 MAIN { get; set; }
        public List<OutputDetailYY113> DETAIL { get; set; }
    }
    public class OutputMainYY113
    {
        public string THDBH { get; set; }
    }

    public class OutputDetailYY113
    {
        public string THMXBH { get; set; }
        public string SXH { get; set; }
        public string HCXFDM { get; set; }
        public string YYBDDM { get; set; }
        public string CLJG { get; set; }
        public string CLQKMS { get; set; }

    }
}
