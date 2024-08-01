using System.Web.Mvc;
using Newtouch.Tools;
using System.Collections.Generic;
using Newtouch.Common;
using System.Linq;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Application.Interface;
using Newtouch.Core.Common;

namespace FrameworkBase.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:15
    /// 描 述：角色菜单授权表
    /// </summary>
    [AutoResolveIgnore]
    public class SysRoleAuthorizeController : BaseController
    {
        private readonly ISysRoleAuthorizeRepo _sysRoleAuthorizeRepo;
        private readonly ISysModuleRepo _sysModuleRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysRoleAuthorizeRepo"></param>
        /// <param name="sysModuleRepo"></param>
        public SysRoleAuthorizeController(ISysRoleAuthorizeRepo sysRoleAuthorizeRepo
            , ISysModuleRepo sysModuleRepo)
        {
            this._sysRoleAuthorizeRepo = sysRoleAuthorizeRepo;
            this._sysModuleRepo = sysModuleRepo;
        }

        /// <summary>
        /// 获取角色授权菜单树
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetPermissionTree(string roleId)
        {
            var moduledata = _sysModuleRepo.GetValidList();
            var authorizedata = new List<SysRoleAuthorizeEntity>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authorizedata = _sysRoleAuthorizeRepo.IQueryable(t => t.zt == "1" && t.RoleId == roleId).ToList();
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