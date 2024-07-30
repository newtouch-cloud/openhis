using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Base.Utilities
{
    public static class MvcHelper
    {
        private static readonly string? _SiteStaticHost;
        static MvcHelper()
        {
            _SiteStaticHost = ConfigInitHelper.SysConfig.AppAPIHost?.SiteStaticHost ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "SiteStaticHost");
        }
        /// <summary>
        /// 静态资源Host
        /// config配置SiteStaticHost
        /// </summary>
        public static string? SiteStaticHost
        {
            get
            {
                return _SiteStaticHost;
            }
        }
        /// <summary>
        /// 资源版本号
        /// 避免静态资源缓存 用的，对应config Version
        /// </summary>
        public static string ver
        {
            get
            {
                string result = string.Empty;
#if DEBUG
                result = DateTime.Now.Ticks.ToString();
#else
                result = AppSettings.GetConfig("Version");
#endif
                return result;
            }
        }

        /// <summary>
        /// 域 http://sample.com/
        /// </summary>
        public static string SiteStaticDomain
        {
            get
            {
                if (!SiteStaticHost.Contains("http"))
                {
                    return "http://" + SiteStaticHost + "/";
                }
                else
                {
                    return SiteStaticHost;
                }
            }
        }
        /// <summary>
        /// 返回带软件版本号的静态资源Uri
        /// </summary>
        /// <param name="url">url 如'~/Home/Index'</param>
        /// <param name="isInSiteStatic">是否资源在独立的静态Domain</param>
        /// <returns></returns>
        public static string GetStaticResourceScriptUrl(string url, bool isInSiteStatic = true)
        {
            string staticResource = "/";
            if (isInSiteStatic)
            {
                staticResource = SiteStaticDomain;
            }
            string fullUrl = staticResource + url.Replace("~/", "");
            return isInSiteStatic ? fullUrl : fullUrl + (!url.Contains("?") ? "?V=" : "&V=") + ver;
        }
    }
}
