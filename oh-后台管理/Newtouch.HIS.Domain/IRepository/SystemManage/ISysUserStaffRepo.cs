using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysUserStaffRepo : IRepositoryBase<SysUserStaffEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="staffIds"></param>
        void submitUserStaff(string userId,  string staffIds);
    }
}
