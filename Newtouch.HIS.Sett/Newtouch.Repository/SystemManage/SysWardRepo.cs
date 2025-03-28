using Newtouch.HIS.Domain.IRepository;
using System.Data.SqlClient;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysWardRepo : RepositoryBase<SysWardVEntity>, ISysWardRepo
    {
        public SysWardRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取有效
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysWardVEntity> GetbqList(string orgId)
        {
            var sql = "select bqCode, bqmc,bqId  from [NewtouchHIS_Base]..V_S_xt_bq width(nolock) where zt = '1' and OrganizeId = @orgId";
            return this.FindList<SysWardVEntity>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

    }
}
