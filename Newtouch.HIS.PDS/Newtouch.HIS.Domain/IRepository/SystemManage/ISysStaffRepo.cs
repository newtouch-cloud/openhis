using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysStaffRepo : IRepositoryBase<SysStaffVEntity>
    {
        /// <summary>
        /// 获取组织机构 有效员工列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysStaffVEntity> GetStaffListByOrganizeId(string orgId);
    }
}
