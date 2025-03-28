using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.DomainServices
{
    public class PrepareMedicineDmnService : DmnServiceBase, IPrepareMedicineDmnService
    {
        public PrepareMedicineDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }


        #region 报损报溢
        /// <summary>
        /// 查询损益药品list
        /// </summary>
        /// <param name="inputCode">关键字</param>
        /// <returns></returns>
        public List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode)
        {
            var strSql = new StringBuilder(@"
SELECT * FROM (
select distinct top 100
    yp.ypCode,yp.ypmc,yp.ycmc sccj,ypsx.ypgg
    ,isnull(sum(isnull(kcxx.kcsl,0)) ,0) kcsl
    ,(select [NewtouchHIS_PDS].dbo.[f_getYfbmYpComplexYpSlandDw](isnull(sum(isnull(kcxx.kcsl,0)) ,0),@yfbmCode,yp.ypcode,@OrganizeId)) xykcstr
    ,yp.zxdw zxdw
    ,CONVERT(VARCHAR(10), isnull(max(kcxx.yxq),'1899-01-01'), 120) yxq
    ,LTRIM(RTRIM(kcxx.ph)) ph,LTRIM(RTRIM(kcxx.pc)) pc
    ,CONVERT(DECIMAL(12,4), [NewtouchHIS_PDS].dbo.f_getyfbmZhyz(yfbm.yfbmCode, yp.ypcode, @OrganizeId)) zhyz
	,[NewtouchHIS_PDS].dbo.f_getyfbmDw(yfbm.yfbmCode, yp.ypCode, @Organizeid) djdw
    ,CONVERT(DECIMAL(12,4),yp.lsj/yp.bzs) lsj
    ,CONVERT(DECIMAL(12,4),yp.pfj/yp.bzs) pfj
    ,CONVERT(DECIMAL(12,4),yp.lsj) Yklsj
    ,CONVERT(DECIMAL(12,4),yp.pfj) Ykpfj
	,CONVERT(DECIMAL(12,4),kcxx.jj/yp.bzs) jj
    ,sfdl.dlmc yplb
    ,case yp.mzzybz when '1' then '正常' when '0' then '停用' end ypzt
from [Newtouch_CIS].[dbo].xt_ksby_kcxx(NOLOCK) kcxx
INNER join NewtouchHIS_Base.dbo.xt_yp(NOLOCK) yp on kcxx.ypdm=yp.ypCode and yp.organizeId=@OrganizeId AND yp.zt='1'
INNER join NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx on ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId AND ypsx.zt='1'
INNER join NewtouchHIS_Base.dbo.V_S_xt_yfbm_yp yfbmyp on yfbmyp.yfbmCode=kcxx.yfbmCode and yfbmyp.dlCode=yp.dlcode and yfbmyp.organizeId=@OrganizeId 
INNER join NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm on yfbm.yfbmCode =yfbmyp.yfbmCode and yfbm.organizeId=@OrganizeId 
left join NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl on sfdl.dlCode=yp.dlCode and sfdl.organizeId=@OrganizeId 
where kcxx.yfbmCode =@yfbmCode 
AND kcxx.organizeId=@OrganizeId
and yp.ypCode in (select ypdm from [Newtouch_CIS].[dbo].zy_bqksbymx)
");

            if (!string.IsNullOrEmpty(inputCode))
            {
                strSql.AppendLine("and (yp.ypcode like @inputCode or yp.ypmc like @inputCode or yp.spm like @inputCode or yp.py like @inputCode)");
            }
            strSql.Append(@"
group by kcxx.jj, yp.ypcode,yp.ypmc,yp.ycmc,ypsx.ypgg,kcxx.yxq,kcxx.ph,kcxx.pc,yfbm.mzzybz,yfbm.yfbmCode,sfdl.dlmc,yp.mzzybz,yp.bzs,yp.mzcls,yp.zycls,yp.bzdw,yp.mzcldw,yp.zycldw,yp.lsj,yp.pfj,zxdw
) a
order by a.ypcode,a.kcsl,a.yxq
                         ");

            var yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            var param = new DbParameter[]
            {
                new SqlParameter("@inputCode","%"+(inputCode??"").Trim()+"%"),
                new SqlParameter ("@yfbmCode",yfbmCode),
                new SqlParameter("@OrganizeId",OrganizeId)
            };
            return FindList<ReportLossAndProfitMedicineInfoVO>(strSql.ToString(), param).ToList();
        }

        /// <summary>
        /// 查询药品的kcsl和jj
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="yxq"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        //public void SelectKcslAndJj(string ypCode, string ph, string pc, DateTime? yxq, out int kcsl, out decimal jj)
        //{
        //    SqlParameter[] param =
        //    {
        //        new SqlParameter("@ypCode",ypCode),
        //        new SqlParameter("@pc",pc),
        //        new SqlParameter("@yfbmCode",Constants.CurrentYfbm.yfbmCode),
        //        new SqlParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
        //    };
        //    var entity = FindList<StockQuantityAndPriceVO>("SELECT ISNULL(kcsl,0) kcsl,ISNULL(jj,0) jj FROM xt_yp_kcxx(nolock) WHERE Tybz='0' AND yfbmCode=@yfbmCode AND Ypdm=@ypCode AND pc= @pc", param).FirstOrDefault();
        //    kcsl = entity.kcsl;
        //    jj = entity.jj;
        //}


        /// <summary>
        /// 提交报损报益
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        public string SubmitReportLossAndProfit(SysMedicineProfitLossEntity syxx)
        {
            try
            {
                using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                {
                    var kcxxRepo = new PrepareMedicineStockInfoRepo(new DefaultDatabaseFactory());
                    var kcxx = kcxxRepo.SelectData(syxx.Ypdm, syxx.Ph, syxx.pc, syxx.yfbmCode, syxx.OrganizeId);
                    var curKcsl = kcxx.Sum(p => p.kcsl);
                    if (syxx.Sysl < 0)//报损  盘库存是否充足
                    {
                        if (kcxx.Sum(p => p.kcsl) + syxx.Sysl < 0)
                            return string.Format("代码为{0}、批号{1}、批次{2}的药品库存不足", syxx.Ypdm, syxx.Ph, syxx.pc);
                    }

                    kcxxRepo.UpdateKcslWithTrans(syxx.Sysl, syxx.pc, syxx.Ph, syxx.Ypdm, syxx.yfbmCode, syxx.OrganizeId,
                        syxx.CreatorCode, db);

                    syxx.Sykc = curKcsl + syxx.Sysl;
                    db.Insert(syxx);

                    db.Commit();
                    return "";
                }
            }
            catch (Exception e)
            {
                return e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        //public void SaveReportLossAndProfit(SysMedicineProfitLossEntity entity, int kcsl, decimal jj)
        //{
        //    var yfbmCode = Constants.CurrentYfbm.yfbmCode;
        //    var organizeId = OperatorProvider.GetCurrent().OrganizeId;
        //    using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
        //    {
        //        //insert 损益表
        //        var sykc = kcsl + entity.Sysl;
        //        entity.Sykc = sykc; //损益库存
        //        entity.Jj = jj;  //进价
        //        db.Insert(entity);

        //        //update 库存
        //        var kcxxEntity = db.IQueryable<SysMedicineStockInfoEntity>().FirstOrDefault(a => a.zt == "1" && a.tybz == 0 && a.yfbmCode == yfbmCode && a.OrganizeId == organizeId && a.ypdm == entity.Ypdm && a.pc == (entity.pc ?? ""));
        //        kcxxEntity.kcsl = kcsl + entity.Sysl;
        //        kcxxEntity.ph = kcxxEntity.ph.Trim();
        //        kcxxEntity.Modify();
        //        db.Update(kcxxEntity);
        //        db.Commit();
        //    }
        //}


        /// <summary>
        /// 报损报溢 保存
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        //public string SaveReportLossAndProfit(List<YpSyxxVo> profitLossEntityList)
        //{
        //    var param = new DbParameter[]
        //    {
        //        new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
        //        new SqlParameter("@syxx", profitLossEntityList.ToDataTable()){
        //            SqlDbType = SqlDbType.Structured,
        //            TypeName = "dbo.yp_syxx"
        //        }
        //    };
        //    return FirstOrDefault<string>("EXEC [dbo].[sp_yp_bsby] @CreatorCode, @syxx", param);
        //}


        /// <summary>
        /// 报损报溢查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        public IList<LossAndProditInfoVO> SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk)
        {
            var strSql = new StringBuilder(@"
         SELECT DISTINCT syxx.syId, FLOOR(syxx.sykc/syxx.Zhyz) sykc, CASE WHEN syxx.sysl > 0 THEN '报溢' ELSE '报损' END AS syqk, yp.ypmc,yp.ypCode, sf.dlmc AS yplb
	    , yp.py, syxx.ph, convert(varchar(100), syxx.yxq, 111) AS yxq, syxx.bgsj 
		,[NewtouchHIS_PDS].dbo.f_getYfbmYpComplexYpSlandDw(syxx.sysl,syxx.yfbmCode,syxx.ypdm,syxx.OrganizeId) sysl
		,syxx.Zhyz
        , convert(decimal(12, 4),kcxx.jj) AS jj 
        , convert(decimal(12, 2),(kcxx.jj * ((CASE WHEN syxx.sysl > 0 THEN syxx.sysl ELSE -syxx.sysl END)/syxx.Zhyz))) AS jjze
		, convert(decimal(12, 4), yp.pfj) pfj
        , convert(decimal(12, 2), (yp.pfj  * ((CASE WHEN syxx.sysl > 0 THEN syxx.sysl ELSE -syxx.sysl END)/syxx.Zhyz))) AS pjze
		, convert(decimal(12, 4), yp.lsj) lsj
	    , convert(decimal(12, 2), (yp.lsj * ((CASE WHEN syxx.sysl > 0 THEN syxx.sysl ELSE -syxx.sysl END)/syxx.Zhyz))) AS ljze
		, ypsyyy.syyy, syxx.CreatorCode, syxx.zrr
	    , syxx.djh, ypsx.ypgg
        , yp.zxdw AS dw, yp.ycmc, staff.Name
        ,syxx.CreateTime
    FROM zy_ksby_syxx(NOLOCK) syxx
    left join (select kcxx.OrganizeId,kcxx.ypdm,max(yfbmCode) yfbmCode,max(kcxx.jj) jj,ph,zt
			  from xt_ksby_kcxx(NOLOCK)  as kcxx 
			  group by kcxx.OrganizeId,kcxx.ypdm,kcxx.yfbmCode,ph,zt) as kcxx on syxx.Ypdm = kcxx.ypdm and syxx.Ph = kcxx.ph and kcxx.yfbmCode = syxx.yfbmCode and kcxx.OrganizeId = @OrganizeId and kcxx.zt != '0'
    INNER JOIN NewtouchHIS_Base.dbo.xt_yp(NOLOCK) yp ON syxx.ypdm = yp.ypCode and yp.OrganizeId=@OrganizeId AND yp.zt='1'
    INNER JOIN NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.zt='1' AND ypsx.OrganizeId=yp.OrganizeId
    LEFT JOIN xt_ksbysyyy(NOLOCK) ypsyyy ON syxx.syyy = ypsyyy.syyyId
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON yp.dlCode = sfdl.dlCode and sfdl.OrganizeId=@OrganizeId
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Staff staff ON syxx.CreatorCode = staff.gh and staff.OrganizeId=@OrganizeId
    LEFT JOIN (SELECT DISTINCT yfbmCode, dlCode FROM NewtouchHIS_Base.dbo.V_S_xt_yfbm_yp where OrganizeId=@OrganizeId) yfbmyp ON yfbmyp.yfbmCode = syxx.yfbmCode AND yfbmyp.dlCode = yp.dlCode
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm ON yfbm.yfbmCode = yfbmyp.yfbmCode and yfbm.OrganizeId=@OrganizeId
    LEFT JOIN (SELECT dlCode, substring(dlmc, 0, len(dlmc)) AS dlmc FROM NewtouchHIS_Base.dbo.V_S_xt_sfdl WHERE OrganizeId=@OrganizeId) sf ON yp.dlCode = sf.dlCode
    WHERE syxx.yfbmCode = @yfbmCode
    and syxx.zt='1'
    AND syxx.OrganizeId=@OrganizeId
    AND (@syqk = -1 OR (@syqk = 1 AND syxx.sysl > 0) OR (@syqk = 2 AND syxx.sysl < 0))
                        ");
            var inSqlParameterList = new List<DbParameter>();
            if (!string.IsNullOrEmpty(startTime))
            {
                strSql.Append(@" AND syxx.bgsj >= @startTime ");
                inSqlParameterList.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                strSql.Append(@" AND syxx.bgsj <= @endTime ");
                inSqlParameterList.Add(new SqlParameter("@endTime", endTime));
            }
            if (!string.IsNullOrEmpty(syyy))
            {
                strSql.Append(@" AND (syxx.syyy = @syyy OR @syyy='-1')");
                inSqlParameterList.Add(new SqlParameter("@syyy", syyy));
            }
            if (!string.IsNullOrEmpty(inputCode))
            {
                strSql.Append(@" AND (yp.py LIKE @inputCode or yp.ypCode like @inputCode or yp.ypmc like @inputCode)");
                inSqlParameterList.Add(new SqlParameter("@inputCode", "%" + (inputCode ?? "") + "%"));
            }
            inSqlParameterList.Add(new SqlParameter("@syqk", syqk));
            inSqlParameterList.Add(new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));
            inSqlParameterList.Add(new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode));
            return QueryWithPage<LossAndProditInfoVO>(strSql.ToString(), pagination, inSqlParameterList.ToArray());

            }


        /// <summary>
        /// 批发价总金额、零售价总金额查询
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="syyy">损益原因</param>
        /// <param name="inputCode">关键字</param>
        /// <param name="syqk">损益情况</param>
        /// <returns></returns>
        public LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk)
        {
            var strSql = new StringBuilder(@"
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpyfbm_yp') and type='U')
BEGIN
	DROP TABLE #tmpyfbm_yp;
END
SELECT DISTINCT yfbmCode, dlCode INTO #tmpyfbm_yp FROM NewtouchHIS_Base.dbo.V_S_xt_yfbm_yp where OrganizeId=@OrganizeId

IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpyfbm') and type='U')
BEGIN
	DROP TABLE #tmpyfbm;
END
SELECT dlCode, substring(dlmc, 0, len(dlmc)) AS dlmc INTO #tmpyfbm FROM NewtouchHIS_Base.dbo.V_S_xt_sfdl WHERE OrganizeId=@OrganizeId

SELECT ISNULL(SUM(a.ljze), 0) Ljze, ISNULL(SUM(a.pjze), 0) Pjze, ISNULL(SUM(a.jjze), 0) Jjze 
FROM (
    SELECT convert(decimal(19, 2), syxx.sysl * syxx.lsj) AS ljze
    , convert(decimal(19, 2), syxx.sysl * syxx.pfj) AS pjze
    , convert(decimal(19, 2), syxx.jj * syxx.sysl) AS jjze
    FROM zy_ksby_syxx(NOLOCK) syxx
    LEFT JOIN xt_ksbysyyy ypsyyy(NOLOCK) ON syxx.syyy = ypsyyy.syyyId
    INNER JOIN NewtouchHIS_Base.dbo.xt_yp(NOLOCK) yp ON syxx.ypdm = yp.ypCode and yp.OrganizeId=syxx.OrganizeId AND yp.zt='1'
    INNER JOIN NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.zt='1' AND ypsx.OrganizeId=yp.OrganizeId 
    INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON yp.dlCode = sfdl.dlCode and sfdl.OrganizeId=syxx.OrganizeId
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Staff staff ON syxx.CreatorCode = staff.gh and staff.OrganizeId=@OrganizeId
    LEFT JOIN #tmpyfbm_yp yfbmyp ON yfbmyp.yfbmCode = syxx.yfbmCode AND yfbmyp.dlCode = yp.dlCode
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm ON yfbm.yfbmCode = yfbmyp.yfbmCode and yfbm.OrganizeId=@OrganizeId
    LEFT JOIN #tmpyfbm sf ON yp.dlCode = sf.dlCode
    WHERE syxx.yfbmCode = @yfbmCode
    AND syxx.OrganizeId=@OrganizeId
    AND (@syqk = -1 OR (@syqk = 1 AND syxx.sysl > 0) OR (@syqk = 2 AND syxx.sysl < 0))
                        ");
            var inSqlParameterList = new List<DbParameter>();
            if (!string.IsNullOrEmpty(startTime))
            {
                strSql.AppendLine(@"AND syxx.bgsj >= @startTime ");
                inSqlParameterList.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                strSql.AppendLine(@"AND syxx.bgsj <= @endTime ");
                inSqlParameterList.Add(new SqlParameter("@endTime", endTime));
            }
            if (!string.IsNullOrEmpty(syyy))
            {
                strSql.AppendLine(@"AND (syxx.syyy = @syyy OR @syyy='-1')");
                inSqlParameterList.Add(new SqlParameter("@syyy", syyy));
            }
            if (!string.IsNullOrEmpty(inputCode))
            {
                strSql.AppendLine(@"AND (yp.py LIKE @inputCode or yp.ypCode like @inputCode or yp.ypmc like @inputCode)");
                inSqlParameterList.Add(new SqlParameter("@inputCode", "%" + (inputCode ?? "") + "%"));
            }
            strSql.AppendLine(") a");
            inSqlParameterList.Add(new SqlParameter("@syqk", syqk));
            inSqlParameterList.Add(new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));
            inSqlParameterList.Add(new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode));
            return FindList<LossAndProditInfoJeVo>(strSql.ToString(), inSqlParameterList.ToArray()).FirstOrDefault();
        }

        #endregion


        #region 科室备药退回
        public string PrepareMedicineReturnSubmit(OperatorModel user, string orgId, BythDjInfoDTO Djnr)
        {
            try
            {

                if (Djnr.isdeldj != null && Djnr.isdeldj != "")
                {
                    string sql = "delete from zy_ksbyth where Id=@Id and OrganizeId=@orgId and zt='1' ";
                    SqlParameter[] para = {
                            new SqlParameter("@Id", Djnr.isdeldj),
                            new SqlParameter("@orgId", orgId) };
                    ExecuteSqlCommand(sql, para);
                    string sql2 = "delete from zy_ksbythmx where ById=@Id and OrganizeId=@orgId and zt='1' ";
                    SqlParameter[] para2 = {
                            new SqlParameter("@Id", Djnr.isdeldj),
                            new SqlParameter("@orgId", orgId) };
                    ExecuteSqlCommand(sql2, para2);
                }

                var shzt = "1";//保存
                DateTime? tjsj = null;
                if (Djnr.issavesubmit == "1")
                {
                    shzt = "2";//提交
                    tjsj = DateTime.Now.Date;
                }
                var request = new
                {
                    OrganizeId = orgId,
                    yhgh = user.rygh,
                    yplist = Djnr
                };
                using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                {
                    var ksbyid = Guid.NewGuid().ToString();
                    var byEntity = new PrapareMedicineReturnEntity
                    {
                        Id = ksbyid,
                        OrganizeId = orgId,
                        djh = Djnr.djh,
                        thzt = shzt,
                        yfbm = Djnr.yfbm,
                        ksbm = Djnr.ksbm,
                        bqbm = Djnr.rkbm,
                        tjsj = tjsj,
                        thyy = Djnr.thyy,
                        zt = "1",
                        CreatorCode = user.rygh,
                        CreateTime = DateTime.Now,
                        LastModifierCode = "",
                        LastModifyTime = null,
                    };
                    db.Insert(byEntity);
                    foreach (var byxx in Djnr.mx)
                    {
                        var bymxEntity = new PrepareMedicineReturnMXEntity
                        {
                            Id = Guid.NewGuid().ToString(),
                            byId = ksbyid,
                            OrganizeId = orgId,
                            ypdm = byxx.ypdm,
                            ypmc = byxx.ypmc,
                            yplb = byxx.yplb,
                            tsl = byxx.tsl,
                            dw = byxx.dw,
                            gg = byxx.gg,
                            yxq = byxx.yxq,
                            //yfbm=byxx.yfbm,
                            sccj = byxx.sccj,
                            ph = byxx.ph,
                            pc = byxx.pc,
                            //thyy=byxx.thyy,
                            zt = "1",
                            CreatorCode = user.rygh,
                            CreateTime = DateTime.Now,
                            LastModifierCode = "",
                            LastModifyTime = null,
                        };
                        db.Insert(bymxEntity);
                    };
                    db.Commit();
                }
                if (Djnr.issavesubmit == "1")
                {
                    var apires = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/Stock/PrepareMedicineReturn", request, autoAppendToken: false);
                    if (apires.code == APIRequestHelper.ResponseResultCode.SUCCESS && apires.msg == "")
                    {
                        return "保存并提交成功！";
                    }
                    else
                    {
                        return "调用药房接口失败，请联系开发人员！";
                    }
                }
                return "科室备药退回保存成功！";
            }
            catch (Exception ex)
            {
                return "" + ex.InnerException.ToString();
            }
        }

        public List<DrugStockInfoVEntity> GetDrugAndStock(string yfcode, string keyWord, string organizeid)
        {
            const string sql = @"
SELECT s.dlmc,s.ypmc,s.ypCode ypdm,s.ypgg gg,SUM(s.kykc) kykc, [NewtouchHIS_PDS].dbo.f_getComplexYpSlandDw(SUM(s.kykc),s.zhyz,s.bmdw,s.zxdw) slStr,s.bmdw dw,CONVERT(INT,s.zhyz) zhyz,CONVERT(INT,s.bzs) bzs
,s.bzdw,s.zxdw,s.pzwh,CONVERT(NUMERIC(11,4),s.zxdwlsj) zxdwlsj,CONVERT(NUMERIC(11,4),s.zxdwlsj*s.zhyz) lsj,CONVERT(NUMERIC(11,4),s.zxdwpfj*s.zhyz) pfj,s.ycmc sccj,s.yklsj,s.ykpfj
,(CONVERT(VARCHAR(11),CONVERT(NUMERIC(11,2),s.zxdwlsj*s.zhyz))+'元/'+s.bmdw) lsjdjdw,yxq,pc,ph
FROM 
(
	select 
sfdl.dlmc, yp.ypmc, a.Ypdm ypCode, ypsx.ypgg, a.kcsl kykc, [NewtouchHIS_PDS].dbo.f_getyfbmDw(@yfbmCode, a.ypdm, @Organizeid) bmdw
	, a.zhyz zhyz, yp.bzs, yp.bzdw, yp.zxdw, ISNULL(ypsx.pzwh,'') pzwh,yp.ycmc
	,yp.lsj/yp.bzs zxdwlsj,yp.pfj/yp.bzs zxdwpfj,yp.lsj yklsj,yp.pfj ykpfj,a.yxq,a.pc,a.ph
from [xt_ksby_kcxx] a	
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=a.ypdm AND yp.OrganizeId=a.OrganizeId 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode=yp.dlCode AND sfdl.OrganizeId=yp.OrganizeId AND sfdl.zt='1'
	WHERE a.yfbmCode=@yfbmCode
	AND a.zt='1'
	AND a.OrganizeId=@Organizeid
	AND (yp.ypCode LIKE '%'+@keyWord+'%' OR yp.ypmc LIKE '%'+@keyWord+'%' OR yp.py LIKE '%'+@keyWord+'%')
) s
GROUP BY s.dlmc,s.ypmc,s.ypCode,s.ypgg,s.bmdw,s.zhyz,s.bzs,s.bzdw,s.zxdw,s.pzwh,s.zxdwlsj,s.zxdwpfj,s.ycmc,s.yklsj,s.ykpfj,s.yxq,s.pc,s.ph
";
            var parms = new List<SqlParameter>
            {
                new SqlParameter("@yfbmCode", yfcode??""),
                new SqlParameter("@Organizeid", organizeid??""),
                new SqlParameter("@keyWord", keyWord??"")
            };
            return FindList<DrugStockInfoVEntity>(sql, parms.ToArray());
        }
        public List<DrugStockInfoVEntity> GetApplyDrugAndStock(string ypcodestr,string yfbmstr, string organizeid)
        {
            const string sql = @"
SELECT s.dlmc,s.ypmc,s.ypCode ypdm,s.ypgg gg,SUM(s.kykc) kykc, [NewtouchHIS_PDS].dbo.f_getComplexYpSlandDw(SUM(s.kykc),s.zhyz,s.bmdw,s.zxdw) slStr,s.bmdw dw,CONVERT(INT,s.zhyz) zhyz,CONVERT(INT,s.bzs) bzs
,s.bzdw,s.zxdw,s.pzwh,CONVERT(NUMERIC(11,4),s.zxdwlsj) zxdwlsj,CONVERT(NUMERIC(11,4),s.zxdwlsj*s.zhyz) lsj,CONVERT(NUMERIC(11,4),s.zxdwpfj*s.zhyz) pfj,s.ycmc sccj,s.yklsj,s.ykpfj
,(CONVERT(VARCHAR(11),CONVERT(NUMERIC(11,2),s.zxdwlsj*s.zhyz))+'元/'+s.bmdw) lsjdjdw,yxq,pc,ph
FROM 
(
	select 
sfdl.dlmc, yp.ypmc, a.Ypdm ypCode, ypsx.ypgg, a.kcsl kykc, [NewtouchHIS_PDS].dbo.f_getyfbmDw(@yfbmCode, a.ypdm, @Organizeid) bmdw
	, a.zhyz zhyz, yp.bzs, yp.bzdw, yp.zxdw, ISNULL(ypsx.pzwh,'') pzwh,yp.ycmc
	,yp.lsj/yp.bzs zxdwlsj,yp.pfj/yp.bzs zxdwpfj,yp.lsj yklsj,yp.pfj ykpfj,a.yxq,a.pc,a.ph
from [xt_ksby_kcxx] a	
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=a.ypdm AND yp.OrganizeId=a.OrganizeId 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode=yp.dlCode AND sfdl.OrganizeId=yp.OrganizeId AND sfdl.zt='1'
	WHERE a.yfbmCode=@yfbmCode
	AND a.zt='1'
	AND a.OrganizeId=@Organizeid
    and yp.ypCode in (select * from f_split(@ypcodestr,','))
) s
GROUP BY s.dlmc,s.ypmc,s.ypCode,s.ypgg,s.bmdw,s.zhyz,s.bzs,s.bzdw,s.zxdw,s.pzwh,s.zxdwlsj,s.zxdwpfj,s.ycmc,s.yklsj,s.ykpfj,s.yxq,s.pc,s.ph
";
            var parms = new List<SqlParameter>
            {
                new SqlParameter("@yfbmCode", yfbmstr??""),
                new SqlParameter("@Organizeid", organizeid??""),
                new SqlParameter("@ypcodestr", ypcodestr??"")
            };
            return FindList<DrugStockInfoVEntity>(sql, parms.ToArray());
        }
        
        public IList<PrepareMedicineReturnVO> GetPrepareMedicineReturnGridJson(Pagination pagination, string thzt, DateTime kssj, DateTime jssj, string OrganizeId)
        {
            var sql = @"select * from (
select 
a.Id,a.thzt,a.djh,a.yfbm,e.yfbmmc yfmc,a.ksbm,a.bqbm, c.name ksmc,d.bqmc bqmc,
a.CreatorCode,a.CreateTime,a.tjsj,a.thyy
 from zy_ksbyth a
left join zy_ksbythmx b on a.id=b.byId and a.organizeId=b.organizeId and a.zt=b.zt
left join [NewtouchHIS_Base].[dbo].[Sys_Department] c on a.ksbm=c.code and a.organizeId=c.organizeId and a.zt=c.zt 
left join [NewtouchHIS_Base].[dbo].[xt_bq] d on  a.bqbm=d.bqCode and a.organizeId=d.organizeId and a.zt=d.zt
left join [NewtouchHIS_Base].[dbo].[xt_yfbm] e on a.yfbm=e.yfbmCode  and a.organizeId=e.organizeId and a.zt=e.zt
where a.zt=1 and a.organizeId=@OrganizeId
and a.CreateTime BETWEEN @kssj  AND  @jssj+' 23:59:59' "
;

            if (!(string.IsNullOrEmpty(thzt) || thzt == "0"))
            {
                sql += (" AND a.thzt=@thzt ");
            }


            sql += (@" )t group by id,thzt,djh,yfbm,yfmc,ksbm,bqbm,  ksmc,bqmc,CreatorCode,CreateTime,tjsj,thyy ");
            var parms = new List<SqlParameter>
            {
                new SqlParameter("@OrganizeId", OrganizeId),
                new SqlParameter("@thzt", thzt),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
        };

            return QueryWithPage<PrepareMedicineReturnVO>(sql, pagination, parms.ToArray(), false);
        }

        public List<PrepareMedicineReturnMXEntity> QueryPrepareMedicine(string djId, string orgId)
        {
            var strSql = new StringBuilder(@"
                select a.* from zy_ksbythmx a
                where a.zt = '1'
                and a.OrganizeId=@orgId
                and a.byId=@Id
                ");

            var param = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@Id", djId),
            };

            return this.FindList<PrepareMedicineReturnMXEntity>(strSql.ToString(), param.ToArray());

        }

        /// <summary>
        /// 提交备药申请单
        /// </summary>
        /// <param name="user"></param>
        /// <param name="orgId"></param>
        /// <param name="Djnr"></param>
        /// <returns></returns>
        public string UpdatePrepareMedicineReturn(PrapareMedicineReturnEntity entity)
        {
            try
            {
                using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                {
                    var ksbyid = Guid.NewGuid().ToString();
                    var dbEntity = entity;
                    //var byEntity = new PrapareMedicineReturnEntity
                    //{
                    //    Id = ksbyid,
                    //    OrganizeId = entity.OrganizeId,
                    //    djh = Djnr.djh,
                    //    thzt = shzt,
                    //    yfbm = Djnr.yfbm,
                    //    ksbm = "",
                    //    bqbm = "",
                    //    tjsj = tjsj,
                    //    thyy = Djnr.thyy,
                    //    zt = "1",
                    //    CreatorCode = user.rygh,
                    //    CreateTime = DateTime.Now,
                    //    LastModifierCode = "",
                    //    LastModifyTime = null,
                    //};
                    db.Update(dbEntity);

                    db.Commit();
                }
                return "成功";
            }
            catch (Exception ex)
            {
                return "" + ex.InnerException.ToString();
            }
        }
        public List<PrepareMedicineReturnVO> QueryPrepareMedicineReturnbyId(string djId, string orgId)
        {
            var strSql = new StringBuilder(@"
select a.*,b.yfbmmc yfmc,c.bqmc,d.name ksmc from zy_ksbyth a
left join [NewtouchHIS_Base].[dbo].[xt_yfbm] b on a.yfbm=b.yfbmCode and a.OrganizeId =b.OrganizeId
left join NewtouchHIS_Base.dbo.xt_bq  c on a.bqbm=c.bqCode and a.OrganizeId =c.OrganizeId
left join NewtouchHIS_Base.dbo.sys_department d on a.ksbm=d.code and a.OrganizeId =d.OrganizeId
  where a.zt = '1'
  and a.OrganizeId=@orgId
  and a.Id=@Id
");
            var param = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@Id", djId),
            };

            return this.FindList<PrepareMedicineReturnVO>(strSql.ToString(), param.ToArray());

        }

        public List<PrepareMedicineReturnMXEntity> QueryPrepareMedicineReturnMXbyId(string djId, string orgId)
        {
            var strSql = new StringBuilder(@"
select a.* from zy_ksbythmx a
  where a.zt = '1'
  and a.OrganizeId=@orgId
  and a.byId=@Id
");


            var param = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@Id", djId),
            };

            return this.FindList<PrepareMedicineReturnMXEntity>(strSql.ToString(), param.ToArray());
        }

        /// <summary>
        /// 获取药品库存数量
        /// </summary>
        /// <param name="ypbm"></param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="yfbm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string BydjQueryKykc(string ypbm, string pc, string ph, string yfbm, string orgId)
        {
            try
            {

                string sql = @"select convert(varchar(50),sum((kcxx.kcsl-kcxx.djsl))) kykc from xt_ksby_kcxx (NOLOCK) kcxx 
where ypdm=@ypbm and ph=@ph and pc=@pc and OrganizeId=@orgId  and yfbmCode=@yfbm  ";
                SqlParameter[] para = {
                            new SqlParameter("@ypbm", ypbm),
                            new SqlParameter("@pc", pc),
                            new SqlParameter("@ph", ph),
                            new SqlParameter("@orgId", orgId),
                            new SqlParameter("@yfbm", yfbm) };
                return this.FirstOrDefault<string>(sql.ToString(), para.ToArray());
            }
            catch (Exception ex)
            {
                return "" + ex.InnerException.ToString();
            }
        }

        /// <summary>
        /// 科室备药退回 撤回
        /// </summary>
        /// <param name="Djh"></param>
        /// <param name="orgId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public string PrepareMedicineReturnback(string Djh, string orgId, OperatorModel user)
        {
            try
            {
                var request = new
                {
                    Djh = Djh,
                    OrganizeId = orgId,
                    yhgh = user.rygh,
                    shzt = "1",
                };
                var apires = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/Stock/WithdrawalPreparationReturn", request, autoAppendToken: false);
                if (apires.code == APIRequestHelper.ResponseResultCode.SUCCESS && apires.msg == "")
                {
                    string sql = "update zy_ksbyth set thzt='4' where djh=@djh and OrganizeId=@orgId and zt='1' ";
                    SqlParameter[] para = {
                            new SqlParameter("@djh", Djh),
                            new SqlParameter("@orgId", orgId)
                };
                    ExecuteSqlCommand(sql, para);
                    return "成功";
                }
                else
                {
                    return "调用药房接口失败，请联系开发人员！";
                }
            }
            catch (Exception ex)
            {
                return "" + ex.InnerException.ToString();
            }
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="ById"></param>
        /// <param name="orgId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public string PrepareMedicineReturndelete(string ById, string orgId, OperatorModel user)
        {
            try
            {

                string sql = "update zy_ksbyth set thzt='6' where Id=@ById and OrganizeId=@orgId and zt='1' ";
                SqlParameter[] para = {
                            new SqlParameter("@ById", ById),
                            new SqlParameter("@orgId", orgId) };
                ExecuteSqlCommand(sql, para);
                return "成功";
            }
            catch (Exception ex)
            {
                return "" + ex.InnerException.ToString();
            }
        }
        #endregion

        #region 药品使用查询
        public IList<MedicineInfoVO> GetMedicineInfoListV2(Pagination pagination, MedicineInfoParam param, string orgId)
        {
            if (string.IsNullOrWhiteSpace(pagination.sidx))
            {
                throw new Exception("pagination.sidx is required");
            }

            var strSql = new StringBuilder(@"
 SELECT a.*,b.name ksmc,c.bqmc,d.yfbmmc,e.ypmc FROM [Newtouch_CIS].dbo.[xt_ksby_kcxx] a
 left join [NewtouchHIS_Base].[dbo].[Sys_Department] b on a.ksbm=b.code and a.organizeId=b.organizeId and b.zt=1
 left join [NewtouchHIS_Base].[dbo].[xt_bq] c on a.bqbm=c.bqcode and a.organizeId=c.organizeId and c.zt=1
 left join [NewtouchHIS_Base].[dbo].[xt_yfbm] d on a.yfbmCode =d.yfbmCode and a.organizeId=d.organizeId  and d.zt=1
 left join [NewtouchHIS_Base].[dbo].[xt_yp] e on a.ypdm=e.ypCode and a.organizeId=e.organizeId and e.zt=1

		WHERE a.zt=1
        AND a.OrganizeId=@OrganizeId 
        AND ( e.ypCode LIKE '%' + @ypmc+ '%'
                OR e.ypmc LIKE '%' + @ypmc + '%'
                OR e.spm LIKE '%' + @ypmc + '%'
                OR e.py LIKE '%' + @ypmc + '%'
            )
");
            var paraList = new List<DbParameter>
            {
                new SqlParameter("@Ypmc", param.ypdm ?? ""),
                new SqlParameter("@OrganizeId", orgId),
            };
            return QueryWithPage<MedicineInfoVO>(strSql.ToString(), pagination, paraList.ToArray());
        }

        #endregion

    }
}
