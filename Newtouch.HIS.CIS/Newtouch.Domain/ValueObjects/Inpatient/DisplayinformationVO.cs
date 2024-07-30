using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class DisplayinformationVO
    {
        public string yzid { get; set; }
        public string zyh { get; set; }
        public string xm { get; set; }
        public string xmmc { get; set; }
        public string ispscs { get; set; }
        public string Result { get; set; }
        public string Remark { get; set; }
        public string yzxz { get; set; }
        public int? zh { get; set; }
    }
}
