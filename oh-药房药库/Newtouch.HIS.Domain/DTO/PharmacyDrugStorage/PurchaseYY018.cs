using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class PurchaseYY018
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY018 MAIN { get; set; }
    }

    public class PurchaseMainYY018
    {
        public string YQBM { get; set; }
        public string PSMXBH { get; set; }
        public string PSDTM { get; set; }
        public string ZXSPBM { get; set; }
        public int PSL { get; set; }
        public string SCPH { get; set; }
        public string YSJG { get; set; }
        public string YSJGBZ { get; set; }
    }
}
