using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using NetTaste;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Services.HttpService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Lib.Services.ApiService
{
    /// <summary>
    /// 接口访问 Token统一处理
    /// </summary>
    public class TokenService
    {
        #region private
        private readonly IHttpClientHelper _httpClient;
        /// <summary>
        /// 获取鉴权中心Api地址
        /// </summary>
        private readonly static string _authCenterHost = ConfigInitHelper.SysConfig?.AppAPIHost?.SiteAuthCenterHost;
        private readonly static string _appTokenCacheKey = "HisToken:App_";
        private readonly static string _userTokenCacheKey = "HisToken:User_";
        #endregion
        public TokenService(IHttpClientHelper httpClient)
        {
            _httpClient = httpClient;
        }
        /// <summary>
        /// App获取授权Token
        /// </summary>
        /// <param name="appId">本应用Id</param>
        /// <param name="accessAppId">申请访问应用Id</param>
        /// <returns></returns>
        public async Task<string> GetAppToken(string appId, string accessAppId)
        {
            string redisKey = $"{_appTokenCacheKey}{appId}:{accessAppId}";
            string redisValue = RedisHelper.GetString(redisKey);
            if (!string.IsNullOrWhiteSpace(redisValue))
            {
                return redisValue;
            }
            var reqObj = new
            {
                AppId = appId,
                Data = accessAppId
            };
            var tokenResult = await _httpClient.PostAsync<Result>(_getAppToken, reqObj.ToJson());
            if (tokenResult != null && tokenResult.BusData != null)
            {
                var tokenObj = JsonConvert.DeserializeObject<JWTToken>(tokenResult.BusData.ToJson());
                if (tokenObj != null && !string.IsNullOrWhiteSpace(tokenObj.Token))
                {
                    RedisHelper.SetString(redisKey, tokenObj.Token);
                    return tokenObj.Token;
                }
            }
            return tokenResult?.ToJson() ?? "";
        }
        /// <summary>
        /// 用户获取Token
        /// </summary>
        /// <returns></returns>
        public async Task<BusResult<string>> GetUserToken(OperatorModel user, string accessAppId)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.OrganizeId) || string.IsNullOrWhiteSpace(user.UserCode))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "用户信息不可为空" };
            }
            var key = GetUserCacheKey(user.OrganizeId, user.UserCode, accessAppId);


            return null;
        }
        //public async Task<BusResult<string>> GetUserTokenRefesh(OperatorModel user, string accessAppId)
        //{
        //    var key = GetUserCacheKey(user.OrganizeId, user.UserCode);
        //    var reqObj = new
        //    {
        //        AppId = appId,
        //        OrganizeId = user.OrganizeId,
        //        Data = new
        //        {
        //            TopOrganizeId = user.TopOrganizeId,
        //            UserCode = user.UserCode,
        //            UserName = user.UserName,
        //            Account = user.UserCode,
        //            Timestamp = DateTime.Now,
        //            KeyLicense = accessAppId
        //        }
        //    };
        //    string tokenstrs = HttpMethods.HttpPostWithToken(SiteTokenAPIHost + "api/Auth/UserTokenRefresh", reqObj.ToJson(), token);
        //    var Token = JsonConvert.DeserializeObject<ApiManageResponse<Tokendata>>(tokenstrs);
        //    RedisHelper.StringSet(key, Token.BusData.data.Token, new TimeSpan(0, 20, 0));
        //    return Token.BusData.data.Token;
        //}

        private string GetUserCacheKey(string orgId, string user, string accessAppId)
        {
            return $"{_userTokenCacheKey}{user}_{orgId}_{accessAppId}";
        }
        #region Method
        //public async Task<BusResult<T>> HttpPostWithBusResult<T>(string data, string uri, OperatorModel user)
        //{
        //    if (!string.IsNullOrWhiteSpace(token))
        //    {
        //        token = $"Bearer {token}";
        //    }

        //    var result = await _httpClient.PostAsync<Result>(uri, data, token);
        //    //授权到期自动续期
        //    if (result != null && result.HttpStatus == HttpStatusCode.Unauthorized)
        //    {
        //        //鉴权失败 刷新token
        //        token = GetUserTokenRefesh(user, token);
        //    }
        //    if (result == null || result.HttpStatus != HttpStatusCode.OK || result.BusData == null)
        //    {
        //        throw new FailedException($"接口返回异常：\r\nRequest:{uri}\r\nResponse:{result?.ToJson()}");
        //    }
        //    var busRet = JsonConvert.DeserializeObject<BusResult<T>>(result.BusData?.ToJson());
        //    if (busRet == null)
        //    {
        //        throw new FailedException($"业务对象映射异常：\r\nRequest:{uri}\r\nResponse:{result.BusData?.ToJson()}");
        //    }
        //    return busRet;
        //}
        public async Task<BusResult<T>> HttpPostWithBusResult<T>(string data, string uri, string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                token = $"Bearer {token}";
            }

            var result = await _httpClient.PostAsync<Result>(uri, data, token);
            if (result == null || result.HttpStatus != HttpStatusCode.OK || result.BusData == null)
            {
                throw new FailedException($"接口返回异常：\r\nRequest:{uri}\r\nResponse:{result?.ToJson()}");
            }
            var busRet = JsonConvert.DeserializeObject<BusResult<T>>(result.BusData?.ToJson());
            if (busRet == null)
            {
                throw new FailedException($"业务对象映射异常：\r\nRequest:{uri}\r\nResponse:{result.BusData?.ToJson()}");
            }
            return busRet;
        }
        /// <summary>
        /// 
        /// </summary>
        private readonly static string _getAppToken = $"{_authCenterHost}api/Auth/GetAppToken";
        /// <summary>
        /// 
        /// </summary>
        private readonly static string _getUserToken = $"{_authCenterHost}api/Auth/GetUserToken";
        /// <summary>
        /// 用户Token刷新
        /// </summary>
        private readonly static string _userTokenRefresh = $"{_authCenterHost}api/Auth/UserTokenRefresh";
        #endregion
    }
}
