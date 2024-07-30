using FrameworkBase.MultiOrg.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Herp API请求帮助类
    /// </summary>
    public class SiteHerpAPIHelper : SiteAPIRequestHelperBase<SiteHerpAPIHelper>
    {
        /// <summary>
        /// 域Addr配置 的 Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteHerpAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Herp";
    }
}
