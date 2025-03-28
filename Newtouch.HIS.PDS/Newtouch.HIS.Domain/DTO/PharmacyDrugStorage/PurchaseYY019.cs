using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class PurchaseYY019
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY019 MAIN { get; set; }
    }
    public class PurchaseMainYY019
    {
        public string YQBM { get; set; }
        public string FPH { get; set; }
        public decimal FPHSZJE { get; set; }
    }
}
