using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// V_S_xt_bq
    /// </summary>
    public class SysWardRepo : RepositoryBase<SysWardVEntity>, ISysWardRepo
    {
        public SysWardRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取系统病区集合
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysWardVEntity> GetListByOrgId(string orgId)
        {
            return FindList<SysWardVEntity>(@"select * from [NewtouchHIS_Base]..V_S_xt_bq(nolock) where OrganizeId = @orgId", new DbParameter[] { new SqlParameter("@orgId", orgId) });
        }
    }
}
