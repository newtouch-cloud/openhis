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
    public class OperatPatVO
    {
        public string zyh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public Int16 nl { get; set; }
        public string py { get; set; }
    }
}
