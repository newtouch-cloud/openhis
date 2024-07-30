using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
    public class MrDeptVO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string organizeId { get; set; }
        /// <summary>
        /// 病案科室ID
        /// </summary>
        public string baksId { get; set; }
        /// <summary>
        /// 病案科室名称
        /// </summary>
        public string baksName { get; set; }
        /// <summary>
        /// his科室编号
        /// </summary>
        public string hisdept { get; set; }
        /// <summary>
        /// his科室名称
        /// </summary>
        public string hisdeptname { get; set; }
    }
}
