using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.VO;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Common;
using Newtouch.Infrastructure.TSQL;
using Newtouch.Tools;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 药房库存
    /// </summary>
    public class PharmacyDrugStorageDmnService : DmnServiceBase, IPharmacyDrugStorageDmnService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="databaseFactory"></param>
        public PharmacyDrugStorageDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }

        /// <summary>
        /// 库存量查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="tybz">停用标志</param>
        /// <param name="zt"></param>
        /// <param name="noShow0Kc"></param>
        /// <returns></returns>
        public IList<MedicineStockQueryVO> SelectStockTotal(Pagination pagination, string inputCode, string tybz, string zt, bool noShow0Kc)
        {
            if (string.IsNullOrWhiteSpace(pagination.sidx))
            {
                throw new Exception("pagination.sidx is required");
            }
            var sql = new StringBuilder(@"
SELECT  yp.ypmc ,yp.ypCode,
	ypsx.ypgg Gg ,yfbm.yfbmmc deptName,kcxx.yfbmCode,bmypxx.Zcxh,yp.ycmc ,
	ISNULL(CONVERT(DECIMAL(12,4),yp.pfj),0) Ykpfj ,
	ISNULL(CONVERT(DECIMAL(12,4),yp.lsj),0) Yklsj ,
	CONVERT(DECIMAL(12,4),(yp.pfj/yp.bzs) * ISNULL(kcxx.zhyz,0)) pfj ,
	CONVERT(DECIMAL(12,4),(yp.lsj/yp.bzs) * ISNULL(kcxx.zhyz,0)) lsj ,
	dbo.[f_getYfbmYpComplexYpSlandDw](SUM(isnull(kcxx.Kcsl, 0)),@yfbmCode,yp.ypcode,@OrganizeId) Kcslstr ,
	CONVERT(DECIMAL(12,2),SUM(ISNULL(kcxx.Kcsl, 0) * (yp.pfj/yp.bzs))) Pjze,
	CONVERT(DECIMAL(12,2),SUM(ISNULL(kcxx.Kcsl, 0) * (yp.lsj/yp.bzs))) Ljze ,
	yp.py py       
FROM NewtouchHIS_Base.dbo.V_S_xt_yp yp 
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON yp.ypId = ypsx.ypId and ypsx.OrganizeId = yp.OrganizeId
	INNER JOIN xt_yp_bmypxx(NOLOCK) bmypxx ON bmypxx.Ypdm = yp.ypCode AND bmypxx.OrganizeId = yp.OrganizeId
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm ON  yfbm.yfbmCode= bmypxx.yfbmCode and yfbm.OrganizeId = yp.OrganizeId AND yfbm.zt='1'
	LEFT JOIN xt_yp_kcxx (NOLOCK) kcxx ON kcxx.Ypdm = yp.ypCode and kcxx.OrganizeId = yp.OrganizeId AND kcxx.yfbmCode= bmypxx.yfbmCode AND ( kcxx.zt = @kczt OR @kczt = '')
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode = yp.dlCode AND sfdl.OrganizeId=kcxx.OrganizeId AND sfdl.zt='1'
WHERE yp.OrganizeId =@OrganizeId
	AND bmypxx.yfbmCode=@yfbmCode
	AND ( bmypxx.zt = @bmypxxzt OR @bmypxxzt = '' )
");
            var paraList = new List<DbParameter>
            {
                new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId),
                new SqlParameter("@kczt", tybz.Trim()),
                new SqlParameter("@bmypxxzt", zt.Trim())
            };
            if (!string.IsNullOrWhiteSpace(inputCode))
            {
                sql.AppendLine("	AND ( yp.py LIKE '%'+@inputCode+'%' or yp.ypmc LIKE '%'+@inputCode+'%' or yp.ypCode LIKE '%'+@inputCode+'%')");
                paraList.Add(new SqlParameter("@inputCode", inputCode.Trim()));
            }
            if (noShow0Kc)
            {
                sql.AppendLine("	AND ( kcxx.Kcsl >0)  --1:不显示零库存 0:显示零库存");
            }
            sql.AppendLine("	GROUP BY yp.ypmc, yp.ypCode, ypsx.ypgg ,yfbm.yfbmmc,kcxx.yfbmCode,bmypxx.Zcxh,yp.ycmc,yp.pfj,yp.lsj,yp.bzs,yp.py, kcxx.zhyz ");
            var list = QueryWithPage<MedicineStockQueryVO>(sql.ToString(), pagination, paraList.ToArray());
            return list;
        }

        /// <summary>
        /// 库存量查询明细
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        public IList<MedicineStockQueryDetailVO> SelectStockDetail(string ypCode, string yfbmCode)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"
SELECT yfbm.yfbmmc deptname, kcxx.pc, kcxx.ph, convert(varchar(100),kcxx.yxq, 111) as yxq, ISNULL(kcxx.ypkw, '') ypkw, kcxx.djsl,kcxx.zhyz,
case when kcxx.zt = 1 then '不控制' else '控制' end kzbz,
(select dbo.[f_getYfbmYpComplexYpSlandDw](kcxx.djsl,@YfbmCode,yp.ypcode,@OrganizeId)) djslstr, 
(select dbo.[f_getYfbmYpComplexYpSlandDw](kcxx.kcsl,@YfbmCode,yp.ypcode,@OrganizeId)) kcslstr,
convert(decimal(12,4),yp.pfj) ykpfj,
convert(decimal(12,4),yp.lsj) yklsj,
convert(decimal(12,2),(kcxx.kcsl * (yp.pfj / yp.bzs))) pjze,
convert(decimal(12,2),(kcxx.kcsl * (yp.lsj / yp.bzs))) ljze,
convert(decimal(12,4),kcxx.jj/yp.bzs*kcxx.zhyz) jj,
convert(decimal(12,4),kcxx.jj) ykjj,
convert(decimal(12,4),yp.pfj/yp.bzs*kcxx.zhyz) pfj,
convert(decimal(12,4),yp.lsj/yp.bzs*kcxx.zhyz) lsj
from xt_yp_kcxx(NOLOCK) kcxx
INNER JOIN newtouchhis_base.dbo.V_S_xt_yp(NOLOCK) yp on kcxx.ypdm = yp.ypcode AND yp.OrganizeId=kcxx.OrganizeId 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx(NOLOCK) ypsx ON yp.ypId = ypsx.ypId and ypsx.OrganizeId = kcxx.OrganizeId 
INNER JOIN dbo.xt_yp_bmypxx(NOLOCK) bmypxx ON bmypxx.yfbmCode = kcxx.yfbmCode AND bmypxx.Ypdm=yp.ypCode AND bmypxx.OrganizeId=kcxx.OrganizeId
INNER JOIN newtouchhis_base.dbo.V_S_xt_yfbm yfbm on yfbm.yfbmcode= bmypxx.yfbmcode AND yfbm.OrganizeId=kcxx.OrganizeId AND yfbm.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode = yp.dlCode AND sfdl.OrganizeId=kcxx.OrganizeId AND sfdl.zt='1'
where kcxx.yfbmcode=@yfbmCode 
AND yp.ypcode = @ypCode 
AND kcxx.OrganizeId=@OrganizeId
group by  kcxx.yxq,kcxx.ph, kcxx.pc, kcxx.ypkw, kcxx.zt, yfbm.yfbmmc, kcxx.djsl,kcxx.jj,yp.ypCode,yp.pfj,yp.bzs,yp.lsj,kcxx.kcsl, kcxx.zhyz
");
            var parms = new DbParameter[]
            {
                new SqlParameter("@ypCode", (ypCode??"").Trim()),
                new SqlParameter("@yfbmCode", (yfbmCode??"").Trim()),
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<MedicineStockQueryDetailVO>(strSql.ToString(), parms).ToList();

        }

        /// <summary>
        /// 单据查询 主信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="qsrj"></param>
        /// <param name="jsrj"></param>
        /// <param name="pdh"></param>
        /// <param name="fph"></param>
        /// <param name="djlx"></param>
        /// <param name="shzt"></param>
        /// <param name="allUseableDjlx"></param>
        /// <param name="orgId"></param>
        /// <param name="curYfbmCode"></param>
        /// <returns></returns>
        public IList<ReceiptQueryVO> SelectReceiptMainInfo(Pagination pagination, DateTime? qsrj, DateTime? jsrj, string pdh, string fph, int? djlx, string shzt, string allUseableDjlx, string orgId, string curYfbmCode)
        {
            var sb = new StringBuilder(@"
select crkdj.djlx, crkdj.shzt
,case crkdj.djlx when 1 then '药品入库' when 2 then '外部出库' when 3 then '直接出库' when 4 then '申领出库' when 5 then '内部发药退回' when 6 then '科室发药' else '' end djlxmc
,crkdj.crkId crkId, crkdj.Pdh pdh
,(case crkdj.djlx when 1 then '' else ckyfbm.yfbmmc end) ckbmmc
,(case crkdj.djlx when 1 then ckypgys.gysmc when 2 then rkypgys.gysmc else '' end) gysmc
,(case crkdj.djlx when 2 then '' else rkyfbm.yfbmmc end) rkbmmc
,crkdj.Czsj czsj, ypcrkfs.crkfsmc crkfsmc 
,convert(decimal(12,2),ISNULL(sum(crkmx.Pfj * crkmx.sl), 0)) pjze
,convert(decimal(12,2),ISNULL(sum(crkmx.Lsj * crkmx.sl), 0)) ljze
,convert(decimal(12,2),ISNULL(sum(crkmx.jj * crkmx.sl), 0)) zje
,convert(decimal(12,2),ISNULL(sum(crkmx.Lsj * crkmx.sl) - convert(decimal(12,2),sum(crkmx.jj * crkmx.sl)), 0)) jxcj
,crkdj.CreateTime,crkdj.Rksj,crkdj.Cksj
from xt_yp_crkdj(nolock) crkdj
INNER JOIN xt_yp_crkmx(NOLOCK) crkmx on crkmx.crkId = crkdj.crkId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=crkmx.Ypdm AND yp.OrganizeId=@Organizeid
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId
left join NewtouchHIS_Base.dbo.V_S_xt_ypgys(nolock) ckypgys on ckypgys.gysCode = crkdj.Ckbm and ckypgys.OrganizeId = crkdj.OrganizeId
left join NewtouchHIS_Base.dbo.V_S_xt_ypgys(nolock) rkypgys on rkypgys.gysCode = crkdj.Rkbm and rkypgys.OrganizeId = crkdj.OrganizeId
left join NewtouchHIS_Base.dbo.V_S_xt_yfbm(nolock) ckyfbm on ckyfbm.yfbmCode = crkdj.Ckbm and ckyfbm.OrganizeId = crkdj.OrganizeId
left join NewtouchHIS_Base.dbo.V_S_xt_yfbm(nolock) rkyfbm on rkyfbm.yfbmCode = crkdj.Rkbm and rkyfbm.OrganizeId = crkdj.OrganizeId
left join NewtouchHIS_Base..sys_department(nolock) depart on crkdj.Rkbm = depart.Code and depart.OrganizeId = crkdj.OrganizeId
left join NewtouchHIS_Base.dbo.V_S_xt_ypcrkfs(nolock) ypcrkfs on ypcrkfs.crkfsCode = crkdj.Crkfsdm
where crkdj.OrganizeId =@Organizeid ");
            sb.AppendLine("and ( ");
            sb.AppendLine(" (crkdj.djlx = " + (int)EnumDanJuLX.yaopinruku + " and ckypgys.gysCode is not null and rkyfbm.yfbmCode is not null) --药品入库 ");
            sb.AppendLine(" or(crkdj.djlx = " + (int)EnumDanJuLX.waibucuku + " and ckyfbm.yfbmCode=@YfbmCode)--外部出库 ");
            sb.AppendLine(" or(rkyfbm.yfbmCode is not null or ckyfbm.yfbmCode is not null)--药房 <->药库 ");
            sb.AppendLine(") ");
            sb.AppendLine(" and(crkdj.Rkbm = @YfbmCode or crkdj.Ckbm = @YfbmCode) ");
            var par = new List<SqlParameter>();
            if (qsrj.HasValue)
            {
                sb.Append(" and crkdj.Czsj >= @startTime");
                par.Add(new SqlParameter("@startTime", qsrj.Value));
            }
            if (jsrj.HasValue)
            {
                sb.Append(" and crkdj.Czsj < @endTime");
                par.Add(new SqlParameter("@endTime", jsrj.Value));
            }
            if (!string.IsNullOrWhiteSpace(pdh))
            {
                sb.Append(" and crkdj.Pdh = @pdh");
                par.Add(new SqlParameter("@pdh", pdh));
            }
            if (!string.IsNullOrWhiteSpace(fph))
            {
                sb.Append(" and crkmx.fph = @fph");
                par.Add(new SqlParameter("@fph", fph));
            }
            if (djlx.HasValue)
            {
                sb.Append(" and crkdj.djlx = @djlx");
                par.Add(new SqlParameter("@djlx", djlx));
            }
            if (!string.IsNullOrWhiteSpace(shzt))
            {
                //单据审核状态 0:未审核 1:已通过 2：未通过
                sb.Append(" and crkdj.shzt = @shzt");
                par.Add(new SqlParameter("@shzt", shzt));
            }
            if (!string.IsNullOrWhiteSpace(allUseableDjlx))
            {
                sb.AppendFormat(" and crkdj.djlx in ({0})", allUseableDjlx);
            }
            sb.Append(@"
group by crkdj.djlx, crkdj.shzt, crkdj.crkId , crkdj.Pdh ,crkdj.Czsj, ypcrkfs.crkfsmc, crkdj.CreateTime,crkdj.Rksj,crkdj.Cksj
,ckypgys.gysmc
,rkypgys.gysmc
,ckyfbm.yfbmmc
,rkyfbm.yfbmmc
,depart.Name");
            par.Add(new SqlParameter("@Organizeid", orgId));
            par.Add(new SqlParameter("@YfbmCode", curYfbmCode));
            return QueryWithPage<ReceiptQueryVO>(sb.ToString(), pagination, par.ToArray());
        }


        /// <summary>
        /// 单据查询 主信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IList<ReceiptQueryVO> SelectReceiptMainInfo(ReceiptQueryParam param)
        {
            var sb = new StringBuilder(@"
select crkdj.djlx, crkdj.shzt
,case crkdj.djlx 
    when 1 then  '药品入库' 
    when 2 then (case crkdj.Crkfsdm when '13' then '基药出库' when '14' then '报损出库' else '外部出库' end)
    when 3 then (case crkdj.Crkfsdm when '13' then '基药出库' when '14' then '报损出库' else '直接出库' end) 
    when 4 then (case crkdj.Crkfsdm when '13' then '基药出库' when '14' then '报损出库' else '申领出库' end)
    when 5 then (case crkdj.Crkfsdm when '13' then '基药出库' when '14' then '报损出库' else '内部发药退回' end)
    when 6 then (case crkdj.Crkfsdm when '13' then '基药出库' when '14' then '报损出库' else '科室发药' end) 
    when 14 then '申请调拨' 
    else '' end djlxmc
,crkdj.crkId crkId, crkdj.Pdh pdh
,(case crkdj.djlx when 1 then '' else ckyfbm.yfbmmc end) ckbmmc
,(case crkdj.djlx when 1 then ckypgys.gysmc when 2 then rkypgys.gysmc else '' end) gysmc
,(case crkdj.djlx when 2 then '' else rkyfbm.yfbmmc end) rkbmmc, crkdj.Rkbm rkbm
,crkdj.Czsj czsj, ypcrkfs.crkfsmc crkfsmc,ypcrkfs.crkfsCode crkfscode
,convert(decimal(12,2),ISNULL(sum(crkmx.Pfj * crkmx.sl), 0)) pjze
,convert(decimal(12,2),ISNULL(sum(crkmx.Lsj * crkmx.sl), 0)) ljze
,convert(decimal(12,2),ISNULL(sum(crkmx.Zje), 0)) zje
,convert(decimal(12,2),ISNULL(sum(crkmx.Lsj * crkmx.sl) - convert(decimal(12,2),sum(crkmx.jj * crkmx.sl)), 0)) jxcj
,crkdj.CreateTime,crkdj.Rksj,crkdj.Cksj
from xt_yp_crkdj(nolock) crkdj
INNER JOIN xt_yp_crkmx(NOLOCK) crkmx on crkmx.crkId = crkdj.crkId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=crkmx.Ypdm AND yp.OrganizeId=@Organizeid
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId
left join NewtouchHIS_Base.dbo.V_S_xt_ypgys(nolock) ckypgys on ckypgys.gysCode = crkdj.Ckbm and ckypgys.OrganizeId = crkdj.OrganizeId
left join NewtouchHIS_Base.dbo.V_S_xt_ypgys(nolock) rkypgys on rkypgys.gysCode = crkdj.Rkbm and rkypgys.OrganizeId = crkdj.OrganizeId
left join NewtouchHIS_Base.dbo.V_S_xt_yfbm(nolock) ckyfbm on ckyfbm.yfbmCode = crkdj.Ckbm and ckyfbm.OrganizeId = crkdj.OrganizeId
left join NewtouchHIS_Base.dbo.V_S_xt_yfbm(nolock) rkyfbm on rkyfbm.yfbmCode = crkdj.Rkbm and rkyfbm.OrganizeId = crkdj.OrganizeId
left join NewtouchHIS_Base..sys_department(nolock) depart on crkdj.Rkbm = depart.Code and depart.OrganizeId = crkdj.OrganizeId
left join NewtouchHIS_Base.dbo.V_S_xt_ypcrkfs(nolock) ypcrkfs on ypcrkfs.crkfsCode = crkdj.Crkfsdm
where crkdj.OrganizeId =@Organizeid  and crkdj.zt='1' 
");
            sb.AppendLine("and ( ");
            sb.AppendLine("( crkdj.djlx = " + (int)EnumDanJuLX.yaopinruku + " and ckypgys.gysCode is not null and rkyfbm.yfbmCode is not null)  --药品入库 ");
            sb.AppendLine(" or (crkdj.djlx = " + (int)EnumDanJuLX.waibucuku + " and ckyfbm.yfbmCode=@YfbmCode)  --外部出库 ");
            sb.AppendLine(" or (rkyfbm.yfbmCode is not null or ckyfbm.yfbmCode is not null)  --药房 <->药库 ");
            sb.AppendLine(") ");
            //sb.AppendLine($" and ( crkdj.Ckbm = @YfbmCode {(param.IsApproval ? "": " or crkdj.Rkbm = @YfbmCode ")}) ");
            var par = new List<SqlParameter>();
            if (param.qsrj.HasValue)
            {
                sb.AppendLine(" and crkdj.Czsj BETWEEN @startTime and @endTime");
                par.Add(new SqlParameter("@startTime", param.qsrj == null ? DateTime.Now.AddDays(-1) : param.qsrj));
                par.Add(new SqlParameter("@endTime", param.jsrj == null ? DateTime.Now : param.jsrj));
            }
            if (!string.IsNullOrWhiteSpace(param.pdh))
            {
                sb.AppendLine(" and crkdj.Pdh like '%'+@pdh+'%' ");
                par.Add(new SqlParameter("@pdh", param.pdh));
            }
            if (!string.IsNullOrWhiteSpace(param.fph))
            {
                sb.AppendLine(" and crkmx.fph like '%'+@fph+'%' ");
                par.Add(new SqlParameter("@fph", param.fph));
            }
            if (param.djlx.HasValue)
            {
                if (param.djlx == 13)
                {
                    sb.AppendLine(" and ypcrkfs.crkfsCode=@djlx");
                }
                else
                {
                    sb.AppendLine(" and crkdj.djlx = @djlx ");
                }

                par.Add(new SqlParameter("@djlx", param.djlx));
            }
            if (!string.IsNullOrWhiteSpace(param.shzt))
            {
                //单据审核状态 0:未审核 1:已通过 2：未通过
                sb.AppendLine(" and crkdj.shzt = @shzt ");
                par.Add(new SqlParameter("@shzt", param.shzt));
            }
            if (param.alldjlx != null && param.alldjlx.Length > 0)
            {
                sb.AppendLine(string.Format(" AND crkdj.djlx IN ({0}) ", string.Join(",", param.alldjlx)));
            }
            sb.AppendLine(FilterGys(param.gys, param.djlx));
            sb.AppendLine(@"group by crkdj.djlx, crkdj.shzt, crkdj.crkId , crkdj.Pdh ,crkdj.Czsj, ypcrkfs.crkfsmc,ypcrkfs.crkfsCode,crkdj.Crkfsdm, crkdj.CreateTime,ckypgys.gysmc,rkypgys.gysmc,ckyfbm.yfbmmc,rkyfbm.yfbmmc,depart.Name,crkdj.Rksj,crkdj.Cksj,crkdj.Rkbm");
            par.Add(new SqlParameter("@Organizeid", param.orgId));
            par.Add(new SqlParameter("@YfbmCode", param.curYfbmCode));
            var cc = new StringBuilder(@"");
            cc.AppendLine("select aa.*,sum(b.zje) jjzje from (" + sb.ToString() + ") aa,(" + sb.ToString() + ") b  group by aa.jxcj,aa.zje,aa.ljze,aa.pjze,aa.djlxmc,aa.djlx, aa.shzt, aa.crkId , aa.Pdh ,aa.Czsj, aa.crkfsmc,aa.crkfscode, aa.CreateTime,aa.gysmc,aa.ckbmmc,aa.rkbmmc,aa.Rksj,aa.Cksj,aa.rkbm");
            return QueryWithPage<ReceiptQueryVO>(cc.ToString(), param.pagination, par.ToArray());
        }

        /// <summary>
        /// 科室备药查询 主信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IList<DrupreparationVO> SelectDrupreparationInfo(SelectDrupreParam param)
        {
            var sb = new StringBuilder(@"
select ksby.Id,ksby.shzt,bq.bqmc,yfbm.yfbmmc,ksby.CreatorCode,
(select sum(convert(decimal(16,4),pfj)) from [dbo].[xt_bqksbymx] mx where mx.byId=ksby.Id) pfj,
(select sum(convert(decimal(16,4),lsj)) from [dbo].[xt_bqksbymx] mx where mx.byId=ksby.Id) lsj,
ksby.CreateTime
 from 
[dbo].[xt_bqksby] ksby
left join [NewtouchHIS_Base].[dbo].[V_S_xt_bq] bq
on ksby.bqbm=bq.bqCode and bq.OrganizeId=ksby.OrganizeId
left join [NewtouchHIS_Base].[dbo].[xt_yfbm] yfbm
on yfbm.yfbmCode=ksby.yfbm and yfbm.OrganizeId=ksby.OrganizeId
where ksby.OrganizeId=@Organizeid
");
            var par = new List<SqlParameter>();
            if (param.qsrj.HasValue)
            {
                sb.AppendLine(" and CONVERT(datetime,convert(varchar,convert(datetime2,convert(varchar,ksby.CreateTime,120)),120)) BETWEEN @startTime and @endTime");
                par.Add(new SqlParameter("@startTime", param.qsrj == null ? DateTime.Now.AddDays(-1) : param.qsrj));
                par.Add(new SqlParameter("@endTime", param.jsrj == null ? DateTime.Now : param.jsrj));
            }
            if (param.shzt != null && param.shzt != "")
            {
                sb.AppendLine(" and ksby.shzt=@shzt ");
                par.Add(new SqlParameter("@shzt", param.shzt));
            }
            par.Add(new SqlParameter("@Organizeid", param.orgId));
            return QueryWithPage<DrupreparationVO>(sb.ToString(), param.pagination, par.ToArray());
        }
        /// <summary>
        /// 过去供应商
        /// </summary>
        /// <param name="gys"></param>
        /// <param name="djlx"></param>
        /// <returns></returns>
        private string FilterGys(string gys, int? djlx)
        {
            if (!string.IsNullOrWhiteSpace(gys))
            {
                var tmp = new List<string>();
                var gysArr = gys.Split(',');
                foreach (var p in gysArr)
                {
                    var t = p.FilterSql();
                    if (!string.IsNullOrWhiteSpace(p))
                    {
                        tmp.Add('\'' + t + '\'');
                    }
                }
                if (tmp.Count > 0)
                {
                    switch (djlx)
                    {
                        case (int)EnumDanJuLX.waibucuku:
                            return string.Format(" AND rkypgys.gysCode IN ({0})", string.Join(",", tmp));
                        case (int)EnumDanJuLX.yaopinruku:
                            return string.Format(" AND ckypgys.gysCode IN ({0})", string.Join(",", tmp));
                        case null:
                        case 0:
                            return string.Format(" AND (rkypgys.gysCode IN ({0}) OR ckypgys.gysCode IN ({0}))", string.Join(",", tmp));
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 单据查询明细
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<ReceiptQueryDetailVO> SelectReceipDetailInfo(string crkId, int djlx, string orgId)
        {
            var sb = new StringBuilder(@"
select crkmxId,ISNULL(RTRIM(LTRIM(Fph)),'') Fph,yp.ypmc,ypsfdl.dlmc yplbmc,ph, pc,ypsx.ypgg gg,yp.bzdw dw --药房、药库之间进销存都用包装单位
,yp.ycmc sccj,yp.py,crkmx.kl,crkmx.Thyy thyy
,crkmx.sl --药房、药库之间进销存都用包装单位数量 ");
            if (new[] { (int)EnumDanJuLX.yaopinruku }.Contains(djlx))
            {
                sb.AppendLine(@"
,dbo.f_getComplexYpSlandDw(crkmx.sl*crkmx.Rkzhyz, yp.bzs, yp.bzdw, yp.zxdw) slanddw 
,(CASE WHEN crkmx.Rkzhyz<=0 THEN '' ELSE (CONVERT(VARCHAR(15),CONVERT(NUMERIC(11,2),crkmx.jj/crkmx.Rkzhyz*yp.bzs))+'元/'+yp.bzdw) END) jjdwdj ");
            }
            else
            {
                sb.AppendLine(@"
,dbo.f_getComplexYpSlandDw(crkmx.sl*crkmx.Ckzhyz, yp.bzs, yp.bzdw, yp.zxdw) slanddw 
,(CASE WHEN crkmx.Ckzhyz<=0 THEN '' ELSE (CONVERT(VARCHAR(15),CONVERT(NUMERIC(11,2),crkmx.jj/crkmx.Ckzhyz*yp.bzs))+'元/'+yp.bzdw) END) jjdwdj ");
            }
            sb.AppendLine(@"
,CONVERT(DECIMAL(12, 2), crkmx.pfj * crkmx.sl ) pjze
,CONVERT(DECIMAL(12, 2), crkmx.lsj * crkmx.sl ) ljze
,ISNULL(crkmx.jj, 0) jj
,CONVERT(DECIMAL(12, 2),ISNULL(crkmx.jj * crkmx.sl, 0))  zje
,CONVERT(DECIMAL(12, 2),(crkmx.lsj * crkmx.sl - ISNULL(crkmx.jj * crkmx.sl, 0))) jxcj
from xt_yp_crkmx(nolock) crkmx
INNER JOIN xt_yp_crkdj(nolock) crkdj on crkmx.crkId = crkdj.crkId AND crkdj.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp(nolock) yp on yp.ypCode = crkmx.Ypdm and yp.OrganizeId = crkdj.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx(nolock) ypsx on ypsx.ypId = yp.ypId and ypsx.OrganizeId = crkdj.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl(nolock) ypsfdl on ypsfdl.dlCode = yp.dlCode and ypsfdl.OrganizeId = crkdj.OrganizeId ");
            if (new[] { (int)EnumDanJuLX.yaopinruku, (int)EnumDanJuLX.neibufayaotuihui }.Contains(djlx))
            {
                ////用入库部门 取账册号
                //sb.Append(@" left join xt_yp_bmypxx(nolock) bmypxx on bmypxx.Ypdm = crkmx.Ypdm and bmypxx.yfbmCode = crkdj.rkbm and bmypxx.OrganizeId = crkdj.OrganizeId ");

                //用入库部门 取计算转换因子
                sb.Append(@"
left join NewtouchHIS_Base.dbo.V_S_xt_yfbm(nolock) zhyzYfbm on zhyzYfbm.yfbmCode = crkdj.rkbm and zhyzYfbm.OrganizeId = crkdj.OrganizeId");
            }
            else
            {
                ////用出库部门 取账册号
                //sb.Append(@" left join xt_yp_bmypxx(nolock) bmypxx on bmypxx.Ypdm = crkmx.Ypdm and bmypxx.yfbmCode = crkdj.ckbm and bmypxx.OrganizeId = crkdj.OrganizeId");

                //用出库部门 取计算转换因子
                sb.Append(@"
left join NewtouchHIS_Base.dbo.V_S_xt_yfbm(nolock) zhyzYfbm on zhyzYfbm.yfbmCode = crkdj.ckbm and zhyzYfbm.OrganizeId = crkdj.OrganizeId");
            }

            sb.Append(@"
where crkmx.crkId = @crkId
");
            return FindList<ReceiptQueryDetailVO>(sb.ToString(), new DbParameter[] {
                new SqlParameter("@crkId",crkId),
                new SqlParameter("@djlx",djlx),
                new SqlParameter("@Organizeid",orgId)
            });
        }

        #region 库存盘点

        /// <summary>
        /// 获取盘点日期 top 500
        /// </summary>
        /// <returns></returns>
        public List<InventoryDateDropDownVO> GetPdDateDropdownList()
        {
            const string strSql = @"
SELECT pdId, pdsj FROM (
    SELECT TOP 500 pdxx.pdId,Convert(Varchar(20),pdxx.kssj,120) + '=>' + Isnull(Convert(Varchar(20),pdxx.jssj,120),' ') pdsj, pdxx.CreateTime
    FROM xt_yp_pdxx(NOLOCK) pdxx
    INNER JOIN dbo.xt_yp_pdxxmx(NOLOCK) pdmx ON pdmx.pdId=pdxx.pdId
    WHERE pdxx.yfbmCode =@yfbmCode 
    and pdxx.OrganizeId=@OrganizeId
    GROUP BY pdxx.pdId, pdxx.Kssj, pdxx.Jssj, pdxx.CreateTime
	ORDER BY pdxx.CreateTime DESC
) a
ORDER BY a.CreateTime DESC ";
            var param = new DbParameter[]
            {
                new SqlParameter("@yfbmCode",Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<InventoryDateDropDownVO>(strSql, param).ToList();
        }

        /// <summary>
        /// 获取未完成的盘点时间
        /// </summary>
        /// <returns></returns>
        public List<InventoryDateDropDownVO> GetHangUpPdDates()
        {
            var strSql = new StringBuilder(@"
SELECT pdId, pdsj FROM (
    SELECT pdxx.pdId,Convert(Varchar(20),pdxx.kssj,120) + '=>' + Isnull(Convert(Varchar(20),pdxx.jssj,120),' ') pdsj, pdxx.CreateTime
    FROM xt_yp_pdxx(NOLOCK) pdxx
    INNER JOIN dbo.xt_yp_pdxxmx(NOLOCK) pdmx ON pdmx.pdId=pdxx.pdId
    WHERE pdxx.yfbmCode =@yfbmCode 
    AND pdxx.OrganizeId=@OrganizeId
    AND pdxx.Jssj IS NULL 
    GROUP BY pdxx.pdId, pdxx.Kssj, pdxx.Jssj, pdxx.CreateTime
) a ");
            var param = new DbParameter[]
            {
                new SqlParameter("@yfbmCode",Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<InventoryDateDropDownVO>(strSql.ToString(), param).ToList();
        }

        /// <summary>
        /// 获取药品类别（盘点）
        /// </summary>
        /// <returns></returns>
        public List<MedicineCategoryVO> GetMedicineCategoryList()
        {
            var strSql = new StringBuilder(@"
SELECT DISTINCT sfdl.dlCode, sfdl.dlmc 
FROM NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.dlCode=sfdl.dlCode AND yp.OrganizeId=sfdl.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId
INNER JOIN dbo.xt_yp_bmypxx(NOLOCK) bmypxx ON bmypxx.Ypdm=yp.ypCode AND bmypxx.OrganizeId=sfdl.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm ON yfbm.yfbmCode=bmypxx.yfbmCode AND yfbm.OrganizeId=sfdl.OrganizeId
WHERE sfdl.OrganizeId=@Organizeid
AND yfbm.yfbmCode=@YfbmCode
                        ");
            SqlParameter[] param = {
                new SqlParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId),
                new SqlParameter("@YfbmCode",Constants.CurrentYfbm.yfbmCode)
            };

            return FindList<MedicineCategoryVO>(strSql.ToString(), param).ToList();
        }

        /// <summary>
        /// 生成盘点信息
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string GenerateInventoryInfo(string yfbmCode, string organizeId, string userCode)
        {
            var paraList = new List<DbParameter>
            {
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@OrganizeId",organizeId ),
                new SqlParameter("@CreatorCode", userCode)
            };
            return FindList<string>(TSqlStock.generate_inventoryInfo, paraList.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// 转化页面显示数量  XX盒XX片
        /// </summary>
        /// <returns></returns>
        public string GetYfbmYpComplexYpSlandDw(int sl, string ypCode)
        {
            var strSql = new StringBuilder(@"
                select dbo.[f_getYfbmYpComplexYpSlandDw](@sl,@yfbmCode,@ypCode,@OrganizeId) as slstr
            ");
            DbParameter[] param =
                {
                   new SqlParameter("@sl",sl),
                   new SqlParameter("@yfbmCode",Constants.CurrentYfbm.yfbmCode),
                   new SqlParameter("@ypCode",ypCode),
                   new SqlParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
                };
            return FirstOrDefault<string>(strSql.ToString(), param);
        }

        /// <summary>
        /// 查询盘点药品信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="pdId"></param>
        /// <param name="pdsj"></param>
        /// <param name="srm"></param>
        /// <param name="ypzt"></param>
        /// <param name="yplb"></param>
        /// <param name="kcxs"></param>
        /// <returns></returns>
        public IList<InventoryInfoVO> SelectInventoryInfoList(Pagination pagination, string pdId, string pdsj, string srm, string ypzt, string yplb, int kcxs)
        {
            var strSql = new StringBuilder(@"
select  pdmxId,CreateTime,ypCode , ypmc , py ,a.ph ,a.pc ,yxq,bzdw,zxdw,llsl_zxbz ,llsl ,sjslstr ,pdscy,
 sjsl,deptSjsl,deptdw ,minSjsl,yksl ,pfj ,lsj ,ykpfj ,yklsj ,zhyz,CONVERT(NUMERIC(11,2),a.lsl*jj)llpfje,CONVERT(NUMERIC(11,2),a.lsl*Lsj)lllsje,CONVERT(NUMERIC(11,2),a.sjs*jj)sjpfje,CONVERT(NUMERIC(11,2),a.sjs*Lsj)sjlsje,pdlsjcy,pdpfjcy,pdsj,jj,ycmc,ypgg from  (
SELECT  pdxxmx.pdmxId ,
		pdxxmx.CreateTime,
		yp.ypCode ,
		yp.ypmc ,
		ypsx.ypgg ,
		yp.ycmc ,
		yp.py ,
		pdxxmx.ph ,
		pdxxmx.pc ,
		CONVERT(VARCHAR(10),pdxxmx.Yxq,120) yxq,
		yp.bzdw, 
		yp.zxdw,
		CONVERT(VARCHAR(10),pdxxmx.Llsl,50)+yp.zxdw llsl_zxbz ,--理论最小包装数量
		(select dbo.[f_getYfbmYpComplexYpSlandDw](pdxxmx.Llsl,@yfbmCode,yp.ypcode,@OrganizeId)) llsl ,--理论药房单位数量 
		(select dbo.[f_getYfbmYpComplexYpSlandDw](pdxxmx.Sjsl,@yfbmCode,yp.ypcode,@OrganizeId)) sjslstr ,--实际药房单位数量 
		(select dbo.[f_getYfbmYpComplexYpSlandDw]((pdxxmx.Sjsl-pdxxmx.Llsl),@yfbmCode,yp.ypcode,@OrganizeId)) pdscy,--盘点数差异
		pdxxmx.sjsl sjsl, --实际最小包装数量
		Floor(pdxxmx.sjsl / pdxxmx.zhyz) deptSjsl,
		dbo.f_getyfbmDw(yfbm.yfbmCode, yp.ypCode, yp.OrganizeId) deptdw ,
		(pdxxmx.sjsl % pdxxmx.zhyz) minSjsl,
		(pdxxmx.Sjsl - pdxxmx.Llsl) yksl ,
		pdxxmx.pfj ,
		pdxxmx.lsj ,
		pdxxmx.ykpfj ,
		pdxxmx.yklsj ,
		pdxxmx.zhyz 
        , CONVERT(NUMERIC(12,2),pdxxmx.Pfj*pdxxmx.Llsl/pdxxmx.Zhyz) llpfje
        , CONVERT(NUMERIC(12,2),pdxxmx.Lsj*pdxxmx.Llsl/pdxxmx.Zhyz) lllsje
        , CONVERT(NUMERIC(12,2),pdxxmx.Pfj*pdxxmx.Sjsl/pdxxmx.Zhyz) sjpfje
        , CONVERT(NUMERIC(12,2),pdxxmx.Lsj*pdxxmx.Sjsl/pdxxmx.Zhyz) sjlsje
		, CONVERT(NUMERIC(12,2),pdxxmx.Lsj*(pdxxmx.Sjsl-pdxxmx.Llsl)/pdxxmx.Zhyz) pdlsjcy --盘点零售价差异
		, CONVERT(NUMERIC(12,2),pdxxmx.Pfj*(pdxxmx.Sjsl-pdxxmx.Llsl)/pdxxmx.Zhyz) pdpfjcy --盘点进价差异
		,@pdsj pdsj,pdxxmx.Llsl lsl,pdxxmx.Sjsl sjs
FROM xt_yp_pdxxmx(NOLOCK) pdxxmx 
INNER JOIN dbo.xt_yp_pdxx(NOLOCK) pdxx ON pdxx.pdId=pdxxmx.pdId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp(NOLOCK) yp ON pdxxmx.Ypdm = yp.ypCode and yp.OrganizeId=pdxx.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=pdxx.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm on pdxx.yfbmCode=yfbm.yfbmCode AND yfbm.OrganizeId=pdxx.OrganizeId
WHERE pdxx.OrganizeId=@OrganizeId
AND pdxxmx.pdId = @pdId
AND yfbm.yfbmCode = @yfbmCode AND (pdxxmx.Sjsl>0  or pdxxmx.Llsl>0)
");
            if (!string.IsNullOrEmpty(srm))
            {
                strSql.AppendLine("And (yp.py LIKE @srm OR yp.Ypmc LIKE @srm) ");
            }
            if (!string.IsNullOrEmpty(ypzt))
            {
                strSql.AppendLine("AND (yp.zt = @ypzt OR @ypzt = '-1') ");
            }
            if (!string.IsNullOrEmpty(yplb))
            {
                strSql.AppendLine("AND (yp.dlCode = @yplb OR @yplb = '-1') ");
            }
            switch (kcxs)
            {
                case (int)EnumKCXS.Xslkc://显示零库存
                    strSql.AppendLine("AND (Llsl = 0 OR Sjsl = 0) ");
                    break;
                case (int)EnumKCXS.Bxsllslwl: //不显示理论数量为0
                    strSql.AppendLine("AND Llsl <> 0 ");
                    break;
                case (int)EnumKCXS.Bxssjslwl:  //不显示实际数量为0
                    strSql.AppendLine("AND Sjsl <> 0 ");
                    break;
                case (int)EnumKCXS.Bxslzdwl:  //不显示两者都为0
                    strSql.AppendLine("AND (Llsl <> 0 AND Sjsl<>0) ");
                    break;
            }

            //			strSql.AppendLine(@"  )a 
            //     left join(
            //    select pc, Ph, jj, Ypdm from
            //   xt_yp_crkdj dj
            //   left join xt_yp_crkmx a on dj.crkId = a.crkId and a.zt = 1
            //where dj.djlx = 1 and dj.OrganizeId = @OrganizeId and dj.zt = 1
            //)b on a.pc = b.pc and a.Ph = b.Ph and a.ypCode = Ypdm  ");

            strSql.AppendLine(@" )a 
    left join(
   select kcxx.OrganizeId,kcxx.ypdm,max(yfbmCode) yfbmCode,max(kcxx.jj) jj 
			  from NewtouchHIS_PDS.dbo.xt_yp_kcxx(NOLOCK)  as kcxx 
			  group by kcxx.OrganizeId,kcxx.ypdm,kcxx.yfbmCode
)b on b.yfbmCode=@yfbmCode and a.ypCode = b.ypdm and b.OrganizeId=@OrganizeId
");
            var inSqlParameterList = new List<DbParameter>
            {
                new SqlParameter("@pdId", pdId),
                new SqlParameter("@pdsj", pdsj),
                new SqlParameter("@srm", "%" + (srm ?? "") + "%"),
                new SqlParameter("@ypzt", (ypzt??"").Trim()),
                new SqlParameter("@yplb", (yplb??"").Trim()),
                new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
            };
            return QueryWithPage<InventoryInfoVO>(strSql.ToString(), pagination, inSqlParameterList.ToArray());
        }

        /// <summary>
        /// 查询盘点药品信息 单位独立
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="pdId"></param>
        /// <param name="pdsj"></param>
        /// <param name="srm"></param>
        /// <param name="ypzt"></param>
        /// <param name="yplb"></param>
        /// <param name="kcxs"></param>
        /// <returns></returns>
        public IList<InventoryInfoVO> SelectInventoryDetailIndependentUnit(Pagination pagination, string pdId, string pdsj, string srm, string ypzt, string yplb, int kcxs)
        {
            var strSql = new StringBuilder(@"
SELECT  pdxxmx.pdmxId ,
		pdxxmx.CreateTime,
		yp.ypCode ,
		yp.ypmc ,
		ypsx.ypgg ,
		yp.ycmc ,
		yp.py ,
		pdxxmx.ph ,
		pdxxmx.pc ,
		CONVERT(VARCHAR(10),pdxxmx.Yxq,120) yxq,
		yp.bzdw, 
		yp.zxdw,
		CONVERT(VARCHAR(10),pdxxmx.Llsl,50)+yp.zxdw llsl_zxbz ,--理论最小包装数量
		(select dbo.[f_getYfbmYpComplexYpSlandDw](pdxxmx.Llsl,@yfbmCode,yp.ypcode,@OrganizeId)) llsl ,--理论药房单位数量 
		(select dbo.[f_getYfbmYpComplexYpSlandDw](pdxxmx.Sjsl,@yfbmCode,yp.ypcode,@OrganizeId)) sjslstr ,--实际药房单位数量 
		pdxxmx.sjsl sjsl, --实际最小包装数量
		Floor(pdxxmx.sjsl / pdxxmx.zhyz) deptSjsl,
		dbo.f_getyfbmDw(yfbm.yfbmCode, yp.ypCode, yp.OrganizeId) deptdw ,
		pdxxmx.sjsl minSjsl,
		(pdxxmx.Sjsl - pdxxmx.Llsl) yksl ,
		pdxxmx.pfj ,
		pdxxmx.lsj ,
		pdxxmx.ykpfj ,
		pdxxmx.yklsj ,
		pdxxmx.zhyz 
        , CONVERT(NUMERIC(12,2),pdxxmx.Pfj*pdxxmx.Llsl/pdxxmx.Zhyz) llpfje
        , CONVERT(NUMERIC(12,2),pdxxmx.Lsj*pdxxmx.Llsl/pdxxmx.Zhyz) lllsje
        , CONVERT(NUMERIC(12,2),pdxxmx.Pfj*pdxxmx.Sjsl/pdxxmx.Zhyz) sjpfje
        , CONVERT(NUMERIC(12,2),pdxxmx.Lsj*pdxxmx.Sjsl/pdxxmx.Zhyz) sjlsje
		,@pdsj pdsj
FROM xt_yp_pdxxmx(NOLOCK) pdxxmx 
INNER JOIN dbo.xt_yp_pdxx(NOLOCK) pdxx ON pdxx.pdId=pdxxmx.pdId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp(NOLOCK) yp ON pdxxmx.Ypdm = yp.ypCode and yp.OrganizeId=pdxx.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=pdxx.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm on pdxx.yfbmCode=yfbm.yfbmCode AND yfbm.OrganizeId=pdxx.OrganizeId
WHERE pdxx.OrganizeId=@OrganizeId
AND pdxxmx.pdId = @pdId
AND yfbm.yfbmCode = @yfbmCode
");
            if (!string.IsNullOrEmpty(srm))
            {
                strSql.AppendLine("And (yp.Ypmc LIKE @srm OR pdxxmx.Ypdm LIKE @srm ) ");
            }
            if (!string.IsNullOrEmpty(ypzt))
            {
                strSql.AppendLine("AND (yp.zt = @ypzt OR @ypzt = '-1') ");
            }
            if (!string.IsNullOrEmpty(yplb))
            {
                strSql.AppendLine("AND (yp.dlCode = @yplb OR @yplb = '-1') ");
            }
            switch (kcxs)
            {
                case (int)EnumKCXS.Xslkc://显示零库存
                    strSql.AppendLine("AND (Llsl = 0 OR Sjsl = 0) ");
                    break;
                case (int)EnumKCXS.Bxsllslwl: //不显示理论数量为0
                    strSql.AppendLine("AND Llsl <> 0 ");
                    break;
                case (int)EnumKCXS.Bxssjslwl:  //不显示实际数量为0
                    strSql.AppendLine("AND Sjsl <> 0 ");
                    break;
                case (int)EnumKCXS.Bxslzdwl:  //不显示两者都为0
                    strSql.AppendLine("AND (Llsl <> 0 AND Sjsl<>0) ");
                    break;
            }

            var inSqlParameterList = new List<DbParameter>
            {
                new SqlParameter("@pdId", pdId),
                new SqlParameter("@pdsj", pdsj),
                new SqlParameter("@srm", "%" + (srm ?? "") + "%"),
                new SqlParameter("@ypzt", (ypzt??"").Trim()),
                new SqlParameter("@yplb", (yplb??"").Trim()),
                new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
            };
            return QueryWithPage<InventoryInfoVO>(strSql.ToString(), pagination, inSqlParameterList.ToArray());
        }

        /// <summary>
        /// 结束盘点
        /// </summary>
        /// <returns></returns>
        public string EndInventoryInfo(string pdId)
        {
            var paraList = new List<DbParameter>
            {
                new SqlParameter("@pdId", pdId),
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId),
                new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode)
            };
            return FindList<string>(TSqlStock.end_inventoryInfo, paraList.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// 取消盘点 
        /// 删除 盘点信息（xt_yp_pdxx）、盘点信息明细（xt_yp_pdxxmx）
        /// </summary>
        public void CancelInventory_old(string pdId)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                //删除盘点表
                var inventoryEntity = db.FindEntity<SysMedicineInventoryEntity>(pdId);
                db.Delete(inventoryEntity);
                //删除盘点明细表
                var inventoryDetailEntityList = db.IQueryable<SysMedicineInventoryDetailEntity>().Where(a => a.pdId == pdId).ToList();
                foreach (var item in inventoryDetailEntityList)
                {
                    db.Delete(item);
                }
                db.Commit();
            }
        }

        /// <summary>
        /// 取消盘点 
        /// 删除 盘点信息（xt_yp_pdxx）、盘点信息明细（xt_yp_pdxxmx）
        /// </summary>
        public void CancelInventory(string pdId)
        {
            var sql = new StringBuilder(@"
DELETE FROM dbo.xt_yp_pdxx WHERE pdId=@pdId_param;
IF @@ERROR <>0
BEGIN
	SELECT 0 AS Jgxx
END
ELSE
BEGIN
	DECLARE @tmpTab TABLE(pdmxId VARCHAR(50));
	INSERT INTO @tmpTab SELECT TOP 500 pdmxId FROM dbo.xt_yp_pdxxmx(NOLOCK) WHERE pdId=@pdId_param;

	WHILE EXISTS(SELECT 1 FROM @tmpTab)
	BEGIN
		DELETE FROM dbo.xt_yp_pdxxmx WHERE pdmxId IN (SELECT pdmxId FROM @tmpTab);
		DELETE FROM @tmpTab;
		INSERT INTO @tmpTab SELECT TOP 500 pdmxId FROM dbo.xt_yp_pdxxmx(NOLOCK) WHERE pdId=@pdId_param;
	END
	SELECT 1 AS Jgxx
END
");
            var result = FindList<int>(sql.ToString(), new DbParameter[]
            {
                new SqlParameter("@pdId_param", pdId)
            }).FirstOrDefault();
            if (result == 0)
            {
                throw new Exception("取消失败！");
            }
        }
        #endregion

        #region 库存结转
        /// <summary>
        /// 获取药品特殊属性
        /// </summary>
        /// <returns></returns>
        public List<DrugSpecialPropertiesVO> GetDrugSpecialPropertiesList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                            select itemDetail.Code,itemDetail.Name 
                            from NewtouchHIS_Base.dbo.V_S_Sys_Items items
                            inner join NewtouchHIS_Base.dbo.V_S_Sys_ItemsDetail itemDetail on itemDetail.ItemId=items.Id
                            where items.Code='XT_YP_TSSX' 
                                  and (itemDetail.TopOrganizeId=@TopOrganizeId or itemDetail.TopOrganizeId='*')
                         ");
            SqlParameter[] param =
                {
                    new SqlParameter("@TopOrganizeId",Constants.TopOrganizeId)
                };
            List<DrugSpecialPropertiesVO> list = this.FindList<DrugSpecialPropertiesVO>(strSql.ToString(), param).ToList();
            return list;
        }

        /// <summary>
        /// 查询需要结转的药品信息 (主表为库存及明细)
        /// </summary>
        /// <returns></returns>
        public List<NeedCarryOverMedicineVO> SelectNeedCarryOverMedicineList()
        {
            var strSql = new StringBuilder(@"
SELECT kcxx.ypdm ,
	 kcxx.ph ,
	kcxx.pc ,
	kcxx.yxq ,
	kcxx.kcsl ,
	yp.pfj ykpfj ,
	yp.lsj yklsj , 
	kcxx.jj ,
	kcxx.zhyz 
FROM dbo.xt_yp_kcxx(NOLOCK) kcxx 
INNER JOIN newtouchhis_base.dbo.V_S_xt_yp yp on kcxx.ypdm = yp.ypCode AND yp.OrganizeId=kcxx.OrganizeId
INNER JOIN newtouchhis_base.dbo.V_S_xt_ypsx ypsx on yp.ypId = ypsx.ypId AND ypsx.OrganizeId=kcxx.OrganizeId
INNER JOIN xt_yp_bmypxx(NOLOCK) bmyp on bmyp.ypdm = kcxx.ypdm and bmyp.yfbmCode = kcxx.yfbmCode and bmyp.OrganizeId=kcxx.OrganizeId
where kcxx.yfbmCode=@YfbmCode ");
            DbParameter[] param =
            {
                new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<NeedCarryOverMedicineVO>(strSql.ToString(), param).ToList();
        }

        /// <summary>
        /// 结转 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="zq"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        public void CarryOverMedicine(List<NeedCarryOverMedicineVO> list, string zq, DateTime kssj, DateTime jssj)
        {
            //将需结转的药品全部插入到结转表
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                foreach (var item in list)
                {
                    var entity = new SysMedicineStockCarryOverEntity
                    {
                        kcId = Guid.NewGuid().ToString(),
                        OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                        yfbmCode = Constants.CurrentYfbm.yfbmCode,
                        Ypdm = item.ypdm,
                        Ph = item.ph,
                        pc = item.pc,
                        Yxq = item.yxq,
                        Kcsl = item.kcsl,
                        Ykpfj = item.ykpfj ?? 0,
                        Yklsj = item.yklsj,
                        Jj = item.jj ?? 0,
                        Zhyz = item.zhyz,
                        kssj = kssj,
                        jssj = jssj,
                        zq = zq
                    };
                    entity.Create();
                    db.Insert(entity);
                }
                db.Commit();
            }
        }

        /// <summary>
        /// 查询已结转药品 （主表是结转表）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zq"></param>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public IList<CarryOverMedicineVO> SelectCarryOverMedicineList(Pagination pagination, string zq, string inputCode)
        {
            var strSql = new StringBuilder(@"
SELECT jz.zq,
	yp.ypmc ,
	jz.ypdm,
	ypsx.ypgg gg ,
	isnull(jz.kcsl, 0)/jz.Zhyz jzsl ,
	dbo.f_getyfbmDw(jz.yfbmCode, jz.Ypdm, jz.OrganizeId) dw ,
	jz.pc,
	yp.ycmc sccj ,
	CONVERT(NUMERIC(12,4),ISNULL(jz.ykpfj/yp.bzs*jz.Zhyz, 0)) pfj ,
	CONVERT(NUMERIC(12,4),isnull(jz.yklsj/yp.bzs*jz.Zhyz, 0)) lsj ,
	CONVERT(NUMERIC(12,2),isnull(jz.ykpfj/yp.bzs*jz.kcsl, 0)) pfze,
	CONVERT(NUMERIC(12,2),isnull(jz.Yklsj/yp.bzs*jz.kcsl, 0)) lsze,
	jz.CreateTime
from xt_yp_kcjz(NOLOCK) jz
INNER JOIN xt_yp_bmypxx(NOLOCK) bmyp on bmyp.ypdm = jz.ypdm and bmyp.yfbmCode = @yfbmCode AND bmyp.OrganizeId = @OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm ON  yfbm.yfbmCode= jz.yfbmCode AND yfbm.OrganizeId=@OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp on bmyp.ypdm = yp.ypCode AND yp.OrganizeId=@OrganizeId 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx on yp.ypId = ypsx.ypId AND ypsx.OrganizeId=@OrganizeId
where jz.yfbmCode = @yfbmCode
and jz.OrganizeId=@OrganizeId
");

            var inSqlParameterList = new List<DbParameter>
            {
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId),
                new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode)
            };
            if (!string.IsNullOrEmpty(zq))
            {
                strSql.Append(" and jz.zq = @zq");
                inSqlParameterList.Add(new SqlParameter("@zq", zq));
            }
            if (!string.IsNullOrEmpty(inputCode))
            {
                strSql.Append(" and ( yp.ypCode like @inputCode or yp.ypmc like @inputCode or yp.py like lower(@inputCode) )");
                inSqlParameterList.Add(new SqlParameter("@inputCode", "%" + inputCode.Trim() + "%"));
            }
            var list = QueryWithPage<CarryOverMedicineVO>(strSql.ToString(), pagination, inSqlParameterList.ToArray());
            return list;
        }

        #endregion

        #region 进销存统计

        /// <summary>
        /// 得到结转账期和账期对应的开始结束时间
        /// </summary>
        /// <returns></returns>
        public List<AccountPeriodDropDownVO> GetAccountPeriodDropDownList()
        {
            var strSql = new StringBuilder();
            strSql.Append(" select distinct zq,jssj from [dbo].[xt_yp_kcjz] where yfbmCode=@yfbmCode and OrganizeId=@OrganizeId");
            SqlParameter[] param =
                {
                    new SqlParameter("@yfbmCode",Constants.CurrentYfbm.yfbmCode),
                    new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
                };
            return FindList<AccountPeriodDropDownVO>(strSql.ToString(), param).ToList();
        }

        /// <summary>
        /// 药品类别 （进销存）
        /// </summary>
        /// <returns></returns>
        public List<MedicineCategoryVO> GetMedicineCategoryList2()
        {
            var strSql = new StringBuilder(@"
SELECT DISTINCT sfdl.dlCode,sfdl.dlmc 
FROM NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm_yp yfbmyp ON yfbmyp.dlCode=sfdl.dlCode AND yfbmyp.OrganizeId=sfdl.OrganizeId AND yfbmyp.zt='1'
WHERE sfdl.OrganizeId=@OrganizeId AND sfdl.zt='1'
ORDER BY sfdl.dlmc
            ");
            DbParameter[] param =
            {
                new SqlParameter("@yfbmCode",Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<MedicineCategoryVO>(strSql.ToString(), param).ToList();
        }

        /// <summary>
        /// 进销存统计
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kszq"></param>
        /// <param name="jszq"></param>
        /// <param name="jsjzsj"></param>
        /// <param name="inputCode"></param>
        /// <param name="deptCode"></param>
        /// <param name="drugType"></param>
        /// <param name="dosage"></param>
        /// <param name="drugState"></param>
        /// <param name="rate"></param>
        /// <param name="ksjzsj"></param>
        /// <returns></returns>
        public IList<PSIStatisticsVO> PsiStatisticsInfoList(Pagination pagination, string kszq, string jszq, DateTime ksjzsj, DateTime jsjzsj, string inputCode, string deptCode, string drugType, string dosage, string drugState, string rate)
        {
            if (string.IsNullOrWhiteSpace(pagination.sidx))
            {
                throw new Exception("pagination.sidx is required");
            }
            var sortby = pagination.sidx;
            if (!string.IsNullOrWhiteSpace(pagination.sord) && pagination.sord.ToUpper() != "ASC")
            {
                if (!sortby.Contains(",") && !sortby.Contains(" "))
                {
                    sortby += " " + pagination.sord;
                }
            }

            var paraList = new List<SqlParameter>
            {
                new SqlParameter("@kszq", kszq),
                new SqlParameter("@jszq", jszq),
                new SqlParameter("@ksjzsj", ksjzsj),
                new SqlParameter("@jsjzsj", jsjzsj),
                new SqlParameter("@inputCode", inputCode.Trim()),
                new SqlParameter("@deptCode", Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId),
                new SqlParameter("@drugType", string.IsNullOrWhiteSpace(drugType)?"-1" :drugType.Trim()),
                new SqlParameter("@dosage", string.IsNullOrWhiteSpace(dosage)?"-1" :dosage.Trim()),
                new SqlParameter("@sub", "1"),
                new SqlParameter("@drugState", string.IsNullOrWhiteSpace(drugState)?"-1" :drugState.Trim()),
                new SqlParameter("@rate", string.IsNullOrWhiteSpace(rate)?-1 :Convert.ToDecimal(rate.Trim())),
                new SqlParameter("@currPageIndex", pagination.page),
                new SqlParameter("@perRows", pagination.rows),
                new SqlParameter("@orderByParam", sortby)
            };
            //中标标志
            var outParameter = new SqlParameter("@records", SqlDbType.Int) { Direction = ParameterDirection.Output };
            paraList.Add(outParameter);
            var list = FindList<PSIStatisticsVO>("exec sp_yp_jxctj @kszq, @jszq,@ksjzsj,@jsjzsj,@inputCode, @deptCode, @OrganizeId, @drugType, @dosage,@sub ,@drugState,@rate, @currPageIndex, @perRows, @orderByParam, @records output", paraList.ToArray());
            pagination.records = outParameter.Value.ToInt();
            return list;
        }


        #endregion

        #region 通过药房代码获取药库

        /// <summary>
        /// 通过药房代码获取药库
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVEntity> GetTheUpperYkbmCodeList(string keyword, string yfbmCode, string orgId)
        {
            const string strSql = @"
SELECT B.yfbmId,B.yfbmmc,B.yfbmCode,A.OrganizeId,B.mzzybz,B.yjbmjb,B.ksCode,A.zt 
FROM xt_yp_fypz(nolock) A 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm(nolock) B on B.yfbmCode=A.ykCode AND b.OrganizeId=a.OrganizeId AND b.zt='1'
WHERE A.yfCode=@yfbmCode 
AND (b.yfbmmc LIKE '%'+@keyword+'%' OR b.yfbmCode LIKE '%'+@keyword+'%' OR b.yfbmId LIKE '%'+@keyword+'%')
AND A.OrganizeId=@Organizeid 
AND a.zt='1' 
";
            DbParameter[] param = {
                new SqlParameter("@keyword",keyword==null?"":keyword.Trim()),
                new SqlParameter("@yfbmCode",yfbmCode),
                new SqlParameter("@Organizeid",orgId)
            };
            return FindList<SysPharmacyDepartmentVEntity>(strSql, param).ToList();
        }
        #endregion

        #region  通过药库代码获取药房

        /// <summary>
        /// 通过药库代码获取发药药房
        /// </summary>
        /// <param name="ykbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVEntity> GetTheLowerYfbmCodeList(string keyword, string ykbmCode, string organizeId)
        {
            var strSql = new StringBuilder(@"
SELECT b.yfbmId, b.yfbmmc, b.yfbmCode, a.OrganizeId, mzzybz, yjbmjb, ksCode, a.zt 
FROM xt_yp_fypz(nolock) A 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm(nolock) B on B.yfbmCode=A.yfCode AND b.OrganizeId=a.OrganizeId AND b.zt='1'
where A.OrganizeId=@OrganizeId
AND a.zt='1'
AND A.ykCode=@yfbmCode
AND (B.yfbmCode LIKE '%'+@keyword+'%' OR B.yfbmmc LIKE '%'+@keyword+'%' OR B.yfbmId LIKE '%'+@keyword+'%') ");
            var param = new List<DbParameter>
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@yfbmCode", ykbmCode),
                new SqlParameter("@keyword", keyword)
            };
            return FindList<SysPharmacyDepartmentVEntity>(strSql.ToString(), param.ToArray());

        }
        #endregion

        #region  通过药房代码获取科室

        /// <summary>
        /// 通过药房代码获取科室
        /// </summary>
        /// <param name="ykbmCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysDepartmentVEntity> GetTheLowerKsCodeList(string ykbmCode, string orgId, string keyword = "")
        {
            var strSql = new StringBuilder(@"
SELECT * 
FROM xt_yp_ksfypz(nolock) A 
LEFT JOIN NewtouchHIS_Base.dbo.sys_department(nolock) B on B.Code=A.ksCode
where A.zt = '1' 
AND B.OrganizeId=@OrganizeId 
AND A.OrganizeId=@OrganizeId
AND A.yfCode = @yfbmCodes
AND (b.Name LIKE '%'+@keyword+'%' OR b.py LIKE '%'+@keyword+'%' OR b.Code LIKE '%'+@keyword+'%')
");
            var param = new List<DbParameter>
            {
                new SqlParameter("@OrganizeId", orgId),
                new SqlParameter("@yfbmCodes", ykbmCode),
                new SqlParameter("@keyword", (keyword??"").Trim())
            };
            return FindList<SysDepartmentVEntity>(strSql.ToString(), param.ToArray());
        }
        #endregion

        #region 资源预定

        /// <summary>
        /// 资源预定 （冻结库存，写mz_cfmxph表）
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        public void BookItem(BookItemDo bookItemDo)
        {
            var param = new List<DbParameter>
            {
                new SqlParameter("@YpCode", bookItemDo.YpCode),
                new SqlParameter("@Sl", bookItemDo.Sl),
                new SqlParameter("@Yfbm", bookItemDo.Yfbm),
                new SqlParameter("@Cfh", bookItemDo.Cfh),
                new SqlParameter("@Yzzxid", bookItemDo.Yzzxid),
                new SqlParameter("@zxId", bookItemDo.zxId),
                new SqlParameter("@OrganizeId", bookItemDo.OrganizeId),
                new SqlParameter("@CreatorCode", bookItemDo.CreatorCode),
                new SqlParameter("@AdtTdrq", bookItemDo.AdtTdrq)
            };
            var outpar = new SqlParameter("@Res", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            param.Add(outpar);
            FindList<object>(@" EXEC [dbo].[sp_yp_book] @YpCode, @Sl, @Yfbm, @Cfh, @Yzzxid, @zxId, @OrganizeId, @CreatorCode, @AdtTdrq, @Res out", param.ToArray());
            bookItemDo.Res = outpar.Value.ToString();
        }

        /// <summary>
        /// 门诊处方预定（冻结库存）
        /// </summary>
        /// <param name="apiResp"></param>
        /// <param name="cfnm"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public string OutPatientBookItem(object apiResp, string cfnm, cdInfoVO vo)
        {
            if (vo.zje <= 0)
            {
                return "总金额不能为零";
            }
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                var pyparlist = apiResp.ToJson().ToList<PyparListVo>();
                foreach (var bookItemDo in pyparlist.Select(t => new BookItemDo
                {
                    YpCode = t.yp,
                    Sl = (int)t.sl,
                    Yfbm = vo.lyyf,
                    Cfh = t.cfh,
                    OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                    CreatorCode = OperatorProvider.GetCurrent().UserCode
                }))
                {
                    OutPatientBookItem(bookItemDo);
                    if (!string.IsNullOrWhiteSpace(bookItemDo.Res))
                    {
                        return bookItemDo.Res;
                    }
                }
                db.Commit();
            }
            return "";
        }

        /// <summary>
        /// 门诊处方预定（冻结库存）
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        public void OutPatientBookItem(BookItemDo bookItemDo)
        {
            var param = new List<DbParameter>
            {
                new SqlParameter("@YpCode", bookItemDo.YpCode),
                new SqlParameter("@Sl", bookItemDo.Sl),
                new SqlParameter("@Yfbm", bookItemDo.Yfbm),
                new SqlParameter("@Cfh", bookItemDo.Cfh),
                new SqlParameter("@OrganizeId", bookItemDo.OrganizeId),
                new SqlParameter("@CreatorCode", bookItemDo.CreatorCode)
            };
            var outpar = new SqlParameter("@Res", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            param.Add(outpar);
            FindList<object>(@" EXEC [dbo].[sp_yp_book_mz] @YpCode, @Sl, @Yfbm, @Cfh, @OrganizeId, @CreatorCode, @Res out", param.ToArray());
            bookItemDo.Res = outpar.Value.ToString();
        }

        /// <summary>
        /// 住院医嘱执行（冻结库存）
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        public void HospitalizationBookItem(BookItemDo bookItemDo)
        {
            var param = new List<DbParameter>
            {
                new SqlParameter("@YpCode", bookItemDo.YpCode),
                new SqlParameter("@Sl", bookItemDo.Sl),
                new SqlParameter("@Yfbm", bookItemDo.Yfbm),
                new SqlParameter("@zxId", bookItemDo.zxId),
                new SqlParameter("@Yzxxid", bookItemDo.Yzzxid),
                new SqlParameter("@OrganizeId", bookItemDo.OrganizeId),
                new SqlParameter("@CreatorCode", bookItemDo.CreatorCode)
            };
            var outpar = new SqlParameter("@Res", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            param.Add(outpar);
            FindList<object>(@" EXEC [dbo].[sp_yp_book_zy] @YpCode, @Sl, @Yfbm, @zxId, @Yzxxid, @OrganizeId, @CreatorCode, @Res out", param.ToArray());
            bookItemDo.Res = outpar.Value.ToString();
        }

        /// <summary>
        /// 医嘱执行 获取药品单价
        /// </summary>
        /// <param name="organizeId">组织ID</param>
        /// <param name="yfbmCode">发药药房</param>
        /// <param name="ypCode">药品编码</param>
        /// <returns></returns>
        public MedicineDjandZhyzVO GetZYYPdj(string organizeId, string yfbmCode, string ypCode)
        {
            const string sql = @"
select case b.mzzybz when '2' then a.lsj/a.bzs*a.zycls else a.lsj/a.bzs*a.mzcls end dj,
case b.mzzybz when '2' then a.zycls else a.mzcls end zhyz 
from NewtouchHIS_Base..V_C_xt_yp a
inner join NewtouchHIS_Base..V_S_xt_yfbm b on a.organizeId = b.organizeId and b.yfbmCode = @yfbmCode
where a.ypCode = @ypCode and a.organizeId = @organizeId
";
            var param = new List<DbParameter>
            {
                new SqlParameter("@organizeId", organizeId),
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@ypCode", ypCode)
            };
            var list = FindList<MedicineDjandZhyzVO>(sql, param.ToArray());
            if (list == null || list.Count <= 0)
            {
                return new MedicineDjandZhyzVO { Dj = 0, Zhyz = 0 };
            }

            return list[0];
        }
        #endregion

        #region 台账相关

        public async Task<List<StandingBookInventoryDetail>> StandingBookInventoryDetailQuery(string ypCode, DateTime kssj, DateTime jssj, string yfbmCode, string orgId)
        {
            var sql = $@"
--药房入库
SELECT '药房入库' crksm, dbo.f_getComplexYpSlandDw(mx.Sl*mx.Rkzhyz,yp.bzs,yp.bzdw,yp.zxdw) rkslanddw, '' ckslanddw, '' jzslanddw, dj.pdh pzh, '' ks, dj.Rksj fsrq
FROM NewtouchHIS_PDS.dbo.xt_yp_crkmx(nolock) mx 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=mx.Ypdm AND yp.OrganizeId=@orgId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=@orgId
INNER JOIN 	NewtouchHIS_PDS.dbo.xt_yp_crkdj(nolock) dj on dj.crkId=mx.crkId 
WHERE dj.OrganizeId=@orgId AND dj.djlx in (1,5)	AND dj.shzt=1
AND dj.Rkbm=@yfbmCode {(string.IsNullOrWhiteSpace(ypCode) ? "" : "AND mx.Ypdm=@ypdm ")}
AND dj.Rksj BETWEEN @kssj AND @jssj

UNION ALL

--门诊发药
SELECT '门诊发药' crksm, '' rkslanddw,  CONCAT(CONVERT(INT,mxph.sl/mx.zhyz),mx.dw) ckslanddw, '' jzslanddw, cf.cfh pzh, cf.ksmc ks, fyjl.CreateTime fsrq 
FROM mz_cfypczjl(nolock) fyjl
INNER JOIN mz_cfmx(nolock) mx ON fyjl.mzcfmxId=mx.Id AND mx.OrganizeId = @orgId
INNER JOIN mz_cf(nolock) cf on cf.cfh = mx.cfh AND cf.OrganizeId = @orgId
INNER JOIN dbo.mz_cfmxph(NOLOCK) mxph ON mxph.cfh=mx.cfh AND mxph.gjzt='0' AND mxph.yp=mx.ypCode AND mxph.OrganizeId=@orgId AND mxph.fyyf=cf.lyyf AND mxph.zt='1' 
WHERE  cf.zt = '1' 
AND fyjl.operateType = '1'
AND cf.lyyf = @yfbmCode {(string.IsNullOrWhiteSpace(ypCode) ? "" : "AND mx.ypCode=@ypdm ")}
AND fyjl.CreateTime BETWEEN CONVERT(DATETIME, @kssj) AND CONVERT(DATETIME, @jssj) 

UNION ALL

--住院发药
SELECT '住院发药' crksm, '' rkslanddw, dbo.f_getYfbmYpComplexYpSlandDw(c.sl, @yfbmCode, yz.ypCode, @orgId) ckslanddw, '' jzslanddw, '' pzh, dept.name ksmc, fyjl.CreateTime fsrq 
FROM zy_ypyzczjl(nolock) fyjl
INNER JOIN zy_ypyzxx(nolock) yz ON fyjl.ypyzxxId = yz.Id and yz.OrganizeId=@orgId
INNER JOIN NewtouchHIS_PDS.dbo.zy_ypyzzxph(NOLOCK) c ON c.zxId=yz.zxId AND c.yzId=yz.yzId AND c.ypCode=yz.ypCode AND c.zt='1' AND c.gjzt='0' AND c.OrganizeId=@orgId
LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.code=yz.ksCode and dept.zt='1' and dept.organizeId=@orgId
WHERE fyjl.operateType = '1' 
AND yz.fyyf=@yfbmCode {(string.IsNullOrWhiteSpace(ypCode) ? "" : "AND yz.ypCode=@ypdm ")}
AND fyjl.CreateTime BETWEEN @kssj AND @jssj

UNION ALL

--库存结转
SELECT '库存结转' crksm, '' rkslanddw,  '' ckslanddw, dbo.f_getYfbmYpComplexYpSlandDw(jz.kcsl, @yfbmCode, jz.Ypdm, @orgId) jzslanddw, '' pzh, '' ks, jz.Jzsj fsrq 
FROM xt_yp_kcjzk(NOLOCK) jz
WHERE jz.organizeId=@orgId 
AND jz.yfbmCode = @yfbmCode {(string.IsNullOrWhiteSpace(ypCode) ? "" : "AND jz.Ypdm=@ypdm ")}
AND jz.Jzsj BETWEEN @kssj AND @jssj
";
            var param = new List<DbParameter>
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@ypdm", ypCode??""),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj)
            };
            return FindList<StandingBookInventoryDetail>(sql, param.ToArray());
        }

        #endregion

        /// <summary>
        /// 获取机构下所有药房
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVEntity> GetYfbmList(string keyword, string orgId)
        {
            const string strSql = @"
SELECT B.yfbmId,B.yfbmmc,B.yfbmCode,B.OrganizeId,B.mzzybz,B.yjbmjb,B.ksCode
FROM NewtouchHIS_Base.dbo.V_S_xt_yfbm(nolock) B 
WHERE B.zt='1' AND B.OrganizeId=@Organizeid and B.mzzybz<>0
AND (isnull(@keyword,'')='' or b.yfbmmc LIKE '%'+@keyword+'%' OR b.yfbmCode LIKE '%'+@keyword+'%' OR b.yfbmId LIKE '%'+@keyword+'%')

";
            DbParameter[] param = {
                new SqlParameter("@keyword",keyword==null?"":keyword.Trim()),
                new SqlParameter("@Organizeid",orgId)
            };
            return FindList<SysPharmacyDepartmentVEntity>(strSql, param).ToList();
        }
    }
}
