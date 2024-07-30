using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class OutputYY161
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY161 MAIN { get; set; }
        public OutputDetailYY161 DETAIL { get; set; }
    }

    public class OutputMainYY161
    {
        public string SFWJ { get; set; }
        public string DCZHPSMXBH { get; set; }
        public int JLS { get; set; }

    }
    public class OutputDetailYY161
    {
        public List<OutputStructYY161> STRUCT { get; set; }
    }
    public class OutputStructYY161
    {
        public string PSMXBH { get; set; }
        public string PSMXZT { get; set; }
        public decimal YYYTGS { get; set; }
        public decimal YYYBGS { get; set; }
        public decimal YSYTGS { get; set; }
        public decimal YSYBGS { get; set; }

    }
}
