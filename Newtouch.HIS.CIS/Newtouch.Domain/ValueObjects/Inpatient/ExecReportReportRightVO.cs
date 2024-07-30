using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class ExecReportReportRightVO
    {
        public string yzId { get; set; }
        /// <summary>
        /// 长临标志
        /// </summary>
        public string clbz { get; set; }
        public string yzh { get; set; }
        /// <summary>
        /// 组号
        /// </summary>
        public int? zh { get; set; }
        public string yznr { get; set; }
        public DateTime? zxsj { get; set; }
        public string shz { get; set; }
        public string zxz { get; set; }
        public int yzlx { get; set; }
        public string ypyfdm { get; set; }
        public int? lyxh { get; set; }
        public string hzxm { get; set; }
        /// <summary>
        /// 是否计费
        /// </summary>
        public string isjf { get; set; }
        public string zyh { get; set; }

    }
}
