using Newtouch.Herp.Domain.DTO.OutputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.InputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class PurchaseYY131
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY131 MAIN { get; set; }
        public PurchaseDetailYY131 DETAIL { get; set; }
    }
    public class PurchaseMainYY131
    {
        public string PSYSLX { get; set; }
        public int JLS { get; set; }
    }
    public class PurchaseDetailYY131
    {
        [XmlElement("STRUCT")]
        public List<PurchaseStructYY131> STRUCT { get; set; }
    }
    public class PurchaseStructYY131
    {
        public string PSMXBH { get; set; }
        public string HCTBDM { get; set; }
        public string SCPH { get; set; }
        public decimal PSL { get; set; }
        public decimal YSTGS { get; set; }
        public decimal YSBGS { get; set; }
        public string YSBZSM { get; set; }


    }
}
