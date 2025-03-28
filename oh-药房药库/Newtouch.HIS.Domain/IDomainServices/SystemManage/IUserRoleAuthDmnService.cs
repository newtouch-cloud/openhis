using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 用户角色权限 公用DmnService
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
        /// 获取UserId 根据RoleId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<string> GetCurUserIdListByRoleId(string roleId);

        /// <summary>
        /// 保存 角色 用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        void submitRoleUser(string roleId, string userIds, string parentOrgId);


    }
}