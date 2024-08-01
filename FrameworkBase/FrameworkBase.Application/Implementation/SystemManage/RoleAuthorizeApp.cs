using FrameworkBase.Application.Interface;
using FrameworkBase.Domain.BusinessObjects;
using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace FrameworkBase.Application.Implementation
{
    /// <summary>
    /// 角色权限App
    /// </summary>
    public sealed class RoleAuthorizeApp : AppBase, IRoleAuthorizeApp
    {
        private readonly ISysRoleAuthorizeRepo _sysRoleAuthorizeRepo;
        private readonly ISysModuleRepo _sysModuleRepo;

        /// <summary>
        /// 权限验证
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

            var authorizeurldata = GetAuthorizeActionModelList(roleIdList);

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
        /// 权限验证（要求必须通过菜单点击方式 打卡页面）
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

            var authorizeurldata = GetAuthorizeActionModelList(roleIdList);

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
        /// <returns></returns>
        private List<AuthorizeActionModel> GetAuthorizeActionModelList(IList<string> roleIdList)
        {
            var authorizeurldata = new List<AuthorizeActionModel>();
            var moduledata = _sysModuleRepo.IQueryable(p => p.zt == "1").ToList();

            var authorizedata = _sysRoleAuthorizeRepo.IQueryable(t => t.zt == "1"
                && roleIdList.Contains(t.RoleId)).ToList();

            foreach (var item in authorizedata)
            {
                SysModuleEntity moduleEntity = moduledata.Find(t => t.Id == item.ItemId);
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
