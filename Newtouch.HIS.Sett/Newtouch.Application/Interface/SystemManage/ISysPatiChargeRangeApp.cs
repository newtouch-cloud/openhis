using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatiChargeRangeApp
    {
        List<SysPatientChargeRangeVO> GetEffectiveList(string keyValue, int? bh = null);
        void DeleteForm(int keyValue);
        void SubmitForm(SysPatientChargeRangeEntity sysPatiChargeRangeEntity, int? keyValue);

    }
}
