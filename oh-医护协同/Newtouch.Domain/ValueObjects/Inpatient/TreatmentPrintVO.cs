using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
   public class TreatmentPrintVO
    {
        public string Id { get; set; }
        public string zyh { get; set; }
        public DateTime zxrq { get; set; }
        public DateTime createtime { get; set; }
        public int? mcsl { get; set; }
        public string creatorcode { get; set; }
        public string dw { get; set; }
        public int? zyxz { get; set; }
        public string yznr { get; set; }
        public string lrz { get; set; }
        public string zt { get; set; }
        public string bedcode { get; set; }
        public string hzxm { get; set; }
        public string clbz { get; set; }
        public int? fzxh { get; set; }
    }
}
