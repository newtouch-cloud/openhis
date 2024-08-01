namespace FrameworkBase.Domain.ValueObjects
{
    /// <summary>
    /// 用户人员VO
    /// </summary>
    public class SysUserStaffVO
    {
        /// <summary>
        /// 人员Id
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 科室Code
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 关联人员姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 关联人员工号
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// 用户编码（登录账号）
        /// </summary>
        public string UserCode { get; set; }

    }
}
