using Newtouch.Common.Operator;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;
using System;
using System.Reflection;
using System.Web;
using static Newtouch.Common.Web.APIRequestHelper;

namespace FrameworkBase.MultiOrg.Infrastructure
{
    /// <summary>
    /// API请求基类
    /// </summary>
    public class SiteAPIRequestHelperBase<T>
    {
        private static string _SiteAPIDomain;

        /// <summary>
        /// 域 http://sample.com/
        /// </summary>
        private static string SiteAPIDomain
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_SiteAPIDomain)) return _SiteAPIDomain;
                var domainConfigKey = (string)GetPRSFieldValue("_domainConfigKey");
                if (string.IsNullOrWhiteSpace(domainConfigKey))
                {
                    throw new Exception("require config field _domainConfigKey");
                }
                _SiteAPIDomain = ConfigurationHelper.GetAppConfigValue(domainConfigKey);
                if (string.IsNullOrWhiteSpace(_SiteAPIDomain))
                {
                    throw new Exception("require config " + domainConfigKey);
                }
                if (!_SiteAPIDomain.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    _SiteAPIDomain = "http://" + _SiteAPIDomain + "/";
                }
                return _SiteAPIDomain;
            }
        }

        private static string _apiName;
        private static string ApiName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_apiName)) return _apiName;
                _apiName = (string)GetPRSFieldValue("_apiName");
                //T的静态成员
                if (string.IsNullOrWhiteSpace(_apiName))
                {
                    throw new Exception("require config field _apiName");
                }
                return _apiName;
            }
        }

        /// <summary>
        /// 是否强刷新Token
        /// </summary>
        /// <param name="force"></param>
        /// <returns></returns>
        public static string GetToken(bool force = false)
        {
            var key = GetTokenCacheKey();
            TokenClass tokenClass;
            if (!force)
            {
                tokenClass = RedisHelper.Get<TokenClass>(key);
                if (tokenClass != null && tokenClass.LastRequestTime >= DateTime.Now.AddMinutes(-19) && !string.IsNullOrWhiteSpace(tokenClass.Token))
                {
                    return tokenClass.Token;
                }
            }
            var tk = GetTokenFromAPI();
            if (string.IsNullOrWhiteSpace(tk))
            {
                return null;
            }
            tokenClass = new TokenClass
            {
                Token = tk,
                LastRequestTime = DateTime.Now,
            };
            RedisHelper.Set(key, tokenClass);
            return tokenClass.Token;
        }

        /// <summary>
        /// post json return TResp
        /// </summary>
        /// <typeparam name="TResp"></typeparam>
        /// <param name="url"></param>
        /// <param name="req">请求体</param>
        /// <param name="tokenExpireReTryTimes">因Token过期重试次数</param>
        /// <param name="autoAppendToken">是否自动追加Token</param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static TResp Request<TResp>(string url, object req, int tokenExpireReTryTimes = 1, bool autoAppendToken = true, HttpContext httpContext = null)
            where TResp : new()
        {
            try
            {
                var apiResp = Newtouch.HIS.API.Common.Helper.HttpClientHelper.HttpPostStringAndRead<object, TResp>(GetAPIUrl(url), req, httpContext: httpContext);
                return apiResp;
            }
            catch (Exception ex)
            {
                var ins = Activator.CreateInstance<TResp>();
                var prop = typeof(TResp).GetProperty("code");
                var submsgProp = typeof(TResp).GetProperty("sub_msg");
                if (prop != null)
                {
                    prop.SetValue(ins, ResponseResultCode.FAIL);
                }
                if (submsgProp != null)
                {
                    submsgProp.SetValue(ins, ex.Message);
                }
                return ins;
            }
        }

        /// <summary>
        /// post json return string
        /// </summary>
        /// <param name="url"></param>
        /// <param name="req">请求体</param>
        /// <param name="tokenExpireReTryTimes">因Token过期重试次数</param>
        /// <param name="autoAppendToken">是否自动追加Token</param>
        /// <returns></returns>
        public static string Request(string url, object req, int tokenExpireReTryTimes = 1, bool autoAppendToken = true)
        {
            var reqStr = formatReqStr(req, autoAppendToken);
            return HttpClientHelper.HttpPostString(GetAPIUrl(url), reqStr);
        }

        /// <summary>
        /// 非Web时请添加private readonly static Func$lt;string$rt; _funcCacheKeyOfToken = ...
        /// </summary>
        /// <returns></returns>
        public static string GetTokenCacheKey()
        {
            var func = GetPRSFieldValue("_funcCacheKeyOfToken");
            if (func != null)
            {
                return ((Func<string>)func).Invoke();
            }

            var opr = OperatorProvider.GetCurrent();
            return string.Format("AccessAPI.Token.{0}.{1}.{2}"
                , ApiName
                , (opr.UserId ?? "").Replace("-", "")
                , (opr.OrganizeId ?? "").Replace("-", ""));
        }

        /// <summary>
        /// 非Web时请添加private readonly static Func$lt;object$rt; _funcReqBodyOfGetToken = ...
        /// </summary>
        /// <returns></returns>
        public static object GetTokenGetReqBody()
        {
            var func = GetPRSFieldValue("_funcReqBodyOfGetToken");
            if (func != null)
            {
                return ((Func<object>)func).Invoke();
            }

            var opr = OperatorProvider.GetCurrent();
            //默认的令牌获取请求 发送内容
            return new
            {
                UserId = opr.UserId,
                Account = opr.UserCode,
                OrganizeId = opr.OrganizeId,
                TopOrganizeId = opr.TopOrganizeId,
                AppId = ConstantsBase.AppId,
                Timestamp = DateTime.Now,
                TokenType = ""
            };
        }

        #region private methods

        /// <summary>
        /// API Url
        /// </summary>
        /// <param name="url">url 如'~/Home/Index'</param>
        /// <returns></returns>
        private static string GetAPIUrl(string url)
        {
            return SiteAPIDomain + url.Replace("~/", "");
        }

        /// <summary>
        /// 令牌获取
        /// </summary>
        /// <returns></returns>
        private static string GetTokenFromAPI(object tokenGetReq = null)
        {
            const string url = "api/Auth/GetToken";
            var req = tokenGetReq ?? GetTokenGetReqBody();
            var resp = Request<DefaultResponse<GetTokenResponse>>(url, req, autoAppendToken: false);
            if (resp != null && resp.code == ResponseResultCode.SUCCESS)
            {
                return resp.data.token;
            }
            return null;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static object GetPRSFieldValue(string name)
        {
            var field = typeof(T).GetField(name, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            return field != null ? field.GetValue(Activator.CreateInstance<T>()) : null;
        }

        /// <summary>
        /// 格式化请求串
        /// </summary>
        /// <param name="req"></param>
        /// <param name="autoAppendToken"></param>
        /// <returns></returns>
        private static string formatReqStr(object req, bool autoAppendToken)
        {
            object appendObj;
            if (autoAppendToken)
            {
                appendObj = new
                {
                    TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Token = GetToken()
                };
            }
            else
            {
                appendObj = new
                {
                    TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
            }
            var reqStr = Newtouch.Tools.Json.Merge(req, appendObj);
            return reqStr;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    internal class GetTokenResponse
    {
        public string token { get; set; }
    }
}
