using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class PurchaseYY001
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY001 MAIN { get; set; }
    }
    public class PurchaseMainYY001
    {
        public string CZLX { get; set; }
        public string PSDBM { get; set; }
        public string PSDMC { get; set; }
        public string PSDZ { get; set; }
        public string LXRXM { get; set; }
        public string LXDH { get; set; }
        public string YZBM { get; set; }
        public string BZXX { get; set; }
    }
}
