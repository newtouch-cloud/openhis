using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 快捷菜单
    /// </summary>
    public interface ISysRoleShortcutMenuDmnService
    {
        /// <summary>
        /// 获取角色开放的快捷菜单 List
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <returns></returns>
        IList<SysShortcutMenuEntity> GetAuthedSCMList(IList<string> roleIdList, string orgId);

        /// <summary>
        /// 获取快捷菜单 已授权的角色列表
        /// </summary>
        /// <param name="scmId"></param>
        /// <returns></returns>
        IList<string> GetAuthedRoleIdList(string scmId, string orgId);

        /// <summary>
        /// 快捷菜单 授权 给指定 角色
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="roleList"></param>
        void UpdateAuthRoleList(string keyValue, string roleList, string orgId);

    }
}
