using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysModuleRepo : IRepositoryBase<SysModuleEntity>
    {
        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        IList<SysModuleEntity> GetValidList();
    }
}
