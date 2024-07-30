using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
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
