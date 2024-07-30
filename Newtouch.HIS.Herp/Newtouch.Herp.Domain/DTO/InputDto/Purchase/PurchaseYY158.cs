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
    public class PurchaseYY158
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY158 MAIN { get; set; }
    }
    public class PurchaseMainYY158
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string FPBH { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string FPMXBHCXTJ { get; set; }

    }
}
