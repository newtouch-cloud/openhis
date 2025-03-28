using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.BusinessObjects;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Application.Interface;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 角色权限App
    /// </summary>
    public sealed class RoleAuthorizeApp : AppBase, IRoleAuthorizeApp
    {
        private readonly ISysRoleAuthorizeRepo _roleAuthorizeRepo;
        private readonly ISysModuleRepo _sysModuleRepo;

        /// <summary>
        /// 访问权限验证
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ActionValidate(IList<string> roleIdList, string action)
        {
            if (action.StartsWith("/Home", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;    //Home/Index特殊
            }

            if (roleIdList == null || roleIdList.Count == 0)
            {
                return false;
            }

            var opr = this.UserIdentity;
            if (string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                return false;
            }

            var authorizeurldata = GetAuthorizeActionModelList(roleIdList, opr.OrganizeId);

            foreach (var item in authorizeurldata)
            {
                if (!string.IsNullOrEmpty(item.UrlAddress))
                {
                    //如果加了HandlerAuthorizeAttribute，就要求有对应的Meum/MenuButton 且有设置Url
                    string[] url = item.UrlAddress.Split('?');
                    if (url[0].Equals(action, System.StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 访问权限验证（要求必须通过菜单点击方式 打卡页面）
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="moduleId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ActionValidate(IList<string> roleIdList, string moduleId, string action)
        {
            if (action.StartsWith("/Home", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;    //Home/Index特殊
            }

            if (roleIdList == null || roleIdList.Count == 0)
            {
                return false;
            }

            var opr = this.UserIdentity;
            if (string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                return false;
            }

            var authorizeurldata = GetAuthorizeActionModelList(roleIdList, opr.OrganizeId);

            authorizeurldata = authorizeurldata.FindAll(t => t.Id.Equals(moduleId, System.StringComparison.OrdinalIgnoreCase));
            foreach (var item in authorizeurldata)
            {
                if (!string.IsNullOrEmpty(item.UrlAddress))
                {
                    //如果加了HandlerAuthorizeAttribute，就要求有对应的Meum/MenuButton 且有设置Url
                    string[] url = item.UrlAddress.Split('?');
                    if (url[0].Equals(action, System.StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #region

        /// <summary>
        /// 获取授权列表
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        private List<AuthorizeActionModel> GetAuthorizeActionModelList(IList<string> roleIdList, string orgId)
        {
            var authorizeurldata = new List<AuthorizeActionModel>();

            var moduleList = _sysModuleRepo.IQueryable(t => t.zt == "1"
 && (t.OrganizeId == null || t.OrganizeId == orgId)).ToList();

            var moduleIdList = moduleList.Select(p => p.Id).ToList();

            var authorizedata = _roleAuthorizeRepo.IQueryable(t => t.zt == "1"
                && roleIdList.Contains(t.RoleId) && moduleIdList.Contains(t.ItemId)
            ).ToList();

            foreach (var item in authorizedata)
            {
                SysModuleEntity moduleEntity = moduleList.Find(t => t.Id == item.ItemId);
                if (moduleEntity != null)
                {
                    authorizeurldata.Add(new AuthorizeActionModel { Id = moduleEntity.Id, UrlAddress = moduleEntity.UrlAddress });
                }
            }

            return authorizeurldata;
        }

        #endregion

    }
}
