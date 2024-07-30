using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.AuthenticationCenter.Services;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.AuthenticationCenter.Controllers
{
    /// <summary>
    /// 鉴权管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJWTService _jwtService;
        private readonly IAppManageDmnService _appManageDmn;
        public AuthController(IJWTService jwtService, IAppManageDmnService appManageDmn)
        {
            _jwtService = jwtService;
            _appManageDmn = appManageDmn;
        }
        /// <summary>
        /// 第三方系统获取Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetAppToken")]
        [AllowAnonymous, HttpPost]
        public async Task<BusResult<JWTToken>> GetTokenAsync(Request<string> request)
        {
            var appInfo = await _appManageDmn.AppKeyGen(new AppFriendAuthKeyRequest { FriendAppId = request.AppId, AppId = request.Data });
            if (appInfo == null)
            {
                return new BusResult<JWTToken> { code = ResponseResultCode.FAIL, msg = "应用未注册，请联系管理员" };
            }
            return await _jwtService.GetTokenAsync(new JWTTokenRequest
            {
                authType = AuthType.WebApi,
                authIdentity = new AuthIdentity
                {
                    AppId = request.AppId,
                    KeyLicense = request.Data ?? request.AppId,
                    AppAuthKey = appInfo
                }
            });
        }
        /// <summary>
        /// 应用授权申请Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetAppFrienAuthToken")]
        [AllowAnonymous, HttpPost]
        public async Task<BusResult<JWTToken>> GetAppFrienAuthTokenAsync(Request<string> request)
        {
            var appInfo = await _appManageDmn.AppKeyGen(new AppFriendAuthKeyRequest { FriendAppId = request.AppId, AppId = request.Data });
            if (string.IsNullOrWhiteSpace(appInfo))
            {
                return new BusResult<JWTToken> { code = ResponseResultCode.FAIL, msg = "未找到相关应用授权信息，请联系管理员" };
            }
            return await _jwtService.GetTokenAsync(new JWTTokenRequest
            {
                authType = AuthType.WebApi,
                authIdentity = new AuthIdentity
                {
                    AppId = request.AppId,
                    KeyLicense = request.Data,
                    AppAuthKey = appInfo
                }
            });
        }
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserTokenRefresh")]
        public async Task<BusResult<JWTToken>> UserTokenRefreshAsync(Request<AuthIdentity> request)
        {
            var valid = await ValidRequest(request);
            if (valid.code != ResponseResultCode.SUCCESS)
            {
                return new BusResult<JWTToken> { code = valid.code, msg = valid.msg };
            }
            return await _jwtService.TokenRefreshAsync(new JWTTokenRequest
            {
                authType = AuthType.Web,
                authIdentity = valid.Data
            });
        }
        /// <summary>
        /// 内部Web系统获取Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetUserToken")]
        [AllowAnonymous, HttpPost]
        public async Task<BusResult<JWTToken>> GetUserTokenAsync(Request<AuthIdentity> request)
        {
            if (request.Data == null)
            {
                return new BusResult<JWTToken> { code = ResponseResultCode.FAIL, msg = "用户信息不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data?.KeyLicense))
            {
                return new BusResult<JWTToken> { code = ResponseResultCode.FAIL, msg = "请传入授权应用AppId" };
            }
            var appInfo = await _appManageDmn.AppKeyGen(new AppFriendAuthKeyRequest { AppId = request.Data.KeyLicense, FriendAppId = request.AppId });
            if (appInfo == null)
            {
                return new BusResult<JWTToken> { code = ResponseResultCode.FAIL, msg = "应用未注册，请联系管理员" };
            }
            request.Data.AppAuthKey = appInfo;
            request.Data.AppId = request.AppId;
            request.Data.OrganizeId = request.OrganizeId;
            request.Data.KeyLicense = request.Data.KeyLicense;
            return await _jwtService.GetTokenAsync(new JWTTokenRequest
            {
                authType = AuthType.Web,
                authIdentity = request.Data
            });
        }

        #region private
        private async Task<BusResult<AuthIdentity>> ValidRequest(Request<AuthIdentity> request)
        {
            if (request.Data == null)
            {
                return new BusResult<AuthIdentity> { code = ResponseResultCode.FAIL, msg = "用户信息不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data?.KeyLicense))
            {
                return new BusResult<AuthIdentity> { code = ResponseResultCode.FAIL, msg = "请传入授权应用AppId" };
            }
            var appInfo = await _appManageDmn.AppKeyGen(new AppFriendAuthKeyRequest { AppId = request.Data.KeyLicense, FriendAppId = request.AppId });
            if (appInfo == null)
            {
                return new BusResult<AuthIdentity> { code = ResponseResultCode.FAIL, msg = "应用未注册，请联系管理员" };
            }
            request.Data.AppAuthKey = appInfo;
            request.Data.AppId = request.AppId;
            request.Data.OrganizeId = request.OrganizeId;
            request.Data.KeyLicense = request.Data.KeyLicense;
            return new BusResult<AuthIdentity> { code = ResponseResultCode.SUCCESS, Data = request.Data };
        }
        #endregion
    }
}
