namespace NewtouchHIS.WebAPI.Manage.Models.Patient
{
    /// <summary>
    /// 患者注册基础信息
    /// </summary>
    public class PatientRegisterBasic
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string? xm { get; set; }
        /// <summary>
        /// 医保卡号
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string? xb { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string? lxdh { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? csrq { get; set; }
    }
    /// <summary>
    /// 患者注册信息
    /// </summary>
    public class PatientRegisterDTO: PatientRegisterBasic
    {

    }
}
