using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    [XmlRoot("XMLDATA")]
    public class OutputYY004
    {
        public OutputHeadYY004 HEAD { get; set; }
        public OutputMainYY004 MAIN { get; set; }
        public OutputDetailYY004 DETAIL { get; set; }
    }
    [XmlRoot(ElementName = "HEAD")]
    public class OutputHeadYY004
    {
        public string JSSJ { get; set; }
        public string ZTCLJG { get; set; }
        public string CWXX { get; set; }
        public string BZXX { get; set; }
    }

    public class OutputMainYY004
    {
        public int JLS { get; set; }
        public string SFWJ { get; set; }
    }
    public class OutputDetailYY004 {
        public List<OutputStructYY004> STRUCT  { get;set;}
    }
    public class OutputStructYY004
    {
        public string FPH { get; set; }
        public string FPRQ { get; set; }
        public decimal FPHSZJE { get; set; }
        public string YQBM { get; set; }
        public string YYBM { get; set; }
        public string PSDBM { get; set; }
        public string DLSGBZ { get; set; }
        public string FPBZ { get; set; }
        public string SFWPSFP { get; set; }
        public string WPSFPSM { get; set; }
        public string FPMXBH { get; set; }
        public string SPLX { get; set; }
        public string SFCH { get; set; }
        public string ZXSPBM { get; set; }
        public string SCPH { get; set; }
        public string SCRQ { get; set; }
        public decimal SPSL { get; set; }
        public string GLMXBH { get; set; }
        public string XSDDH { get; set; }
        public int SXH { get; set; }
        public string YXRQ { get; set; }
        public decimal WSDJ { get; set; }
        public decimal HSDJ { get; set; }
        public decimal SL { get; set; }
        public decimal SE { get; set; }
        public decimal HSJE { get; set; }
        public decimal PFJ { get; set; }
        public decimal LSJ { get; set; }
        public string PZWH { get; set; }
    }
}
