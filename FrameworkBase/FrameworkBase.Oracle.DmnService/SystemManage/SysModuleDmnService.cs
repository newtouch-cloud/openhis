using System.Collections.Generic;
using System.Linq;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;
using FrameworkBase.Domain.Entity;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;

namespace FrameworkBase.Oracle.DmnService
{
    /// <summary>
    /// 菜单相关
    /// </summary>
    public sealed class SysModuleDmnService : DmnServiceBase, ISysModuleDmnService
    {
        private readonly ISysModuleRepo _sysModuleRepo;
        private readonly ISysRoleAuthorizeRepo _sysRoleAuthorizeRepo;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysModuleDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据角色 获取 配置的 权限 菜单
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="isRoot"></param>
        /// <returns></returns>
        public IList<SysModuleEntity> GetMenuList(IList<string> roleIdList, bool isRoot = false)
        {
            //
            string subpage = EnumModuleTargetType.subpage.ToString();
            var query = _sysModuleRepo.IQueryable().Where(p => p.Target != subpage && p.zt == "1");
            if (isRoot)
            {
                ;
            }
            else
            {
                if (roleIdList == null)
                {
                    return null;
                }
                //向Role开放的
                var idList = _sysRoleAuthorizeRepo.IQueryable(t => t.zt == "1" && roleIdList.Contains(t.RoleId)).Select(p => p.ItemId);
                query = query.Where(p => idList.Contains(p.Id));
            }
            var showDescToMenuTitle = ConfigurationHelper.GetAppConfigBoolValue("IS_ShowDescToMenuTitle");
            if (!(showDescToMenuTitle == true))
            {
                //在菜单中不显示Description
                var list = query.ToList();
                list.ForEach(p => p.Description = null);
                return list.OrderBy(t => t.px).ToList();
            }
            return query.OrderBy(t => t.px).ToList();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            if (_sysModuleRepo.IQueryable().Count(t => t.ParentId.Equals(keyValue)) > 0)
            {
                throw new FailedException("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                _sysModuleRepo.Delete(t => t.Id == keyValue);
            }
        }

    }
}
