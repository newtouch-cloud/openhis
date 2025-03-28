/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 

*********************************************************************************/
using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class OrganizeController : ControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysModuleDmnService _sysModuleDmnService;

        public OrganizeController(ISysOrganizeDmnService sysOrganizeDmnService
            , ISysModuleDmnService sysModuleDmnService)
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysModuleDmnService = sysModuleDmnService;
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
            var data = _sysOrganizeDmnService.GetValidListByParentOrg(orgId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.Id == orgId ? null : item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetAuthOrganizeList(string keyValue, string authType = "module")
        {
            var orgList = _sysOrganizeDmnService.GetValidTopOrgList();
            var treeList = new List<TreeViewModel>();
            IList<SysOrganizeVEntity> currentOrgEntityList = new List<SysOrganizeVEntity>();
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
            foreach (var item in orgList)
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
