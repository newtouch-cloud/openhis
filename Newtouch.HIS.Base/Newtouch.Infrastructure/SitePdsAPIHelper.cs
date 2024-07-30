using System;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 药房药库
    /// </summary>
    public class SitePdsApiHelper
    {
        /// <summary>
        /// 结算API
        /// </summary>
        private static readonly string _sitePdsAPIHost;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SitePdsApiHelper()
        {
            _sitePdsAPIHost = ConfigurationHelper.GetAppConfigValue("SitePdsAPIHost");
            if (string.IsNullOrWhiteSpace(_sitePdsAPIHost))
            {
                _sitePdsAPIHost = "localhost:20138";
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
            var key = string.Format("Token.Access.API.Sett.{0}.{1}", opr.UserId.Replace("-", ""), opr.OrganizeId.Replace("-", ""));
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
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <param name="tokenExpireReTryTimes">因Token过期重试次数</param>
        /// <returns></returns>
        public static T Request<T>(string url, object req, int tokenExpireReTryTimes = 1) where T : new()
        {
            return HIS.API.Common.Helper.HttpClientHelper.HttpPostStringAndRead<object, T>(GetPdsApiUrl(url), req);
        }

        #region private methods

        /// <summary>
        /// 结算API Host
        /// </summary>
        private static string SitePdsApiHost
        {
            get
            {
                return _sitePdsAPIHost;
            }
        }

        /// <summary>
        /// 域 http://sample.com/
        /// </summary>
        private static string SitePdsApiDomain
        {
            get
            {
                if (!SitePdsApiHost.Contains("http"))
                {
                    return "http://" + SitePdsApiHost + "/";
                }
                return SitePdsApiHost;
            }
        }

        /// <summary>
        /// 结算系统API Url
        /// </summary>
        /// <param name="url">url 如'~/Home/Index'</param>
        /// <returns></returns>
        private static string GetPdsApiUrl(string url)
        {
            return SitePdsApiDomain + url.Replace("~/", "");
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
                TokenType = "Base.web"
            };

            var resp = Request<APIRequestHelper.DefaultResponse<GetTokenResponse>>("api/Auth/GetToken", req);

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