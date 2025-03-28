using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class OutputTest
    {
        public OutputHeadTest HEAD { get; set; }
        public OutputMainTest MAIN { get; set; }
    }
    public class OutputHeadTest
    {
        public string JSSJ { get; set; }
        public string ZTCLJG { get; set; }
        public string CWXX { get; set; }
        public string BZXX { get; set; }
    }

    public class OutputMainTest
    {
        public int JLS { get; set; }
        public string SFWJ { get; set; }
    }
}
