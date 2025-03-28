using System;
using System.Web;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 医护协同工作站
    /// </summary>
    public class SiteCisAPIHelper
    {
        /// <summary>
        /// 结算API
        /// </summary>
        private static readonly string _siteCisAPIHost;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SiteCisAPIHelper()
        {
            _siteCisAPIHost = ConfigurationHelper.GetAppConfigValue("SiteCisAPIHost");
            if (string.IsNullOrWhiteSpace(_siteCisAPIHost))
            {
                _siteCisAPIHost = "localhost:17361";
            }
        }

        /// <summary>
        /// GetToken
        /// </summary>
        /// <param name="force">是否强刷新Token</param>
        /// <returns></returns>
        public static string GetToken(bool force = false)
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return null;
            }
            var key = string.Format("Token.Access.API.Cis.{0}.{1}", opr.UserId.Replace("-", ""), opr.OrganizeId.Replace("-", ""));
            APIRequestHelper.TokenClass tokenClass;
            if (!force)
            {
                tokenClass = RedisHelper.Get<APIRequestHelper.TokenClass>(key);
                if (tokenClass != null && tokenClass.LastRequestTime >= DateTime.Now.AddMinutes(-19))
                {
                    return tokenClass.Token;
                }
            }
            var tk = GetTokenFromApi();
            if (string.IsNullOrWhiteSpace(tk))
            {
                return null;
            }
            tokenClass = new APIRequestHelper.TokenClass
            {
                Token = tk,
                LastRequestTime = DateTime.Now
            };
            RedisHelper.Set(key, tokenClass);
            return tokenClass.Token;
        }

        /// <summary>
        /// post json return T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <param name="tokenExpireReTryTimes">因Token过期重试次数</param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static T Request<TV, T>(string url, TV req, int tokenExpireReTryTimes = 1, HttpContext httpContext = null)
            where T : new()
            where TV : class
        {
            return HIS.API.Common.Helper.HttpClientHelper.HttpPostStringAndRead<TV, T>(GetCisApiUrl(url), req, httpContext: httpContext);
        }

        #region private methods

        /// <summary>
        /// 结算API Host
        /// </summary>
        private static string SiteCisApiHost
        {
            get
            {
                return _siteCisAPIHost;
            }
        }

        /// <summary>
        /// 域 http://sample.com/
        /// </summary>
        private static string SiteCisApiDomain
        {
            get
            {
                if (!SiteCisApiHost.Contains("http"))
                {
                    return "http://" + SiteCisApiHost + "/";
                }
                return SiteCisApiHost;
            }
        }

        /// <summary>
        /// 医护协同系统API Url
        /// </summary>
        /// <param name="url">url 如'~/Home/Index'</param>
        /// <returns></returns>
        private static string GetCisApiUrl(string url)
        {
            return SiteCisApiDomain + url.Replace("~/", "");
        }

        #endregion

        #region

        /// <summary>
        /// GetTokenFromApi
        /// </summary>
        /// <returns></returns>
        private static string GetTokenFromApi()
        {
            var opr = OperatorProvider.GetCurrent();
            var req = new
            {
                UserId = opr.UserId,
                Account = opr.UserCode,
                OrganizeId = opr.OrganizeId,
                TopOrganizeId = opr.TopOrganizeId,
                AppId = opr.AppId,
                Timestamp = DateTime.Now,
                TokenType = "PDS"
            };
            var resp = Request<object, APIRequestHelper.DefaultResponse<GetTokenResponse>>("api/Auth/GetToken", req);
            return resp.code == APIRequestHelper.ResponseResultCode.SUCCESS ? resp.data.Token : null;
        }

        /// <summary>
        /// GetTokenResponse
        /// </summary>
        protected class GetTokenResponse
        {
            public string Token { get; set; }
        }

        #endregion
    }
}