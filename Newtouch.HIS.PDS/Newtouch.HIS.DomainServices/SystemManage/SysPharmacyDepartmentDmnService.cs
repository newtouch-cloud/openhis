using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 药房药库
    /// </summary>
    public class SysPharmacyDepartmentDmnService : DmnServiceBase, ISysPharmacyDepartmentDmnService
    {

        public SysPharmacyDepartmentDmnService(IDefaultDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }

        /// <summary>
        /// 根据药品代码获取已授权的药房药库
        /// </summary>
        /// <param name="ypId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<SysPharmacyDepartmentVEntity> SelectEmpowermentYfbmByYp(string ypId, string organizeId)
        {
            const string sql = @"
SELECT DISTINCT yfbm.* 
FROM dbo.xt_yp_bmypxx(NOLOCK) bmyp
INNER JOIN NewtouchHIS_Base.dbo.V_C_xt_yp yp ON yp.ypCode=bmyp.Ypdm AND yp.OrganizeId=bmyp.OrganizeId AND yp.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm_yp bmdl ON bmdl.dlCode=yp.dlCode AND bmdl.OrganizeId=bmyp.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm ON yfbm.OrganizeId = bmyp.OrganizeId AND yfbm.yfbmCode=bmyp.yfbmCode AND yfbm.zt='1'
WHERE bmyp.zt='1'
AND yp.ypId=@ypId
AND bmyp.OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@ypId", ypId),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FindList<SysPharmacyDepartmentVEntity>(sql, param);
        }
    }
}
