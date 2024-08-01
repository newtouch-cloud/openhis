using FrameworkBase.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.Domain.ValueObjects
{
    /// <summary>
    /// 系统人员VO
    /// </summary>
    [NotMapped]
    public class SysStaffVO : SysStaffEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IntGender { get; set; }

    }
}
