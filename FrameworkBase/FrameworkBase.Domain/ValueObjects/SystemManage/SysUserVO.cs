using FrameworkBase.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.Domain.ValueObjects
{
    /// <summary>
    /// 用户VO
    /// </summary>
    [NotMapped]
    public class SysUserVO : SysUserEntity
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 科室Code
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 是否锁定登录
        /// </summary>
        public bool? Locked { get; set; }

        /// <summary>
        /// 是否锁定登录
        /// </summary>
        public int? IntLocked { get; set; }
    }

}
