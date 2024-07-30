using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class PatientSettleHisVO : PatMedInsurSettVO
    {
        public int jsnm { get; set; }
        public int? cxjsnm { get; set; }
        public string organizeid { get; set; }
        public string jslb { get; set; }
        public string xm { get; set; }

        public DateTime? jsksrq { get; set; }

        public DateTime? jsjsrq { get; set; }

        public decimal? zyts { get; set; }
        public decimal? zje { get; set; }
        public decimal? xjzf { get; set; }
        public decimal? ysk { get; set; }

        public DateTime? CreateTime { get; set; }

        public string CreatorCode { get; set; }

        public string jszt { get; set; }

        public string jsxz { get; set; }

        public string blh { get; set; }
        public string brxz { get; set; }
        public string xjzffsmc { get; set; }
        public string fph { get; set; }
    }
}
