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
    public class PurchaseYY133
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY133 MAIN { get; set; }
    }
    public class PurchaseMainYY133
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string FPID { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string FPDM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string FPH { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string QYBM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public decimal FPHSZJE { get; set; }
    }
}
