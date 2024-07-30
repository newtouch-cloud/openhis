using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
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
        /// 查看药房部门药品信息
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<PharmacyDepartmentOpenMedicineRepoVO> OpenMedicineIndex(string organizeId, string keyword = null)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return new List<PharmacyDepartmentOpenMedicineRepoVO>();
            }
            const string sql = @"
SELECT * 
FROM xt_yfbm_yp(NOLOCK) yfbmyp
LEFT JOIN xt_yfbm as yfbm on yfbm.yfbmCode=yfbmyp.yfbmCode
LEFT JOIN xt_sfdl as sfdl on sfdl.dlCode=yfbmyp.dlCode
WHERE yfbmyp.OrganizeId=@organizeId 
AND yfbm.OrganizeId=@organizeId 
AND sfdl.OrganizeId=@organizeId
AND (isnull(@keyword, '') = '' 
	OR yfbmyp.yfbmCode like @searchkeyword 
	OR yfbmyp.dlCode like @searchkeyword
	OR yfbm.yfbmmc like @searchkeyword 
	OR sfdl.dlmc like @searchkeyword)
order by yfbmyp.yfbmCode, yfbmyp.CreateTime desc
";
            DbParameter[] parame = {
                            new SqlParameter("@keyword",keyword ?? ""),
                            new SqlParameter("@organizeId",organizeId),
                            new SqlParameter("@searchkeyword", "%" + (keyword ?? "") + "%")
            };
            return FindList<PharmacyDepartmentOpenMedicineRepoVO>(sql, parame);
        }

        /// <summary>
        /// 查看药房部门药品信息(大类)
        /// </summary>
        /// <param name="dlCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public PharmacyDepartmentOpenMedicineRepoVO SelectDepartmentMedicine(string dlCode, string yfbmCode, string organizeId)
        {
            const string sql = @"
SELECT * 
FROM xt_yfbm_yp(NOLOCK) yfbmyp
WHERE yfbmyp.OrganizeId=@organizeId 
AND yfbmyp.dlCode=@dlCode
AND yfbmyp.yfbmCode=@yfbmCode
AND yfbmyp.zt='1'
";
            DbParameter[] parame = {
                new SqlParameter("@dlCode",dlCode ?? ""),
                new SqlParameter("@organizeId",organizeId),
                new SqlParameter("@yfbmCode", yfbmCode)
            };
            return FirstOrDefault<PharmacyDepartmentOpenMedicineRepoVO>(sql, parame);
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
FROM xt_yfbm_yp(NOLOCK) yfbmyp
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

