using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;

namespace HIS.BaseAPI.Controllers
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Route("api/sysuser")]
    [ApiController]
    public class SysUserController : ControllerBase
    {
        private readonly ISysUserDmnService _sysUserDmn;
        public SysUserController(ISysUserDmnService sysUserDmn)
        {
            _sysUserDmn = sysUserDmn;
        }
        /// <summary>
        /// 获取系统用户（分页）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSysUserPage")]
        public async Task<BusResult<PageResponseRow<List<SysUserVO>>>> GetPagintionUserListAsync(Request<OLPagination<QueryParamsBase>> request)
        {
            var data = await _sysUserDmn.GetPagintionUserList(request.Data, request.OrganizeId ?? request.Data?.queryParams?.orgId);
            return new BusResult<PageResponseRow<List<SysUserVO>>> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        /// <summary>
        /// 机构用户树（科室分组）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrgUserTreeWithDept")]
        public async Task<BusResult<List<BsTreeSelectExtDataModel>>> GetOrgUserTreeWithDeptAsync(Request<QueryParamsBase> request)
        {
            var data = await _sysUserDmn.GetOrgUserTreeWithDeptAsync(request.OrganizeId ,request.Data?.keyword,request.Data?.stringList);
            return new BusResult<List<BsTreeSelectExtDataModel>> { code = ResponseResultCode.SUCCESS, Data = data };
        }


    }
}
