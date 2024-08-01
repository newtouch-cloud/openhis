using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace FrameworkBase.MultiOrg.Domain.IRepository
{
    /// <summary>
    /// 用户角色关联关系
    /// </summary>
    public interface ISysUserRoleRepo : IRepositoryBase<SysUserRoleEntity>
    {
        /// <summary>
        /// 获取UserId关联RoleId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<string> GetRoleIdListByUserId(string userId);

    }
}
