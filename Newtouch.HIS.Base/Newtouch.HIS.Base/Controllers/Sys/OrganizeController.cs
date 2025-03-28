using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Common;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Base.Controllers
{
    public class OrganizeController : ControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysOrganizeRepository _sysOrganizeRepository;
        private readonly ISysUserApp _sysUserApp;
        private readonly ISysApplicationDmnService _sysApplicationDmnService;

        public OrganizeController(ISysOrganizeDmnService sysOrganizeDmnService
            , ISysOrganizeRepository sysOrganizeRepository, ISysUserApp sysUserApp
            , ISysApplicationDmnService sysApplicationDmnService)
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysOrganizeRepository = sysOrganizeRepository;
            this._sysUserApp = sysUserApp;
            this._sysApplicationDmnService = sysApplicationDmnService;
        }

        //grid json
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _sysOrganizeRepository.GetPagintionListByTopOrg(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
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
            if (string.IsNullOrWhiteSpace(entity.Code))
            {
                throw new FailedException("编码不能为空");
            }
            entity.zt = entity.zt == "true" ? "1" : "0";

            if (string.IsNullOrWhiteSpace(keyValue))
            {
                //新增
                entity.ParentId = null;
                entity.TopOrganizeId = null;
            }

            if (string.IsNullOrWhiteSpace(entity.ParentId) || entity.ParentId == "&nbsp;")
            {
                entity.ParentId = null;
            }

            _sysOrganizeRepository.SubmitForm(entity, keyValue);

            if (string.IsNullOrWhiteSpace(keyValue) && string.IsNullOrWhiteSpace(entity.ParentId))
            {
                //已经成功创建了一个顶级组织机构 尝试为该机构初始化一个系统管理员admin
                try
                {
                    _sysUserApp.CreateDefaultAdminToOrg(entity.Id, Constants.LogonDefaultPassword);
                    //return Success("操作成功。且已成功初始化系统管理员admin");
                    return Success("操作成功<br/>且已成功初始化系统管理员admin");
                }
                catch (Exception ex)
                {
                }
            }

            return Success("操作成功。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public ActionResult GetAuthOrganizeList(string appid)
        {
            var orgList = _sysOrganizeRepository.IQueryable().Where(p => p.zt == "1").ToList();
            var treeList = new List<TreeViewModel>();
            IList<SysOrganizeEntity> appCurrentOrgEntityList = new List<SysOrganizeEntity>();
            if (!string.IsNullOrWhiteSpace(appid))
            {
                appCurrentOrgEntityList = _sysApplicationDmnService.GetAuthedOrgListByAppId(appid);
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
                tree.checkstate = appCurrentOrgEntityList.Count(t => t.Id == item.Id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

    }
}