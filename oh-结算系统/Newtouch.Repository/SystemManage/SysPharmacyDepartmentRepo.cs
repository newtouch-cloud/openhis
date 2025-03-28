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
    public class SysPharmacyDepartmentRepo : RepositoryBase<SysPharmacyDepartmentVEntity>, ISysPharmacyDepartmentRepo
    {
        public SysPharmacyDepartmentRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetYjbmjbByCode(string code, string orgId)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            var sql = @"SELECT  CONVERT(VARCHAR(4), yjbmjb)
                        FROM NewtouchHIS_Base..V_S_xt_yfbm
                        WHERE   yfbmCode =@code
                                AND OrganizeId = @orgId";
            return this.FirstOrDefault<string>(sql
                , new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@code", code) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetMzzybzByCode(string code, string orgId)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            var sql = @"SELECT  CONVERT(VARCHAR(1), mzzybz)
                        FROM NewtouchHIS_Base..V_S_xt_yfbm
                        WHERE   yfbmCode =@code
                                AND OrganizeId = @orgId";
            return this.FirstOrDefault<string>(sql
                , new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@code", code) });
        }

        /// <summary>
        /// 获取发药药房列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzzybz"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVEntity> GetPharmacyDepartmentList(string orgId, string mzzybz)
        {
            var sql = @"SELECT * FROM NewtouchHIS_Base..V_S_xt_yfbm
WHERE mzzybz = @mzzybz AND OrganizeId = @orgId and zt = '1' and yjbmjb = 2";
            return this.FindList<SysPharmacyDepartmentVEntity>(sql, new[] { new SqlParameter("@orgId", orgId ?? "")
                , new SqlParameter("@mzzybz", mzzybz ?? "") });
        }

    }
}
