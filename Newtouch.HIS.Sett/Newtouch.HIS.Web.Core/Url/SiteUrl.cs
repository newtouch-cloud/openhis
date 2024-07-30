using System;
using System.Configuration;

namespace Newtouch.HIS.Web.Core.Url
{
    /// <summary>
    /// 
    /// </summary>
    public static class SiteUrl
    {
        private static readonly string _SiteStaticHost;

        static SiteUrl()
        {
            _SiteStaticHost = ConfigurationManager.AppSettings["SiteStaticHost"];
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Version
        {
            get
            {
                string result = string.Empty;
#if DEBUG
                result = DateTime.Now.Ticks.ToString();
#else
                result = ConfigurationManager.AppSettings["Version"];
#endif
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SiteStaticHost
        {
            get
            {
                return _SiteStaticHost;
            }
        }

        private static string SiteStaticDomain
        {
            get
            {
                return "http://" + SiteStaticHost + "/";
            }
        }

        /// <summary>
        /// 返回带软件版本号的Uri
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetStaticResourceScriptUrl(string url, bool isInSiteStatic = true)
        {
            var staticResource = "/";
            if (isInSiteStatic)
            {
                staticResource = SiteStaticDomain;
            }
            return staticResource + url.Replace("~/", "") + (!url.Contains("?") ? "?V=" : "&V=") + Version;
        }

    }
}
