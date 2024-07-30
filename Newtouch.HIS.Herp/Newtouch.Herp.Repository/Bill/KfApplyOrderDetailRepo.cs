using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 申领单明细
    /// </summary>
    public class KfApplyOrderDetailRepo : RepositoryBase<KfApplyOrderDetailEntity>, IKfApplyOrderDetailRepo
    {
        public KfApplyOrderDetailRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 修改已发数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sl">部门单位数量，与表kf_applyOrderDetail.zhyz*sl=最小单位数量</param>
        /// <returns></returns>
        public int UpdateYfsl(long id, int sl)
        {
            const string sql = @"
UPDATE dbo.kf_applyOrderDetail 
SET yfsl=yfsl+@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode 
WHERE Id=@sldmxId AND zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@sldmxId", id),
                new SqlParameter("@sl", sl),
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 修改已发数量，并返回变更记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sl">部门单位数量，与表kf_applyOrderDetail.zhyz*sl=最小单位数量</param>
        /// <returns></returns>
        public KfApplyOrderDetailEntity UpdateYfslAndSelectChangeRecord(long id, int sl)
        {
            const string sql = @"
UPDATE dbo.kf_applyOrderDetail 
SET yfsl=yfsl+sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode 
WHERE Id=@sldmxId AND zt='1'
SELECT * FROM dbo.kf_applyOrderDetail(NOLOCK) WHERE Id=@sldmxId AND zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@sldmxId", id),
                new SqlParameter("@sl", sl),
            };
            return FirstOrDefault<KfApplyOrderDetailEntity>(sql, param);
        }

        /// <summary>
        /// 获取申领单明细信息
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<KfApplyOrderDetailEntity> SelectData(string sldh, string organizeId)
        {
            const string sql = @"
SELECT mx.* 
FROM dbo.kf_applyOrderDetail(NOLOCK) mx
INNER JOIN dbo.kf_applyOrder(NOLOCK) m ON m.Id=mx.sldId AND m.zt='1'
WHERE m.OrganizeId=@OrganizeId 
AND m.sldh=@sldh
AND mx.zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@sldh", sldh),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<KfApplyOrderDetailEntity>(sql, param);
        }
    }
}