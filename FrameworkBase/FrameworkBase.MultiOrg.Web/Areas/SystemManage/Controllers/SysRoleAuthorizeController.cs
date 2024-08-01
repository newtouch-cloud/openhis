using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Common;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common;

namespace FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 角色授权
    /// </summary>
    [AutoResolveIgnore]
    public class SysRoleAuthorizeController : OrgControllerBase
    {
        private readonly ISysModuleDmnService _sysModuleDmnService;
        private readonly ISysRoleAuthorizeRepo _sysRoleAuthorizeRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysModuleDmnService"></param>
        /// <param name="_sysRoleAuthorizeRepo"></param>
        public SysRoleAuthorizeController(ISysModuleDmnService sysModuleDmnService
            , ISysRoleAuthorizeRepo _sysRoleAuthorizeRepo)
        {
            this._sysModuleDmnService = sysModuleDmnService;
            this._sysRoleAuthorizeRepo = _sysRoleAuthorizeRepo;
        }

        /// <summary>
        /// 编辑角色时 的 菜单树
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ActionResult GetPermissionTree(string roleId)
        {
            var moduledata = _sysModuleDmnService.GetMenuListByOrg(this.OrganizeId).Where(p => p.zt == "1").ToList();
            var authorizedata = new List<SysRoleAuthorizeEntity>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authorizedata = _sysRoleAuthorizeRepo.IQueryable(t => t.zt == "1" && t.RoleId == roleId).ToList();
            }
            var treeList = new List<TreeViewModel>();
            foreach (SysModuleEntity item in moduledata)
            {
                if (!(item.zt == "1"))
                {
                    //181122排除无效的
                    continue;
                }
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
