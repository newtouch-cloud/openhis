using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;

namespace NewtouchHIS.Framework.Web.Implementation
{
    public interface ILoginAppService : IScopedDependency
    {
        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<SysUserVEntity> CheckPwdAsync(LoginBO request);
        /// <summary>
        /// 用户数据初始化
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userCacheKey"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<OperatorModel> BuildLoginStatusOprAsync(string userId, OperatorModel user);
    }
    public class LoginAppService : AppServiceBase, ILoginAppService
    {
        public LoginAppService(IHttpClientHelper httpClient, IAuthCenterAppService authCenterApp) : base(httpClient, authCenterApp)
        {
            ClientAppId = ConfigInitHelper.SysConfig.AppId ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "AppId");
            AppId = ConfigInitHelper.SysConfig.AppAPIHostName?.HisAppBaseAPIHost ?? "HIS.BaseAPI";
            Host = ConfigInitHelper.SysConfig.AppAPIHost?.HisAppBaseAPIHost ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "HisAppBaseAPIHost");

        }
        #region API 列表
        /// <summary>
        /// 登录校验
        /// </summary>
        public string CheckPwdApi => $"{Host}api/login/CheckPwd";
        public string BuildLoginStatusOprApi => $"{Host}api/login/BuildLoginStatusOpr";

        #endregion
        public async Task<SysUserVEntity> CheckPwdAsync(LoginBO request)
        {
            var result = await HttpPostAnonymous<SysUserVEntity>(RequestAsync(request).ToJson(), CheckPwdApi);
            if (result != null && result.code == ResponseResultCode.SUCCESS && result.Data != null)
            {
                return result.Data;
            }
            else if (result != null && result.code == ResponseResultCode.FAIL)
            {
                throw new FailedException($"登录失败，请联系管理员:{result?.msg}");
            }
            throw new FailedException($"登录失败，请联系管理员");
        }
        public async Task<OperatorModel> BuildLoginStatusOprAsync(string userId, OperatorModel user)
        {
            if (string.IsNullOrWhiteSpace(user.OrganizeId))
            {
                user.OrganizeId = user.TopOrganizeId;
            }
            return await HttpPostWithToken<OperatorModel, string>(RequestAsync(userId), BuildLoginStatusOprApi, user);
        }
    }
}
