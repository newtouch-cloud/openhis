using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using System.Linq;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Common;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class ModuleController : ControllerBase
    {
        private readonly ISysModuleDmnService _sysModuleDmnService;
        private readonly IModuleApp _moduleApp;

        public ModuleController(IModuleApp moduleApp, ISysModuleDmnService sysModuleDmnService)
        {
            this._moduleApp = moduleApp;
            this._sysModuleDmnService = sysModuleDmnService;
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
            foreach (SysModuleEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        //grid json
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string keyword)
        {

            var data =_sysModuleDmnService.GetMenuListByOrg(this.OrganizeId);
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
            foreach (SysModuleEntity item in newList)
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

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _moduleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
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
                if (this.OrganizeId != Constants.TopOrganizeId
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

        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _moduleApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}