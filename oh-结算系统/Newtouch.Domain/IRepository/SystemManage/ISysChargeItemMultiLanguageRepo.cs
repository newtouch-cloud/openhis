using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysChargeItemMultiLanguageRepo : IRepositoryBase<SysChargeItemMultiLanguageVEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<SysChargeItemMultiLanguageVEntity> SelectALLEffectiveList(string orgId);
    }
}
