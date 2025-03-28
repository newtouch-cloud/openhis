using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Web;

namespace Newtouch.EMR.Infrastructure
{
    /// <summary>
    /// CIS API请求帮助类
    /// </summary>
    public class SiteCISAPIHelper : SiteAPIRequestHelperBase<SiteCISAPIHelper>
    {
        /// <summary>
        /// 域Addr配置 的 Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteCISAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "CIS";

    }

    /// <summary>
    /// 同步住院病人信息
    /// </summary>
    public class InpatientPatientInfoDTO
    {
        /// <summary>
        /// 组织机构id
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 用户工号
        /// </summary>
        public string rygh { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        public string GetMAC { get; set; }

    }
}
