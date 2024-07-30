using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysUserStaffDutyDmnService : IScopedDependency
    {
        /// <summary>
        /// 人员岗位关联关系 List
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        Task<List<SysStaffDutyComplexVEntity>> GetStaffDutyListByOrganizeId(string orgId, string staffId = null);
        /// <summary>
        /// 查询员工列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysDutyStaffVO>> GetStaffByDutyCode(string orgId, string keyword = null);
    }

}
