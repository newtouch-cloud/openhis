using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    /// <summary>
    /// 手术安排对象
    /// </summary>
    public class ArrangementVO
    {
        public string zyh { get; set; }
        public string hzxm { get; set; }
        public string yznr { get; set; }
        public string id { get; set; }
        public string aprq { get; set; }
        public string ssAddr { get; set; }
        public string urgent { get; set; }
        public string surgeonId { get; set; }
        public string surgeonName { get; set; }
        public string assistant { get; set; }
        public string assistantName { get; set; }
        public string assistant2 { get; set; }
        public string assistantName2 { get; set; }
        public string assistant3 { get; set; }
        public string assistantName3 { get; set; }
        public string anesthesiaType { get; set; }
        public string remark { get; set; }
    }
}
