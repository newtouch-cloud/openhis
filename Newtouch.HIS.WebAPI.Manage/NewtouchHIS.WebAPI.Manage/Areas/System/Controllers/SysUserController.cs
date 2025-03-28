using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Organize;

namespace NewtouchHIS.WebAPI.Manage.Areas.System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysUserController : ControllerBase
    {
        private readonly ISysUserDmnService _sysUserDmn;
        public SysUserController(ISysUserDmnService sysUserDmn)
        {
            _sysUserDmn = sysUserDmn;
        }
        //[HttpPost]
        //[Route("GetUserList")]
        //public async Task<BusResult<List<SysUserEntity>>> GetUserAsync(RequestBus<string> request)
        //{
        //    var data = await _sysUserDmn.GerUsers();
        //    return new BusResult<List<SysUserEntity>> { code = ResponseResultCode.SUCCESS, Data = data };
        //}
    }
}
