using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class OutputYY003
    {
        public OutputHeadYY003 HEAD { get; set; }
        public OutputMainYY003 MAIN { get; set; }
        public OutputDetailYY003 DETAIL { get; set; }
    }

    [XmlRoot(ElementName = "HEAD")]
    public class OutputHeadYY003
    {
        public string JSSJ { get; set; }
        public string ZTCLJG { get; set; }
        public string CWXX { get; set; }
        public string BZXX { get; set; }
    }

    public class OutputMainYY003
    {
        public int JLS { get; set; }
        public string SFWJ { get; set; }
    }
    public class OutputDetailYY003
    {
        public List<OutputStructYY003> STRUCT { get; set; }
    }
    public class OutputStructYY003
    {
        public string PSDH { get; set; }
        public string YQBM { get; set; }
        public string PSDBM { get; set; }
        public string CJRQ { get; set; }
        public string PSMXBH { get; set; }
        public string PSDTM { get; set; }
        public string ZXLX { get; set; }
        public string CGLX { get; set; }
        public string SPLX { get; set; }
        public string YPLX { get; set; }
        public string ZXSPBM { get; set; }
        public string CPM { get; set; }
        public string YPJX { get; set; }
        public string GG { get; set; }
        public string BZDWXZ { get; set; }
        public string BZDWMC { get; set; }
        public string YYDWMC { get; set; }
        public decimal BZNHSL { get; set; }
        public string SCQYMC { get; set; }
        public string SCPH { get; set; }
        public string SCRQ { get; set; }
        public string YXRQ { get; set; }
        public string JHDH { get; set; }
        public string XSDDH { get; set; }
        public string DDMXBH { get; set; }
        public int SXH { get; set; }
        public decimal PSL { get; set; }

    }
}
