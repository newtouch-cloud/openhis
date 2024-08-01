using System;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace FrameworkBase.MultiOrg.DmnService
{
    /// <summary>
    /// 组织机构（医疗机构）
    /// </summary>
    public sealed class SysOrganizeDmnService : DmnServiceBase, ISysOrganizeDmnService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysOrganizeDmnService(IDefaultDatabaseFactory databaseFactory) 
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取组织下的所有有效组织机构（parent）
        /// </summary>
        /// <param name="parentOrgId"></param>
        /// <param name="containsSelf"></param>
        /// <returns></returns>
        public List<SysOrganizeVEntity> GetValidListByParentOrg(string parentOrgId, bool? containsSelf = true)
        {
            var sql = @" 
    WITH cteTree
        AS (SELECT *
              FROM [NewtouchHIS_Base]..V_S_Sys_Organize(nolock)
              WHERE Id = @parentOrgId and zt = '1' --第一个查询作为递归的基点(锚点)
            UNION ALL
            SELECT [NewtouchHIS_Base]..V_S_Sys_Organize.*     --第二个查询作为递归成员， 下属成员的结果为空时，此递归结束。
              FROM
                   cteTree INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) ON cteTree.Id= [NewtouchHIS_Base]..V_S_Sys_Organize.ParentId and [NewtouchHIS_Base]..V_S_Sys_Organize.zt = '1') 
        SELECT  *
          FROM cteTree 

";
            var list =  this.FindList<SysOrganizeVEntity>(sql, new SqlParameter[] {
                new SqlParameter("@parentOrgId",parentOrgId)
            });
            if (list.Count > 0)
            {
                if (containsSelf == false)
                {
                    list = list.Where(p => p.Id != parentOrgId).ToList();
                    foreach (var item in list)
                    {
                        if (item.ParentId == parentOrgId)
                        {
                            item.ParentId = null;
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 根据Id获取组织机构名称
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetNameByOrgId(string orgId)
        {
            var sql = @"select Name from [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) where Id = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

        /// <summary>
        /// 获取 UserId 对应的 医机构 List（关联了机构内的人员）
        /// </summary>
        /// <returns></returns>
        public IList<SysOrganizeVEntity> GetMedicalOrganizeListByUserId(string userId)
        {
            var sql = @"select distinct c.* from [NewtouchHIS_Base]..V_S_Sys_UserStaff(nolock) a
left join [NewtouchHIS_Base]..V_S_Sys_Staff(nolock) b
on a.StaffId = b.Id
left join [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) c
on b.OrganizeId = c.Id

where a.zt = '1' and b.zt = '1' and c.zt = '1' and c.Id is not null and a.UserId = @userId";
            return this.FindList<SysOrganizeVEntity>(sql, new[] { new SqlParameter("@userId", userId) });
        }

        /// <summary>
        /// 获取 医疗机构 List
        /// </summary>
        /// <returns></returns>
        public IList<SysOrganizeVEntity> GetMedicalOrganizeList()
        {
            var sql = @"select c.* 
from [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) c

where c.zt = '1'
and (c.CategoryCode = 'Hospital' or c.CategoryCode = 'Clinic')";
            return this.FindList<SysOrganizeVEntity>(sql);
        }

        /// <summary>
        /// 通过机构类别来判断 是否 是 诊所
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
        /// 通过机构类别来判断 是否 是 诊所
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
        /// 通过机构类别来判断 是否 是 医疗机构（有具体的业务）
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
        /// 根据Id获取组织机构Code
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetCodeByOrgId(string orgId)
        {
            var sql = @"select Code from [NewtouchHIS_Base]..V_S_Sys_Organize(nolock) where Id = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

    }
}
