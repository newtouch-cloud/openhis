namespace NewtouchHIS.WebAPI.Manage.Models.CIS
{
    public class DoctorAdviceReq
    {
        public string zyh { get; set; }
        public string yzlx { get; set; }
    }

    public class InspectionExaminationReq
    {
        public string jzh { get; set; }
        public DateTime ksrq { get; set; }
        public DateTime jsrq { get; set; }
        //报告类型 jy：检验 jc:检查
        public string reportType { get; set; }
        /// <summary>
        /// 门诊住院标志  mz:门诊 zy:住院
        /// </summary>
        public string mzzybz { get; set; }
    }
}
