using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysCashPaymentModelRepo : IRepositoryBase<SysCashPaymentModelEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<SysCashPaymentModelEntity> GetLazyList();
    }
}
