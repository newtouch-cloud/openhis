using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface INonTreatmentItemBillingRepo : IRepositoryBase<NonTreatmentItemBillingEntity>
    {
        /// <summary>
        /// 保存
        /// </summary>
        void SaveBilling(List<NonTreatmentItemBillingEntity> entityList);
    }
}
