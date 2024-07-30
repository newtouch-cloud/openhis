using System.Web.Mvc;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using Newtouch.HIS.Domain.IDomainServices;
using System.Linq;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Common.Model;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class RoleController : ControllerBase
    {
        private readonly IRoleApp _roleApp;
        private readonly IModuleApp _moduleApp;
        private readonly IRoleAuthorizeApp _roleAuthorizeApp;
        private readonly IModuleButtonApp _moduleButtonApp;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ISysRoleRepo _sysRoleRepo;
        private readonly ISysUserRoleRepo _sysUserRoleRepo;

        public RoleController(IRoleApp roleApp, IModuleApp moduleApp, IModuleButtonApp moduleButtonApp, IRoleAuthorizeApp roleAuthorizeApp
            , ISysUserDmnService sysUserDmnService, IUserRoleAuthDmnService userRoleAuthDmnService
            , ISysRoleRepo sysRoleRepo ,ISysUserRoleRepo sysUserRoleRepo)
        {
            this._roleApp = roleApp;
            this._moduleApp = moduleApp;
            this._moduleButtonApp = moduleButtonApp;
            this._roleAuthorizeApp = roleAuthorizeApp;
            this._sysUserDmnService = sysUserDmnService;
            this._userRoleAuthDmnService = userRoleAuthDmnService;
            this._sysRoleRepo = sysRoleRepo;
            this._sysUserRoleRepo = sysUserRoleRepo;
        }

        //角色列表 gridjson
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string OrganizeId)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                OrganizeId = this.OrganizeId;
            }
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;    //不返回
            }
            var data = new
            {
                rows = _roleApp.GetPagintionList(OrganizeId, pagination, keyword),
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
            var data = _roleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SysRoleEntity roleEntity, string permissionIds, string keyValue)
        {
            roleEntity.zt = roleEntity.zt == "true" ? "1" : "0";

            _roleApp.SubmitForm(roleEntity, permissionIds.Split(','), keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _roleApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存角色用户关联关系
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult submitRoleUser(string roleId, List<FirstSecond> userList)
        {
            _userRoleAuthDmnService.submitRoleUser(roleId, userList);
            return Success("操作成功");
        }

        /// <summary>
        /// 角色（树）   （用户主动关联角色）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetRoleList(string userId, string organizeId)
        {
            var roleList = _sysRoleRepo.IQueryable(p => p.zt == "1" && p.OrganizeId == organizeId).ToList();
            var treeList = new List<TreeViewModel>();
            var userCurrentRoleEntityList = new List<string>();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                userCurrentRoleEntityList = _sysUserRoleRepo.GetRoleIdListByUserId(userId).ToList();
            }
            foreach (var role in roleList)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = role.Id;
                tree.text = role.Name;
                tree.value = role.Code;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = userCurrentRoleEntityList.Count(t => t == role.Id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }


    }
}