using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 采购单
    /// </summary>
    public class CgOrderRepo : RepositoryBase<CgOrderEntity>, ICgOrderRepo
    {
        public CgOrderRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取采购单信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public CgOrderEntity SelectData(string orderNo, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.cg_order(NOLOCK) 
WHERE orderNo=@orderNo AND zt='1' AND OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@orderNo",orderNo),
                new SqlParameter("@OrganizeId",organizeId)
            };
            return FirstOrDefault<CgOrderEntity>(sql, param);
        }

        /// <summary>
        /// 审核采购单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orderType"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public int AuditPurchasingOrder(string orderNo, int orderType, string userCode, string organizeId, string remark)
        {
            const string sql = @"
UPDATE dbo.cg_order SET orderType=@orderType, remark=@remark, LastModifierCode=@userCode, LastModifyTime=GETDATE()
WHERE orderNo=@orderNo AND orderType=0 AND orderProcess=0 AND zt='1' AND OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@orderType", orderType),
                new SqlParameter("@userCode", userCode),
                new SqlParameter("@orderNo", orderNo),
                new SqlParameter("@remark", remark),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return ExecuteSqlCommand(sql, param);
        }
    }
}
