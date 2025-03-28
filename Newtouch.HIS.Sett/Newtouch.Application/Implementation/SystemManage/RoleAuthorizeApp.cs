using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 角色权限App
    /// </summary>
    public class RoleAuthorizeApp : IRoleAuthorizeApp
    {
        private readonly ISysRoleAuthorizeRepo _roleAuthorizeRepository;
        private readonly ISysModuleRepo _moduleRepository;
        private readonly ISysModuleButtonRepo _moduleButtonRepository;
        private readonly ISysOrganizeAuthorizeRepo _sysOrganizeAuthorizeRepo;


        public RoleAuthorizeApp(ISysRoleAuthorizeRepo roleAuthorizeRepository, ISysModuleRepo moduleRepository,
            ISysModuleButtonRepo moduleButtonRepository, ISysOrganizeAuthorizeRepo sysOrganizeAuthorizeRepo)
        {
            this._roleAuthorizeRepository = roleAuthorizeRepository;
            this._moduleRepository = moduleRepository;
            this._moduleButtonRepository = moduleButtonRepository;
            this._sysOrganizeAuthorizeRepo = sysOrganizeAuthorizeRepo;
        }

        /// <summary>
        /// 获取角色授权列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<SysRoleAuthorizeEntity> GetValidList(string roleId)
        {
            return _roleAuthorizeRepository.IQueryable(t => t.zt == "1" && t.RoleId == roleId).ToList();
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool ActionValidate(IList<string> roleIdList, string moduleId, string action)
        {
            if (roleIdList == null || roleIdList.Count == 0)
            {
                return false;
            }

            var authorizeurldata = new List<AuthorizeActionModel>();
            var moduledata = _moduleRepository.IQueryable().ToList();
            var buttondata = _moduleButtonRepository.IQueryable().ToList();

            //包括 菜单按钮
            var orgAuthedItemId = _sysOrganizeAuthorizeRepo.IQueryable(t => t.zt == "1"
 && (t.TopOrganizeId == "*" || t.TopOrganizeId == Constants.TopOrganizeId)).Select(p => p.ItemId).ToList();

            var authorizedata = _roleAuthorizeRepository.IQueryable(t => t.zt == "1"
                && roleIdList.Contains(t.RoleId) && orgAuthedItemId.Contains(t.ItemId)
            ).ToList();

            foreach (var item in authorizedata)
            {
                if (item.ItemType == 1)
                {
                    SysModuleEntity moduleEntity = moduledata.Find(t => t.Id == item.ItemId);
                    if (moduleEntity != null)
                    {
                        authorizeurldata.Add(new AuthorizeActionModel { Id = moduleEntity.Id, UrlAddress = moduleEntity.UrlAddress });
                    }
                }
                else if (item.ItemType == 2)
                {
                    SysModuleButtonEntity moduleButtonEntity = buttondata.Find(t => t.Id == item.ItemId);
                    if (moduleButtonEntity != null)
                    {
                        authorizeurldata.Add(new AuthorizeActionModel { Id = moduleButtonEntity.ModuleId, UrlAddress = moduleButtonEntity.UrlAddress });
                    }
                }
            }

            authorizeurldata = authorizeurldata.FindAll(t => t.Id.Equals(moduleId));
            foreach (var item in authorizeurldata)
            {
                if (!string.IsNullOrEmpty(item.UrlAddress))
                {
                    //如果加了HandlerAuthorizeAttribute，就要求有对应的Meum/MenuButton 且有设置Url
                    string[] url = item.UrlAddress.Split('?');
                    if (item.Id == moduleId && url[0] == action)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
