namespace NewtouchHIS.WebAPI.Manage.Models.EMR
{
    /// <summary>
    /// 病人列表类型 在院、出院、门诊
    /// </summary>
    public class MedicalPatientRequest
    {
        public string brbz{ get; set; }
        public DateTime ksrq { get; set; }
        public DateTime jsrq { get; set; }
        public string srz { get; set; }
    }
    /// <summary>
    /// 病历文书tree 和诊断列表
    /// </summary>
    public class MedicalHomeRequest
    { 
        /// <summary>
        /// 住院号/门诊号
        /// </summary>
        public string zyh { get; set; }
    }
}
