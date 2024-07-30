using System.Data.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Infrastructure.Model;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// V_S_xt_yfbm
    /// </summary>
    public class SysPharmacyDepartmentRepo : RepositoryBase<SysPharmacyDepartmentVEntity>, ISysPharmacyDepartmentRepo
    {
        public SysPharmacyDepartmentRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// get Yjbmjb by yfbmcode and orgid
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
            const string sql = @"SELECT  CONVERT(VARCHAR(4), yjbmjb)
                        FROM NewtouchHIS_Base..V_S_xt_yfbm
                        WHERE   yfbmCode =@code
                                AND OrganizeId = @orgId";
            return FirstOrDefault<string>(sql, new DbParameter[] { new SqlParameter("@orgId", orgId), new SqlParameter("@code", code) });
        }

        /// <summary>
        /// get mzzybz by yfbmcode and orgid
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
            const string sql = @"SELECT  CONVERT(VARCHAR(1), mzzybz)
                        FROM NewtouchHIS_Base..V_S_xt_yfbm
                        WHERE   yfbmCode =@code
                                AND OrganizeId = @orgId";
            return FirstOrDefault<string>(sql, new DbParameter[] { new SqlParameter("@orgId", orgId), new SqlParameter("@code", code) });
        }

        /// <summary>
        /// 根据yfbmcode获取药房部门实体
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public LoginUserCurrentYfbmModel GetUserYfbmByCode(string code, string orgId)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            const string sql = @"SELECT a.yfbmCode,b.yfbmmc,CONVERT(VARCHAR(1), b.mzzybz) as mzzybz,CONVERT(VARCHAR(4), b.yjbmjb) as yfbmjb
                        FROM NewtouchHIS_Base..V_S_xt_yfbm a
                        INNER JOIN [NewtouchHIS_Base]..V_S_xt_yfbm b on a.OrganizeId = b.OrganizeId and b.zt = '1' and a.yfbmCode = b.yfbmCode
                        WHERE a.zt = '1' AND a.yfbmCode =@code
                              AND a.OrganizeId = @orgId";
            return FirstOrDefault<LoginUserCurrentYfbmModel>(sql, new DbParameter[] { new SqlParameter("@orgId", orgId), new SqlParameter("@code", code) });
        }
    }
}
