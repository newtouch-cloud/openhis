using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository.Inpatient
{
   public interface IDrug_withdrawalzy_tyjlRepo : IRepositoryBase<Drug_withdrawalzy_tyjlEntity>
    {
        IList<DrugwithdrawalTreeVO> Griddata(string patInfo, string keyword, string kssj, string jssj, string orgId);

        IList<GrugTreezsVO> treecx(string keyword, string staffId, string orgId);

    }
}
