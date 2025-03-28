using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.TSQL;
using Newtouch.Tools;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 结转
    /// </summary>
    public class SysMedicineStockCarryDownDmnService : DmnServiceBase, ISysMedicineStockCarryDownDmnService
    {

        public SysMedicineStockCarryDownDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 查询已结转药品
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="jzsj"></param>
        /// <param name="keyWork"></param>
        /// <returns></returns>
        public IList<CarryOverMedicineVO> SelectCarryDownMedicineList(Pagination pagination, string jzsj, string keyWork)
        {
            var strSql = new StringBuilder(@"
SELECT CONVERT(VARCHAR(23), jz.Jzsj, 120) Jzsj,
	yp.ypmc ,
	jz.ypdm,
	ypsx.ypgg gg ,
    isnull(jz.kcsl, 0)/dbo.f_getyfbmZhyz(@yfbmCode,jz.ypdm,@OrganizeId) jzsl,
	dbo.f_getYfbmYpComplexYpSlandDw(jz.kcsl, @YfbmCode, jz.Ypdm, @Organizeid) jzslstr ,
	dbo.f_getyfbmDw(jz.yfbmCode, jz.Ypdm, jz.OrganizeId) dw ,
	jz.pc,
	yp.ycmc sccj ,
	CONVERT(NUMERIC(12,4),ISNULL(jz.ykpfj/yp.bzs*dbo.f_getyfbmZhyz(@yfbmCode,jz.ypdm,@OrganizeId), 0)) pfj ,
	CONVERT(NUMERIC(12,4),isnull(jz.yklsj/yp.bzs*dbo.f_getyfbmZhyz(@yfbmCode,jz.ypdm,@OrganizeId), 0)) lsj ,
	CONVERT(NUMERIC(12,2),isnull(jz.ykpfj/yp.bzs*jz.kcsl, 0)) pfze,
	CONVERT(NUMERIC(12,2),isnull(jz.Yklsj/yp.bzs*jz.kcsl, 0)) lsze,
	jz.CreateTime
FROM xt_yp_kcjzk(NOLOCK) jz
INNER JOIN xt_yp_bmypxx(NOLOCK) bmyp on bmyp.ypdm = jz.ypdm and bmyp.yfbmCode = @yfbmCode AND bmyp.OrganizeId = @OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.xt_yp(NOLOCK) yp on bmyp.ypdm = yp.ypCode AND yp.OrganizeId=@OrganizeId 
INNER JOIN NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx on yp.ypId = ypsx.ypId
WHERE jz.yfbmCode = @yfbmCode
AND jz.OrganizeId=@OrganizeId
");

            var inSqlParameterList = new List<DbParameter>
            {
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId),
                new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode)
            };
            if (!string.IsNullOrEmpty(jzsj))
            {
                var tmp = Constants.MinDateTime;
                DateTime.TryParse(jzsj, out tmp);

                strSql.AppendLine("AND (jz.Jzsj >= @Jzsj1 AND jz.Jzsj < @Jzsj2) ");
                inSqlParameterList.Add(new SqlParameter("@Jzsj1", tmp));
                inSqlParameterList.Add(new SqlParameter("@Jzsj2", tmp.AddSeconds(1)));
            }
            if (!string.IsNullOrEmpty(keyWork))
            {
                strSql.AppendLine("AND ( yp.ypCode like @srm or yp.ypmc like @srm or yp.py like lower(@srm) )");
                inSqlParameterList.Add(new SqlParameter("@srm", "%" + keyWork.Trim() + "%"));
            }
            return QueryWithPage<CarryOverMedicineVO>(strSql.ToString(), pagination, inSqlParameterList.ToArray());
        }

        /// <summary>
        /// 结转 将需结转的药品全部插入到结转表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="jzsj">结转时间</param>
        public string CarryOverMedicine(List<NeedCarryOverMedicineVO> list, string yfbmCode, string organizeId, DateTime jzsj)
        {
            var param = new List<DbParameter>
            {
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@jzsj", jzsj),
                new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
                new SqlParameter("@jzyp", list.ToDataTable()){
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.NeedCarryDownMedicine"
                }
            };
            var outpar = new SqlParameter("@result", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            param.Add(outpar);
            FindList<object>(@"EXEC dbo.[yp_xt_kcjz] @jzsj, @OrganizeId, @yfbmCode, @CreatorCode, @jzyp, @result output", param.ToArray());
            return outpar.Value.ToString();
        }

        /// <summary>
        /// 结转 将需结转的药品全部插入到结转表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="jzsj"></param>
        /// <param name="userCode">结转时间</param>
        /// <returns></returns>
        public string CarryOverMedicine(List<NeedCarryOverMedicineVO> list, string yfbmCode, string organizeId, DateTime jzsj, string userCode)
        {
            var param = new List<DbParameter>
            {
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@jzsj", jzsj),
                new SqlParameter("@CreatorCode", userCode),
                new SqlParameter("@jzyp", list.ToDataTable()){
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.NeedCarryDownMedicine"
                }
            };
            return FindList<string>(TSqlStock.yp_xt_kcjz, param.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// 结转 将需结转的药品全部插入到结转表
        /// </summary>
        /// <param name="list"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="jzsj"></param>
        /// <param name="userCode">结转时间</param>
        /// <returns></returns>
        public string CarryOverMedicine(string yfbmCode, string organizeId, DateTime jzsj, string userCode)
        {
            var param = new List<DbParameter>
            {
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@jzsj", jzsj),
                new SqlParameter("@CreatorCode", userCode)
            };
            return FindList<string>(TSqlStock.yp_xt_kcjz_v2, param.ToArray()).FirstOrDefault();
        }
    }
}
