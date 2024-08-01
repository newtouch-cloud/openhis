namespace FrameworkBase.MultiOrg.Domain.ValueObjects
{
    /// <summary>
    /// 岗位人员VO
    /// </summary>
    public class SysDutyStaffVO
    {
        /// <summary>
        /// 职员姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 职员拼音
        /// </summary>
        public string StaffPY { get; set; }

        /// <summary>
        /// 职员工号
        /// </summary>
        public string StaffGh { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 科室拼音
        /// </summary>
        public string kspy { get; set; }

        /// <summary>
        /// 人员岗位代码
        /// </summary>
        public string DutyCode { get; set; }
    }
}
