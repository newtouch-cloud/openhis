using FrameworkBase.Domain.Entity;
using System.Collections.Generic;

namespace FrameworkBase.Domain.IDomainServices
{
    /// <summary>
    /// 用户角色权限
    /// </summary>
    public interface IUserRoleAuthDmnService
    {
        /// <summary>
        /// 获取用户已授权的角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysRoleEntity> GetUserRoleList(string userId);

        /// <summary>
        /// 更新用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIdList"></param>
        void UpdateUserRole(string userId, string[] roleIdList);

    }
}
