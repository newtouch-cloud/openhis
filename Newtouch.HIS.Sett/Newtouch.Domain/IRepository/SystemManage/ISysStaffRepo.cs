
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysStaffExRepo : IRepositoryBase<SysStaffVEntity>
    {
        /// <summary>
        /// 职位对应员工返回list
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<StaffDutyVO> GetStaffDutyListByOrganizeId(string orgId);

    }
}
