namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.RCIntegrateRequest
{
    /// <summary>
    /// 远程会诊集成请求体
    /// </summary>
    public class RCIntegrateRequestEntity
    {
        /// <summary>
        /// 平台用户唯一：医生登录系统的用户名
        /// 必填
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户名称
        /// 必填
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 平台病人 id
        /// 必填
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// 病人就诊流水号
        /// </summary>
        public string series { get; set; }

        /// <summary>
        /// 病人姓名
        /// 必填
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 病人手机号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 18 位身份证号
        /// 必填
        /// </summary>
        public string certid { get; set; }

        /// <summary>
        /// 性别 1 男 2 女
        /// 必填
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// yyyy-MM-dd
        /// 必填
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// 医保类型 1 自费 2 医保
        /// 必填
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// 医保卡卡号或者就诊卡卡号
        /// </summary>
        public string cardId { get; set; }

        /// <summary>
        /// 机构编码
        /// 必填
        /// </summary>
        public string organCode { get; set; }

        /// <summary>
        /// 调用 pc 端指定模块:test
        /// 必填
        /// </summary>
        public string m { get; set; }

        /// <summary>
        /// 调用 pc 端指定模块的某个方法:/eh.bus.web.cloud.wizard.Wizard
        /// 必填
        /// </summary>
        public string clz { get; set; }
    }
}