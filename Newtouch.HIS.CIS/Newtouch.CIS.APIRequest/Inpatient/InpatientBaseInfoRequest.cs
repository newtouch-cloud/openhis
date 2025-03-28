using Newtouch.HIS.API.Common;

namespace Newtouch.CIS.APIRequest.Inpatient
{
    /// <summary>
    /// 获取住院病人基本信息
    /// </summary>
    public class InpatientBaseInfoRequest : JSONRequestBase
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }
    }
}