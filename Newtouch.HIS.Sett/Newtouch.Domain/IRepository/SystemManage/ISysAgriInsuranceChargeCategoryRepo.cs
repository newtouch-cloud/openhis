using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 农保收费大类
    /// </summary>
    public interface ISysAgriInsuranceChargeCategoryRepo : IRepositoryBase<SysAgriInsuranceChargeCategoryVEntity>
    {
        /// <summary>
        /// 获取所有农保收费大类下拉框
        /// </summary>
        /// <returns></returns>
        IList<SysAgriInsuranceChargeCategoryVEntity> GetnbdlSelect(string orgId);
    }
}
