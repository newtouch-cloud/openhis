using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysPatientChargeRangeRepo : IRepositoryBase<SysPatientChargeRangeEntity>
    {
        SysPatientChargeRangeEntity GetForm(int keyValue, string orgId);
        void DeleteForm(int keyValue, string orgId);
        void SubmitForm(SysPatientChargeRangeEntity sysPatiChargeRangeEntity, int? keyValue, string orgId);
    }
}
