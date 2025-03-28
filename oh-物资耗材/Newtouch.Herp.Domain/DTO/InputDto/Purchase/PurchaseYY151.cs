using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.Herp.Domain.DTO.OutputDto;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.Herp.Domain.DTO.InputDto.Purchase
{

    [XmlRoot("XMLDATA")]
    public class PurchaseYY151
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY151 MAIN { get; set; }
    }
    public class PurchaseMainYY151
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string QSRQ { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string JZRQ { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string QYBM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DDLX { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CGLX { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DJTXF { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DDMXBH { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DDBH { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CGFS { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DDMXBHCXTJ { get; set; }

    }
}
