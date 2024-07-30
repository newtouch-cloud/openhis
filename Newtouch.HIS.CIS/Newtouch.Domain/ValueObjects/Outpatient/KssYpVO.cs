using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// 抗生素药品信息
    /// </summary>
    public class KssYpVO
    {
        /// <summary>
        /// 药品Id
        /// </summary>
        public int ypId { get; set; }
        /// <summary>
        /// 药品Code
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }
    }
}
