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
    public class PurchaseYY164
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY164 MAIN { get; set; }
    }
    public class PurchaseMainYY164
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string QYBM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string QYMC { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string QYBMCXTJ { get; set; }

    }
}
