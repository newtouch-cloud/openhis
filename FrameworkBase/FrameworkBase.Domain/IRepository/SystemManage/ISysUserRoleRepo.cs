using System.Collections.Generic;
using Newtouch.Infrastructure.EF;
using FrameworkBase.Domain.Entity;

namespace FrameworkBase.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:19
    /// 描 述：用户角色表
    /// </summary>
    public interface ISysUserRoleRepo : IRepositoryBase<SysUserRoleEntity>
    {
        /// <summary>
        /// 获取Role关联UserId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IList<string> GetUserIdListByRoleId(string roleId);

        /// <summary>
        /// 获取UserId关联RoleId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<string> GetRoleIdListByUserId(string userId);

        /// <summary>
        /// 提交角色用户关联关系
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        void SubmitRoleUser(string roleId, string userIds);

    }
}