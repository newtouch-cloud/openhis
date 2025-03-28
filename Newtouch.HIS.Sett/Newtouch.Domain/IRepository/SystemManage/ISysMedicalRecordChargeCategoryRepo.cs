using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 病案收费大类
    /// </summary>
    public interface ISysMedicalRecordChargeCategoryRepo : IRepositoryBase<SysMedicalRecordChargeCategoryVEntity>
    {
        /// <summary>
        /// 获取所有病案收费大类下拉框
        /// </summary>
        /// <returns></returns>
        IList<SysMedicalRecordChargeCategoryVEntity> GetdlSelect(string orgId);
    }
}
