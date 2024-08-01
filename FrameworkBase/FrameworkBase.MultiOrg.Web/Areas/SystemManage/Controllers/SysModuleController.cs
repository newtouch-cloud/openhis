using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Common;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;

namespace FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    [AutoResolveIgnore]
    public class SysModuleController : OrgControllerBase
    {
        private readonly ISysModuleDmnService _sysModuleDmnService;
        private readonly ISysModuleRepo _sysModuleRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysModuleDmnService"></param>
        /// <param name="sysModuleRepo"></param>
        public SysModuleController(ISysModuleDmnService sysModuleDmnService
            , ISysModuleRepo sysModuleRepo)
        {
            this._sysModuleDmnService = sysModuleDmnService;
            this._sysModuleRepo = sysModuleRepo;
        }

        /// <summary>
        /// 下拉（选择菜单 编辑 上级）
        /// </summary>
        /// <param name="justTop">仅顶级</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(bool justTop = true)
        {
            var data = _sysModuleDmnService.GetMenuListByOrg(this.OrganizeId)
                .Where(p => p.zt == "1").ToList();
            if (justTop)
            {
                data = data.Where(p => string.IsNullOrWhiteSpace(p.ParentId)).ToList();
            }
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        /// <summary>
        /// 菜单列表 grid json
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = _sysModuleDmnService.GetMenuListByOrg(this.OrganizeId);
            List<SysModuleEntity> newList = data.ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                newList = data.ToList().TreeWhere(t => t.Name.Contains(keyword), parentId: "ParentId");
                if (newList.Count == 1)
                {
                    //如果有子节点，包含之
                    newList.AddRange(data.Where(p => p.ParentId == newList[0].Id));
                }
            }
            var treeList = new List<TreeGridModel>();
            foreach (var item in newList)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = newList.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson(null));
        }

        /// <summary>
        /// GetFormJson
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _sysModuleRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 提交保存菜单
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysModuleEntity moduleEntity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                if (!this.UserIdentity.IsRoot && !this.UserIdentity.IsAdministrator)
                {
                    if (string.IsNullOrEmpty(this.OrganizeId))
                    {
                        return Error("操作失败。");
                    }
                    moduleEntity.OrganizeId = this.OrganizeId;
                }
            }
            else
            {
                //是修改行为
                if (this.OrganizeId != ConstantsBase.TopOrganizeId
                    && this.OrganizeId != moduleEntity.OrganizeId)
                {
                    {
                        return Error("操作失败。数据错误");
                    }
                }
            }

            moduleEntity.zt = moduleEntity.zt == "true" ? "1" : "0";
            _sysModuleDmnService.SubmitForm(moduleEntity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                _sysModuleDmnService.DeleteModule(keyValue);
            }
            return Success("删除成功。");
        }

    }
}
