using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// 系统人员
    /// </summary>
    public sealed class SysStaffRepo : RepositoryBase<SysStaffVEntity>, ISysStaffRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysStaffRepo(IDefaultDatabaseFactory databaseFactory) 
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取组织机构 有效员工列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysStaffVEntity> GetValidStaffListByOrganizeId(string orgId)
        {
            const string sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) where OrganizeId = @orgId and zt = '1'";
            return FindList<SysStaffVEntity>(sql, new DbParameter[] { new SqlParameter("@orgId", orgId) });
        }

        /// <summary>
        /// 根据工号 获取组织机构 有效员工列表
        /// </summary>
        /// <param name="gh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SysStaffVEntity GetValidStaffByGh(string gh, string orgId)
        {
            const string sql = "select * from [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) where OrganizeId = @orgId and gh = @gh and zt = '1'";
            return FirstOrDefault<SysStaffVEntity>(sql, new DbParameter[] { new SqlParameter("@orgId", orgId), new SqlParameter("@gh", gh) });
        }

        /// <summary>
        /// 根据工号 获取 人员姓名
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        public string GetNameByGh(string gh, string orgId)
        {
            const string sql = "select Name from [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) where gh = @gh and OrganizeId = @orgId";
            return FirstOrDefault<string>(sql, new DbParameter[] { new SqlParameter("@orgId", orgId), new SqlParameter("@gh", gh) });
        }

    }
}
