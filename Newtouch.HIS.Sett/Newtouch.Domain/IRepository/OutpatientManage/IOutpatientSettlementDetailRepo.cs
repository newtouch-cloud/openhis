using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutpatientSettlementDetailRepo : IRepositoryBase<OutpatientSettlementDetailEntity>
    {
        List<OutpatientSettlementDetailEntity> SelectMzjsmxByJsnm(int jsnm, string orgId);
    }
}
