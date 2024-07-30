using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.Domain.VO;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using DbParameter = System.Data.Common.DbParameter;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 门诊处方
    /// </summary>
    public class MzCfRepo : RepositoryBase<MzCfEntity>, IMzCfRepo
    {
        public MzCfRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 修改发药标致 未发=>已排
        /// </summary>
        /// <param name="cfhs"></param>
        /// <returns></returns>
        public int UpdateFybzByCfh(List<string> cfhs)
        {
            if (cfhs == null || cfhs.Count <= 0) return 0;
            var strCfh = new StringBuilder();
            cfhs.ForEach(p => { strCfh.Append(p + ","); });
            const string sql = " UPDATE dbo.mz_cf SET fybz='1' WHERE cfh IN (@cfhs) AND fybz='0' ;";
            DbParameter[] param =
            {
                new SqlParameter("@cfhs", strCfh.ToString().Trim(','))
            };
            return ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 修改发药标致
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int UpdateFybzByCfh(string cfh, EnumFybz fybz, string organizeId)
        {
            const string sql = @"
UPDATE dbo.mz_cf SET fybz=@fybz 
WHERE cfh=@cfh and OrganizeId=@OrganizeId and zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@cfh", cfh),
                new SqlParameter("@fybz", ((int)fybz).ToString()),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return _dataContext.Database.ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// 修改发药标致 已排=>未发
        /// </summary>
        /// <param name="cfhs">处方号</param>
        /// <param name="organizeId">组织机构</param>
        /// <returns></returns>
        public int UpdateFybzToWFByCfh(List<string> cfhs, string organizeId)
        {
            if (cfhs == null || cfhs.Count <= 0) return 0;
            var strCfh = new StringBuilder();
            cfhs.ForEach(p => { strCfh.Append("'" + p + "',"); });
            string sql = string.Format(" UPDATE dbo.mz_cf SET fybz='0' WHERE cfh IN ({0}) AND OrganizeId = '{1}' AND fybz='1' ;", strCfh.ToString().Trim(','), organizeId);
            return ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// 获取未排药的处方
        /// </summary>
        /// <returns></returns>
        public List<MzCfEntity> GetNoArrangedCfList(DateTime? bT = null, DateTime? eT = null)
        {
            var sql = new StringBuilder(@"
SELECT [Id]
      ,[cfnm]
      ,[cfh]
      ,[jsnm]
      ,[CardNo]
      ,[xm]
      ,[Fph]
      ,[nl]
      ,[brxzmc]
      ,[ysmc]
      ,[ksmc]
      ,[sfsj]
      ,[OrganizeId]
      ,[CreatorCode]
      ,[CreateTime]
      ,[LastModiFierCode]
      ,[LastModifyTime]
      ,[Zje]
      ,[lyyf]
      ,[je]
      ,[fybz]
  FROM [dbo].[mz_cf](nolock)
  WHERE fybz=" + (int)EnumFybz.Wp);
            if (bT != null)
            {
                sql.AppendLine(" AND CreateTime >='" + ((DateTime)bT).ToString(Constants.DateTimeFormat) + "'");
            }
            if (eT != null)
            {
                sql.AppendLine(" AND CreateTime <='" + ((DateTime)eT).ToString(Constants.DateTimeFormat) + "'");
            }
            return FindList<MzCfEntity>(sql.ToString());
        }

        /// <summary>
        /// 获取处方
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="fybz"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IList<MzCfEntity> GetCfsByKeyword(string keyword, int fybz = (int)EnumFybz.Yp, Pagination pagination = null)
        {
            var sql = new StringBuilder(@"
SELECT [Id]
      ,[cfnm]
      ,[cfh]
      ,[jsnm]
      ,[CardNo]
      ,[xm]
      ,[Fph]
      ,[nl]
      ,[brxzmc]
      ,[ysmc]
      ,[ksmc]
      ,[sfsj]
      ,[OrganizeId]
      ,[CreatorCode]
      ,[CreateTime]
      ,[LastModiFierCode]
      ,[LastModifyTime]
      ,[Zje]
      ,[lyyf]
      ,[je]
      ,[fybz]
  FROM [dbo].[mz_cf](nolock)
  WHERE fybz=" + fybz);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" AND (xm LIKE @searchkeyValue OR CardNo LIKE @searchkeyValue OR Fph LIKE @searchkeyValue) ");
            }
            return QueryWithPage<MzCfEntity>(sql.ToString(), pagination, new[] { new SqlParameter("@searchkeyValue", "%" + (string.IsNullOrWhiteSpace(keyword) ? "" : keyword.Trim()) + "%") });
        }

        /// <summary>
        /// 返回去重后的姓名和收费时间
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        public IList<patientInfoVO> GetXmAndCardNo(Pagination pagination, string yfbmCode, string organizeId, string keyword = "", EnumFybz fybz = EnumFybz.Wp)
        {
            var sql = new StringBuilder(@"
SELECT DISTINCT cf.CardNo, cf.xm
FROM dbo.mz_cf(NOLOCK) cf
INNER JOIN dbo.mz_cfmx(NOLOCK) cfmx ON cfmx.cfh=cf.cfh AND cfmx.OrganizeId=cf.OrganizeId AND cfmx.zt='1' 
INNER JOIN dbo.mz_cfmxph(NOLOCK) mxph ON mxph.fyyf=cf.lyyf AND mxph.cfh=cf.cfh AND mxph.yp=cfmx.ypCode AND mxph.zt='1' AND mxph.gjzt='0' AND mxph.OrganizeId=cf.OrganizeId
WHERE cf.OrganizeId=@OrganizeId
AND cf.zt='1'
AND cf.lyyf=@yfbmCode
AND cf.jsnm>0 
AND cf.fybz=@fybz 
AND cfmx.Id=mxph.cfmxId
");
            var param = new List<DbParameter>
            {
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@fybz", ((int)fybz).ToString())
            };
            if (string.IsNullOrWhiteSpace(keyword)) return QueryWithPage<patientInfoVO>(sql.ToString(), pagination, param.ToArray());
            sql.AppendLine("AND (cf.xm LIKE @xm OR cf.CardNo LIKE @cardNo OR cf.Cfh LIKE @cfh)");
            param.Add(new SqlParameter("@xm", "%" + keyword + "%"));
            param.Add(new SqlParameter("@cardNo", "%" + keyword + "%"));
            param.Add(new SqlParameter("@cfh", "%" + keyword + "%"));
            return QueryWithPage<patientInfoVO>(sql.ToString(), pagination, param.ToArray());
        }

        /// <summary>
        /// 根据卡号和姓名获取处方信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public cfInfoVo GetCfInfo(string cardNo, string xm, int fybz = 0, string organizeId = "")
        {
            string sql = @"
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#fph') and type='U')
BEGIN
	DROP TABLE #fph;
END

DECLARE @zje NUMERIC(9,2);
DECLARE @cfh VARCHAR(500), @tmpCfh VARCHAR(50), @cfhComplete VARCHAR(MAX);
DECLARE @fph VARCHAR(500), @tmpfph VARCHAR(50), @FphComplete VARCHAR(MAX);
SET @cfh='';
SET @fph='';
SET @cfhComplete='';
SET @FphComplete='';

PRINT '1'

SELECT DISTINCT ISNULL(Fph, '') Fph 
INTO #fph FROM dbo.mz_cf(NOLOCK) 
WHERE fybz=@fybz AND xm=@xm AND CardNo=@CardNo AND zt = @zt AND jsnm>0 AND sfsj<=GETDATE()

WHILE EXISTS(SELECT 1 FROM #fph)
BEGIN
	PRINT '2'
	SELECT TOP 1 @tmpfph=Fph FROM #fph;
	IF (LEN(@fph)+LEN(@fph)+1)>597 AND (LEN(@fph)+LEN(@fph)+1)<=601
	BEGIN
		PRINT '2.1'
		SET @fph=@fph+'...';
	END
    ELSE IF (LEN(@fph)+LEN(@fph)+1)<=597
    BEGIN
		PRINT '2.2'
		SET @fph=@fph+','+@tmpfph;
	END
    SET @FphComplete=@FphComplete+','+@tmpfph;

	PRINT '3'
	IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#cfh') and type='U')
	BEGIN
		DROP TABLE #cfh;
	END
	SELECT DISTINCT ISNULL(cfh, '') cfh INTO #cfh FROM dbo.mz_cf(NOLOCK) WHERE fybz=@fybz AND ISNULL(Fph, '')=@tmpfph AND xm=@xm AND CardNo=@CardNo AND zt = @zt
	WHILE EXISTS(SELECT 1 FROM #cfh)
	BEGIN
		PRINT '4'
		SELECT TOP 1 @tmpCfh=cfh FROM #cfh;
		IF (LEN(@cfh)+LEN(@tmpCfh)+1)>597 AND (LEN(@cfh)+LEN(@tmpCfh)+1)<=601
		BEGIN
			PRINT '4.1';
			SET @cfh=@cfh+'...';
		END
		ELSE IF (LEN(@cfh)+LEN(@tmpCfh)+1)<=597
		BEGIN
			PRINT '4.2';
			SET @cfh=@cfh+','+@tmpCfh;
		END
		SET @cfhComplete=@cfhComplete+','+@tmpCfh;

		DELETE FROM #cfh WHERE cfh=@tmpCfh
	END

	DELETE FROM #fph WHERE Fph=@tmpfph
END

PRINT '5'
SELECT @zje=SUM(ISNULL(b.je, 0)) FROM dbo.mz_cf(NOLOCK) a 
INNER JOIN dbo.mz_cfmx(NOLOCK) b on a.cfh = b.cfh and a.OrganizeId = b.OrganizeId
WHERE a.fybz=@fybz AND a.xm=@xm AND a.CardNo=@CardNo AND a.zt = @zt
PRINT '6'
SELECT (CASE WHEN LEN(@cfh)>0 THEN SUBSTRING(@cfh, 2, LEN(@cfh)-1) ELSE @cfh END) [cfh]
	  ,(CASE WHEN LEN(@fph)>0 THEN SUBSTRING(@fph, 2, LEN(@fph)-1) ELSE @fph END) [Fph]
	  , @zje Zje, cf.[Id]
      ,cf.[cfnm]
      ,cf.[jsnm]
      ,cf.[CardNo]
      ,cf.[xm]
      ,cf.[nl]
      ,cf.[brxzmc]
      ,cf.[ysmc]
      ,cf.[ksmc]
      ,cf.[sfsj]
      ,CONVERT(VARCHAR(23), cf.[sfsj], 120) ShowSfsj
      ,cf.[OrganizeId]
      ,cf.[lyyf]
      ,cf.[je]
      ,cf.[fybz] 
      ,(CASE WHEN LEN(@FphComplete)>0 THEN SUBSTRING(@FphComplete, 2, LEN(@FphComplete)-1) ELSE @FphComplete END) FphComplete
      ,(CASE WHEN LEN(@cfhComplete)>0 THEN SUBSTRING(@cfhComplete, 2, LEN(@cfhComplete)-1) ELSE @cfhComplete END) cfhComplete
FROM dbo.mz_cf(NOLOCK) cf
WHERE cf.fybz=@fybz 
AND cf.xm=@xm 
AND cf.CardNo=@CardNo
AND cf.OrganizeId=@Organizeid
AND cf.zt = @zt
";
            var param = new List<DbParameter>
            {
                new SqlParameter("@fybz", fybz.ToString()),
                new SqlParameter("@xm", xm),
                new SqlParameter("@CardNo", cardNo),
                new SqlParameter("@Organizeid", organizeId),
                new SqlParameter("@zt", "1")
            };
            return FindList<cfInfoVo>(sql, param.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// 根据卡号和姓名获取处方信息
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<MzCfEntity> GetRpInfo(string yfbmCode, string cardNo, string xm, EnumFybz fybz = EnumFybz.Yp, string organizeId = "")
        {
            const string sql = @"
SELECT * 
FROM dbo.mz_cf(NOLOCK) cf
WHERE cf.CardNo=@CardNo
AND cf.OrganizeId=@OrganizeId
AND cf.xm=@xm
AND cf.fybz=@fybz
AND cf.zt='1'
AND cf.lyyf=@yfbmCode
";
            var param = new DbParameter[]
            {
                new SqlParameter("@CardNo", cardNo),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@xm", xm),
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@fybz", ((int)fybz).ToString()),
            };
            return FindList<MzCfEntity>(sql, param);
        }

        /// <summary>
        /// 根据卡号和姓名获取处方信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<MzCfEntity> GetCfs(string cardNo, string xm, int fybz = 0, string organizeId = "")
        {
            return IQueryable().Where(p => p.CardNo == cardNo && p.xm == xm && p.zt == "1" && p.fybz == fybz.ToString() && p.OrganizeId == organizeId).ToList();
        }

        /// <summary>
        /// 根据卡号和姓名获取处方信息
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="ispay"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<MzCfEntity> GetCf(string yfbmCode, string cardNo, string xm, EnumFybz fybz, bool ispay, string organizeId)
        {
            var sql = new StringBuilder(@"
SELECT * FROM dbo.mz_cf(NOLOCK) 
WHERE CardNo LIKE @cardNo
AND xm LIKE @xm
AND fybz=@fybz
AND OrganizeId=@OrganizeId
AND zt='1'
AND lyyf=@yfbmCode
");
            var param = new DbParameter[]
            {
                new SqlParameter("@cardNo", cardNo??""),
                new SqlParameter("@xm",xm ??""),
                new SqlParameter("@fybz",((int)fybz).ToString() ),
                new SqlParameter("@yfbmCode",yfbmCode),
                new SqlParameter("@OrganizeId",organizeId )
            };
            if (ispay)
            {
                sql.AppendLine("AND jsnm>0 ");
            }

            return _dataContext.Database.SqlQuery<MzCfEntity>(sql.ToString(), param).ToList();
        }

        /// <summary>
        /// 查询已发或者已退处方 默认显示前500条
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public IList<CfxxVO> SelectDispensedRpList(searchFyInfoReqVO p)
        {
            const string sql = @"
SELECT DISTINCT s.xm, s.CardNo, s.cfh, s.Fph, s.ysmc, s.fybz, s.sfsj, s.CreateTime, s.OrganizeId, s.fysj 
FROM (
	SELECT d.xm, d.CardNo, d.cfh, d.Fph, d.ysmc, d.fybz, d.sfsj, d.CreateTime, d.OrganizeId, MAX(d.CreateTime) fysj, SUM(d.sl) sl
	FROM(
		SELECT cf.xm, cf.CardNo, cf.cfh, cf.Fph, cf.ysmc, cf.fybz, cf.sfsj, cf.CreateTime, cf.OrganizeId, czjl.CreateTime fysj
		,ISNULL(mxph.sl,0) sl, mxph.ph, mxph.pc, cfmx.ypCode
		FROM dbo.mz_cf(NOLOCK) cf
		LEFT JOIN dbo.mz_cfmx(NOLOCK) cfmx ON cfmx.cfh=cf.cfh AND cfmx.OrganizeId=cf.OrganizeId AND cfmx.zt='1'
		LEFT JOIN dbo.mz_cfmxph(NOLOCK) mxph ON mxph.cfh=cf.cfh AND mxph.yp=cfmx.ypCode AND mxph.OrganizeId=cf.OrganizeId AND mxph.zt='1' AND mxph.gjzt='0' AND mxph.fyyf=cf.lyyf 
		LEFT JOIN dbo.mz_cfypczjl(NOLOCK) czjl ON czjl.mzcfmxId=cfmx.Id AND czjl.operateType='1'
		WHERE cf.fybz IN ('2','3')
		AND czjl.CreateTime BETWEEN @kssj AND @jssj
		AND ISNULL(cf.xm, '') LIKE '%'+@xm+'%'
		AND ISNULL(cf.CardNo, '') LIKE '%'+@CardNo+'%'
		AND ISNULL(cf.Fph, '') LIKE '%'+@fph+'%'
		AND ISNULL(cf.cfh, '') LIKE '%'+@cfh+'%'
		AND ISNULL(cf.lyyf, '')=@yfbmCode
		AND ISNULL(cf.OrganizeId, '')=@OrganizeId
		AND cf.zt='1'
		UNION ALL
		SELECT cf.xm, cf.CardNo, cf.cfh, cf.Fph, cf.ysmc, cf.fybz, cf.sfsj, cf.CreateTime, cf.OrganizeId, czjl.CreateTime fysj
		,-1*ISNULL(tfmx.sl,0) sl, tfmx.ph, tfmx.pc, cfmx.ypCode
		FROM dbo.mz_cf(NOLOCK) cf
		LEFT JOIN dbo.mz_cfmx(NOLOCK) cfmx ON cfmx.cfh=cf.cfh AND cfmx.OrganizeId=cf.OrganizeId AND cfmx.zt='1'
		LEFT JOIN dbo.mz_tfmx(NOLOCK) tfmx ON tfmx.cfh=cf.cfh AND tfmx.ypCode=cfmx.ypCode AND tfmx.OrganizeId=cf.OrganizeId AND tfmx.zt='1' 
		LEFT JOIN dbo.mz_cfypczjl(NOLOCK) czjl ON czjl.mzcfmxId=cfmx.Id AND czjl.operateType='1'
		WHERE cf.fybz IN ('2','3')
		AND czjl.CreateTime BETWEEN @kssj AND @jssj
		AND ISNULL(cf.xm, '') LIKE '%'+@xm+'%'
		AND ISNULL(cf.CardNo, '') LIKE '%'+@CardNo+'%'
		AND ISNULL(cf.Fph, '') LIKE '%'+@fph+'%'
		AND ISNULL(cf.cfh, '') LIKE '%'+@cfh+'%'
		AND ISNULL(cf.lyyf, '')=@yfbmCode
		AND ISNULL(cf.OrganizeId, '')=@OrganizeId
		AND cf.zt='1'
	) d
	GROUP BY d.xm, d.CardNo, d.cfh, d.Fph, d.ysmc, d.fybz, d.sfsj, d.CreateTime, d.OrganizeId, d.ph, d.pc, d.ypCode
) s
WHERE s.sl>0
";
            var param = new DbParameter[]
            {
                new SqlParameter("@xm", p.xm??""),
                new SqlParameter("@CardNo", p.kh??""),
                new SqlParameter("@fph",p.fph??"" ),
                new SqlParameter("@cfh", p.cfh??""),
                new SqlParameter("@OrganizeId", p.OrganizeId??""),
                new SqlParameter("@yfbmCode",p.yfbmCode??"" ),
                new SqlParameter("@kssj", p.begindate),
                new SqlParameter("@jssj", p.enddate)
            };
            return FindList<CfxxVO>(sql, param);
        }

        /// <summary>
        /// 批量插入处方信息 返回受影响行
        /// </summary>
        /// <param name="mzCfs"></param>
        /// <returns></returns>
        public int InsertBatch(List<MzCfEntity> mzCfs)
        {
            var param = new List<DbParameter>
            {
                new SqlParameter("@mzCf", mzCfs.ToDataTable())
                {
                     SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.mzcf"
                }
            };
            var outpar = new SqlParameter("@Res", SqlDbType.Int, 100)
            {
                Direction = ParameterDirection.Output
            };
            param.Add(outpar);
            FindList<object>(@" EXEC [dbo].[sp_cf_insertBatch] @mzCf, @Res out", param.ToArray());
            var result = 0;
            int.TryParse(outpar.Value.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 删除老的处方信息
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="cfnm"></param>
        /// <returns></returns>
        public int DeleteCf(string cfh, long cfnm)
        {
            var param = new List<DbParameter>
            {
                new SqlParameter("@zt",0.ToString()),
                new SqlParameter("@cfh",cfh),
                new SqlParameter("@cfnm",cfnm)
            };
            return (string.IsNullOrWhiteSpace(cfh) || cfnm <= 0)
                ? 0
                : ExecuteSqlCommand(@" UPDATE dbo.mz_cf SET zt=@zt WHERE cfh=@cfh AND cfnm=@cfnm AND fybz='0' ;",
                    param.ToArray());
        }

        /// <summary>
        /// 作废处方，退费用
        /// </summary>
        /// <param name="cfh">处方号</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int DeleteCf(string cfh, string organizeId)
        {
            var param = new List<DbParameter>
            {
                new SqlParameter("@zt",0.ToString()),
                new SqlParameter("@cfh",cfh),
                new SqlParameter("@OrganizeId",organizeId)
            };
            return (string.IsNullOrWhiteSpace(cfh))
                ? 0
                : ExecuteSqlCommand(@" UPDATE dbo.mz_cf SET zt=@zt WHERE cfh=@cfh AND OrganizeId=@OrganizeId AND zt = '1';",
                    param.ToArray());
        }

        /// <summary>
        /// 根据处方号获取有效的处方
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<MzCfEntity> SelectRpList(string cfh, string organizeId)
        {
            const string sql = "SELECT * FROM dbo.mz_cf(NOLOCK) WHERE cfh=@cfh AND OrganizeId=@OrganizeId AND zt='1'";
            var param = new DbParameter[]
            {
                new SqlParameter("@cfh",cfh ),
                new SqlParameter("@OrganizeId",organizeId )
            };
            return FindList<MzCfEntity>(sql, param);
        }

        /// <summary>
        /// 更新性别
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="xb"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public int UpdateGender(string cfh, string xb, string orgId)
        {
            var param = new DbParameter[]
            {
                new SqlParameter("@xb", xb),
                new SqlParameter("@cfh", cfh),
                new SqlParameter("@OrganizeId", orgId)
            };
            return ExecuteSqlCommand(
                "UPDATE dbo.mz_cf SET xb=@xb WHERE cfh=@cfh AND OrganizeId=@OrganizeId AND zt='1' ", param);
        }
    }
}
