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
    public class PurchaseYY101
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY101 MAIN { get; set; }
    }
    public class PurchaseMainYY101
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CZLX { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PSDBM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PSDMC { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PSDZ { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LXRXM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LXDH { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string YZBM { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string BZXX { get; set; }
    }
}
