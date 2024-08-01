using FrameworkBase.Domain.Entity;
using System.Collections.Generic;

namespace FrameworkBase.Domain.IDomainServices
{
    /// <summary>
    /// 菜单相关
    /// </summary>
    public interface ISysModuleDmnService
    {
        /// <summary>
        /// 根据角色 获取 配置的 权限 菜单
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="isRoot"></param>
        /// <returns></returns>
        IList<SysModuleEntity> GetMenuList(IList<string> roleIdList, bool isRoot = false);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);

    }
}
