using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// 系统诊断
    /// </summary>
    public class SysDiagnosisRepo : RepositoryBase<SysDiagnosisVEntity>, ISysDiagnosisRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysDiagnosisRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据关键字模糊查找（有效的）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword">null或Empty返回所有</param>
        /// <param name="zdlx">诊断类型（字典DiagnosisType）</param>
        /// <param name="ybnhlx">医保农合类型（区分医保还是新农合）</param>
        /// <returns></returns>
        public IList<SysDiagnosisVEntity> GetList(string orgId, string keyword, string zdlx = null, string ybnhlx = null)
        {
            var sql = @"
select top 200 * from [NewtouchHIS_Base]..V_S_xt_zd(nolock) 
where (OrganizeId = '*' or OrganizeId = @orgId) 
and zt = '1'
and (zdCode like @searchKeyword or zdmc like @searchKeyword or py like @searchKeyword or icd10 like @searchKeyword or icd10fjm like @searchKeyword)
and (isnull(@zdlx, '') = '' or zdlx = @zdlx)   AND ( ybnhlx ='*' OR  ISNULL(@ybnhlx, '') = '' OR ybnhlx = @ybnhlx  )
order by isnull(icd10,'ZZZZ')";
            return this.FindList<SysDiagnosisVEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                    ,new SqlParameter("@orgId", orgId)
                    ,new SqlParameter("@zdlx", zdlx ?? "")
                    ,new SqlParameter("@ybnhlx", ybnhlx ?? "")
            });
        }

        /// <summary>
        /// 根据编码查找实体（可能无效）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public SysDiagnosisVEntity GetEntityByCode(string orgId, string code)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_xt_zd(nolock) where (OrganizeId = '*' or OrganizeId = @orgId) 
and zdCode = @code";
            return this.FirstOrDefault<SysDiagnosisVEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@code", code ?? "")
                    ,new SqlParameter("@orgId", orgId)
            });
        }

        /// <summary>
        /// 根据编码查找实体（可能无效）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public SysDiagnosisVEntity SelectData(string orgId, string code)
        {
            const string sql = @"
SELECT * FROM [NewtouchHIS_Base].dbo.V_S_xt_zd(nolock) 
WHERE (OrganizeId = '*' or OrganizeId = @orgId)
AND zt='1' 
AND zdCode = @code ";
            return FirstOrDefault<SysDiagnosisVEntity>(sql, new DbParameter[] {
                new SqlParameter("@code", code ?? "")
                ,new SqlParameter("@orgId", orgId)
            });
        }

        /// <summary>
        /// 根据编码查找实体（可能无效）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="icd10"></param>
        /// <returns></returns>
        public SysDiagnosisVEntity SelectDataByICD10(string orgId, string icd10)
        {
            const string sql = @"
SELECT * FROM [NewtouchHIS_Base].dbo.V_S_xt_zd(nolock) 
WHERE (OrganizeId = '*' or OrganizeId = @orgId)
AND zt='1' 
AND icd10=@icd10 ";
            return FirstOrDefault<SysDiagnosisVEntity>(sql, new DbParameter[] {
                new SqlParameter("@icd10", icd10 ?? "")
                ,new SqlParameter("@orgId", orgId)
            });
        }


    }
}
