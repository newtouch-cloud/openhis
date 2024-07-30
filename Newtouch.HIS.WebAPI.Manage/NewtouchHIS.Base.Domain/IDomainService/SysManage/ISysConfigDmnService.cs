using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    public interface ISysConfigDmnService : IScopedDependency
    {
        /// <summary>
        /// 菜单列表 by AppId
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        /// <param name="validLimit"></param>
        /// <returns></returns>
        Task<List<SysModuleVO>?> GetMenuListbyAppId(string orgId, string appId, string userId = null, bool validLimit = true, bool? systemMenuLimit = true);
        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="roleIdList"></param>
        /// <param name="isRoot"></param>
        /// <param name="isAdministrator"></param>
        /// <returns></returns>
        Task<List<SysModuleEntity>?> GetMenuList(string orgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false,string? userId = null,bool validLimit = true);
        /// <summary>
        /// 获取对当前角色开放的菜单列表（加载开放的菜单树）
        /// 业务系统区分
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <param name="roleIdList"></param>
        /// <param name="isRoot"></param>
        /// <param name="isAdministrator"></param>
        /// <param name="userId"></param>
        /// <param name="validLimit"></param>
        /// <returns></returns>
        Task<List<SysModuleEntity>?> GetMenuList(string appId, string orgId, IList<string> roleIdList, bool isRoot = false, bool isAdministrator = false, string? userId = null, bool validLimit = true);
        /// <summary>
        /// 系统字典表
        /// </summary>
        /// <returns></returns>
        Task<List<SysItemExtendVO>> GetItemDetailsList(string orgId);
    }
}
