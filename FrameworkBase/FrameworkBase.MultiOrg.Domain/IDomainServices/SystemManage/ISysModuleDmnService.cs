using FrameworkBase.MultiOrg.Domain.Entity;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Domain.IDomainServices
{
    /// <summary>
    /// 系统菜单 DmnService
    /// </summary>
    public interface ISysModuleDmnService
    {
        /// <summary>
        /// 获取系统菜单 grid
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysModuleEntity> GetMenuListByOrg(string orgId);

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysModuleEntity moduleEntity, string keyValue);

        /// <summary>
        /// 获取对当前角色开放的菜单列表（加载开放的菜单树）
        /// </summary>
        /// <param name="orgId">医疗机构</param>
        /// <param name="roleIdList"></param>
        /// <param name="isRoot"></param>
        /// <param name="isAdministrator"></param>
        /// <returns></returns>
        IList<SysModuleEntity> GetMenuList(string orgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="moduleId">菜单Id</param>
        void DeleteModule(string moduleId);

    }
}
