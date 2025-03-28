using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 新农合门诊补偿序号关联表
    /// </summary>
    public class OutpatientXnhOutpIdRelRepo : RepositoryBase<OutpatientXnhOutpIdRelEntity>, IOutpatientXnhOutpIdRelRepo
    {
        public OutpatientXnhOutpIdRelRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 修改状态无效
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int UpdateZtDisable(string mzh, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.mz_xnh_outpIdRel SET zt='0', LastModifyTime=GETDATE(), LastModifierCode=@usercode 
WHERE mzh=@mzh AND OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@userCode", userCode),
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 修改状态无效
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int UpdateZtDisable(string mzh, string outpId, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.mz_xnh_outpIdRel SET zt='0', LastModifyTime=GETDATE(), LastModifierCode=@usercode 
WHERE mzh=@mzh AND OrganizeId=@OrganizeId AND outpId=@outpId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@outpId", outpId),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@userCode", userCode),
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 修改处理状态
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="processingStatus">处理状态 0-未处理 1-明细已上传 2-已结算 3-已红冲  4-门诊回退 </param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int UpdateProcessingStatus(string mzh, int processingStatus, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.mz_xnh_outpIdRel SET processingStatus=@processingStatus, LastModifyTime=GETDATE(), LastModifierCode=@usercode 
WHERE mzh=@mzh AND OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@processingStatus", processingStatus),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@userCode", userCode),
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 修改处理状态
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="processingStatus">处理状态 0-未处理 1-明细已上传 2-已结算 3-已红冲  4-门诊回退 </param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int UpdateProcessingStatus(string mzh, int processingStatus, string outpId, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.mz_xnh_outpIdRel SET processingStatus=@processingStatus, LastModifyTime=GETDATE(), LastModifierCode=@usercode 
WHERE mzh=@mzh AND OrganizeId=@OrganizeId AND outpId=@outpId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@processingStatus", processingStatus),
                new SqlParameter("@outpId", outpId),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@userCode", userCode),
            };
            return ExecuteSqlCommand(sql, param);
        }
    }
}


