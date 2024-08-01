using FrameworkBase.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace FrameworkBase.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:15
    /// 描 述：用户登录信息表
    /// </summary>
    public interface ISysUserLogOnRepo : IRepositoryBase<SysUserLogOnEntity>
    {
        /// <summary>
        /// 更新 可 登录 状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="locked"></param>
        void UpdateLockedStatus(string userId, bool? locked);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        void RevisePassword(string userPassword, string keyValue);

    }
}