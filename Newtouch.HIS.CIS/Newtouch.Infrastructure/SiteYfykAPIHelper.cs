using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.Infrastructure
{
    public class SiteYfykAPIHelper:SiteAPIRequestHelperBase<SiteYfykAPIHelper>
    {
        /// <summary>
        /// 域Addr配置 的 Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteYfykAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Yfyk";
    }
}
