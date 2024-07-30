using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysModuleExRepo : IRepositoryBase<SysModuleEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<SysModuleEntity> GetModuleTreeJsonByName(string name);

    }
}
