using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 药房部门
    /// </summary>
    public interface ISysPharmacyDepartmentDmnService
    {

        /// <summary>
        /// 根据药品代码获取已授权的药房药库
        /// </summary>
        /// <param name="ypId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysPharmacyDepartmentVEntity> SelectEmpowermentYfbmByYp(string ypId, string organizeId);
    }
}