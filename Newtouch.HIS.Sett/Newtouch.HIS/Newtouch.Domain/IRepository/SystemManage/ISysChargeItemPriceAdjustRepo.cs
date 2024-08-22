using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysChargeItemPriceAdjustRepo : IRepositoryBase<SysChargeItemPriceAdjustEntity>
    {
        List<SysChargeItemPriceAdjustEntity> GetEffectiveList(string keyValue, string orgId);
        SysChargeItemPriceAdjustEntity GetForm(int? keyValue, string orgId);
        void DeleteForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity, string orgId);
        void SubmitForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity, int? keyValue, string orgId);
    }
}
