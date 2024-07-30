using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// 门诊分诊，各诊室待就诊数量
    /// </summary>
    public class OutpatientConsultCountVO
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 诊室名称
        /// </summary>
        public string zsmc { get; set; }
        /// <summary>
        /// 剩余待就诊数量
        /// </summary>
        public int num { get; set; }
    }
}
