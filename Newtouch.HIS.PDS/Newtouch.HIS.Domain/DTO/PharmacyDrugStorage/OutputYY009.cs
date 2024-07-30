using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class OutputYY009
    {
        public OutputHeadYY009 HEAD { get; set; }
        public OutputMainYY009 MAIN { get; set; }
        public List<OutputDetailYY009> DETAIL { get; set; }
    }

    public class OutputHeadYY009
    {
        public string JSSJ { get; set; }
        public string ZTCLJG { get; set; }
        public string CWXX { get; set; }
        public string BZXX { get; set; }
    }

    public class OutputMainYY009
    {
        public string DDBH { get; set; }
    }

    public class OutputDetailYY009
    {
        public int DDMXBH { get; set; }
        public string SXH { get; set; }
        public string SPLX { get; set; }
        public string ZXSPBM { get; set; }
        public string YYSPBM { get; set; }
        public decimal CLJG { get; set; }
        public decimal CLQKMS { get; set; }
    }
}

