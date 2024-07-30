using Newtouch.Herp.Domain.DTO.OutputDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Herp.Domain.DTO.InputDto.Purchase
{
    [XmlRoot("XMLDATA")]
    public class PurchaseYY157
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY157 MAIN { get; set; }
    }
    public class PurchaseMainYY157
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string QSRQ { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string JZRQ { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string QYBM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string FPDM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string FPH { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string FPBHCXTJ { get; set; }
    }
}
