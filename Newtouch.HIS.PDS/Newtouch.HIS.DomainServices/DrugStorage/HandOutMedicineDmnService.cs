using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.TSQL;
using Newtouch.Tools;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 发药
    /// </summary>
    public class HandOutMedicineDmnService : DmnServiceBase, IHandOutMedicineDmnService
    {
        public HandOutMedicineDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }

        /// <summary>
        /// 输入码自动提示（一级部门提示，没有转换因子，例如药剂科）
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="srm"></param>
        /// <returns></returns>
        public List<HandOutMedicinesrmVO> GetsrmInfoFlevelList(string yfbmCode, string srm)
        {
            var par = new DbParameter[] {
                    new SqlParameter("@yfbmCode",yfbmCode),
                    new SqlParameter("@srm","%"+(string.IsNullOrEmpty(srm)?"":srm.Trim())+"%"),
                    new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<HandOutMedicinesrmVO>(@" EXEC[dbo].[xt_yp_GetsrmInfoFlevelList] @srm, @yfbmCode, @OrganizeId ", par);
        }

        /// <summary>
        /// 获取药品信息  根据批次批号分组
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<HandOutMedicinesrmVO> GetDrugGroupByPc(string ypCode, string yfbmCode, string organizeId)
        {
            const string sql = @"
SELECT s.ypmc,s.ycmc,s.ph,s.pc,s.zhyz,s.ypCode,s.zxdw,s.bzs,s.bzdw,s.bmdw,s.yxq,CONVERT(NUMERIC(11,2),s.zxdwjj*s.zhyz) jj
,ISNULL(CONVERT(INT,s.kykcsl/s.zhyz),0) bmdwsl
,dbo.f_getComplexYpSlandDw(s.kykcsl,s.zhyz,s.bmdw, s.zxdw) klsl
,ISNULL(CONVERT(INT,s.kykcsl/s.bzs),0) bzdwsl 
FROM (
	SELECT a.ypmc ,ycmc ,LTRIM(RTRIM(b.ph)) ph ,LTRIM(RTRIM(b.pc)) pc ,a.ypCode , ISNULL(CONVERT(NUMERIC(11,4),(b.jj/a.bzs)),0) zxdwjj,a.zxdw, a.bzs, a.bzdw
	,ISNULL(CONVERT(INT,CASE e.mzzybz WHEN '0' THEN a.bzs WHEN '1' THEN a.mzcls WHEN '2' THEN a.zycls WHEN '3' THEN a.mzcls END), 0) zhyz 
	,(CASE e.mzzybz WHEN '0' THEN a.bzdw WHEN '1' THEN a.mzcldw WHEN '2' THEN a.zycldw WHEN '3' THEN a.mzcldw END) bmdw , 
	CONVERT(DATETIME,ISNULL(MAX(CONVERT(VARCHAR(10), b.yxq, 120)), '1899-01-01')) yxq ,
	ISNULL(SUM(b.kcsl - b.djsl), 0) kykcsl
	FROM NewtouchHIS_Base.dbo.V_S_xt_yp(NOLOCK) a 
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=a.ypId AND ypsx.OrganizeId=a.OrganizeId
	INNER JOIN dbo.xt_yp_bmypxx(NOLOCK) bmypxx ON bmypxx.Ypdm=a.ypCode AND bmypxx.OrganizeId=a.OrganizeId AND bmypxx.zt='1'
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm e WITH ( NOLOCK ) ON e.yfbmCode=bmypxx.yfbmCode AND e.OrganizeId=a.OrganizeId AND e.zt='1'
	LEFT JOIN dbo.xt_yp_kcxx(NOLOCK) b ON b.ypdm = a.ypCode AND b.yfbmCode = bmypxx.yfbmCode AND b.OrganizeId=a.OrganizeId AND b.zt='1'
	WHERE e.yfbmCode=@yfbmCode
	AND bmypxx.yfbmCode=@yfbmCode
	AND a.ypCode=@ypCode
	AND a.OrganizeId=@OrganizeId
	GROUP BY a.ypmc, a.ycmc, b.ph, b.pc, a.ypCode, a.pfj, a.lsj, b.jj, a.zxdw, a.bzs, e.mzzybz, b.yxq, a.bzdw, a.mzcls, a.zycls, a.mzcldw, a.zycldw
) s
ORDER BY s.yxq ASC
";
            var param = new DbParameter[]
            {
                new SqlParameter("@ypCode",ypCode),
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@OrganizeId", organizeId),
            };
            return FindList<HandOutMedicinesrmVO>(sql, param);
        }

        /// <summary>
        /// 获取退药药品明细
        /// </summary>
        /// <param name="ckbm">出库部门 申请退药的部门</param>
        /// <param name="rkbm">入库部门 药品即将退回到的部门</param>
        /// <param name="srm">输入码 关键字</param>
        /// <returns></returns>
        public List<HandOutMedicinesrmVO> GetTyypBySrm(string ckbm, string rkbm, string srm)
        {
            var par = new DbParameter[] {
                new SqlParameter("@srm",string.IsNullOrEmpty(srm)?"":srm.Trim()),
                new SqlParameter("@rkbm", rkbm),
                new SqlParameter("@yfbmCode", Constants.GetCurrentYfbm(OperatorProvider.GetCurrent().UserId).yfbmCode),
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<HandOutMedicinesrmVO>(@" EXEC[dbo].[sp_yp_getTyypBySrm] @srm, @rkbm, @YfbmCode, @OrganizeId ", par);
        }

        /// <summary>
        ///  获取科室药品库存记录
        /// </summary>
        /// <param name="ypdm"></param>
        /// <returns></returns>
        public List<MedicinepcInfo> GetpcList(string ypdm)
        {
            var yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var strSql = new StringBuilder(@"
        SELECT  *
        FROM    ( SELECT    ph , pc ,
                            CONVERT(VARCHAR(10), Yxq, 120) Yxq ,
                            FLOOR(kcsl-djsl) Sl
                    FROM    XT_YP_KCXX(NOLOCK)
                    WHERE   Ypdm = @ypCode
                            AND yfbmCode = @yfbmCode
							AND OrganizeId=@OrganizeId
							AND (kcsl-djsl)>0
                    GROUP BY  Ph , pc , Yxq , zhyz, kcsl, djsl
                ) tmp
        WHERE   Sl > 0
        ORDER BY Yxq
");
            DbParameter[] par = {
                new SqlParameter("@ypCode",ypdm),
                new SqlParameter("@yfbmCode",yfbmCode),
                new SqlParameter("@OrganizeId",Common.Operator.OperatorProvider.GetCurrent().OrganizeId),
            };
            return FindList<MedicinepcInfo>(strSql.ToString(), par);
        }

        /// <summary>
        /// 申领出库
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="fydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string HandOutMedicineByRequest(Dictionary<string, List<XT_YP_LS_NBFYMXK>> xtYpLsNbfymxk, string fydh, string fyfs, int type)
        {
            #region 申领出库
            foreach (var key in xtYpLsNbfymxk.Keys)
            {
                var ckbm = Constants.CurrentYfbm.yfbmCode;
                var organizeid = OperatorProvider.GetCurrent().OrganizeId;
                var usercode = OperatorProvider.GetCurrent().UserCode;

                #region 根据药品信息调用YP_XT_GetSLFYInfo 获得批次，批号，有效期
                var dt = new DataTable();
                dt.Columns.Add("Ypdm");
                dt.Columns.Add("Sl");
                dt.Columns.Add("zhyz");
                dt.Columns.Add("bzdw");
                dt.Columns.Add("Lsj");
                dt.Columns.Add("Pfj");
                dt.Columns.Add("sldmxId");
                foreach (var item in xtYpLsNbfymxk[key].Where(p => p.fysl > 0))
                {
                    var row = dt.NewRow();
                    row["Ypdm"] = item.Ypdm;
                    row["Sl"] = item.fysl;
                    row["zhyz"] = item.zhyz;
                    row["bzdw"] = item.bzdw;
                    row["Lsj"] = item.Lsj;
                    row["Pfj"] = item.Pfj;
                    row["sldmxId"] = item.sldmxId;
                    dt.Rows.Add(row);
                }
                var sqlParList2 = new List<SqlParameter>
                {
                    new SqlParameter("@YfbmCode", ckbm),
                    new SqlParameter("@Organizeid", organizeid),
                    new SqlParameter("@OrderDetailsType", dt)
                    {
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "dbo.OrderDetailsType"
                    }
                };
                var outpar2 = new SqlParameter("@res", SqlDbType.NVarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                sqlParList2.Add(outpar2);
                var retun = FindList<XT_YP_LS_NBFYMXK>("EXEC dbo.YP_XT_GetSLFYInfo @YfbmCode, @Organizeid, @OrderDetailsType,@res Out", sqlParList2.ToArray());
                if (!string.IsNullOrWhiteSpace(outpar2.Value.ToString())) return outpar2.Value.ToString();
                if (retun == null || retun.Count <= 0) return "无可用药品";
                #endregion
                var dtt = new DataTable();
                dtt.Columns.Add("Ypdm");
                dtt.Columns.Add("Ph");
                dtt.Columns.Add("pc");
                dtt.Columns.Add("Yxq");
                dtt.Columns.Add("Lsj");
                dtt.Columns.Add("Pfj");
                dtt.Columns.Add("fysl");
                dtt.Columns.Add("zhyz");
                dtt.Columns.Add("lsje");
                dtt.Columns.Add("bzdw");
                dtt.Columns.Add("sldmxId");
                retun.ForEach(p =>
                {
                    var row = dtt.NewRow();
                    row["Ypdm"] = p.Ypdm;
                    row["Ph"] = p.Ph;
                    row["pc"] = p.pc;
                    row["Yxq"] = p.Yxq;
                    row["Lsj"] = p.Lsj;
                    row["Pfj"] = p.Pfj;
                    row["fysl"] = p.fysl;
                    row["zhyz"] = p.zhyz;
                    row["lsje"] = p.lsje;
                    row["bzdw"] = p.bzdw;
                    row["sldmxId"] = p.sldmxId;
                    dtt.Rows.Add(row);
                });
                var param = new List<DbParameter>
                {
                    new SqlParameter("@Rkbm", key),
                    new SqlParameter("@Ckbm", Constants.CurrentYfbm.yfbmCode),
                    new SqlParameter("@Pdh", fydh),
                    new SqlParameter("@Rksj", DateTime.Now),
                    new SqlParameter("@Cksj", DateTime.Now),
                    new SqlParameter("@Ckczy", usercode),
                    new SqlParameter("@Crkfsdm", fyfs),
                    new SqlParameter("@usercode", OperatorProvider.GetCurrent().UserCode),
                    new SqlParameter("@organizeid", OperatorProvider.GetCurrent().OrganizeId),
                    new SqlParameter("@type", type),
                    new SqlParameter("@XT_YP_LS_NBFYMX", dtt){
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "dbo.XT_YP_LS_NBFYMXK"
                    }
                };
                var outpar1 = new SqlParameter("@result", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                param.Add(outpar1);
                var proResult = FindList<object>("EXEC [dbo].[YP_XT_NBFYTH] @Rkbm,@Ckbm,@Pdh,@Ckczy,@Crkfsdm,@XT_YP_LS_NBFYMX,@usercode,@organizeid,@type,@result out", param.ToArray());
                if (outpar1.Value == null || string.IsNullOrWhiteSpace(outpar1.Value.ToString()))
                {
                    continue;
                }
                return outpar1.Value.ToString();
            }
            #endregion
            return "";
        }

        /// <summary>
        /// 退药
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="tybm"></param>
        /// <param name="tydh"></param>
        /// <param name="fyfs"></param>
        public string RequestOfReturnMedicine(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string tybm, string tydh, string fyfs, int type)
        {
            var organizeid = OperatorProvider.GetCurrent().OrganizeId;
            var usercode = OperatorProvider.GetCurrent().UserCode;
            var ckbm = Constants.CurrentYfbm.yfbmCode;
            var param = new List<DbParameter>
            {
                new SqlParameter("@Rkbm", tybm),
                new SqlParameter("@Ckbm", ckbm),
                new SqlParameter("@Pdh", tydh),
                new SqlParameter("@Ckczy", usercode),
                new SqlParameter("@Crkfsdm", fyfs),
                new SqlParameter("@usercode", usercode),
                new SqlParameter("@organizeid", organizeid),
                new SqlParameter("@type", type),
                new SqlParameter("@XT_YP_LS_NBFYMX", xtYpLsNbfymxk.ToDataTable()){
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.XT_YP_LS_NBFYMXK"
                }
            };
            var outpar = new SqlParameter("@result", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            param.Add(outpar);
            var proResult = FindList<object>("EXEC [dbo].[YP_XT_NBFYTH] @Rkbm,@Ckbm,@Pdh,@Ckczy,@Crkfsdm,@XT_YP_LS_NBFYMX,@usercode,@organizeid, @type,@result out", param.ToArray());
            return outpar.Value.ToString();
        }

        /// <summary>
        /// 发药(直接出库)
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="lybm"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string HandOutMedicine(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string lybm, string pdh, string fyfs, int type)
        {
            var organizeid = OperatorProvider.GetCurrent().OrganizeId;
            var usercode = OperatorProvider.GetCurrent().UserCode;
            var ckbm = Constants.CurrentYfbm.yfbmCode;
            var param = new List<DbParameter>
            {
                 new SqlParameter("@Rkbm", lybm),
                 new SqlParameter("@Ckbm", ckbm),
                 new SqlParameter("@Pdh", pdh),
                 new SqlParameter("@Rksj", DateTime.Now),
                 new SqlParameter("@Cksj", DateTime.Now),
                 new SqlParameter("@Ckczy", usercode),
                 new SqlParameter("@Crkfsdm", fyfs),
                 new SqlParameter("@usercode", usercode),
                 new SqlParameter("@organizeid", organizeid),
                 new SqlParameter("@type", type),
                 new SqlParameter("@XT_YP_LS_NBFYMX", xtYpLsNbfymxk.ToDataTable()){
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.XT_YP_LS_NBFYMXK"
                }
            };
            var outpar = new SqlParameter("@result", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            param.Add(outpar);
            var proResult = FindList<object>("EXEC [dbo].[YP_XT_NBFYTH] @Rkbm,@Ckbm,@Pdh,@Ckczy,@Crkfsdm,@XT_YP_LS_NBFYMX,@usercode,@organizeid,@type,@result out", param.ToArray());
            return outpar.Value.ToString();
        }

        /// <summary>
        /// 药房向科室发药
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="lybm"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string DispensingMedicineToKs(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string lybm, string pdh, string fyfs, int type)
        {
            var organizeid = OperatorProvider.GetCurrent().OrganizeId;
            var usercode = OperatorProvider.GetCurrent().UserCode;
            var ckbm = Constants.CurrentYfbm.yfbmCode;
            var param = new List<DbParameter>
            {
                 new SqlParameter("@Rkbm", lybm),
                 new SqlParameter("@Ckbm", ckbm),
                 new SqlParameter("@Pdh", pdh),
                 new SqlParameter("@Ckczy", usercode),
                 new SqlParameter("@Crkfsdm", fyfs),
                 new SqlParameter("@usercode", usercode),
                 new SqlParameter("@organizeid", organizeid),
                 new SqlParameter("@type", type),
                 new SqlParameter("@XT_YP_LS_NBFYMX", xtYpLsNbfymxk.ToDataTable()){
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.XT_YP_LS_NBFYMXK"
                }
            };
            var outpar = new SqlParameter("@result", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            param.Add(outpar);
            var proResult = FindList<object>("EXEC [NewtouchHIS_PDS].[dbo].[YP_XT_YF_KSFY] @Rkbm,@Ckbm,@Pdh,@Ckczy,@Crkfsdm,@XT_YP_LS_NBFYMX,@usercode,@organizeid,@type,@result out", param.ToArray());
            return outpar.Value.ToString();
        }

        /// <summary>
        /// 获取批量出库药品库存信息
        /// </summary>
        /// <param name="yfbmCode">当前部门</param>
        /// <param name="rkbm">入库部门</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        public IList<DrugStockInfoVEntity> GetDirectDeliveryDrugsList(Pagination pagination, string yfbmCode, string rkbm, string organizeid)
        {
            var param = new DbParameter[]
            {
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@rkbm", rkbm),
                new SqlParameter("@Organizeid", organizeid)
            };
            return QueryWithPage<DrugStockInfoVEntity>(TSqlStock.direct_delivery_batch_search, pagination, param);
        }

        /// <summary>
        /// 获取批量出库药品库存信息
        /// </summary>
        /// <param name="yfbmCode">当前部门</param>
        /// <param name="rkbm">入库部门</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        public IList<DrugStockInfoVEntity> GetDirectDeliveryDrugsList(string yfbmCode, string rkbm, string organizeid)
        {
            var param = new DbParameter[]
            {
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@rkbm", rkbm),
                new SqlParameter("@Organizeid", organizeid)
            };
            return FindList<DrugStockInfoVEntity>(TSqlStock.direct_delivery_batch_search, param);
        }

        /// <summary>
        /// 前天提交批量直接出库
        /// </summary>
        /// <param name="paraDto"></param>
        /// <returns></returns>
        public string DirectDeliveryBatchSubmit(DirectDeliveryBatchDTO paraDto)
        {
            var param = new DbParameter[]
            {
                new SqlParameter("@yfbmCode", paraDto.yfbmCode),
                new SqlParameter("@rkbm",  paraDto.rkbm),
                new SqlParameter("@djh",  paraDto.djh),
                new SqlParameter("@crkId", paraDto.crkId),
                new SqlParameter("@djlx", paraDto.djlx),
                new SqlParameter("@userCode", paraDto.userCode),
                new SqlParameter("@crkfs", paraDto.crkfs),
                new SqlParameter("@shzt", paraDto.shzt),
                new SqlParameter("@Organizeid", paraDto.Organizeid),
                new SqlParameter("@ypCodes", paraDto.ypCodes),
            };
            return FindList<string>(TSqlStock.direct_delivery_batch_submit, param).FirstOrDefault();
        }
    }
}
