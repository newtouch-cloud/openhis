using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 采购计划
    /// </summary>
    public class CgPurchaseOrderRepo : RepositoryBase<CgPurchaseOrderEntity>, ICgPurchaseOrderRepo
    {
        public CgPurchaseOrderRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 撤销采购计划
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int CancelPurchasingPlan(string cgdh, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.cg_purchaseOrder SET auditState=3,LastModifyTime=GETDATE(),LastModifierCode=@userCode
WHERE cgdh=@cgdh AND OrganizeId=@OrganizeId AND zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@cgdh", cgdh),
                new SqlParameter("@userCode", userCode),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 查询采购计划
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public CgPurchaseOrderEntity SelectData(string cgdh, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.cg_purchaseOrder(NOLOCK) 
WHERE cgdh=@cgdh AND OrganizeId=@OrganizeId AND zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@cgdh", cgdh),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FirstOrDefault<CgPurchaseOrderEntity>(sql, param);
        }
    }
}
