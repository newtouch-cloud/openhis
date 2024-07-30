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
    public class PurchaseYY154
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY154 MAIN { get; set; }
    }
    public class PurchaseMainYY154
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string QSRQ { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string JZRQ { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string QYBM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string PSDH { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string PSDBHCXTJ { get; set; }

    }
}
