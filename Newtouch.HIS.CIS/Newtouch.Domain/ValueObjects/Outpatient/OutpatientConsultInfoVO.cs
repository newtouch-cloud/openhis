using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// 诊室信息（小屏）
    /// </summary>
    public class OutpatientConsultInfoVO
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
        /// 医生姓名
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// 医生介绍
        /// </summary>
        public string ysjs { get; set; }
        /// <summary>
        /// 就诊中
        /// </summary>
        public string jzz { get; set; }
        /// <summary>
        /// 就诊中序号
        /// </summary>
        public Int16? jzzxh { get; set; }
        /// <summary>
        /// 待就诊
        /// </summary>
        public string djz { get; set; }
        /// <summary>
        /// 待就诊序号
        /// </summary>
        public Int16? djzxh { get; set; }
    }
}
