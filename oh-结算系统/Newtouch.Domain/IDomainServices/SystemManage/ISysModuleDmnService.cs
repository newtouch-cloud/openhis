using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 系统菜单、菜单按钮 DmnService
    /// </summary>
    public interface ISysModuleDmnService
    {
        /// <summary>
        /// 根据角色 获取 配置的 权限 菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IList<SysModuleEntity> GetMenuListByTopOrg(string topOrgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false);
        
        /// <summary>
        /// 获取系统菜单 grid
        /// </summary>
        /// <returns></returns>
        IList<SysModuleVO> GetMenuListByTopOrg(string topOrgId);
        
        /// <summary>
        /// 获取当前机构 已开放 的菜单
        /// </summary>
        /// <returns></returns>
        IList<SysModuleEntity> GetOpenMenuListByTopOrg(string topOrgId);

        /// <summary>
        /// 提交新建、更新 菜单
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysModuleEntity moduleEntity, string keyValue, string topOrgId = null);
        
        /// <summary>
        /// （菜单、菜单按钮）获取应用已授权的组织机构列表
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        IList<SysOrganizeVEntity> GetAuthedOrgListByModuleId(string moduleId, int itemType);

        /// <summary>
        /// （菜单、菜单按钮）将菜单授权给指定机构
        /// </summary>
        /// <param name="moduleId"></param>
        void UpdateAuthOrganizeList(string moduleId, string orgList, int itemType);

        /// <summary>
        /// （菜单、菜单按钮）将菜单授权给所有机构
        /// </summary>
        /// <param name="moduleId"></param>
        void AuthAllOrganize(string moduleId, int itemType);

        /// <summary>
        /// （菜单、菜单按钮）撤销全部授权（组织机构）
        /// </summary>
        /// <param name="moduleId"></param>
        void AuthCancelAllOrganize(string moduleId, int itemType);



        /// <summary>
        /// 查看用户是否可访问该系统（根据对他开放的权限菜单来判定）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="topOrgId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        bool CheckLoginable(string topOrgId, string account);

    }
}
