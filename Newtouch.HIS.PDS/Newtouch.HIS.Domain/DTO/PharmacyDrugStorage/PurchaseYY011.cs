using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class PurchaseYY011
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY011 MAIN { get; set; }
    }
    public class PurchaseMainYY011
    {
        public string CZLX { get; set; }
        public string YYBM { get; set; }
        public string PSDBM { get; set; }
        public string YQBM { get; set; }
        public int THDBH { get; set; }
        public string DLCGBZ { get; set; }
        public string SPLX { get; set; }
        public string ZXSPBM { get; set; }
        public string CGJLDW { get; set; }
        public string SCPH { get; set; }
        public int THSL { get; set; }
        public int THDJ { get; set; }
        public int THZJ { get; set; }
        public string THYY { get; set; }
    }
}
