using System.Web.Mvc;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using System.Linq;
using System.Collections.Generic;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Common;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class RoleAuthorizeController : ControllerBase
    {
        private readonly IModuleApp _moduleApp;
        private readonly IModuleButtonApp _moduleButtonApp;
        private readonly IRoleAuthorizeApp _roleAuthorizeApp;
        private readonly ISysModuleDmnService _sysModuleDmnService;

        public RoleAuthorizeController(IModuleApp moduleApp, IModuleButtonApp moduleButtonApp, IRoleAuthorizeApp roleAuthorizeApp
            , ISysModuleDmnService sysModuleDmnService)
        {
            this._moduleApp = moduleApp;
            this._moduleButtonApp = moduleButtonApp;
            this._roleAuthorizeApp = roleAuthorizeApp;
            this._sysModuleDmnService = sysModuleDmnService;
        }

        /// <summary>
        /// 系统权限树 （菜单+按钮）
        /// </summary>
        /// <param name="roleId">当前角色Id</param>
        /// <returns></returns>
        public ActionResult GetPermissionTree(string roleId)
        {
            //且有效菜单
            var moduledata = _sysModuleDmnService.GetMenuListByOrg(this.OrganizeId).Where(p => p.zt == "1").ToList();
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
            return Content(treeList.TreeViewJson(null));
        }
    }
}