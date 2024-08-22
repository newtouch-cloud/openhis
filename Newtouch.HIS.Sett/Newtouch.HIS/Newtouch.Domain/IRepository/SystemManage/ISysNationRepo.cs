using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysNationRepo : IRepositoryBase<SysNationVEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<SysNationVEntity> GetmzList();
    }
}
