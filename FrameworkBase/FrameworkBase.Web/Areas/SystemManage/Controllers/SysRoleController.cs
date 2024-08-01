using System.Web.Mvc;
using Newtouch.Tools;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Common;
using System.Linq;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;

namespace FrameworkBase.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:13
    /// 描 述：系统角色
    /// </summary>
    [AutoResolveIgnore]
    public class SysRoleController : BaseController
    {
        private readonly ISysRoleRepo _sysRoleRepo;
        private readonly ISysRoleDmnService _sysRoleDmnService;
        private readonly ISysUserRoleRepo _sysUserRoleRepo;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="sysRoleRepo"></param>
        /// <param name="sysRoleDmnService"></param>
        /// <param name="sysUserRoleRepo"></param>
        public SysRoleController(ISysRoleRepo sysRoleRepo
            , ISysRoleDmnService sysRoleDmnService
            , ISysUserRoleRepo sysUserRoleRepo)
        {
            this._sysRoleRepo = sysRoleRepo;
            this._sysRoleDmnService = sysRoleDmnService;
            this._sysUserRoleRepo = sysUserRoleRepo;
        }

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetPagintionGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _sysRoleRepo.GetPaginationList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _sysRoleRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 提交保存
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
            _sysRoleDmnService.SubmitForm(roleEntity, permissionIds.Split(','), keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除数据（物理删除）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            _sysRoleDmnService.DeleteForm(keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 保存角色用户关联关系
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitRoleUser(string roleId, string userIds)
        {
            _sysUserRoleRepo.SubmitRoleUser(roleId, userIds);
            return Success("操作成功");
        }

        /// <summary>
        /// 角色（树）   （用户主动关联角色）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult GetRoleList(string userId)
        {
            var roleList = _sysRoleRepo.GetValidList();  //获取有效岗位列表
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