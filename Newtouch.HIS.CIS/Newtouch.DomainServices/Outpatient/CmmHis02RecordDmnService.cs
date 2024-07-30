using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// 推送中药信息
    /// </summary>
    public class CmmHis02RecordDmnService : DmnServiceBase, ICmmHis02RecordDmnService
    {
        public CmmHis02RecordDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取未同步的药品
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="dlType"></param>
        /// <returns></returns>
        public List<SysMedicineExVEntity> SelectNoSyncMedicines(string organizeId, string dlType = "TCM")
        {
            const string sql = @"
;WITH dl AS 
(
	SELECT DISTINCT dllx.dlCode FROM NewtouchHIS_Base.dbo.V_S_xt_sfdl_lx dllx 
	WHERE dllx.OrganizeId=@OrganizeId AND dllx.zt='1' AND dllx.Type=@dlType
)
SELECT yp.* , ypsx.ypgg gg
FROM NewtouchHIS_Base.dbo.V_S_xt_yp yp
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId
LEFT JOIN dbo.cmm_his_02_record(NOLOCK) re ON re.code=yp.ypCode AND re.zt='1' AND re.OrganizeId=yp.OrganizeId
INNER JOIN dl dllx ON dllx.dlCode=yp.dlCode 
WHERE re.Id IS NULL
AND yp.OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@dlType", dlType)
            };
            return FindList<SysMedicineExVEntity>(sql, param);
        }

        /// <summary>
        /// 获取未同步的药品
        /// </summary>
        /// <param name="topSize">单次请求总条数</param>
        /// <param name="organizeId"></param>
        /// <param name="dlType"></param>
        /// <returns></returns>
        public List<SysMedicineExVEntity> SelectNoSyncMedicines(int topSize, string organizeId, string dlType = "TCM")
        {
            var sql = string.Format(@"
;WITH dl AS 
(
	SELECT DISTINCT dllx.dlCode FROM NewtouchHIS_Base.dbo.V_S_xt_sfdl_lx dllx 
	WHERE dllx.OrganizeId=@OrganizeId AND dllx.zt='1' AND dllx.Type=@dlType
)
SELECT TOP {0} yp.* , ypsx.ypgg gg
FROM NewtouchHIS_Base.dbo.V_S_xt_yp yp
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId
LEFT JOIN dbo.cmm_his_02_record(NOLOCK) re ON re.code=yp.ypCode AND re.zt='1' AND re.OrganizeId=yp.OrganizeId
INNER JOIN dl dllx ON dllx.dlCode=yp.dlCode 
WHERE re.Id IS NULL
AND yp.OrganizeId=@OrganizeId ", topSize);
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@dlType", dlType)
            };
            return FindList<SysMedicineExVEntity>(sql, param);
        }

        /// <summary>
        /// 获取同步失败的药品
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<SysMedicineExVEntity> SelectSyncFailedMedicines(string organizeId)
        {
            const string sql = @"
SELECT yp.*, ypsx.ypgg gg
FROM NewtouchHIS_Base.dbo.V_S_xt_yp yp
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId
INNER JOIN dbo.cmm_his_02_record(NOLOCK) re ON re.code=yp.ypCode AND re.zt='1' AND re.OrganizeId=yp.OrganizeId
WHERE yp.OrganizeId=@OrganizeId
AND (re.resultCode='0' OR ISNULL(re.resultCode,'')='')
";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<SysMedicineExVEntity>(sql, param);
        }
    }
}