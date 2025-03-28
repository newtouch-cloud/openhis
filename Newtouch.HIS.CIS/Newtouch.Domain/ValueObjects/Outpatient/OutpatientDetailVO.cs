using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    public class OutpatientDetailVO
    {
        public DateTime jzsj { get; set; }
        public string mzh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string brxzmc { get; set; }
        public string jzks { get; set; }
        public int cfs { get; set; }
        public decimal cfje { get; set; }
        public int jcs { get; set; }
        public decimal jcje { get; set; }
        public decimal fjxm { get; set; }
    }
}
