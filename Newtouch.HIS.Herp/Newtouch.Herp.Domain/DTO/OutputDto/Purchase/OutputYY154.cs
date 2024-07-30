using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class OutputYY154
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY154 MAIN { get; set; }
        public OutputDetailYY154 DETAIL { get; set; }
    }

    public class OutputMainYY154
    {
        public string SFWJ { get; set; }
        public string DCZHPSDBH { get; set; }
        public int JLS { get; set; }
    }
    public class OutputDetailYY154
    {
        public List<OutputStructYY154> STRUCT { get; set; }
    }
    public class OutputStructYY154
    {
        public string PSDBH { get; set; }
        public string PSDH { get; set; }
        public string QYBM { get; set; }
        public int PSMXTS { get; set; }
        public string PSDZT { get; set; }

    }
}
