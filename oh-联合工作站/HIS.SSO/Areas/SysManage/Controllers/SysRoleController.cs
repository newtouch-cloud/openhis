using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;

namespace HIS.SSO.Areas.SysManage.Controllers
{
    [Area("SysManage")]
    public class SysRoleController : OrgControllerBase
    {

        private readonly ISysRoleDmnService _sysRoleDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        public SysRoleController(ISysRoleDmnService sysRoleDmnService, IUserRoleAuthDmnService userRoleAuthDmnService)
        {
            _sysRoleDmnService = sysRoleDmnService;
            _userRoleAuthDmnService = userRoleAuthDmnService;
        }
        public override IActionResult Index()
        {
            //ViewBag.OrganizeId = UserIdentity?.OrganizeId;
            //ViewBag.TopOrganizeId = UserIdentity?.TopOrganizeId;
            return View();
        }
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetRoleList(string orgId,string userId)
        {
            var roleList = await _sysRoleDmnService.GetRoleList(orgId);
            if (roleList == null || roleList.Count == 0)
            {
                return Content(new List<BsTreeSelectModel>().ToJson());
            }
            var userRoles = (await _sysRoleDmnService.GetUserRoleList(userId, orgId))!.Select(p => p.Id).ToList();
            var treeList = roleList.Select(x => new BsTreeSelectModel
            {
                href = x.Id,
                text = x.Name,
                selected = userRoles.Any(y => y == x.Id)
            }).ToList();
            return Content(treeList.ToJson());
        }
        public async Task<ActionResult> GetGridJson(Pagination pagination, string keyword, string OrganizeId)
        {

            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                OrganizeId = this.OrganizeId;
            }
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;    //不返回
            }

            var data = new
            {
                rows = await _sysRoleDmnService.GetPagintionList(OrganizeId, keyword),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());

        }
       
        public override IActionResult Form()
        {
            //ViewBag.OrganizeId = UserIdentity?.OrganizeId;
            //ViewBag.TopOrganizeId = UserIdentity?.TopOrganizeId;
            return View();
        }

        public async Task<ActionResult> GetFormJson(string keyValue)
        {
            var data = await _sysRoleDmnService.GetForm(keyValue);
            return Content(data.ToJson());
        }
        public ActionResult SubmitForm(SysRoleEntity roleEntity, string permissionIds, string keyValue)
        {
            roleEntity.zt = roleEntity.zt == "true" ? "1" : "0";
            //主要是为了唯一主键
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.ModifiedEntity(OrganizeId, UserIdentity.UserCode, true);
                roleEntity.Id = keyValue;
            }
            else
            {
                roleEntity.NewEntity(OrganizeId, UserIdentity.UserCode);
                roleEntity.Id = Guid.NewGuid().ToString();
            }

            List<SysRoleAuthorizeEntity> roleAuthorizeEntityList = new List<SysRoleAuthorizeEntity>();
            if (permissionIds != null && permissionIds != "")
            {
                foreach (var itemId in permissionIds.Split(','))
                {
                    SysRoleAuthorizeEntity roleAuthorizeEntity = new SysRoleAuthorizeEntity();
                    roleAuthorizeEntity.NewEntity(UserIdentity.UserCode);
                    roleAuthorizeEntity.Id = Guid.NewGuid().ToString();
                    roleAuthorizeEntity.RoleId = roleEntity.Id;
                    roleAuthorizeEntity.ItemId = itemId.ToString();
                    roleAuthorizeEntity.zt = "1";   //始终1 有效
                    roleAuthorizeEntityList.Add(roleAuthorizeEntity);
                }
            }
            _sysRoleDmnService.SubmitForm(roleEntity, roleAuthorizeEntityList, keyValue, UserIdentity.UserCode, OrganizeId);
            return (ActionResult)Success("操作成功。");
        }
        /// <summary>
        /// 保存角色用户关联关系
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult submitRoleUser(string roleId, List<FirstSecond> userList)
        {
            _userRoleAuthDmnService.submitRoleUser(roleId, userList, UserIdentity.UserCode);
            return (ActionResult)Success("操作成功");
        }
        public ActionResult DeleteForm(string keyValue)
        {
            _sysRoleDmnService.DeleteForm(keyValue);
            return (ActionResult)Success("删除成功。");
        }
    }
}
