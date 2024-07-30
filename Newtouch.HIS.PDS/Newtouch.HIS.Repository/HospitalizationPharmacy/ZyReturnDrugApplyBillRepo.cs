using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 住院退药申请单
    /// </summary>
    public class ZyReturnDrugApplyBillRepo : RepositoryBase<ZyReturnDrugApplyBillEntity>, IZyReturnDrugApplyBillRepo
    {
        public ZyReturnDrugApplyBillRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 修改处理状态
        /// </summary>
        /// <param name="applyNo"></param>
        /// <param name="organizeId"></param>
        /// <returns> >0:成功  其余:失败 </returns>
        public ZyReturnDrugApplyBillEntity SelectData(string applyNo, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.zy_returnDrugApplyBill
WHERE applyNo=@applyNo AND OrganizeId=@OrganizeId AND zt='1' AND ProcessState='0'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@applyNo", applyNo),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return _dataContext.Database.SqlQuery<ZyReturnDrugApplyBillEntity>(sql, param).FirstOrDefault();
        }

        /// <summary>
        /// 修改处理状态
        /// </summary>
        /// <param name="applyNo"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="db"></param>
        /// <returns> >0:成功  其余:失败 </returns>
        public int UpdateProcessStateDoingWithTrans(string applyNo, string userCode, string organizeId, Infrastructure.EF.EFDbTransaction db)
        {
            const string sql = @"
UPDATE dbo.zy_returnDrugApplyBill 
SET ProcessState='1', LastModifierCode=@userCode, LastModifyTime=GETDATE() 
WHERE applyNo=@applyNo AND OrganizeId=@OrganizeId AND zt='1' AND ProcessState='0'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@applyNo", applyNo),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@userCode",userCode )
            };
            return db.ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 修改处理状态
        /// </summary>
        /// <param name="applyNo"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns> >0:成功  其余:失败 </returns>
        public int UpdateProcessStateCompleteWithTrans(string applyNo, string userCode, string organizeId, Infrastructure.EF.EFDbTransaction db)
        {
            const string sql = @"
UPDATE dbo.zy_returnDrugApplyBill 
SET ProcessState='2', LastModifierCode=@userCode, LastModifyTime=GETDATE() 
WHERE applyNo=@applyNo AND OrganizeId=@OrganizeId AND zt='1' AND ProcessState='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@applyNo", applyNo),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@userCode",userCode )
            };
            return db.ExecuteSqlCommand(sql, param);
        }
    }
}