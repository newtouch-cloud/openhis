
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface IOutpatientSettlementPaymentModelRepo : IRepositoryBase<OutpatientSettlementPaymentModelEntity>
    {
        List<OutpatientSettlementPaymentModelEntity> SelectMzjszffsByJsnm(int jsnm, string orgId);
    }
}
