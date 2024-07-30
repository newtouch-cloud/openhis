using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Common;
using Newtouch.HIS.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ModuleController : ControllerBase
    {
        private readonly ISysModuleExRepo _sysModuleRepo;
        private readonly ISysModuleDmnService _sysModuleDmnService;

        /// <summary>
        /// 加载时，获取左边树节点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetModuleTreeJsonByName(string name)
        {
            var data = _sysModuleRepo.GetModuleTreeJsonByName(name);
            {
                var authedList = _sysModuleDmnService.GetMenuList(this.OrganizeId
                    , this.UserIdentity.RoleIdList, this.UserIdentity.IsRoot, this.UserIdentity.IsAdministrator);
                data = data.TreeWhere(p => authedList.Any(i => i.Id == p.Id), parentId:"ParentId");
            }
            var treeList = new List<TreeViewModel>();
            foreach (SysModuleEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.UrlAddress;
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }

    }
}
