namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// 远程医疗请求参数
    /// </summary>
    public class IntegrateRcDto
    {
        /// <summary>
        /// 医生账号
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 患者手机号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 患者身份证号
        /// </summary>
        public string certid { get; set; }

        /// <summary>
        /// 患者性别: 1 男 2 女
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// 患者出生日期
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// 患者类型: 1 自费 2 医保
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// 医保卡卡号或者就诊卡卡号
        /// </summary>
        public string series { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string organCode { get; set; }
    }
}