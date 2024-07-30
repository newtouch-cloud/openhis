using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 组织机构 DmnService
    /// </summary>
    public class SysOrganizeDmnService : DmnServiceBase, ISysOrganizeDmnService
    {
        public SysOrganizeDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取组织机构类型（集团、医院）
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetOrganizeTypeByOrganizeId(string orgId)
        {
            var Sql = @"select b.Name from [NewtouchHIS_Base]..V_S_Sys_Organize a
left join [NewtouchHIS_Base]..V_S_Sys_ItemsDetail b
on a.CategoryCode = b.Code
where a.Id = @orgId and (b.TopOrganizeId = @topOrgId or b.TopOrganizeId = '*')";
            return this.FirstOrDefault<string>(Sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@topOrgId", Constants.TopOrganizeId) });
        }

        /// <summary>
        /// 是否含有下级机构，有下级机构返回true
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool IsHasLowerOrganize(string orgId)
        {
            var Sql = @"select 1 from [NewtouchHIS_Base]..V_S_Sys_Organize where ParentId = @orgId";
            return this.FirstOrDefault<int>(Sql, new[] { new SqlParameter("@orgId", orgId) }) == 1;
        }

        /// <summary>
        /// 获取组织下的所有有效组织（parent）
        /// </summary>
        /// <returns></returns>
        public List<SysOrganizeVEntity> GetValidListByParentOrg(string parentOrgId)
        {
            var sql = @" 
    WITH cteTree
        AS (SELECT *
              FROM [NewtouchHIS_Base]..V_S_Sys_Organize
              WHERE Id = @parentOrgId --第一个查询作为递归的基点(锚点)
            UNION ALL
            SELECT [NewtouchHIS_Base]..V_S_Sys_Organize.*     --第二个查询作为递归成员， 下属成员的结果为空时，此递归结束。
              FROM
                   cteTree INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Organize ON cteTree.Id= [NewtouchHIS_Base]..V_S_Sys_Organize.ParentId) 
        SELECT  *
          FROM cteTree 

";
            return this.FindList<SysOrganizeVEntity>(sql, new SqlParameter[] {
                new SqlParameter("@parentOrgId",parentOrgId)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool IsHospital(string orgId)
        {
            var sql = "select CategoryCode from [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) where Id = @orgId";
            var code = this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
            return code == "Hospital";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool IsClinic(string orgId)
        {
            var sql = "select CategoryCode from [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) where Id = @orgId";
            var code = this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
            return code == "Clinic";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool IsMedicalOrganize(string orgId)
        {
            var sql = "select CategoryCode from [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) where Id = @orgId";
            var code = this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
            return code == "Hospital" || code == "Clinic";
        }

        /// <summary>
        /// 获取所有的顶级组织机构
        /// </summary>
        /// <returns></returns>
        public IList<SysOrganizeVEntity> GetValidTopOrgList()
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_Organize where ParentId is null";
            return this.FindList<SysOrganizeVEntity>(sql);
        }

    }
}
