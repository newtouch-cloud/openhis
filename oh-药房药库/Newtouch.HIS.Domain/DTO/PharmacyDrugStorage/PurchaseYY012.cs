using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class PurchaseYY012
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY012 MAIN { get; set; }
    }
    public class PurchaseMainYY012
    {
        public string YYBM { get; set; }
        public string YQBM { get; set; }
        public string PSDBM { get; set; }
        public string DLCGBZ { get; set; }
        public decimal THSL { get; set; }
        public string THDBH { get; set; }
    }
}
