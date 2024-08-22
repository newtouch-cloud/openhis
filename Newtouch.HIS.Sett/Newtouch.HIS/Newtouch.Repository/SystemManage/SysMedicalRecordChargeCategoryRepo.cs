using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 病案收费大类
    /// </summary>
    public class SysMedicalRecordChargeCategoryRepo : RepositoryBase<SysMedicalRecordChargeCategoryVEntity>, ISysMedicalRecordChargeCategoryRepo
    {
        public SysMedicalRecordChargeCategoryRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取所有病案收费大类下拉框
        /// </summary>
        /// <returns></returns>
        public IList<SysMedicalRecordChargeCategoryVEntity> GetdlSelect(string orgId)
        {
            var sql = "select dlCode, dlmc, py from [NewtouchHIS_Base]..V_S_xt_basfdl width(nolock) where OrganizeId = @orgId and zt = '1'";
            return this.FindList<SysMedicalRecordChargeCategoryVEntity>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

    }
}
