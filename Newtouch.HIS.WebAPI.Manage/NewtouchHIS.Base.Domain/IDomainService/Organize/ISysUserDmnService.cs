using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.Organize
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface ISysUserDmnService : IScopedDependency
    {
        /// <summary>
        /// 系统用户（分页）
        /// </summary>
        /// <param name="request"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<PageResponseRow<List<SysUserVO>>> GetPagintionUserList(OLPagination<QueryParamsBase> request, string orgId);
        /// <summary>
        /// 根据OrganizeId获取 系统人员VO（且有对应的SysUser）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<SysUserStaffVO>> GetSatffVOListByOrg(string orgId, string? toporgId);
        /// <summary>
        /// 机构用户树（科室分组）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="checkIds"></param>
        /// <returns></returns>
        Task<List<BsTreeSelectExtDataModel>> GetOrgUserTreeWithDeptAsync(string? orgId, string? keyword, List<string>? checkIds);
        Task<List<SysUserOrgandDmtVO>> GetSatffVOListByOrgorKeyvalue(string orgId, string? keyvalue);
    }

}
