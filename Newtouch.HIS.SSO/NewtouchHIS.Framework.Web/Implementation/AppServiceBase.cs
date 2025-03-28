using Mapster;
using Newtonsoft.Json;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Services.HttpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Framework.Web.Implementation
{
    public class AppServiceBase
    {
        protected readonly IHttpClientHelper _httpClient;
        /// <summary>
        /// 鉴权Token换取
        /// </summary>
        protected readonly IAuthCenterAppService _authCenterApp;

        protected AppServiceBase(IHttpClientHelper httpClient, IAuthCenterAppService authCenterApp)
        {
            _httpClient = httpClient;
            _authCenterApp = authCenterApp;
        }
        //获取当前用户
        protected OperatorModel? currentUser => OperatorProvider.GetCurrent();
        protected string TopOrganizeId = ConfigInitHelper.SysConfig.Top_OrganizeId ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "Top_OrganizeId");
        /// <summary>
        /// 服务提供应用Id
        /// </summary>
        protected string? AppId;
        /// <summary>
        /// 请求服务AppId
        /// </summary>
        protected string ClientAppId = ConfigInitHelper.SysConfig.AppId ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "AppId");
        /// <summary>
        /// 应用Host地址
        /// </summary>
        protected string? Host;
        /// <summary>
        /// 接口列表
        /// </summary>
        protected static List<AppAPIMethods>? Methods;
        public async virtual Task<List<AppAPIMethods>>? GetMethodsAsync() => await Task.Run(() => Methods);
        public async virtual Task<AppAPIHostDic> GetServiceAsync()
            => await Task.Run(() => new AppAPIHostDic { AppId = AppId, Host = Host });
        /// <summary>
        /// token 缓存key
        /// </summary>
        /// <param name="userCacheKey"></param>
        /// <returns></returns>
        //public virtual string Cache_GetUserKey(string userCacheKey) => "";

        public string Cache_GetUserTokenKey(string user, string orgId, string appId)
        {
            return $"{SystemKey.AssemblyUserTokenKey(user, orgId)}_{appId}";
        }

        protected virtual Request<T> RequestAsync<T>(T t, string? orgId = null)
        {
            return new Request<T>
            {
                AppId = ClientAppId,
                Timestamp = DateTime.Now,
                OrganizeId = orgId ?? currentUser?.OrganizeId,
                Data = t
            };
        }
        /// <summary>
        /// Post 接口调用(匿名)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        protected async Task<BusResult<T>> HttpPostAnonymous<T>(string data, string uri)
        {
            var result = await _httpClient.PostAsync<Result>(uri, data);
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
        /// Post 接口调用（token）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="uri"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        protected async Task<BusResult<T>> HttpPostWithBusResult<T>(string data, string uri, string token)
        {
            var result = await _httpClient.PostAsync<Result>(uri, data, token);
            if (result != null && result.HttpStatus == HttpStatusCode.Unauthorized)
            {
                return new BusResult<T> { code = ResponseResultCode.INVALIDTOKEN, msg = result.Message };
            }
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
        /// 自动组装用户信息
        /// </summary>
        /// <typeparam name="TResp"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public async Task<TResp> HttpPostWithToken<TResp, TRequest>(TRequest request, string method, string? appId = null)
        {
            var currentUser = OperatorProvider.GetCurrent();
            if (currentUser == null)
            {
                throw new FailedException($"当前用户信息获取失败：用户信息为空");
            }
            if(currentUser.IsRoot||currentUser.IsAdministrator)
            {
                currentUser.OrganizeId = currentUser.OrganizeId ?? TopOrganizeId;
            }
            if (string.IsNullOrWhiteSpace(currentUser.OrganizeId) || string.IsNullOrWhiteSpace(currentUser.UserCode))
            {
                throw new FailedException($"当前用户信息获取失败：机构信息/职工编码为空");
            }
            string cacheKey = Cache_GetUserTokenKey(currentUser.UserCode, currentUser.OrganizeId, appId ?? AppId);
            return await HttpPostWithToken<TResp, TRequest>(RequestAsync(request), method, cacheKey, currentUser);


        }
        public async Task<TResp> HttpPostWithToken<TResp, TRequest>(Request<TRequest> request, string method, OperatorModel user)
        {
            return await HttpPostWithToken<TResp, TRequest>(request, method, null, user);
        }
        /// <summary>
        /// HttpPost 携带Token 接口组装
        /// </summary>
        /// <typeparam name="TResp"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <param name="method"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="FailedException"></exception>
        public async Task<TResp> HttpPostWithToken<TResp, TRequest>(Request<TRequest> request, string method, string userCacheKey, OperatorModel user)
        {
            //读取token
            if (string.IsNullOrEmpty(user.UserCode)|| (string.IsNullOrEmpty(user.OrganizeId)&& string.IsNullOrEmpty(user.TopOrganizeId)))
            {
                throw new FailedException(ResponseResultCode.FAILOfAuth,"用户身份信息异常:工号/机构信息不可为空");
            }
            string key = Cache_GetUserTokenKey(user?.UserCode, user?.OrganizeId, AppId);
            string token = RedisHelper.GetString(key);
            if (string.IsNullOrEmpty(token) && user != null)
            {
                token = await _authCenterApp.GetUserToken(user, AppId);
                RedisHelper.SetString(key, token);
            }
            var result = await HttpPostWithBusResult<TResp>(request.ToJson(), method, token);
            if (result != null && result.code == ResponseResultCode.INVALIDTOKEN)
            {
                if (user == null)
                {
                    throw new FailedException("用户信息读取失败，接口访问未授权");
                }
                token = await _authCenterApp.GetUserToken(user, AppId);
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new FailedException("Token获取失败，请联系管理员");
                }
                else
                {
                    RedisHelper.SetString(key, token);
                    result = await HttpPostWithBusResult<TResp>(request.ToJson(), method, token);
                }
            }
            if (result != null && result.code == ResponseResultCode.SUCCESS)
            {
                return result.Data.Adapt<TResp>();
            }
            throw new FailedException(result?.msg);

        }
    }
}
