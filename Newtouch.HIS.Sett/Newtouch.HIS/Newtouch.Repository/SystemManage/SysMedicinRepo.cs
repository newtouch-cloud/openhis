using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.IRepository;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository.SystemManage
{
    /// <summary>
    /// 系统药品
    /// </summary>
    public class SysMedicinRepo : RepositoryBase<SysMedicineVEntity>, ISysMedicinRepo
    {
        public SysMedicinRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 根据药品Code获取dlCode
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public string GetdlCodeByYpCode(string orgId, string ypCode)
        {
            var sql = "select dlCode from [NewtouchHIS_Base]..V_S_xt_yp width(nolock) where ypCode = @ypCode and OrganizeId = @orgId";
            return this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@ypCode", ypCode) });
        }

        /// <summary>
        /// 获取药品实体
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public SysMedicineVEntity GetEntityByCode(string orgId, string ypCode)
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_xt_yp width(nolock) where ypCode = @ypCode and OrganizeId = @orgId";
            return this.FirstOrDefault<SysMedicineVEntity>(sql, new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@ypCode", ypCode) });
        }

    }
}
