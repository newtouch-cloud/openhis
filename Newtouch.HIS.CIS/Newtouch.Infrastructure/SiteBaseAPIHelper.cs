using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Base API请求帮助类
    /// </summary>
    public class SiteBaseAPIHelper : SiteAPIRequestHelperBase<SiteBaseAPIHelper>
    {
        /// <summary>
        /// 域Addr配置 的 Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteBaseAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Base";

    }
}
