using Newtonsoft.Json;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Redis;
using Newtouch.Tools;
using Newtouch.Tools.Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    public class AuthenManageHelper
    {
        public readonly static string SiteTokenAPIHost = ConfigurationManager.AppSettings["SiteTokenAPIHost"];//token站点
        public readonly static string SiteConmonAPIHost = ConfigurationManager.AppSettings["SiteConmonAPIHost"]; //api中心站点
        public readonly static string appId = ConfigurationManager.AppSettings["AppId"];//api中心站点访问appid
        public readonly static string accessAppId = ConfigurationManager.AppSettings["Token_ApiManage"];//Apimanage AppId
        //用户登陆缓存token
        public static string userCacheToken = "";
        /// <summary>
        /// 获取中心接口api token
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            string tokenstr = RedisHelper.StringGet("AuthenGetTokenKey");
            string tokenstrs = "";
            if (!string.IsNullOrWhiteSpace(tokenstr))
            {
                tokenstrs = tokenstr;
            }
            else
            {
                var reqObj = new
                {
                    AppId = appId,
                    Data = accessAppId
                };
                tokenstrs = HttpMethods.HttpPost(SiteTokenAPIHost + "api/Auth/GetAppToken", reqObj.ToJson(), null);
                AuthenTokenHelper Token = JsonConvert.DeserializeObject<AuthenTokenHelper>(tokenstrs);
                Tokendata data = Token.BusData.data.ToString().ToObject<Tokendata>();
                RedisHelper.StringSet("AuthenGetTokenKey", data.Token, new TimeSpan(0, 20, 0));
                tokenstrs = data.Token;
            }
            return tokenstrs;
        }

        public static string HttpPost(string data, string uri, string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                token = $"Bearer {token}";
            }
            return HttpMethods.HttpPostWithToken(uri, data, token);
        }

        /// <summary>
        /// Post 请求
        /// 鉴权失败自动刷新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="uri"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static BusData<T> HttpPost<T>(string data, string uri, OperatorModel user)
        {
            string token = "";
            try
            {
                token = GetUserToken(user);
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new FailedException("[鉴权]未获取到 Api 身份令牌，请联系管理员");
                }
                token = $"Bearer {token}";
                string response = HttpMethods.HttpPostWithToken(uri, data, token);
                ApiManageResponse<T> apiResp = JsonConvert.DeserializeObject<ApiManageResponse<T>>(response);
                if (apiResp.HttpStatus == (int)HttpStatusCode.Unauthorized)
                {
                    //鉴权失败 刷新token
                    token = GetUserTokenRefesh(user, token);
                    //重新请求接口
                    response = HttpMethods.HttpPostWithToken(uri, data, token);
                    apiResp = JsonConvert.DeserializeObject<ApiManageResponse<T>>(response);
                }
                if (apiResp.HttpStatus != (int)HttpStatusCode.OK)
                {
                    throw new FailedException($"[Http]请求异常：{apiResp.HttpStatus}=>{apiResp.Message}");
                }
                return apiResp.BusData;
            }
            catch (Exception ex)
            {
                throw new FailedException($"请求异常：{ex.Message}");
            }
        }
        /// <summary>
        /// token 过期 强制刷新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetUserTokenRefesh(OperatorModel user, string token)
        {
            var key = GetUserCacheKey(user.OrganizeId, user.UserCode);
            var reqObj = new
            {
                AppId = appId,
                OrganizeId = user.OrganizeId,
                Data = new
                {
                    TopOrganizeId = user.TopOrganizeId,
                    UserCode = user.UserCode,
                    UserName = user.UserName,
                    Account = user.UserCode,
                    Timestamp = DateTime.Now,
                    KeyLicense = accessAppId
                }
            };
            string tokenstrs = HttpMethods.HttpPostWithToken(SiteTokenAPIHost + "api/Auth/UserTokenRefresh", reqObj.ToJson(), token);
            var Token = JsonConvert.DeserializeObject<ApiManageResponse<Tokendata>>(tokenstrs);
            RedisHelper.StringSet(key, Token.BusData.data.Token, new TimeSpan(0, 20, 0));
            return Token.BusData.data.Token;
        }
        /// <summary>
        /// 用户登陆后获取api通用 token
        /// </summary>
        /// <returns></returns>
        public static string GetUserToken(OperatorModel user)
        {
            var key = GetUserCacheKey(user.OrganizeId, user.UserCode);
            string tokenstr = RedisHelper.StringGet(key);
            string tokenstrs = "";
            if (!string.IsNullOrWhiteSpace(tokenstr))
            {
                tokenstrs = tokenstr;
            }
            else
            {
                var reqObj = new
                {
                    AppId = appId,
                    OrganizeId = user.OrganizeId,
                    Data = new
                    {
                        TopOrganizeId = user.TopOrganizeId,
                        UserCode = user.UserCode,
                        UserName = user.UserName,
                        Account = user.UserCode,
                        Timestamp = DateTime.Now,
                        KeyLicense = accessAppId
                    }
                };
                tokenstrs = HttpMethods.HttpPost(SiteTokenAPIHost + "api/Auth/GetUserToken", reqObj.ToJson(), null);
                var Token = JsonConvert.DeserializeObject<ApiManageResponse<Tokendata>>(tokenstrs);
                RedisHelper.StringSet(key, Token.BusData.data.Token, new TimeSpan(0, 20, 0));
                tokenstrs = Token.BusData.data.Token;
            }
            return tokenstrs;
        }

        public static string GetUserCacheKey(string orgId, string user = null)
        {
            if (string.IsNullOrWhiteSpace(accessAppId))
            {
                throw new FailedException("[鉴权]未读取到接口AppId配置（Token_ApiManage）信息");
            }
            if (string.IsNullOrWhiteSpace(userCacheToken))
            {
                userCacheToken = string.Format(CacheKey.AccessToken, appId, orgId, accessAppId) + $"_{user}";
            }
            return userCacheToken;
        }
    }

    #region ResponseDTO
    public class ApiManageResponse<T>
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public BusData<T> BusData { get; set; }
    }
    public class BusData<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }


    public class AuthenTokenHelper
    {
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public BusData BusData { get; set; }

    }
    public class BusData
    {
        public int code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }
    public class Tokendata
    {
        public string Token { get; set; }
    }

    #endregion
}
