using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPharmacyDepartmentRepo : IRepositoryBase<SysPharmacyDepartmentVEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetYjbmjbByCode(string code, string orgId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetMzzybzByCode(string code, string orgId);

        /// <summary>
        /// 获取发药药房列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzzybz"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetPharmacyDepartmentList(string orgId, string mzzybz);

    }
}
