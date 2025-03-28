using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class OrganizeController : ControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysOrganizeRepository _sysOrganizeRepository;
        private readonly ISysModuleDmnService _sysModuleDmnService;

        public OrganizeController(ISysOrganizeDmnService sysOrganizeDmnService
            , ISysOrganizeRepository sysOrganizeRepository
            , ISysModuleDmnService sysModuleDmnService)
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysOrganizeRepository = sysOrganizeRepository;
            this._sysModuleDmnService = sysModuleDmnService;
        }

        //下拉 数据源
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson()
        {
            var data = _sysOrganizeRepository.GetValidListByTopOrg(Constants.TopOrganizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (SysOrganizeEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        //下拉 数据源
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetChildTreeSelectJson(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                orgId = base.GetAuthOrganizeId();
            }
            var data = _sysOrganizeRepository.GetValidListByParentOrg(orgId);
            var treeList = new List<TreeSelectModel>();
            foreach (SysOrganizeEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.Id == orgId ? null : item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        //grid json
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(string keyword)
        {
            string topOrgId = base.GetAuthOrganizeId();
            //集团用户可以修改所有的组织机构，每个机构的操作人员只能维护自己机构下的子机构。
            var data = _sysOrganizeRepository.GetListByParentOrg(topOrgId);
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword) || t.Code.Contains(keyword), parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (var item in data)
            {
                item.AllowEdit = item.Id != topOrgId; //最高级不可修改，其他可修改

                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Any(t => t.ParentId == item.Id);
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.Id == topOrgId ? null : item.ParentId;
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
            var data = _sysOrganizeRepository.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SysOrganizeEntity entity, string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue) && entity.ParentId == keyValue)
            {
                throw new FailedException("上级机构选择错误");
            }
            if (string.IsNullOrWhiteSpace(entity.ParentId))
            {
                throw new FailedException("请选择上级组织机构");
            }
            if (string.IsNullOrWhiteSpace(entity.Code))
            {
                throw new FailedException("编码不能为空");
            }
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.TopOrganizeId = Constants.TopOrganizeId;
            _sysOrganizeRepository.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _sysOrganizeRepository.Delete(p => p.Id == keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetAuthOrganizeList(string keyValue, string authType = "module")
        {
            var orgList = _sysOrganizeRepository.GetValidTopOrgList();
            var treeList = new List<TreeViewModel>();
            IList<SysOrganizeEntity> currentOrgEntityList = new List<SysOrganizeEntity>();
            if (authType == "module")
            {
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    currentOrgEntityList = _sysModuleDmnService.GetAuthedOrgListByModuleId(keyValue, 1);
                }
            }
            else if (authType == "modulebutton")
            {
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    currentOrgEntityList = _sysModuleDmnService.GetAuthedOrgListByModuleId(keyValue, 2);
                }
            }

            foreach (SysOrganizeEntity item in orgList)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = currentOrgEntityList.Count(t => t.Id == item.Id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

    }
}