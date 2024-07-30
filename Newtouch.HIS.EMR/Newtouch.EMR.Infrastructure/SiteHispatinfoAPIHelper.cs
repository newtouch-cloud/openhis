using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.EMR.Infrastructure
{
    public class SiteHispatinfoAPIHelper: SiteAPIRequestHelperBase<SiteHispatinfoAPIHelper>
    {
        /// <summary>
        /// 域Addr配置 的 Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteSettAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Sett";
    }
}
