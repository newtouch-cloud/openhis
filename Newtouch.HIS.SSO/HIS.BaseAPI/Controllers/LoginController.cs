using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Lib.Base.Model;

namespace HIS.BaseAPI.Controllers
{
    /// <summary>
    /// 用户登录验证
    /// </summary>
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ISysUserVDmnService _userVDmnService;
        private readonly ISysCacheDmnService _sysCacheDmn;
        public LoginController(ISysUserVDmnService userVDmnService,ISysCacheDmnService sysCacheDmn)
        {
            _userVDmnService = userVDmnService;
            _sysCacheDmn = sysCacheDmn;
        }
        /// <summary>
        /// 用户登录密码验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous,HttpPost]
        [Route("CheckPwd")]
        public async Task<BusResult<SysUserVEntity>> CheckPwdAsync(Request<LoginBO> request)
        {
            if (request == null || request.Data == null || (string.IsNullOrWhiteSpace(request.Data!.Username) && (string.IsNullOrWhiteSpace(request.Data!.Password))))
            {
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = "登录失败，请输入用户名与密码" };
            }
            var logResult = await _userVDmnService.UserLogin(request.Data);
            if (logResult == null || logResult.Data == null)
            {
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = logResult!.msg ?? "登录失败，未找到用户信息" };
            }
            if (logResult.code != ResponseResultCode.SUCCESS)
            {
                return new BusResult<SysUserVEntity> { code = logResult.code, msg = logResult.msg };
            }
            return new BusResult<SysUserVEntity> { code = ResponseResultCode.SUCCESS, msg = logResult.msg, Data = new SysUserVEntity
            {
                Account=logResult.Data.Account,
                Id=logResult.Data.Id,
                TopOrganizeId=logResult.Data.TopOrganizeId,
                Locked=logResult.Data.Locked,                
            }};
        }

        /// <summary>
        /// 用户数据初始化
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("BuildLoginStatusOpr")]
        [HttpPost]
        public async Task<BusResult<OperatorModel>> BuildLoginStatusOprAsync(Request<string> request)
        {
            if (request.Data == null)
            {
                return new BusResult<OperatorModel> { code = ResponseResultCode.FAIL, msg = "机构Id及用户信息不可为空" };
            }
            var userOpr = await _userVDmnService.BuildLoginStatusOpr(request.Data);
            return new BusResult<OperatorModel> { code = ResponseResultCode.SUCCESS, Data = userOpr };
        }
        /// <summary>
        /// token换取用户身份信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetUserInfo")]
        [HttpPost]
        public async Task<BusResult<UserIdentity>> GetUserInfoAsync(Request<UALoginBO> request)
        {
            if(request.Data==null||string.IsNullOrWhiteSpace(request.Data.access_token))
            {
                return new BusResult<UserIdentity> { code = ResponseResultCode.INVALIDTOKEN, msg = "access_token不可为空" };
            }
            return await _sysCacheDmn.GetUserDataCache(request.Data.access_token);
        }
    }
}
