using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Dto
{
    /// <summary>
    /// 获取病人就诊信息 请求报文
    /// </summary>
    public class PatientTreatmentInfoQueryRequestDTO : RequestBase
    {
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }
    }
}