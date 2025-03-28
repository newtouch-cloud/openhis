using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.WebAPI.Manage.Controllers
{
    /// <summary>
    /// 鉴权管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Obsolete("该功能即将下线，请访问鉴权中心")]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
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
            //return await _jwtService.GetTokenAsync(new JWTTokenRequest
            //{
            //    authType = AuthType.WebApi,
            //    authIdentity = new AuthIdentity
            //    {
            //        AppId = request.AppId
            //    }
            //});
            return null;
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
            //return await _jwtService.GetTokenAsync(new JWTTokenRequest
            //{
            //    authType = AuthType.Web,
            //    authIdentity = request.Data
            //});
            return null;
        }
    }
}
