using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class RoleController : ControllerBase
    {
        private readonly IRoleApp _roleApp;
        private readonly IModuleApp _moduleApp;
        private readonly IModuleButtonApp _moduleButtonApp;
        private readonly IRoleAuthorizeApp _roleAuthorizeApp;
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;

        //角色列表 gridjson
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _roleApp.GetPagintionList(Constants.TopOrganizeId, pagination, keyword),
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
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SysRoleEntity roleEntity, string permissionIds, string keyValue)
        {
            roleEntity.OrganizeId = "*";    //该版本角色跟TopOrganizeId，不到具体的医院
            roleEntity.TopOrganizeId = Constants.TopOrganizeId;
            roleEntity.zt = roleEntity.zt == "true" ? "1" : "0";
            _roleApp.SubmitForm(roleEntity, permissionIds.Split(','), keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _roleApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存Sys_UserRole
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult submitRoleUser(string roleId, string userIds)
        {
            var organizeId = base.GetAuthOrganizeId();  //默认 权限OrganizeId

            _userRoleAuthDmnService.submitRoleUser(roleId, userIds, organizeId);
            return Success("操作成功");
        }

    }
}
