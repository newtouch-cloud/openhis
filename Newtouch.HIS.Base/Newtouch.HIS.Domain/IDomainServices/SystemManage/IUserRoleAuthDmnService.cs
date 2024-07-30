using Newtouch.Common.Model;
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
        IList<SysRoleEntity> GetUserRoleList(string userId, string orgId);

        /// <summary>
        /// 获取角色 关联 用户（org）
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<FirstSecond> GetCurUserIdListByRoleId(string roleId);

        /// <summary>
        /// 保存 角色 用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        void submitRoleUser(string roleId, List<FirstSecond> userList);

        /// <summary>
        /// 更新用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <param name="roleIdList"></param>
        void UpdateUserRole(string userId, string orgId, string[] roleIdList);
    }
}
