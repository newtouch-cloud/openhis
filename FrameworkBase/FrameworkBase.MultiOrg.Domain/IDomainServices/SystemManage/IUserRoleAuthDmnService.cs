using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Common.Model;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Domain.IDomainServices
{
    /// <summary>
    /// 用户角色权限相关
    /// </summary>
    public interface IUserRoleAuthDmnService
    {
        /// <summary>
        /// 获取用户已授权的角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId">医疗机构Id</param>
        /// <returns></returns>
        IList<SysRoleEntity> GetUserRoleList(string userId, string orgId);

        /// <summary>
        /// 提交保存角色
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="keyValue"></param>
        void SubmitRole(SysRoleEntity roleEntity, string[] permissionIds, string keyValue);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        void DeleteRole(string roleId);

        /// <summary>
        /// 保存 角色 用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userList"></param>
        void SubmitRoleUser(string roleId, List<FirstSecond> userList);

        /// <summary>
        /// 获取角色 关联 用户（org）
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IList<FirstSecond> GetCurUserIdListByRoleId(string roleId);

        /// <summary>
        /// 更新用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <param name="roleIdList"></param>
        void UpdateUserRole(string userId, string orgId, string[] roleIdList);
    }
}
