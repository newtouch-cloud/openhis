using HIS.SSO.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Framework.Web.Controllers.SysManage;

namespace HIS.SSO.Areas.SysManage.Controllers
{
    [Area("SysManage")]
    public class SysModuleController : SysModuleBaseController
    {
        private readonly IRoleAuthorizeApp _roleAuthorizeApp;
        public SysModuleController(ISysModuleDmnService sysModuleDmnService, ISysConfigDmnService sysConfigDmn, IRoleAuthorizeApp roleAuthorizeApp)
            : base(sysModuleDmnService, sysConfigDmn)
        {
            _roleAuthorizeApp = roleAuthorizeApp;
        }

        public override IActionResult Index()
        {
            UserModel user = new UserModel();
            user.OrganizeId = OrganizeId;
            user.TopOrganizeId = this.UserIdentity?.TopOrganizeId;
            user.SyncAuthed = (this.UserIdentity.IsAdministrator || this.UserIdentity.IsHospAdministrator) ? true : false;
            return View(user);
        }
        /// <summary>
        /// 系统权限树 （菜单+按钮）
        /// </summary>
        /// <param name="roleId">当前角色Id</param>
        /// <returns></returns>
        public async Task<ActionResult> GetPermissionTree(string roleId)
        {
            //且有效菜单
            var moduledata = await _sysModuleDmnService.GetEntitybyorgId(this.OrganizeId);
            var authorizedata = new List<SysRoleAuthorizeEntity>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authorizedata = await _roleAuthorizeApp.GetValidList(roleId);
            }
            var treeList = new List<TreeViewModel>();
            foreach (SysModuleEntity item in moduledata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = moduledata.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.ItemId == item.Id);
                tree.hasChildren = true;
                tree.img = item.Icon == "" ? "" : item.Icon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        public IActionResult SyncForm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubmitMenuSync(List<SysModuleExtendVO> list, string parentMenu, string parentMenuId, string fromAppId)
        {
            //已选现有菜单
            SysModuleEntity? parentEty = new SysModuleEntity();
            if (!string.IsNullOrWhiteSpace(parentMenuId))
            {
                parentEty = await _sysModuleDmnService.GetEntity(parentMenuId);
            }
            if (!string.IsNullOrWhiteSpace(parentMenu) && (parentEty == null || string.IsNullOrWhiteSpace(parentMenuId)))
            {
                //新增父级菜单(自定义菜单)
                var addParentResp = await _sysModuleDmnService.AddEntity(new SysModuleVO
                {
                    Name = parentMenu,
                    Target = "expand",
                    OrganizeId = OrganizeId,
                    Icon = "fa fa-laptop",
                }, UserIdentity.UserCode, OrganizeId);
                if (addParentResp == null || addParentResp.code != NewtouchHIS.Lib.Base.Model.ResponseResultCode.SUCCESS || addParentResp?.Data == null)
                {
                    return Error($"父级菜单添加失败：{addParentResp?.msg}|{addParentResp?.Data}");
                }
                parentMenuId = addParentResp.Data;
            }
            //查找待导入菜单中的父级菜单是否存在  当列表中未找到父级且未选择父级时反馈失败
            var selectedParentMenu = list.Where(p => !string.IsNullOrWhiteSpace(p.ParentId))?.Select(p => p.ParentId).Distinct().ToList();
            if (selectedParentMenu != null)
            {
                var parentExists = list.Where(p => selectedParentMenu.Contains(p.Id)).ToList();
                if (selectedParentMenu.Count != parentExists.Count && string.IsNullOrWhiteSpace(parentMenuId))
                {
                    return Error("请选择父级菜单");
                }
            }
            list.ForEach(p =>
            {
                p.AppId = string.IsNullOrWhiteSpace(p.ParentId) ? null : fromAppId;
            });
            var result = await _sysModuleDmnService.AddRange(list.Adapt<List<SysModuleVO>>(), UserIdentity.UserCode, OrganizeId, parentMenuId);
            if (result.code != NewtouchHIS.Lib.Base.Model.ResponseResultCode.SUCCESS)
            {
                return Error($"操作失败：{result.msg}");
            }
            return Success();
        }
    }
}
