using Newtouch.Herp.Domain.DTO.OutputDto.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto
{
    [XmlRoot("XMLDATA")]
    public class OutputYY156
    {
            public OutputHead HEAD { get; set; }
            public OutputMainYY156 MAIN { get; set; }
            public OutputDetailYY156 DETAIL { get; set; }
    }
    public class OutputMainYY156
        {
            public string SFWJ { get; set; }
            public string DCZHFPMXBH { get; set; }
            public int JLS { get; set; }
        }
        public class OutputDetailYY156
        {
            public List<OutputStructYY156> STRUCT { get; set; }
        }
        public class OutputStructYY156
        {
            public string FPBH { get; set; }
            public string FPMXBH { get; set; }
            public string FPDM { get; set; }
            public string FPH { get; set; }
            public string FPRQ { get; set; }
            public decimal FPHSZJE { get; set; }
            public string QYBM { get; set; }
            public string YYBM { get; set; }
            public string PSDBM { get; set; }
            public string CGLX { get; set; }
            public string FPBZ { get; set; }
            public string SFWPSFP { get; set; }
            public string WPSFPSM { get; set; }
            public string SFCH { get; set; }
            public string HCTBDM { get; set; }
            public string HCXFDM { get; set; }
            public string YYBDDM { get; set; }
            public string GGHXSM { get; set; }
            public string GLMXBH { get; set; }
            public string XSDDH { get; set; }
            public string SCPH { get; set; }
            public string SCRQ { get; set; }
            public string YXEQ { get; set; }
            public decimal SPSL { get; set; }
            public decimal WSDJ { get; set; }
            public decimal HSDJ { get; set; }
            public decimal SL { get; set; }
            public decimal SE { get; set; }
            public decimal HSJE { get; set; }
            public decimal PFJ { get; set; }
            public decimal LSJ { get; set; }
            public string ZCZH { get; set; }

        }
  
}
