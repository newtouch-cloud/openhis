using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class OutputYY164
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY164 MAIN { get; set; }
        public OutputDetailYY164 DETAIL { get; set; }
    }
    public class OutputMainYY164
    {
        public string SFWJ { get; set; }
        public string DCZHQYBM { get; set; }
        public int JLS { get; set; }

    }
    public class OutputDetailYY164
    {
        public List<OutputStructYY164> STRUCT { get; set; }
    }
    public class OutputStructYY164
    {
        public string QYBM { get; set; }
        public string QYMC { get; set; }
        public string QYDZ { get; set; }
        public string LXR { get; set; }
        public string LXDH { get; set; }

    }
}
