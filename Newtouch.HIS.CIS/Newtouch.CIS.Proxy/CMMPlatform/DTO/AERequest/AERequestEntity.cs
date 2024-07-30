namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.AERequest
{
    /// <summary>
    /// 辩证论治集成请求参数体
    /// </summary>
    public class AERequestEntity
    {
        /// <summary>
        /// 资源名称：AE_EDIT
        /// 必填
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// 机构编号
        /// 必填
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// 医生工号：即医生登录系统的用户名
        /// 必填
        /// </summary>
        public string chisZggh { get; set; }

        /// <summary>
        /// 医生姓名
        /// 必填
        /// </summary>
        public string chisEmpName { get; set; }

        /// <summary>
        /// 接诊编号
        /// 必填
        /// </summary>
        public string clinicId { get; set; }

        /// <summary>
        /// 病人姓名
        /// 必填
        /// </summary>
        public string brxm { get; set; }

        /// <summary>
        /// 病人性别-1/2/9 男/女/未知
        /// 必填
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 患者编号：患者唯一标识
        /// 必填
        /// </summary>
        public string patientid { get; set; }

        /// <summary>
        /// 返回数据给 HIS 的代理页面地址
        /// 必填
        /// </summary>
        public string returnHisUrl { get; set; }
    }
}