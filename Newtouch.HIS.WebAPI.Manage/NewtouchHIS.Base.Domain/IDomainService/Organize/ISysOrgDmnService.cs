using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysOrgDmnService : IScopedDependency
    {
        /// <summary>
        /// 获取当前组织机构列表 
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        Task<List<SysOrgVo>> GetOrganizeList(string? topOrgId, bool showTopOrg = false);
        /// <summary>
        /// 组织机构 递归查询
        /// </summary>
        /// <param name="parentOrgId"></param>
        /// <returns></returns>
        Task<List<SysOrgVo>> GetOrganizeTree(string parentOrgId);
    }
}
