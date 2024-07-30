using Newtouch.Common.Operator;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;
using System;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Base API请求帮助类
    /// </summary>
    public class SiteBaseApiHelper
    {
        /// <summary>
        /// Base系统API
        /// </summary>
        private static readonly string _SiteBaseAPIHost;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SiteBaseApiHelper()
        {
            _SiteBaseAPIHost = ConfigurationHelper.GetAppConfigValue("SiteBaseAPIHost");
            if (string.IsNullOrWhiteSpace(_SiteBaseAPIHost))
            {
                _SiteBaseAPIHost = "localhost:20951";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="force">是否强刷新Token</param>
        /// <returns></returns>
        public static string GetToken(bool force = false)
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr == null) return null;
            var key = string.Format("Token.Access.API.Base.{0}.{1}", opr.UserId.Replace("-", ""), opr.OrganizeId.Replace("-", ""));
            TokenClass tokenClass;
            if (!force)
            {
                tokenClass = RedisHelper.Get<TokenClass>(key);
                if (tokenClass != null && tokenClass.LastRequestTime >= DateTime.Now.AddMinutes(-19))
                {
                    return tokenClass.Token;
                }
            }
            var tk = GetTokenFromAPI();
            if (string.IsNullOrWhiteSpace(tk))
            {
                return null;
            }
            tokenClass = new TokenClass()
            {
                Token = tk,
                LastRequestTime = DateTime.Now,
            };
            RedisHelper.Set(key, tokenClass);
            return tokenClass.Token;
        }

        /// <summary>
        /// post json return T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <param name="tokenExpireReTryTimes">因Token过期重试次数</param>
        /// <returns></returns>
        public static T Request<TV, T>(string url, TV req, int tokenExpireReTryTimes = 1)
            where T : new()
            where TV : class
        {
            return HIS.API.Common.Helper.HttpClientHelper.HttpPostStringAndRead<TV, T>(GetBaseAPIUrl(url), req);
        }

        #region private methods

        /// <summary>
        /// BaseAPIHost
        /// config配置_SiteBaseAPIHost
        /// </summary>
        private static string SiteBaseApiHost
        {
            get
            {
                return _SiteBaseAPIHost;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static string SiteBaseApiDomain
        {
            get
            {
                if (!SiteBaseApiHost.Contains("http"))
                {
                    return "http://" + SiteBaseApiHost + "/";
                }
                return SiteBaseApiHost;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetBaseAPIUrl(string url)
        {
            return SiteBaseApiDomain + url.Replace("~/", "");
        }

        #endregion

        #region

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetTokenFromAPI()
        {
            var url = "api/Auth/GetToken";
            var opr = OperatorProvider.GetCurrent();
            {
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

                var resp = Request<object, DefaultResponse<GetTokenResponse>>(url, req);

                if (resp.code == ResponseResultCode.SUCCESS)
                {
                    return resp.data.token;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        protected class GetTokenResponse
        {
            public string token { get; set; }
        }

        #endregion

    }
}
