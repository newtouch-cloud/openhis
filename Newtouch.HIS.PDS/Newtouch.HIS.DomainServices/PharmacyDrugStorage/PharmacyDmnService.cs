using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.PharmacyDrugStorage
{
    public class PharmacyDmnService : DmnServiceBase, IPharmacyDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        public PharmacyDmnService(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 查询药房窗口信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="yfckCode"></param>
        /// <param name="yfckmc"></param>
        /// <param name="topOrganizeId"></param>
        /// <returns></returns>
        public IList<PharmacyWindowVO> GetPagintionList(Pagination pagination, string organizeId, string keyword = null)
        {
            var strSql = @"select yfck.yfckId,yfck.yfckCode,yfck.yfckmc,yfck.zt,yfck.px,yfck.CreateTime,yfck.CreatorCode
        from xt_yfck yfck
    where yfck.OrganizeId=@OrganizeId
and (isnull(@keyword, '') = '' or yfck.yfckCode like @searchkeyword or yfck.yfckmc like @searchkeyword )";
            SqlParameter[] param = new SqlParameter[] {
                            new SqlParameter("@keyword",keyword ?? ""),
                            new SqlParameter("@OrganizeId",organizeId),
                            new SqlParameter("@searchkeyword", "%" + (keyword ?? "") + "%")
                        };
            return this.QueryWithPage<PharmacyWindowVO>(strSql, pagination, param);
        }

        /// <summary>
        /// 查看药房部门药品信息(大类)
        /// </summary>
        /// <param name="dlCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<PharmacyDepartmentOpenMedicineRepoVO> SelectDepartmentMedicine(string dlCode, string organizeId)
        {
            const string sql = @"
SELECT * 
FROM [NewtouchHIS_Base].dbo.xt_yfbm_yp(NOLOCK) yfbmyp
WHERE yfbmyp.OrganizeId=@organizeId 
AND yfbmyp.dlCode=@dlCode
AND yfbmyp.zt='1'
";
            DbParameter[] parame = {
                new SqlParameter("@dlCode",dlCode ?? ""),
                new SqlParameter("@organizeId",organizeId)
            };
            return FindList<PharmacyDepartmentOpenMedicineRepoVO>(sql, parame);
        }
    }
}
