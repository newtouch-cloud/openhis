namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.HEALRequest
{
    /// <summary>
    /// 治未病集成请求参数体
    /// </summary>
    public class HEALRequestEntity
    {
        /// <summary>
        /// 资源名称：HEAL_CHECKBODY
        /// 必填
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// 患者证件类型：1-身份证，2-军官证
        /// 必填
        /// </summary>
        public string CertificatesType { get; set; }

        /// <summary>
        /// 患者证件号码
        /// 必填
        /// </summary>
        public string CertificatesNumber { get; set; }

    }
}