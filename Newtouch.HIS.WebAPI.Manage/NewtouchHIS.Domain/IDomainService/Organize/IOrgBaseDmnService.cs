using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Domain.IDomainService
{
    public interface IOrgBaseDmnService : IScopedDependency
    {
        /// <summary>
        /// HIS 科室查询
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<HisDeptVO>> HisDeptQuery(string orgId, string? keyword);
        /// <summary>
        /// HIS 医生查询
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<HisStaffVO>> HisDocQuery(string orgId, string? keyword);
    }
}
