namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.EMRRequest
{
    /// <summary>
    /// 集成电子病历
    /// </summary>
    public class EMRRequestEntity
    {
        /// <summary>
        /// 资源名称：EMR_WRITE
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// 医生编号：即医生登录系统的用户名
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// 患者编号：患者唯一标识
        /// </summary>
        public string patientId { get; set; }
    }
}