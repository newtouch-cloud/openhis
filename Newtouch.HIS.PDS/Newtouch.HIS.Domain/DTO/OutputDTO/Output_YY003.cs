using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDTO
{
    public class Output_YY003
    {
        public int JLS { get; set; }
        public string SFWJ { get; set; }
        public List<Detail_YY003> Detail_YY003 { get; set; }
    }

    public class Detail_YY003 {
        public string PSDH { get; set; }
        public string YQBM { get; set; }
        public string PSDBM { get; set; }
        public string CJRQ { get; set; }
        public string PSMXBH { get; set; }
        public string PSDTM { get; set; }
        public string ZXLX { get; set; }
        public string CGLX { get; set; }
        public string SPLX { get; set; }
        public string YPLX { get; set; }
        public string ZXSPBM { get; set; }
        public string CPM { get; set; }
        public string YPJX { get; set; }
        public string GG { get; set; }
        public string BZDWXZ { get; set; }
        public string BZDWMC { get; set; }
        public string YYDWMC { get; set; }
        public int? BZNHSL { get; set; }
        public string SCQYMC { get; set; }
        public string SCPH { get; set; }
        public string SCRQ { get; set; }
        public string YXRQ { get; set; }
        public string JHDH { get; set; }
        public string XSDDH { get; set; }
        public string DDMXBH { get; set; }
        public int? SXH { get; set; }
        public int? PSL { get; set; }
    }
}
