using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// 患者基本信息查询请求参数
    /// </summary>
    public class PatientBaseInfoQueryRequest : RequestBase
    {

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }
    }
}