using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysUserYfbmRepo : IRepositoryBase<SysUserYfbmEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="yfbmCode"></param>
        void submitUserYfbm(string userId, string yfbmCode);
    }
}
