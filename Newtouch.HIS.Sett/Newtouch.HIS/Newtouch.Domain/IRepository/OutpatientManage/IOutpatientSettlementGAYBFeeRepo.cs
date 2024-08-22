using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutpatientSettlementGAYBFeeRepo : IRepositoryBase<OutpatientSettlementGAYBFeeEntity>
    {
        void InsertEntity(OutpatientSettlementGAYBFeeEntity entity);
    }
}
