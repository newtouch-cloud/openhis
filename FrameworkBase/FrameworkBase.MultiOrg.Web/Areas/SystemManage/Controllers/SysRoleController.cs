using Newtouch.Tools;
using System.Web.Mvc;
using System.Collections.Generic;
using Newtouch.Core.Common;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Common.Model;
using Newtouch.Common;
using System.Linq;

namespace FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 角色Controller
    /// </summary>
    [AutoResolveIgnore]
    public class SysRoleController : OrgControllerBase
    {
        private readonly ISysRoleRepo _sysRoleRepo;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ISysUserRoleRepo _sysUserRoleRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysRoleRepo"></param>
        /// <param name="userRoleAuthDmnService"></param>
        /// <param name="sysUserRoleRepo"></param>
        public SysRoleController(ISysRoleRepo sysRoleRepo
            , IUserRoleAuthDmnService userRoleAuthDmnService
            , ISysUserRoleRepo sysUserRoleRepo)
        {
            this._sysRoleRepo = sysRoleRepo;
            this._userRoleAuthDmnService = userRoleAuthDmnService;
            this._sysUserRoleRepo = sysUserRoleRepo;
        }

        /// <summary>
        /// 角色列表 gridjson
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                if (string.IsNullOrWhiteSpace(this.OrganizeId))
                {
                    return Error("操作失败。");
                }
                organizeId = this.OrganizeId;
            }
            var data = new
            {
                rows = _sysRoleRepo.GetPagintionList(organizeId, pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
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
            var data = _sysRoleRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 提交保存角色
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(SysRoleEntity roleEntity, string permissionIds, string keyValue)
        {
            roleEntity.zt = roleEntity.zt == "true" ? "1" : "0";

            _userRoleAuthDmnService.SubmitRole(roleEntity, permissionIds.Split(','), keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _userRoleAuthDmnService.DeleteRole(keyValue);
            }
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存角色用户关联关系
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userList"></param>
        /// <returns></returns>
        public ActionResult SubmitRoleUser(string roleId, List<FirstSecond> userList)
        {
            _userRoleAuthDmnService.SubmitRoleUser(roleId, userList);
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
