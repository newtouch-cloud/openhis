using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class PurchaseYY004
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY004 MAIN { get; set; }

    }
    public class PurchaseMainYY004
    {
        public string YQBM { get; set; }
        public string FPMXBH { get; set; }
    }
}
