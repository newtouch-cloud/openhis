using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MedicalRecord
{
    public class AdviceListRequestVO
    {
        /// <summary>
        /// 当日医嘱
        /// </summary>
        public bool dryz { get; set; }

        public string yzlb { get; set; }
        /// <summary>
        /// 未审核
        /// </summary>
        public bool wsh { get; set; }
        /// <summary>
        /// 全部，有效
        /// </summary>
        public string yx { get; set; }
        public DateTime kssj { get; set; }
        public DateTime jssj { get; set; }
        public int yzlx { get; set; }
        public string orgId { get; set; }
        public string zyh { get; set; }
        public string deptName { get; set; }
    }
}
