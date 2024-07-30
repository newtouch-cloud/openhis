namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    public class ScheduleRequest
    {
        /// <summary>
        /// 科室
        /// </summary>
        public string? ks { get; set; }
        public string? ysgh { get; set; }
        /// <summary>
        /// 开始日期(yyyy-MM-dd)
        /// </summary>
        public DateTime? ksrq { get; set; }
        /// <summary>
        /// 结束日期(yyyy-MM-dd)
        /// </summary>
        public DateTime? jsrq { get; set; }
        /// <summary>
        /// mz_ghpb_schedule 预约既定日程Id
        /// </summary>
        public string? ScheduId { get; set; }
        /// <summary>
        /// 是否网络可约 0 不可约
        /// </summary>
        public string? IsBook { get; set; }
        /// <summary>
        /// 门诊类型 普通专家副高等 mjzbz
        /// </summary>
        public string? RegType { get; set; }
    }
    public class SchedulePageRequest: ScheduleRequest
    {
        public string pagination { get; set; }
    }

}
