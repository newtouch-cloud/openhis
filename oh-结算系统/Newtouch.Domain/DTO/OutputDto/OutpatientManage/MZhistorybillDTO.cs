using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO
{
    public class MZhistorybillDTO
    {
        public int jsnm { get; set; }

        public string jslx { get; set; }


        public decimal zje { get; set; }

        public string jszt { get; set; }

        public DateTime jsrq { get; set; }

        public DateTime? tfrq { get; set; }
    }


    public class MZhistorybillMXDTO
    {
        public int jsnm { get; set; }
        public int jsmxnm { get; set; }

        public string sfxm { get; set; }

        public string dw { get; set; }

        public decimal dj { get; set; }

        public decimal sl { get; set; }

        public decimal je { get; set; }

    }
}
