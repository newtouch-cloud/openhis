using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.OutputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class OutputYY157
    {
        public OutputHead HEAD { get; set; }
        public OutputMainYY157 MAIN { get; set; }
        public OutputDetailYY157 DETAIL { get; set; }
    }

    public class OutputMainYY157
    {
        public string SFWJ { get; set; }
        public string DCZHFPBH { get; set; }
        public int JLS { get; set; }
    }
    public class OutputDetailYY157
    {
        public List<OutputStructYY157> STRUCT { get; set; }
    }
    public class OutputStructYY157
    {
        public string FPBH { get; set; }
        public string FPDM { get; set; }
        public string FPH { get; set; }
        public string FPRQ { get; set; }
        public decimal FPHSZJE { get; set; }
        public string QYBM { get; set; }
        public string YYBM { get; set; }
        public string PSDBM { get; set; }
        public string CGLX { get; set; }
        public string FPBZ { get; set; }
        public int FPMXTS { get; set; }
        public string FPZT { get; set; }


    }
}
