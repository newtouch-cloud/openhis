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
    public class PurchaseYY111
    {
        public PurchaseHead HEAD { get; set; }
        public PurchaseMainYY111 MAIN { get; set; }
        public PurchaseDetailY111 DETAIL { get; set; }
        //public List<STRUCT> DETAIL { get; set; }
    }
    public class PurchaseMainYY111
    {
        public string CZLX { get; set; }
        public string YYBM { get; set; }
        public string PSDBM { get; set; }
        public string DDLX { get; set; }
        public string DDBH { get; set; }
        public string YYJHDH { get; set; }
        public string ZWDHRQ { get; set; }
        public string CGFS { get; set; }
        public string XTBM { get; set; }
        public string SFHBSFW { get; set; }
        public int JLS { get; set; }
    }

    public class PurchaseDetailY111
    {
        [XmlElement("STRUCT")]
        public List<PurchaseStructYY111> STRUCT { get; set; }
    }

    public class PurchaseStructYY111
    {
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string SXH { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string CGLX { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string HCTBDM { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string HCXFDM { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string YYBDDM { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string CGGGXH { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string PSSM { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public decimal CGSL { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public decimal CGDJ { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string QYBM { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string SFJJ { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string PSYQ { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string DCPSBS { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string CWXX { get; set; }
         [DisplayFormat(ConvertEmptyStringToNull = false)] public string BZSM { get; set; }

    }
}
