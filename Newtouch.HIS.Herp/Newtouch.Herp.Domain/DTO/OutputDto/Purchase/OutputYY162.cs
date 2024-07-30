using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class OutputYY162
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY162 MAIN { get; set; }
        public OutputDetailYY162 DETAIL { get; set; }
    }

    public class OutputMainYY162
    {
        public string SFWJ { get; set; }
        public string DCZHTHMXBH { get; set; }
        public int JLS { get; set; }

    }
    public class OutputDetailYY162
    {
        public List<OutputStructYY162> STRUCT { get; set; }
    }
    public class OutputStructYY162
    {
        public string THMXBH { get; set; }
        public string THMXZT { get; set; }
        public string THDQYCLSM { get; set; }

    }
}
