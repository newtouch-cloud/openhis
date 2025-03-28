using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;

namespace NewtouchHIS.WebAPI.Manage.Areas.System.Controllers
{
    /// <summary>
    /// 系统应用注册授权
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppManageController : ControllerBase
    {
        private readonly IAppManageDmnService _appManageDmn;
        public AppManageController(IAppManageDmnService appManageDmn)
        {
            _appManageDmn = appManageDmn;
        }
        /// <summary>
        /// 查询应用注册信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ApplicationQuery")]
        public async Task<BusResult<AuthAppVO>> ApplicationQueryAsync(RequestBus<string> request)
        {
            var data = await _appManageDmn.GetAppInfo(new AppFriendAuthKeyRequest { AppId = request.AppId });
            return new BusResult<AuthAppVO> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        /// <summary>
        /// 添加授权应用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddApplication")]
        public async Task<BusResult<SysAuthAppEntity>> AddApplicationAsync(RequestBus<AuthAppVO> request)
        {
            var data = await _appManageDmn.AddAuthApp(request.Data);
            return new BusResult<SysAuthAppEntity> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        /// <summary>
        /// 删除授权应用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DelApplication")]
        public async Task<BusResult<bool>> DelApplicationAsync(RequestBus<AuthAppVO> request)
        {
            var data = await _appManageDmn.DelAuthApp(request.Data);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, Data = data };
        }
    }
}
