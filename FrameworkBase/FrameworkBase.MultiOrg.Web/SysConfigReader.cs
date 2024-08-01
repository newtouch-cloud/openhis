using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;

//命名空间：System.Web.Mvc 好用
namespace System.Web.Mvc
{
    /// <summary>
    /// 配置读取帮助类（解决cshtml里读取配置 ViewBag麻烦的问题）
    /// </summary>
    public class SysConfigReader
    {
        private static string organizeId
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    var orgId = HttpContext.Current.Items["IDENTITY_ORGANIZEID"];
                    if (orgId != null)
                    {
                        return orgId.ToString();
                    }
                }
                return "";
            }
        }

        private static string actionName
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    var actionName = HttpContext.Current.Items["REQUEST_ACTIONNAME"];
                    if (actionName != null)
                    {
                        return actionName.ToString();
                    }
                }
                return "";
            }
        }

        private static string _reportServerHOST;
        private static string reportServerHOST
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_reportServerHOST))
                {
                    _reportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
                }
                return _reportServerHOST;
            }
        }

        /// <summary>
        /// 根据Code获取Value
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string String(string code)
        {
            var orgId = organizeId;
            if (!string.IsNullOrWhiteSpace(orgId))
            {
                var configRepo = DependencyDefaultInstanceResolver.GetInstance<ISysConfigRepo>();
                if (configRepo != null)
                {
                    return configRepo.GetValueByCode(code, orgId);
                }
            }
            return "";
        }

        /// <summary>
        /// 根据Code获取Value Int
        /// </summary>
        /// <param name="code"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int Int(string code, int defaultValue = 0)
        {
            var orgId = organizeId;
            if (!string.IsNullOrWhiteSpace(orgId))
            {
                var configRepo = DependencyDefaultInstanceResolver.GetInstance<ISysConfigRepo>();
                if (configRepo != null)
                {
                    return configRepo.GetIntValueByCode(code, orgId, defaultValue);
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 根据Code获取Value Decimal
        /// </summary>
        /// <param name="code"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public decimal Decimal(string code, decimal defaultValue = 0)
        {
            var orgId = organizeId;
            if (!string.IsNullOrWhiteSpace(orgId))
            {
                var configRepo = DependencyDefaultInstanceResolver.GetInstance<ISysConfigRepo>();
                if (configRepo != null)
                {
                    return configRepo.GetDecimalValueByCode(code, orgId, defaultValue);
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 根据Code获取Value Bool
        /// </summary>
        /// <param name="code"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool? Bool(string code, bool? defaultValue = null)
        {
            var orgId = organizeId;
            if (!string.IsNullOrWhiteSpace(orgId))
            {
                var configRepo = DependencyDefaultInstanceResolver.GetInstance<ISysConfigRepo>();
                if (configRepo != null)
                {
                    return configRepo.GetBoolValueByCode(code, orgId, defaultValue);
                }
            }
            return defaultValue;
        }

        #region 报表Url获取

        /// <summary>
        /// 获取ReportingService报表链接，会加上orgId=参数
        /// </summary>
        /// <param name="reportName">可不指定，默认配置rptlink_ActionName</param>
        /// <returns></returns>
        public static string OrgReportLink(string reportName = null)
        {
            var link = ReportLink(reportName);
            if (link.IndexOf("&rs:", StringComparison.OrdinalIgnoreCase) == -1)
            {
                return link + "&rs:orgId=" + organizeId;
            }
            else
            {
                return link + "&orgId=" + organizeId;
            }
        }

        /// <summary>
        /// 获取ReportingService报表链接
        /// </summary>
        /// <param name="reportName">可不指定，默认配置rptlink_ActionName</param>
        /// <returns></returns>
        public static string ReportLink(string reportName = null)
        {
            var host = reportServerHOST;
            if (!string.IsNullOrWhiteSpace(host))
            {
                if (string.IsNullOrWhiteSpace(reportName))
                {
                    //reportName默认赋值ActionName
                    reportName = actionName;
                }
                string uri = null;
                if (!string.IsNullOrWhiteSpace(reportName))
                {
                    var code = "rptlink_" + reportName;
                    uri = String(code);
                }
                if (!string.IsNullOrWhiteSpace(uri))
                {
                    if (uri.StartsWith("/"))
                    {
                        uri = uri.Substring(1);
                    }
                    return host + uri;
                }
            }
            return "";
        }

        #endregion

    }
}
