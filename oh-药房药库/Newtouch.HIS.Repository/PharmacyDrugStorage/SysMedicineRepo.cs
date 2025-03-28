using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 系统药品 V_S_xt_yp
    /// </summary>
    public class SysMedicineRepo : RepositoryBase<SysMedicineVEntity>, ISysMedicineRepo
    {
        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysMedicineRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取一个药品对象
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public SysMedicineVEntity GetMedicineByCode(string orgId, string ypCode)
        {
            const string sql = "select * from [NewtouchHIS_Base]..V_S_xt_yp(nolock) where OrganizeId = @orgId and ypCode = @ypCode";
            return FirstOrDefault<SysMedicineVEntity>(sql, new DbParameter[] { new SqlParameter("@orgId", orgId), new SqlParameter("@ypCode", ypCode) });
        }

        /// <summary>
        /// 获取一个药品集合 strwhere 暂时无效
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<SysMedicineVEntity> GetMedicineListByOrg(string orgId)
        {
            const string sql = "select * from [NewtouchHIS_Base]..V_S_xt_yp(nolock) where OrganizeId = @orgId";
            return FindList<SysMedicineVEntity>(sql, new DbParameter[] { new SqlParameter("@orgId", orgId) });
        }
    }
}
