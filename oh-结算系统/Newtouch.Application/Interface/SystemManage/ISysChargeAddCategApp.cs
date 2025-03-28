using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysChargeAddCategApp
    {
        List<SysChargeAdditionalCategoryEntity> GetEffectiveList(int keyValue);
        void DeleteForm(int keyValue);
        void SubmitForm(SysChargeAdditionalCategoryEntity sysChargeAddCategEntity, int keyValue);
        string GetSysChargeAddCategSelect();
    }
}
