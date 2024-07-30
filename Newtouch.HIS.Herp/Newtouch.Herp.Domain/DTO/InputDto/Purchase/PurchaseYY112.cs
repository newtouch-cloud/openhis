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
    public class PurchaseYY112
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY112 MAIN { get; set; }
    }
    public class PurchaseMainYY112
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string YYBM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string PSDBM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string DDLX { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public string DDBH { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)] public int SPSL { get; set; }
    }
}
