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
    public class PurchaseYY160
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY160 MAIN { get; set; }
    }
    public class PurchaseMainYY160
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string QSRQ { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string JZRQ { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string FPDM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string FPH { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string FPBHCXTJ { get; set; }

    }
}
