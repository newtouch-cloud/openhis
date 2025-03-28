using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class PurchaseYY010
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY010 MAIN { get; set; }
    }
    public class PurchaseMainYY010
    {
        public string YYBM { get; set; }
        public string PSDBM { get; set; }
        public string DDLX { get; set; }
        public string DDBH { get; set; }
        public int SPSL { get; set; }
    }
    
}
