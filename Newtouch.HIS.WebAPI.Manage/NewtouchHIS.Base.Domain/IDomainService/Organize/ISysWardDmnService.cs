using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysWardDmnService : IScopedDependency
    {
        /// <summary>
        /// 获取有效
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysWardVEntity>> GetbqList(string orgId);
        /// <summary>
        /// 获取科室绑定的病区
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>

        Task<List<SysDepartmentWardRelationVO>> GetWardbyDept(string orgId, string ks, string keyword);
    }
}
