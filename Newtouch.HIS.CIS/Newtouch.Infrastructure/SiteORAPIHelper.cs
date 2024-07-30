using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.Infrastructure
{
    public class SiteORAPIHelper: SiteAPIRequestHelperBase<SiteORAPIHelper>
    {
        /// <summary>
        /// 域Addr配置 的 Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteORAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "OR";
    }
}
