using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// PDS API请求帮助类
    /// </summary>
    public class SitePDSAPIHelper : SiteAPIRequestHelperBase<SitePDSAPIHelper>
    {
        /// <summary>
        /// 域Addr配置 的 Key
        /// </summary>
        private readonly static string _domainConfigKey = "SitePDSAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "PDS";

    }

}
