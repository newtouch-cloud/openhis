namespace NewtouchHIS.WebAPI.Manage.Models.Organize
{
    /// <summary>
    /// 职工信息
    /// </summary>
    public class StaffVO
    {
        /// <summary>
        /// 职工工号
        /// </summary>
        public string? ysgh { get; set; }

        /// <summary>
        /// 职工名称
        /// </summary>
        public string? xm { get; set; }


    }
    public class SysDoctorVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string organizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DutyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dutyName { get; set; }
    }
    /// <summary>
    /// 职工信息查询类
    /// </summary>
    public class StaffRequest
    {
        public string? ks { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string? ysgh { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string? ysxm { get; set; }
    }
}
