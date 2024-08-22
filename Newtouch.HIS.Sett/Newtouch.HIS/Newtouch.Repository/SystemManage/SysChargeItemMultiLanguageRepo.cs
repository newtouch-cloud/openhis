using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysChargeItemMultiLanguageRepo : RepositoryBase<SysChargeItemMultiLanguageVEntity>, ISysChargeItemMultiLanguageRepo
    {
        public SysChargeItemMultiLanguageRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取所有有效数据
        /// </summary>
        /// <returns></returns>
        public IList<SysChargeItemMultiLanguageVEntity> SelectALLEffectiveList(string orgId)
        {
            var sql = "select * from [NewtouchHIS_Base]..[V_S_xt_sfxm_dyy] with(nolock) where OrganizeId = @orgId and zt = '1'";
            return this.FindList<SysChargeItemMultiLanguageVEntity>(sql
                , new[] { new SqlParameter("@orgId", orgId) });
        }

    }
}


