using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysStaffDutyRepo : IRepositoryBase<SysStaffDutyEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        IList<SysStaffDutyEntity> GetListByStaffId(string staffId);
    }
}
