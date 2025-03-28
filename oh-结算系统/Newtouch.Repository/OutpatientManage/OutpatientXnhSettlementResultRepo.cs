using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 门诊新农合结算
    /// </summary>
    public class OutpatientXnhSettlementResultRepo : RepositoryBase<OutpatientXnhSettlementResultEntity>, IOutpatientXnhSettlementResultRepo
    {
        public OutpatientXnhSettlementResultRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据outpId获取数据
        /// </summary>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<OutpatientXnhSettlementResultEntity> SelectData(string outpId, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.mz_xnh_settResult(NOLOCK) 
WHERE outpId=@outpId AND OrganizeId=@OrganizeId AND zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@outpId", outpId),
                new SqlParameter("@OrganizeId",organizeId ),
            };
            return FindList<OutpatientXnhSettlementResultEntity>(sql, param);
        }

        /// <summary>
        /// 修改结算标志
        /// </summary>
        /// <param name="jszt">结算状态 1-已结 2-已退   默认已结</param>
        /// <param name="outpId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int UpdateJszt(int jszt, string outpId, string userCode, string organizeId)
        {
            const string sql = @"
UPDATE dbo.mz_xnh_settResult SET jszt=@jszt, LastModifyTime=GETDATE(), LastModifierCode=@userCode 
WHERE outpId=@outpId AND zt='1' AND OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@jszt", jszt),
                new SqlParameter("@userCode", userCode),
                new SqlParameter("@outpId", outpId),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return ExecuteSqlCommand(sql, param);
        }
    }
}


