using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class OutputYY011
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY011 MAIN { get; set; }
    }
    

    public class OutputMainYY011
    {
        public string THDBH { get; set; }
    }
}
