using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysChargeProjPriceAdjApp
    {
        List<SysChargeItemPriceAdjustEntity> GetEffectiveList(string keyValue);
        SysChargeItemPriceAdjustEntity GetForm(int? keyValue);
        void DeleteForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity);
        void SubmitForm(SysChargeItemPriceAdjustEntity sysAgInsurChargeCategEntity, int? keyValue);

    }
}
