using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FrameworkBase.MultiOrg.Repository.SystemManage
{
    /// <summary>
    /// 中医证候
    /// </summary>
    public class SysTCMSyndromeRepo : RepositoryBase<SysTCMSyndromeVEntity>, ISysTCMSyndromeRepo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysTCMSyndromeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据关键字模糊查找（有效的）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysTCMSyndromeVEntity> GetList(string orgId, string keyword)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_xt_zyzh(nolock) where (OrganizeId = '*' or OrganizeId = @orgId) and zt = '1'
and (zhCode like @searchKeyword or zhmc like @searchKeyword or py like @searchKeyword)
order by py";
            return this.FindList<SysTCMSyndromeVEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                    ,new SqlParameter("@orgId", orgId)
            });
        }

        /// <summary>
        /// 根据编码查找实体（可能无效）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public SysTCMSyndromeVEntity GetEntityByCode(string orgId, string code)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_xt_zyzh(nolock) where (OrganizeId = '*' or OrganizeId = @orgId) 
and zhCode = @code";
            return this.FirstOrDefault<SysTCMSyndromeVEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@code", code ?? "")
                    ,new SqlParameter("@orgId", orgId)
            });
        }


    }
}
