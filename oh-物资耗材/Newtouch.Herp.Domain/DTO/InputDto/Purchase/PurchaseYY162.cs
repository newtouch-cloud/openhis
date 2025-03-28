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
    public class PurchaseYY162
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY162 MAIN { get; set; }
    }
    public class PurchaseMainYY162
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string THDBH { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string THMXBHCXTJ { get; set; }

    }
}
