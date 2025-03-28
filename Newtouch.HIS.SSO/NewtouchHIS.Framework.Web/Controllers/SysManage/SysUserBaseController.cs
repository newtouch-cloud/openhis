using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;

namespace NewtouchHIS.Framework.Web.Controllers
{
    public class SysUserBaseController : OrgControllerBase
    {
        private readonly ISysUserAppService _userAppService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmn;
        public SysUserBaseController(ISysUserAppService userAppService, IUserRoleAuthDmnService userRoleAuthDmn)
        {
            _userAppService = userAppService;
            _userRoleAuthDmn = userRoleAuthDmn;
        }
        public IActionResult UserRoles()
        {
            return View();
        }
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetPagintionGridJson(OLPagination<QueryParamsBase> pagination, string orgId, string keyword)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return Error("定位当前权限内的组织机构失败");
            }
            pagination.queryParams = new QueryParamsBase
            {
                orgId = orgId,
                keyword = keyword ?? ""
            };
            var userPage = await _userAppService.GetSysUserPageAsync(pagination);
            return Content(userPage.ToJson());
        }
        public async Task<ActionResult> GetSysUserSelectorTree(string? orgId = null, string? roleId = null, string? from = null, bool isShowEmpty = false, bool isExpand = true, bool isContansChildOrg = true)
        {
            List<string> roleUser = new List<string>();
            if (!string.IsNullOrWhiteSpace(roleId))
            {
                roleUser = (await _userRoleAuthDmn.GetCurUserIdListByRoleId(roleId)).Select(p => p.First).ToList();
            }
            var userTree = await _userAppService.GetOrgUserTreeWithDeptAsync(new QueryParamsBase
            {
                stringList = roleUser,
            });
            return Content(userTree.ToJson());
        }

        /// <summary>
        /// 更新 用户角色
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="organizeId"></param>
        /// <param name="roleList"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public async Task<IActionResult> UpdateUserRole(string keyValue, string organizeId, List<string> roleList)
        {
            var result = await _userRoleAuthDmn.UpdateUserRole(keyValue, organizeId, roleList, UserIdentity.UserCode);
            return result ? Success("操作成功（用户重新登录生效）") : Error("操作失败");
        }

    }
}
