using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class RoleAuthorizeController : ControllerBase
    {
        private readonly IRoleAuthorizeApp _roleAuthorizeApp;
        private readonly ISysModuleDmnService _sysModuleDmnService;

        public RoleAuthorizeController(IRoleAuthorizeApp roleAuthorizeApp
            , ISysModuleDmnService sysModuleDmnService)
        {
            this._roleAuthorizeApp = roleAuthorizeApp;
            this._sysModuleDmnService = sysModuleDmnService;
        }

        public ActionResult GetPermissionTree(string roleId)
        {
            var moduledata = _sysModuleDmnService.GetOpenMenuListByTopOrg(Constants.TopOrganizeId);
            var buttondata = _sysModuleDmnService.GetOpenMenuButtonListByTopOrg(Constants.TopOrganizeId);
            var authorizedata = new List<SysRoleAuthorizeEntity>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authorizedata = _roleAuthorizeApp.GetValidList(roleId);
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
            foreach (SysModuleButtonEntity item in buttondata)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = item.ModuleId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.ItemId == item.Id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }
    }
}
