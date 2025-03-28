using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysStaffDmnService : IScopedDependency
    {
        Task<SysStaffEntity> FindKey(string userId);
        /// <summary>
        /// 根据UserId获取职工信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<SysStaffVEntity>> GetStaffListByUserId(string userId);
        /// <summary>
        /// 根据工号获取医生科室信息
        /// </summary>
        /// <param name="rygh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysStaffDeptVO>> GetStaffDeptByGh(string rygh, string orgId);
    }
}
