using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;

namespace NewtouchHIS.Framework.Web.Implementation
{
    public interface IAuthCenterAppService
    {
        /// <summary>
        /// App访问Token
        /// </summary>
        /// <returns></returns>
        Task<string> GetAppToken(string accessAppId);
        /// <summary>
        /// 用户Token获取
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> GetUserToken(OperatorModel user, string accessAppId);
        /// <summary>
        /// 用户Token续期
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> GetUserTokenRefesh(OperatorModel user, string token, string accessAppId);
    }
    public class AuthCenterAppService : IAuthCenterAppService
    {
        private readonly IHttpClientHelper _httpClient;
        /// <summary>
        /// 服务提供应用Id
        /// </summary>
        internal string? AppId;
        /// <summary>
        /// 请求服务AppId
        /// </summary>
        internal string? ClientAppId;
        /// <summary>
        /// 应用Host地址
        /// </summary>
        internal string? Host;
        public AuthCenterAppService(IHttpClientHelper httpClient)
        {
            _httpClient = httpClient;
            ClientAppId = ConfigInitHelper.SysConfig.AppId ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "AppId");
            Host = ConfigInitHelper.SysConfig.AppAPIHost?.SiteAuthCenterHost ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "SiteAuthCenterHost");
        }
        #region 接口列表
        /// <summary>
        /// App访问Token
        /// </summary>
        public string GetAppTokenApi => $"{Host}api/Auth/GetAppToken";
        /// <summary>
        /// 用户访问Token
        /// </summary>
        public string GetUserTokenApi => $"{Host}api/Auth/GetUserToken";
        /// <summary>
        /// 用户Token续期
        /// </summary>
        public string UserTokenRefreshApi => $"{Host}api/Auth/UserTokenRefresh";

        #endregion
        /// <summary>
        /// App获取授权Token
        /// </summary>
        /// <param name="appId">本应用Id</param>
        /// <param name="accessAppId">申请访问应用Id</param>
        /// <returns></returns>
        public async Task<string> GetAppToken(string accessAppId)
        {
            var reqObj = new
            {
                AppId = ClientAppId,
                Data = accessAppId
            };
            return await HttpPostWithTokenAsync(reqObj.ToJson(), GetAppTokenApi);
        }

        /// <summary>
        /// 用户获取Token
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUserToken(OperatorModel user, string accessAppId)
        {
            var reqObj = new
            {
                AppId = ClientAppId,
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
            return await HttpPostWithTokenAsync(reqObj.ToJson(), GetUserTokenApi, null);
        }
        /// <summary>
        /// 用户Token续期
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> GetUserTokenRefesh(OperatorModel user, string token, string accessAppId)
        {
            var reqObj = new
            {
                AppId = ClientAppId,
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
            return await HttpPostWithTokenAsync(reqObj.ToJson(), UserTokenRefreshApi, token);
        }

        private async Task<string> HttpPostWithTokenAsync(string request, string method, string? token = null)
        {
            Result tokenResult;
            if(!string.IsNullOrWhiteSpace(token))
            {
                tokenResult = await _httpClient.PostAsync<Result>(method, request, token);
            }
            else
            {
                tokenResult = await _httpClient.PostAsync<Result>(method, request);
            }
            
            if (tokenResult != null && tokenResult.BusData != null)
            {
                var tokenObj = JsonConvert.DeserializeObject<BusResult<JWTToken>>(tokenResult.BusData.ToJson());
                if (tokenObj != null && tokenObj.code==ResponseResultCode.SUCCESS && tokenObj.Data!=null)
                {
                    return tokenObj.Data.Token;
                }
                else if(tokenObj != null && tokenObj.code==ResponseResultCode.FAIL)
                {
                    throw new FailedException($"{tokenObj.msg}");
                }
                else
                {
                    throw new FailedException($"Path:({method}):{tokenObj?.msg}");
                }
            }
            
            throw new FailedException($"[鉴权]接口返回异常({method}):{tokenResult?.ToJson()}");
        }
    }
}
