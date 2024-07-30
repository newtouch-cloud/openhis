using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysPatientChargeAdditionalRepo : IRepositoryBase<SysPatientChargeAdditionalEntity>
    {
        void DeleteForm(SysPatientChargeAdditionalEntity sysPatiChargeAddEntity, string orgId);
        void SubmitForm(SysPatientChargeAdditionalEntity sysPatiChargeAddEntity, int? keyValue, string orgId);

        /// <summary>
        /// 获取服务费比例
        /// </summary>
        /// <param name="brxz">病人性质</param>
        /// <param name="dl">大类</param>
        /// <param name="sfxm">收费项目</param>
        /// <returns></returns>
        SysPatientChargeAdditionalEntity GetFWFBL(string brxz, string dl, string sfxm, string orgId);

        List<SysPatientChargeAdditionalEntity> SelectALLEffectiveList(string orgId);
    }
}
