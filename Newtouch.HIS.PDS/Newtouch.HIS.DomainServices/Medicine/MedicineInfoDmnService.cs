using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.TSQL;
using Newtouch.Tools;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 药品信息
    /// </summary>
    public class MedicineInfoDmnService : DmnServiceBase, IMedicineInfoDmnService
    {
        private SysPharmacyDepartmentMedicineRepo sysPharmacyDepartmentMedicineRepo;

        public MedicineInfoDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 本部门药品信息 查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<MedicineInfoVO> GetMedicineInfoList(Pagination pagination, MedicineInfoParam param, string orgId)
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

            var strSql = new StringBuilder(@"
EXEC dbo.YP_XT_BBMYPXXK 
@Ksdm = @Ksdm, 
@Ypmc = @Ypmc,
@Yplb = @Yplb,
@Ypsx = @Ypsx, 
@Ypjx = @Ypjx, 
@Syzt = @Syzt,
@Ypzt = @Ypzt,
@orgId= @orgId,
@currPageIndex=@currPageIndex,
@perRows=@perRows,
@orderByParam=@orderByParam,@records=@records output
");
            var paraList = new List<SqlParameter>
            {
                new SqlParameter("@Ksdm", Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@Ypmc", param.ypdm ?? ""),
                new SqlParameter("@Yplb", param.yplb ?? ""),
                new SqlParameter("@Ypsx", param.ypsx ?? ""),
                new SqlParameter("@Ypjx", param.ypjx ?? ""),
                new SqlParameter("@Syzt", param.syzt),
                new SqlParameter("@Ypzt", param.ypzt),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@currPageIndex", pagination.page),
                new SqlParameter("@perRows", pagination.rows),
                new SqlParameter("@orderByParam", sortby)
            };
            var outParameter = new SqlParameter("@records", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            paraList.Add(outParameter);
            var list = FindList<MedicineInfoVO>(strSql.ToString(), paraList.ToArray());
            pagination.records = outParameter.Value.ToInt();
            return list;
        }

        /// <summary>
        /// 本部门药品信息 查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<MedicineInfoVO> GetMedicineInfoListV2(Pagination pagination, MedicineInfoParam param, string orgId)
        {
            if (string.IsNullOrWhiteSpace(pagination.sidx))
            {
                throw new Exception("pagination.sidx is required");
            }

            var strSql = new StringBuilder(@"
SELECT TOP 1000
        yp.ypId ,
        yp.ypCode ,
        yp.ypmc ,
        yp.ycmc ,
        sx.ypgg ,
        SUM(ISNULL(kc.kcsl, 0)) kcsl ,
        bm.Zcxh ,
        CASE bm.zt WHEN 1 THEN '正常' WHEN 0 THEN '控制' END syzt ,
		dbo.f_getYfYpComplexYpSlandDw(SUM(ISNULL(kc.kcsl, 0)), fm.mzzybz, bm.Ypdm, bm.OrganizeId) klsl ,
        SUM(CONVERT(NUMERIC(11, 2),ISNULL(kc.kcsl, 0)/ yp.bzs)) YkKcsl ,
        yp.bzdw YkDw ,
       dbo.f_getyfbmDw(bm.yfbmCode, bm.Ypdm, bm.OrganizeId) deptdw ,
        --CONVERT(DECIMAL(11, 4),yp.lsj/yp.bzs * dbo.f_getyfbmZhyz(bm.yfbmCode, bm.Ypdm, bm.OrganizeId)) lsj ,
        --CONVERT(DECIMAL(11, 4),yp.pfj/yp.bzs * dbo.f_getyfbmZhyz(bm.yfbmCode, bm.Ypdm, bm.OrganizeId)) pfj ,
		CONVERT(DECIMAL(11, 2),yp.lsj/yp.bzs*(CASE fm.mzzybz WHEN '0' THEN yp.bzs WHEN '1' THEN yp.mzcls WHEN '2' THEN yp.zycls WHEN '3' THEN yp.mzcls END)) lsj ,
        CONVERT(DECIMAL(11, 4),yp.pfj/yp.bzs*(CASE fm.mzzybz WHEN '0' THEN yp.bzs WHEN '1' THEN yp.mzcls WHEN '2' THEN yp.zycls WHEN '3' THEN yp.mzcls END)) pfj ,
        jx.jxmc ,
        bm.Ypkw ,
        bm.Pxfs1 ,
        bm.Pxfs2 ,
        bm.Kcsx ,
        bm.Kcxx ,
        bm.Jhd ,
        bm.Jhl ,
        RTRIM(LTRIM(dl.dlmc)) AS yplb ,
        CASE yp.zt WHEN '1' THEN '正常' WHEN '0' THEN '停用' END ypzt ,
        bm.Sysx
FROM    dbo.XT_YP_BMYPXX(nolock) bm
        INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_yp yp ON yp.ypCode = bm.Ypdm and yp.OrganizeId=bm.OrganizeId
        INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_ypsx sx ON sx.ypId = yp.ypId and sx.OrganizeId=bm.OrganizeId
        INNER JOIN [NewtouchHIS_Base]..V_S_xt_yfbm fm ON fm.yfbmCode = bm.yfbmCode and fm.OrganizeId=bm.OrganizeId
        LEFT  JOIN dbo.XT_YP_KCXX(nolock) kc ON kc.yfbmCode = bm.yfbmCode AND kc.Ypdm = bm.Ypdm AND kc.OrganizeId=bm.OrganizeId
        LEFT JOIN [NewtouchHIS_Base]..V_S_xt_sfdl dl ON dl.dlcode = yp.dlcode and dl.OrganizeId=bm.OrganizeId and dl.zt='1'
        LEFT  JOIN [NewtouchHIS_Base].dbo.V_S_xt_ypjx jx ON jx.jxCode = yp.jx
		WHERE bm.OrganizeId=@OrganizeId 
		AND bm.yfbmCode =@YfbmCode
		AND ( yp.dlcode = @Yplb OR @Yplb = '' )
        AND ( bm.Ypsxdm = CAST(@Ypsx AS VARCHAR(255)) OR CAST(@Ypsx AS VARCHAR(255)) =  '' )	
        AND ( yp.jx = @Ypjx OR @Ypjx =  '' ) 
        AND ( bm.zt = CAST(@Syzt AS VARCHAR(20)) OR CAST(@Syzt AS VARCHAR(20)) =  '' )
        AND ( yp.zt = @Ypzt OR @Ypzt =  '' )
        AND ( yp.ypCode LIKE '%' + @ypmc+ '%'
                OR yp.ypmc LIKE '%' + @ypmc + '%'
                OR yp.spm LIKE '%' + @ypmc + '%'
                OR yp.py LIKE '%' + @ypmc + '%'
            )
GROUP BY yp.ypId ,sx.ypgg ,bm.Zcxh ,bm.zt,bm.yfbmCode,bm.OrganizeId,bm.Ypdm,bm.Ypkw ,bm.Pxfs1 ,bm.Pxfs2 ,bm.Kcsx ,bm.Kcxx ,bm.Jhd ,bm.Jhl ,bm.Sysx,fm.mzzybz,yp.bzs,yp.mzcls, yp.zycls,yp.bzdw,yp.lsj,yp.pfj,yp.ypCode ,yp.ypmc ,yp.ycmc,yp.zt,jx.jxmc,dl.dlmc
");
            var paraList = new List<DbParameter>
            {
                new SqlParameter("@YfbmCode", Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@Ypmc", param.ypdm ?? ""),
                new SqlParameter("@Yplb", param.yplb ?? ""),
                new SqlParameter("@Ypsx", param.ypsx ?? ""),
                new SqlParameter("@Ypjx", param.ypjx ?? ""),
                new SqlParameter("@Syzt", param.syzt),
                new SqlParameter("@Ypzt", param.ypzt),
                new SqlParameter("@OrganizeId", orgId),
            };
            return QueryWithPage<MedicineInfoVO>(strSql.ToString(), pagination, paraList.ToArray());
        }


        /// <summary>
        /// 同步本部门药品信息
        /// </summary>
        /// <param name="yp"></param>
        /// <param name="operateType">0:添加  1：删除</param>
        /// <param name="orgId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        public string FreshList(string[] yp, int operateType, string orgId, string yfbmCode)
        {
            var ret = new StringBuilder();
            if (yp == null || yp.Length <= 0) return ret.ToString();
            foreach (var item in yp)
            {
                var t = Fresh(item, operateType, orgId, yfbmCode);
                if (!string.IsNullOrWhiteSpace(t)) ret.Append(t).Append("； ");
            }
            return ret.ToString();
        }

        /// <summary>
        /// 同步本部门药品信息
        /// </summary>
        /// <param name="ypId"></param>
        /// <param name="operateType">0:添加  1：删除</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        private string Fresh(string ypId, int operateType, string orgId, string yfbmCode)
        {
            var param = new DbParameter[]
            {
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@OrganizeId", orgId),
                new SqlParameter("@ypId", ypId),
                new SqlParameter("@operateType", operateType)
            };
            return FirstOrDefault<string>(TSqlDrugs.bm_yp_xxtb, param);
        }

        /// <summary>
        /// 控制
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="type">1 控制 2 正常</param>
        /// <param name="orgId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        public int ControlMedicine(string ypCode, string type, string orgId, string yfbmCode)
        {
            var par = new DbParameter[] {
                    new SqlParameter("@type",type),
                    new SqlParameter("@Ksdm",yfbmCode),
                    new SqlParameter("@OrganizeId",orgId),
                    new SqlParameter("@Ypdm",ypCode)
                };
            return ExecuteSqlCommand(@"update XT_YP_BMYPXX set zt=@type where yfbmCode=@Ksdm and Ypdm=@Ypdm AND OrganizeId=@OrganizeId", par);
        }

        /// <summary>
        /// 内部发药查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public IList<HandOutMedicineQueryVO> GetHandOutMedicineInfoList(Pagination pagination, QueryMedicineInfoReqVO vo)
        {
            var ckbm = Constants.CurrentYfbm.yfbmCode;
            var strSql = new StringBuilder(@"
SELECT  dj.djlx,
        dj.crkId,
        --ISNULL(SUM(CONVERT(DECIMAL(12,2), djmx.Lsj*djmx.Sl)),0) ckje ,
        yp.dlcode ,
        sfdl.dlmc AS Yplbmc ,
        jx.jxmc ,
        dj.Ckczy ,
        dj.Rkczy ,
        detail.NAME sx,
        dj.Czsj Cksj ,
        bm.yfbmmc ckbm ,
        bm2.yfbmmc rkbm ,
        yp.py ,
        djmx.Ypdm ,
        yp.ypmc ,
        ypsx.ypgg ,
        djmx.Pfj ,
        djmx.Lsj ,
        yp.bzdw Dw ,
        djmx.Sl,
        ( dbo.f_getComplexYpSlandDw(djmx.Sl*yp.bzs, yp.bzs, yp.bzdw, yp.zxdw) ) fysl ,
        djmx.Ph ,
        CONVERT(VARCHAR(100), djmx.Yxq, 111) AS Yxq ,
        djmx.Zje,
		ISNULL(CONVERT(INT,(djmx.Ckbmkc / yp.bzs)), 0) kc ,
        djmx.Wg ,
        dj.Pdh ,
        djmx.Jj JJ,
		yp.ycmc
FROM    XT_YP_CRKDJ(NOLOCK) dj
        INNER JOIN XT_YP_CRKMX(NOLOCK) djmx ON dj.crkId = djmx.crkId
        INNER JOIN NewtouchHIS_Base..V_S_xt_yp yp ON djmx.Ypdm = yp.ypCode AND yp.OrganizeId=dj.OrganizeId
        INNER JOIN NewtouchHIS_Base..V_S_xt_ypsx ypsx ON ypsx.ypId = yp.ypId AND ypsx.OrganizeId=dj.OrganizeId
        INNER JOIN NewtouchHIS_Base..V_S_xt_ypcrkfs crkfs ON dj.Crkfsdm = crkfs.crkfsCode
        INNER JOIN NewtouchHIS_Base..V_S_xt_yfbm bm ON bm.yfbmCode = dj.Ckbm AND bm.OrganizeId=dj.OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_yfbm bm2 ON bm2.yfbmCode = dj.Rkbm AND bm2.OrganizeId=dj.OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl ON yp.dlCode = sfdl.dlcode AND sfdl.OrganizeId=dj.OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_ypjx jx ON yp.jx = jx.jxCode
        LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_ItemsDetail detail ON detail.Code = ypsx.yptssx AND detail.OrganizeId=dj.OrganizeId 
        LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Items iem ON iem.Id = detail.ItemId AND iem.Code = 'XT_YP_TSSX'
        LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Staff ry ON ry.gh = Ckczy AND dj.OrganizeId = ry.OrganizeId
WHERE   dj.OrganizeId=@Organizeid
		AND bm.yfbmCode=@Ckbm
		AND( dj.Rkbm = @rkbm OR @rkbm = '' )
        AND(yp.jx = @jx OR  @jx = '0' )
        AND(ypsx.yptssx = @sx OR @sx = '' )
        AND(yp.dlCode = @yplb OR @yplb = '' )
");
            var parms = new List<SqlParameter>
            {
                new SqlParameter("@jx", vo.jxbh),
                new SqlParameter("@sx", vo.yptssx ?? ""),
                new SqlParameter("@yplb", vo.yplb ?? ""),
                new SqlParameter("@rkbm", vo.shbm ?? ""),
                new SqlParameter("@Organizeid", OperatorProvider.GetCurrent().OrganizeId),
                new SqlParameter("@ckbm", ckbm)
            };
            if (!string.IsNullOrWhiteSpace(vo.begindate))
            {
                var bt = Convert.ToDateTime(vo.begindate);
                bt = bt < Constants.MinDateTime ? Constants.MinDateTime : bt;
                strSql.AppendLine(@"        AND dj.Czsj >= @startTime ");
                parms.Add(new SqlParameter("@startTime", bt.ToString(Constants.DateTimeFormat)));
            }
            if (!string.IsNullOrWhiteSpace(vo.enddate))
            {
                var et = Convert.ToDateTime(vo.enddate);
                et = et < Constants.MinDateTime ? Constants.MinDateTime : et;
                strSql.AppendLine(@"        AND dj.Czsj <= @endTime ");
                parms.Add(new SqlParameter("@endTime", et.ToString(Constants.DateTimeFormat)));
            }
            if (!string.IsNullOrWhiteSpace(vo.Pdh))
            {
                strSql.AppendLine(@"        AND dj.Pdh LIKE RTRIM(LTRIM(@pdh))");
                parms.Add(new SqlParameter("@pdh", "%" + (vo.Pdh ?? "") + "%"));
            }
            strSql.AppendLine("        AND (dj.djlx =" + (int)EnumDanJuLX.zhijiefayao + " OR dj.djlx=" + (int)EnumDanJuLX.shenlingfayao + ") ");
            if (vo.djlx != "-1" && !string.IsNullOrWhiteSpace(vo.djlx))
            {
                strSql.AppendLine("        AND dj.djlx=" + vo.djlx);
            }
            return QueryWithPage<HandOutMedicineQueryVO>(strSql.ToString(), pagination, parms.ToArray());
        }

        /// <summary>
        /// 本部门同步药品候选信息 查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<MedicineInfoVO> GetTbMedicineInfoList(Pagination pagination, MedicineInfoParam param, string orgId)
        {
            const string sql = @"
SELECT DISTINCT TOP 1000
        RTRIM(LTRIM(dl.dlmc)) AS yplb
        ,yp.ypId 
        ,yp.ypCode 
        ,yp.ypmc 
        ,yp.ycmc 
        ,sx.ypgg 
        ,CONVERT(DECIMAL(11, 4), yp.lsj/ yp.bzs*(CASE fm.mzzybz WHEN '0' THEN yp.bzs WHEN '1' THEN yp.mzcls WHEN '2' THEN yp.zycls WHEN '3' THEN yp.mzcls END)) lsj 
        ,CONVERT(DECIMAL(11, 4), yp.pfj/ yp.bzs*(CASE fm.mzzybz WHEN '0' THEN yp.bzs WHEN '1' THEN yp.mzcls WHEN '2' THEN yp.zycls WHEN '3' THEN yp.mzcls END)) pfj 
        ,CASE fm.mzzybz WHEN '0' THEN yp.bzdw WHEN '1' THEN yp.mzcldw WHEN '2' THEN yp.zycldw WHEN '3' THEN yp.mzcldw END deptdw 
        ,jx.jxmc 
        ,ISNULL(bm.bmypId, '') bmypId
FROM    [NewtouchHIS_Base].dbo.V_S_xt_yp yp 
        INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_ypsx sx ON sx.ypId = yp.ypId and sx.OrganizeId=yp.OrganizeId 
        INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_yfbm_yp ks ON ks.dlCode = yp.dlCode and ks.OrganizeId=yp.OrganizeId
        INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_yfbm fm ON fm.yfbmCode = ks.yfbmCode and fm.OrganizeId=yp.OrganizeId
        LEFT JOIN [NewtouchHIS_Base].dbo.V_S_xt_sfdl dl ON dl.dlcode = yp.dlcode and dl.OrganizeId=yp.OrganizeId and dl.zt='1'
        LEFT JOIN [NewtouchHIS_Base].dbo.xt_ypjx jx ON jx.jxCode = yp.jx AND jx.zt='1'
		LEFT JOIN dbo.XT_YP_BMYPXX(nolock) bm ON bm.Ypdm = yp.ypCode and bm.OrganizeId=yp.OrganizeId AND bm.yfbmCode = CAST(@yfbmCode AS VARCHAR(255))
WHERE	yp.OrganizeId=@OrganizeId 		
		AND fm.yfbmCode=@yfbmCode
		AND ( yp.dlcode = @Yplb OR @Yplb = '' )
		AND ( yp.jx = @Ypjx OR @Ypjx =  '' )
		AND ( yp.ypCode LIKE '%' + CAST(@srm AS VARCHAR(50))+ '%'
				OR yp.ypmc LIKE '%' + CAST(@srm AS VARCHAR(50)) + '%'
				OR yp.spm LIKE '%' + CAST(@srm AS VARCHAR(50)) + '%'
				OR yp.py LIKE '%' + CAST(@srm AS VARCHAR(50)) + '%'
			)
";
            var paraList = new List<DbParameter>
            {
                new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@srm", param.srm ?? ""),
                new SqlParameter("@Yplb", param.yplb ?? ""),
                new SqlParameter("@Ypjx", param.ypjx ?? ""),
                new SqlParameter("@OrganizeId", orgId)
            };
            return QueryWithPage<MedicineInfoVO>(sql, pagination, paraList.ToArray());
        }

        /// <summary>
        /// 获取本部门过期药品
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<MedicineInfoVO> GetExpiredDrugsData(Pagination pagination, MedicineInfoParam param, string orgId)
        {
            string strSql = @"select a.yxq,dl.dlmc yplb,a.yfbmCode,a.OrganizeId,a.ypdm ypCode,yp.ypId,yp.ypmc,yp.ycmc,yp.ypgg,jx.jxmc,a.kcsl,yp.lsj,yp.pfj,bmypxx.bmypId,NewtouchHIS_PDS.dbo.f_getyfbmDw(a.yfbmCode,a.ypdm,a.OrganizeId) deptDw,yp.zt ypzt from NewtouchHIS_PDS.dbo.xt_yp_kcxx(NOLOCK) a 
INNER JOIN NewtouchHIS_Base.dbo.V_C_xt_yp yp on yp.ypCode = a.ypdm and a.OrganizeId = yp.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_ypjx jx ON jx.jxCode = yp.jx
INNER JOIN NewtouchHIS_PDS.dbo.xt_yp_bmypxx(NOLOCK) bmypxx ON yp.ypCode=bmypxx.Ypdm AND yp.OrganizeId=bmypxx.OrganizeId and a.yfbmCode = bmypxx.yfbmCode
LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl dl on dl.dlCode=yp.dlCode AND dl.OrganizeId=yp.OrganizeId and dl.zt='1' 
WHERE a.kcsl > 0 AND a.yxq < GETDATE() AND a.yfbmCode =@yfbmCode AND a.OrganizeId = @OrganizeId
    AND ( yp.ypCode LIKE '%' + CAST(@ypCode AS VARCHAR(20))+ '%'
    OR yp.ypmc LIKE '%' + CAST(@ypCode AS VARCHAR(20)) + '%'
    OR yp.py LIKE '%' + CAST(@ypCode AS VARCHAR(20)) + '%') 
    AND yp.dlCode LIKE '%' + CAST(@dlCode AS VARCHAR(20)) + '%' AND yp.jx LIKE '%' + CAST(@jx AS VARCHAR(20)) + '%' AND yp.zt LIKE '%' + CAST(@zt AS VARCHAR(20)) + '%'";
            var paraList = new List<DbParameter>
            {
                new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@OrganizeId", orgId),
                new SqlParameter("@ypCode", param.ypdm ?? ""),
                new SqlParameter("@jx", param.ypjx ?? ""),
                new SqlParameter("@dlCode", param.yplb ?? ""),
                new SqlParameter("@zt", param.ypzt ?? "")
            };
            return QueryWithPage<MedicineInfoVO>(strSql, pagination, paraList.ToArray());
        }

        /// <summary>
        /// 获取药房人员列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysStaffVEntity> GetStaffListByOrg(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            var sql = "select * from [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] " +
                "where zt = 1 and OrganizeId = @orgId " +
                "and DepartmentCode in ( " +
                "select ksCode from [NewtouchHIS_Base].[dbo].[xt_yfbm] " +
                "where zt = 1 and OrganizeId = @orgId " +
                ")";
            return this.FindList<SysStaffVEntity>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

    }
}
