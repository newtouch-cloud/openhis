using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace FrameworkBase.MultiOrg.Domain.IRepository
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public interface ISysUserRepo : IRepositoryBase<SysUserVEntity>
    {
        /// <summary>
        /// 根据用户名 获取Entity
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        SysUserVEntity GetEntityByUserName(string account);
    }
}
