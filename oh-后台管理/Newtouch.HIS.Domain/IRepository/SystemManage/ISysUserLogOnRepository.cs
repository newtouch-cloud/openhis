using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysUserLogOnRepository : IRepositoryBase<SysUserLogOnEntity>
    {
        /// <summary>
        /// 更新 可 登录 状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="locked"></param>
        void UpdateLockedStatus(string userId, bool? locked);

    }
}
