using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_C_Sys_StaffDuty")]
    public class SysStaffDutyComplexVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StaffPY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StaffGh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DutyCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string kflb { get; set; }

    }
}
