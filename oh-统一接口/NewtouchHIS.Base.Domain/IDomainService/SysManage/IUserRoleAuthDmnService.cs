using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    /// <summary>
    /// 系统菜单 （增删改接口）
    /// </summary>
    public interface IUserRoleAuthDmnService : IScopedDependency
    {
        /// <summary>
        /// 获取角色 关联 用户（org）
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<List<FirstSecond>> GetCurUserIdListByRoleId(string roleId);
        /// <summary>
        /// 保存 角色 用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        void submitRoleUser(string roleId, List<FirstSecond> userList, string usercode);
        /// <summary>
        /// 更新用户 角色绑定关系
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <param name="roleIdList"></param>
        /// <param name="usercode"></param>
        /// <returns></returns>
        Task<bool> UpdateUserRole(string userId, string orgId, List<string> roleIdList, string usercode);
    }
}
