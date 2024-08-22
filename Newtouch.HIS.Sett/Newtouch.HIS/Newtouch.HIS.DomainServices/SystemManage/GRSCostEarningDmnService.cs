using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Interface;
using Newtouch.HIS.Domain.Entity.SystemManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.HIS.DomainServices
{
    public class GRSCostEarningDmnService : DmnServiceBase, IGRSCostEarningDmnService
    {

        private readonly Ijgss_CostInfoRepo _jgss_CostInfoRepo;
        private readonly Ijgss_EarningDetailInfoRepo jgss_EarningDetailInfoRepo;
        private readonly Ijgss_EarningInfoRepo jgss_EarningInfoRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ICache _cache;
        private readonly Ijgss_AttachmentInfoRepo _jgss_AttachmentInfoRepo;
        public GRSCostEarningDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {


        }

        /// <summary>
        /// 批量添加成本信息
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="srxxId"></param>
        public void AddCostList(List<jgssCostVO> vo, string srxxId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (vo != null && vo.Count() > 0)
                {
                    foreach (var item in vo)
                    {
                        jgss_CostInfoEntity entity = new jgss_CostInfoEntity();
                        entity.srxxId = srxxId;
                        entity.je = item.je;
                        entity.Create(true);
                        item.MapperTo(entity);
                        db.Insert(entity);
                    }
                    db.Commit();
                }
            }
        }

        /// <summary>
        /// 新增站点收支统计表
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="keyvalue"></param>
        /// <param name="orgId"></param>
        public void submitEarningInfo(SiteCostEarningVO vo, string keyvalue, string orgId)
        {
            //修改
            if (!string.IsNullOrWhiteSpace(keyvalue))
            {
                var srxxentity = jgss_EarningInfoRepo.FindEntity(p => p.Id == keyvalue && p.zt == "1");
                if (srxxentity != null)
                {
                    using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                    {
                        db.Delete<jgss_EarningInfoEntity>(p => p.Id == keyvalue && p.zt == "1");
                        db.Delete<jgss_EarningDetailInfoEntity>(p => p.srxxId == keyvalue && p.zt == "1");
                        db.Delete<jgss_CostInfoEntity>(p => p.srxxId == keyvalue && p.zt == "1");
                        db.Delete<jgss_AttachmentInfoEntity>(p => p.srxxId == keyvalue && p.zt == "1");
                        db.Commit();
                    }
                }
            }

            if (vo == null) return;
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var srxxId = "";
                if (vo.srxx != null)
                {
                    var srxxentity = jgss_EarningInfoRepo.FindEntity(p => p.siteId == vo.srxx.siteId && p.year == vo.srxx.year && p.month == vo.srxx.month && p.zt == "1");
                    if (srxxentity != null)
                    {
                        throw new FailedException("该机构" + vo.srxx.year + "年" + vo.srxx.month + "月收支数据已统计，请核实");
                    }
                    //GRS收入信息
                    var srxx = new jgss_EarningInfoEntity();
                    vo.srxx.MapperTo(srxx);
                    if (string.IsNullOrWhiteSpace(keyvalue))
                    {
                        srxx.Create(true);
                    }
                    else
                    {
                        srxx.Create(true);
                    }

                    srxxId = srxx.Id;
                    db.Insert(srxx);
                    //共担成本比例

                    var fcentity = db.FindEntity<SysConfigEntity>(p => p.Code == "jgssfdbl" && p.zt == "1" && p.OrganizeId == orgId);
                    if (fcentity != null)
                    {
                        fcentity.Value = vo.srxx.jgssfdbl;
                        db.Update(fcentity);
                    }
                    else
                    {
                        var config = new SysConfigEntity
                        {
                            Code = "jgssfdbl",
                            Name = "机构收支共担成本GRS分担比例",
                            Value = vo.srxx.jgssfdbl,
                            Memo = "GRS实收计算时用到,单位 %",
                            OrganizeId = orgId
                        };
                        config.Create(true);
                        db.Insert(config);
                    }
                    _cache.Remove("set:systemconfig_" + orgId);//清空配置的缓存，否则下次不生效

                    if (vo.srxxList != null && vo.srxxList.Count > 0)
                    {
                        //GRS收入信息详情表
                        foreach (var item in vo.srxxList)
                        {
                            var earning = new jgss_EarningDetailInfoEntity { srxxId = srxxId };
                            item.MapperTo(earning);
                            earning.Create(true);
                            db.Insert(earning);
                        }
                    }

                    if (vo.cbxxList != null && vo.cbxxList.Count > 0)
                    {
                        foreach (var item in vo.cbxxList)
                        {
                            var cost = new jgss_CostInfoEntity { srxxId = srxxId };
                            item.MapperTo(cost);
                            cost.Create(true);
                            db.Insert(cost);
                        }
                    }

                    if (vo.fjxx != null && vo.fjxx.Count > 0)
                    {
                        foreach (var item in vo.fjxx)
                        {
                            var atta = new jgss_AttachmentInfoEntity { srxxId = srxxId };
                            item.MapperTo(atta);
                            atta.Create(true);
                            db.Insert(atta);
                        }
                    }
                    db.Commit();
                }
            }
            //更新实收
            Getss(vo.srxx.siteId, vo.srxx.year, vo.srxx.month);
        }

        /// <summary>
        /// 查看站点收支统计表
        /// </summary>
        /// <param name="srxxId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SiteCostEarningVO GetEarningInfo(string srxxId, string orgId)
        {
            var rtnvo = new SiteCostEarningVO
            {
                cbxxList = new List<jgssCostVO>(),
                srxx = new jgssEarningVO(),
                srxxList = new List<jgssEarningGridVO>(),
                fjxx = new List<jgssAttachmentVO>()
            };
            var srxx = jgss_EarningInfoRepo.FindEntity(p => p.Id == srxxId && p.zt == "1");

            rtnvo.srxx = srxx.MapperTo<jgss_EarningInfoEntity, jgssEarningVO>();
            if (!string.IsNullOrWhiteSpace(srxx.shr))
            {
                rtnvo.srxx.shr = _sysUserDmnService.GetNameByAccount(srxx.shr, Constants.TopOrganizeId);
            }
            if (!string.IsNullOrWhiteSpace(srxx.CreatorCode))
            {


                rtnvo.srxx.cjr = _sysUserDmnService.GetNameByAccount(srxx.CreatorCode, srxx.siteId);
            }

            var srxxlist = jgss_EarningDetailInfoRepo.IQueryable().Where(p => p.srxxId == srxxId && p.zt == "1").ToList();
            if (srxxlist.Count > 0)
            {
                foreach (var t in srxxlist)
                {
                    var srlist = t.MapperTo<jgss_EarningDetailInfoEntity, jgssEarningGridVO>();
                    rtnvo.srxxList.Add(srlist);
                }
            }

            var fjxxlist = _jgss_AttachmentInfoRepo.IQueryable().Where(p => p.srxxId == srxxId && p.zt == "1").ToList();
            if (fjxxlist.Any())
            {
                foreach (var t in fjxxlist)
                {
                    var list = t.MapperTo<jgss_AttachmentInfoEntity, jgssAttachmentVO>();
                    rtnvo.fjxx.Add(list);
                }
            }

            const string strsql = @"
SELECT  cbxx.Id ,
cbxx.cblb ,
cbxx.kmcode ,
CASE cbxx.cblb
WHEN 'GRSbillObj' THEN 'GRS成本'
WHEN 'ShareObj' THEN '共担科目'
END lbmc ,
itmd.Name kmmc ,
cbxx.je
FROM    dbo.jgss_cbxx cbxx
LEFT JOIN NewtouchHIS_Base..V_C_Sys_ItemsDetail itmd ON itmd.CateCode = cbxx.cblb
AND itmd.Code = cbxx.kmcode
AND (itmd.OrganizeId = @orgId or                                          itmd.OrganizeId='*')
WHERE   cbxx.srxxId = @srxxId order by cbxx.cblb
";

            var inSqlParameterList = new DbParameter[]
            {
                new SqlParameter("@orgId", srxx.siteId),
                new SqlParameter("@srxxId", srxxId)
            };
            rtnvo.cbxxList = FindList<jgssCostVO>(strsql, inSqlParameterList).ToList();

            return rtnvo;
        }

        /// <summary>
        /// 站点收支统计表-收入信息详情
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<jgssSiteCostEarningVO> GetCosttable(string site, string year, string month)
        {
            var paraList = new DbParameter[]
            {
                new SqlParameter("@orgId", site),
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };
            return FindList<jgssSiteCostEarningVO>(@"EXEC dbo.jgss_incometable @orgId = @orgId, @year = @year,@month=@month", paraList);
        }

        public List<jgssxxGridVO> GetjgszInfoList(Pagination pagination, string siteId, string year, string month, string shzt, bool verify)
        {
            var rtnlist = new List<jgssxxGridVO>();
            StringBuilder strsql = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
            strsql.Append(@"SELECT   sr.Id ,o.Name zdmc ,
                            sr.[year] ,
                            sr.[month] ,
                            sr.zsr ,
                           ( SELECT    ISNULL(SUM(je), 0)
                          FROM      jgss_cbxx cbxx
                          WHERE     cbxx.srxxId = sr.Id
                                    AND cbxx.cblb = 'ShareObj'
                                    AND cbxx.zt = '1'
                        ) gdcb ,
                        ( SELECT    ISNULL(SUM(je), 0)
                          FROM      jgss_cbxx cbxx
                          WHERE     cbxx.srxxId = sr.Id
                                    AND cbxx.cblb = 'GRSbillObj'
                                    AND cbxx.zt = '1'
                        ) grscb ,
                            sr.jgss ,
                            sr.grsss,
                            ( SELECT    SUM(hssr)
                              FROM      dbo.jgss_srxxdetail
                              WHERE     srxxId = sr.Id
                                        AND zt = '1'
                            ) hssr ,
                            ( SELECT    SUM(ce)
                              FROM      dbo.jgss_srxxdetail
                              WHERE     srxxId = sr.Id
                                        AND zt = '1'
                            ) ce ,
		                    sr.shzt
                    FROM    dbo.jgss_srxx sr
                            LEFT JOIN dbo.jgss_srxxdetail srlist ON sr.Id = srlist.srxxId
                                                                    AND srlist.zt = '1'
                            LEFT JOIN dbo.jgss_cbxx gdcb ON gdcb.srxxId = sr.Id
                                                            AND gdcb.cblb = 'ShareObj'
                                                            AND gdcb.zt = '1'
                            LEFT JOIN dbo.jgss_cbxx grscb ON grscb.srxxId = sr.Id
                                                             AND grscb.zt = '1'
                            LEFT JOIN NewtouchHIS_Base..V_S_Sys_Organize o ON o.Id = sr.siteId
                                                                              AND o.zt = '1'
                    WHERE   (sr.siteId in (select * from dbo.f_split(@siteId,',')) or isnull(@siteId,'-1')='-1')
                            AND (sr.[year] = @year or isnull(@year,'-1')='-1')
                            AND (sr.[month] = @month or isnull(@month,'-1')='-1')
                            AND sr.zt = '1'
                            and (sr.shzt in (select * from dbo.f_split(@shzt,',')) or isnull(@shzt,'')='')
                    GROUP BY o.Name ,
                            sr.[year] ,
                            sr.[month] ,
                            sr.zsr ,
                            sr.jgss ,
                            sr.grsss,sr.Id,sr.shzt");
            inSqlParameterList.Add(new SqlParameter("@siteId", siteId));
            inSqlParameterList.Add(new SqlParameter("@year", year));
            inSqlParameterList.Add(new SqlParameter("@month", month));
            if (string.IsNullOrWhiteSpace(shzt) && verify)
            {
                inSqlParameterList.Add(new SqlParameter("@shzt", (int)EnumOrgshzt.DS + "," + (int)EnumOrgshzt.TG + "," + (int)EnumOrgshzt.WTG + "," + (int)EnumOrgshzt.YTJ));
            }
            else
            {
                inSqlParameterList.Add(new SqlParameter("@shzt", shzt ?? ""));
            }

            return this.QueryWithPage<jgssxxGridVO>(strsql.ToString(), pagination, inSqlParameterList.ToArray()).ToList();
        }

        /// <summary>
        /// 查看站点收费金额详情
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="siteId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<jgszchargemoneyVO> GetMoneyDetailList(Pagination pagination, string siteId, string year, string month, string type, string dlcode)
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
                new SqlParameter("@type", type),
                new SqlParameter("@orgId", siteId),
                new SqlParameter("@year", year),
                new SqlParameter("@month", month),
                new SqlParameter("@dlcode", dlcode),
                new SqlParameter("@currPageIndex", pagination.page),
                new SqlParameter("@perRows", pagination.rows),
                new SqlParameter("@orderByParam", sortby)
            };
            var outParameter = new SqlParameter("@records", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            paraList.Add(outParameter);
            var list = FindList<jgszchargemoneyVO>("exec jgss_GetMoneyDetailList @type,@orgId,@year,@month,@dlcode,@currPageIndex,@perRows,@orderByParam, @records output", paraList.ToArray());
            pagination.records = outParameter.Value.ToInt();
            return list;
        }

        /// <summary>
        /// 查看站点收费金额详情
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="type"></param>
        /// <param name="dlCode"></param>
        /// <returns></returns>
        public IList<jgszchargemoneyVO> GetMoneyDetailList(string siteId, string year, string month, string type, string dlCode)
        {
            var sql = "";
            switch (type)
            {
                case "outpatient":
                    #region outpatient sql
                    sql = @"
--门诊
DECLARE @tmpData TABLE(
	sfxmmc VARCHAR(256),
	sfxmcode VARCHAR(20),
	dj NUMERIC(11,4),
	sl INT,
	zje NUMERIC(11,2),
	xm VARCHAR(50),
	patid INT
)
DECLARE @cnt INT;
SELECT  @cnt = cnt
FROM    [dbo].[CntByOrgId](@orgId);
IF ( @cnt > 0 )--存在执行表记录，按照执行逻辑走,否则按照mz_xm逻辑走
BEGIN
	INSERT INTO @tmpData ( sfxmmc, sfxmcode, dj, sl, zje, xm, patid )
	SELECT  sfxm.sfxmmc ,
            sfxm.sfxmcode,
			xm.dj  dj,
			CAST(sum(jsmx.sl) AS INT) sl ,
			sum(ISNULL(xm.dj, 0) * ISNULL(jsmx.sl, 0)) zje,xx.xm,
			xm.patid
	FROM    mz_jzjhmx(NOLOCK) mx
			LEFT JOIN mz_jzjh (NOLOCK) jzjh ON jzjh.jzjhId = mx.jzjhId AND jzjh.OrganizeId =@orgId AND jzjh.zt = '1'
			LEFT JOIN mz_gh (NOLOCK) mzgh ON mzgh.ghnm = jzjh.ghnm AND mzgh.OrganizeId =@orgId AND mzgh.zt = '1' AND jzjh.OrganizeId =@orgId
			LEFT JOIN mz_xm xm ON xm.jzjhmxId = mx.jzjhmxId AND xm.OrganizeId =@orgId
			LEFT JOIN xt_brjbxx xx ON xx.patid = xm.patid AND xx.OrganizeId =@orgId
			LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm AND sfxm.OrganizeId =@orgId AND sfxm.zt = '1'
			LEFT JOIN [NewtouchHIS_Base]..V_S_xt_sfdl sfdl ON xm.dl = sfdl.dlCode AND sfdl.OrganizeId =@orgId AND sfdl.zt='1'
			LEFT join mz_jsmx jsmx on jsmx.mxnm=xm.xmnm and jsmx.OrganizeId=@orgId
			left join mz_js js on js.jsnm=jsmx.jsnm and js.OrganizeId=@orgId
	WHERE   mx.OrganizeId =@orgId
			AND xm.zt = 1
			AND mx.zt = 1
			AND jzjh.zt = 1
			AND mzgh.zt = 1
			AND xx.zt = 1
			AND ISNULL(js.tbz, 0) < 1
			AND js.zt=1
			AND YEAR(mx.jzsj) =@year
			AND MONTH(mx.jzsj) = @month
			AND xm.dl = @dlcode
			AND mx.jzsj IS NOT NULL 
	GROUP by sfxm.sfxmcode,sfxmmc,xx.xm,xm.dj,xm.patid

	;WITH tt AS (
		SELECT	xm.sfxm sfxmcode,
				sum(xm.je) jhzje,
				jz.patid
		FROM	mz_jzjhmx(NOLOCK) jzmx
				INNER JOIN mz_jzjh (NOLOCK) jz ON jz.jzjhId = jzmx.jzjhId AND jz.OrganizeId =jzmx.OrganizeId AND jz.zt = '1'
				INNER JOIN mz_xm(NOLOCK) xm ON xm.jzjhmxId = jzmx.jzjhmxId AND xm.OrganizeId =jzmx.OrganizeId AND xm.zt = '1'
				LEFT JOIN [NewtouchHIS_Base]..V_S_xt_sfdl sfdl ON xm.dl = sfdl.dlCode AND sfdl.OrganizeId =jzmx.OrganizeId AND sfdl.zt='1'
		WHERE	jzmx.OrganizeId =@orgId				
				AND jzmx.zt = '1'
				AND YEAR(jzmx.jzsj) =@year
				AND MONTH(jzmx.jzsj) = @month
				AND xm.dl = @dlcode
				AND jzmx.jzsj IS NOT NULL 
		GROUP BY xm.sfxm, jz.patid
	)
	
	SELECT a.sfxmmc, a.sfxmcode, a.dj, a.sl, a.xm
	,a.zje --结算总金额
	,b.jhzje --记账计划总金额
	,b.jhzje-a.zje tzje --退总金额
	FROM @tmpData a
	LEFT JOIN tt b ON b.patid=a.patid AND a.sfxmcode=b.sfxmCode
	
	END
ELSE
BEGIN
	SELECT  sfxm.sfxmCode,
			sfxm.sfxmmc ,
			xm.dj dj,
			CAST(sum(xm.sl)  AS INT) sl,
			sum(ISNULL(xm.dj, 0) * ISNULL(xm.sl, 0)) zje,
			xx.xm
			,sum(ISNULL(xm.dj, 0) * ISNULL(xm.sl, 0)) jhzje --记账计划总金额
			,0 tzje --退总金额
	FROM    mz_xm(NOLOCK) xm
			LEFT JOIN mz_gh (NOLOCK) mzgh ON mzgh.ghnm = xm.ghnm AND mzgh.OrganizeId = @orgId AND mzgh.zt = '1'
			LEFT JOIN xt_brjbxx xx ON xx.patid = xm.patid AND xx.OrganizeId = @orgId
			LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm AND sfxm.OrganizeId =@orgId
			LEFT JOIN [NewtouchHIS_Base]..V_S_xt_sfdl sfdl ON xm.dl = sfdl.dlCode AND sfdl.OrganizeId =@orgId
	WHERE   xm.OrganizeId = @orgId
			AND xm.zt = 1
			AND mzgh.zt = 1
			AND xx.zt = 1
			AND YEAR(ISNULL(xm.ssrq, xm.jsrq)) = @year
			AND MONTH(ISNULL(xm.ssrq, xm.jsrq)) =@month
			AND xm.dl =  @dlcode
	group by sfxm.sfxmCode,sfxm.sfxmmc,xx.xm,xm.dj,mzgh.patid
END
";
                    #endregion
                    break;
                case "inpatient":
                    #region inpatient sql
                    sql = @"
--住院
DECLARE @tmpData TABLE(
	Id VARCHAR(50),
	sfxmmc VARCHAR(256),
	sfxmcode VARCHAR(20),
	dj NUMERIC(11,4),
	sl INT,
	zje NUMERIC(11,2),
	xm VARCHAR(50),
	patid INT,
	zyh VARCHAR(50),
	zxzt INT, -- 3：已停止
	Processed INT -- 0:未处理  1：已处理
)
INSERT INTO @tmpData ( Id, sfxmmc, sfxmcode, dj, sl, zje, xm, patid, zyh, zxzt, Processed)
SELECT  NEWID() Id,
		sfxm.sfxmmc ,
		sfxm.sfxmCode,
		jzmx.jzjhmxId,
		jzmx.dj,
		CAST(sum(jzmx.sl)  AS INT) sl,
		SUM(ISNULL(jzmx.dj, 0) * ISNULL(jzmx.sl, 0)) zje,
		xx.xm,
		xx.patid,
		zyjz.zyh,
		jzmx.zxzt,
		0
FROM	dbo.zy_jzjh(NOLOCK) zyjz
		INNER JOIN dbo.zy_jzjhmx(NOLOCK) jzmx ON jzmx.jzjhId = zyjz.jzjhId	AND jzmx.OrganizeId = zyjz.OrganizeId AND jzmx.zt = '1'
		LEFT JOIN zy_brjbxx(NOLOCK) xx ON xx.zyh = zyjz.zyh	AND xx.OrganizeId =zyjz.OrganizeId
		INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm sfxm ON sfxm.sfxmCode = jzmx.sfxmCode AND sfxm.OrganizeId =zyjz.OrganizeId
		INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_sfdl sfdl ON sfxm.sfdlCode = sfdl.dlCode AND sfdl.OrganizeId =zyjz.OrganizeId
WHERE	zyjz.OrganizeId = @orgId
		AND xx.zt = 1
		AND zyjz.zt = 1
		AND YEAR(zyjz.CreateTime) = @year
		AND MONTH(zyjz.CreateTime) =@month
		AND sfdl.dlCode=@dlcode
GROUP BY sfxm.sfxmCode,sfxm.sfxmmc ,xx.xm,jzmx.dj,xx.patid, zyjz.zyh, jzmx.zxzt,jzmx.jzjhmxId

IF EXISTS(SELECT 1 FROM @tmpData WHERE zxzt=3)
BEGIN	
	WHILE EXISTS(SELECT 1 FROM @tmpData WHERE Processed=0)
	BEGIN
		DECLARE @Id VARCHAR(50), @zyh VARCHAR(50), @jzjhmxId VARCHAR(50), @xm VARCHAR(50), @sfxmCode VARCHAR(50), @sfxmmc VARCHAR(256), @dj NUMERIC(11,4), @patid INT, @zxzt INT, @sl INT;
		SELECT TOP 1 @Id=Id, @zyh=zyh, @xm=xm, @jzjhmxId=jzjhmxId, @sfxmCode=sfxmcode, @sfxmmc=sfxmmc, @dj=dj, @patid=patid, @zxzt=zxzt, @sl=sl FROM @tmpData WHERE Processed=0
		IF @zxzt=3
		BEGIN
			DECLARE @tsl INT;
			SELECT @tsl=@sl-SUM(sl) FROM dbo.zy_xmjfb(NOLOCK) WHERE zyh=@zyh AND jzjhmxId=@jzjhmxId AND OrganizeId=@orgId AND zt='1' AND sfxm=@sfxmCode
			IF ISNULL(@tsl,0)>0
			BEGIN
			INSERT INTO @tmpData( Id ,sfxmmc ,sfxmcode ,dj ,	sl ,	zje ,		xm ,patid ,zyh ,zxzt ,Processed)
					VALUES (NEWID(),@sfxmmc, @sfxmCode, @dj, -1*@tsl, -1*@tsl*@dj, @xm, @patid, @zyh, 3, 1)  
			END 
		END
		UPDATE @tmpData SET Processed=1 WHERE Id=@Id
	END
END 

SELECT xm, sfxmmc, sfxmcode, dj, sl, zje FROM @tmpData ORDER BY xm, sfxmmc, sl desc
";
                    #endregion
                    break;
                case "fzlxm":
                    #region fzlxm sql
                    sql = @"
--fzlxm
SELECT  sfxm.sfxmCode,
        sfxm.sfxmmc ,
        fzlxmjfb.dj ,
        sum(fzlxmjfb.sl) sl,
        sum(ISNULL(fzlxmjfb.dj, 0) * ISNULL(fzlxmjfb.sl, 0)) zje,
		fzlxmjfb.xm,
		sum(ISNULL(fzlxmjfb.dj, 0) * ISNULL(fzlxmjfb.sl, 0)) jhzje,
		0 tzje
FROM    [dbo].[V_C_Sys_HbtfFzlxmjfb] fzlxmjfb
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm ON sfxm.sfxmCode = fzlxmjfb.sfxmCode AND sfxm.OrganizeId = @orgId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl ON sfdl.dlCode = sfxm.sfdlCode AND sfdl.OrganizeId = @orgId
WHERE   fzlxmjfb.OrganizeId = @orgId
        AND YEAR(fzlxmjfb.jzrq) =@year
        AND MONTH(fzlxmjfb.jzrq) =  @month
		AND fzlxmjfb.dlCode= @dlcode
        AND fzlxmjfb.sl > 0
GROUP BY sfxm.sfxmCode,sfxm.sfxmmc ,fzlxmjfb.xm, fzlxmjfb.dj , fzlxmjfb.sl 
";
                    #endregion
                    break;
            }

            var param = new DbParameter[]
            {
                new SqlParameter("@orgId", siteId),
                new SqlParameter("@year", year),
                new SqlParameter("@month", month),
                new SqlParameter("@dlcode", dlCode)
            };
            return FindList<jgszchargemoneyVO>(sql, param);
        }

        /// <summary>
        /// 获取机构实收和GRS实收
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public jgssandgrsssVO Getss(string site, string year, string month)
        {
            var paraList = new DbParameter[]
               {
                new SqlParameter("@orgId", site),
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
               };
            return this.FirstOrDefault<jgssandgrsssVO>(@"EXEC dbo.jgss_jgssandgrsss @orgId = @orgId, @year = @year,@month=@month", paraList);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="srxxId"></param>
        public void deletess(string srxxId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var srxx = db.FindEntity<jgss_EarningInfoEntity>(p => p.Id == srxxId && p.zt == "1");
                srxx.zt = "0";
                db.Update<jgss_EarningInfoEntity>(srxx);


                var srxxlist = db.IQueryable<jgss_EarningDetailInfoEntity>(p => p.srxxId == srxxId && p.zt == "1").ToList();

                if (srxxlist != null && srxxlist.Count() > 0)
                {
                    foreach (var item in srxxlist)
                    {
                        item.zt = "0";
                        db.Update<jgss_EarningDetailInfoEntity>(item);
                    }
                }

                var cbxxlist = db.IQueryable<jgss_CostInfoEntity>(p => p.srxxId == srxxId && p.zt == "1").ToList();

                if (cbxxlist != null && cbxxlist.Count() > 0)
                {
                    foreach (var item in cbxxlist)
                    {
                        item.zt = "0";
                        db.Update<jgss_CostInfoEntity>(item);
                    }
                }

                var fjxxlist = db.IQueryable<jgss_AttachmentInfoEntity>(p => p.srxxId == srxxId && p.zt == "1").ToList();

                if (fjxxlist != null && fjxxlist.Count() > 0)
                {
                    foreach (var item in fjxxlist)
                    {
                        item.zt = "0";
                        db.Update<jgss_AttachmentInfoEntity>(item);
                    }
                }
                db.Commit();
            }
        }


        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns>文件上传路径</returns>
        public string UploadFile(HttpPostedFileBase file, out string parth)
        {
            parth = "";
            var result = "";
            if (!ValidateFile(file)) return result;
            try
            {
                var urn = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("LocalFileBaseDir");
                urn = string.IsNullOrWhiteSpace(urn) ? "\\结算系统" : urn;

                var url = CommmHelper.GetLocalFilePath("\\结算系统"); //HttpContext.Current.Request.MapPath(urn);
                if (!FileHelper.IsExistDirectory(url)) FileHelper.CreateDirectory(url);
                var ext = FileWebHelper.FileNameExtension(file.FileName);
                var newFileName = Guid.NewGuid() + ext;
                result = url + "/" + newFileName;
                parth = Path.Combine(url, newFileName);
                file.SaveAs(parth);

                return result;
            }
            catch (Exception e)
            {
                result = "false";
                throw new FailedException("上传附件失败" + e.InnerException);
            }
        }

        /// <summary>
        /// 文件效验
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ValidateFile(HttpPostedFileBase file)
        {
            if (!ValidateExt(file)) return false;
            if (!ValidateSize(file)) return false;
            return true;
        }

        /// <summary>
        /// 类型验证
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ValidateExt(HttpPostedFileBase file)
        {
            var ext = FileWebHelper.FileNameExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(ext)) return false;
            var legalFileUploadExt = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("legalFileUploadExt") ?? "";
            if (string.IsNullOrWhiteSpace(legalFileUploadExt))
            {
                throw new FailedException("缺少文件类型验证配置");
            }
            return legalFileUploadExt.ToLower().Contains(ext.ToLower());
        }

        /// <summary>
        /// 大小验证
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool ValidateSize(HttpPostedFileBase file)
        {
            var maxSize = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("jgzsMaxSize");
            maxSize = string.IsNullOrWhiteSpace(maxSize) ? 5.ToString() : maxSize;
            var size = file.ContentLength;
            if (string.IsNullOrWhiteSpace(maxSize))
            {
                throw new FailedException("缺少文件大小验证配置");
            }
            return size / 1024 / 1024 <= Convert.ToInt32(maxSize);
        }
        #endregion

        /// <summary>
        /// 获取上个月的成本信息
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<jgssCostVO> getLastcbxx(string site, string year, string month)
        {
            StringBuilder strsql = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
            strsql.Append(@"SELECT  cbxx.Id ,
                            cbxx.cblb ,
                            cbxx.kmcode ,
                            CASE cbxx.cblb
                              WHEN 'GRSbillObj' THEN 'GRS成本'
                              WHEN 'ShareObj' THEN '共担科目'
                            END lbmc ,
                            itmd.Name kmmc ,
                            cbxx.je
                    FROM    dbo.jgss_cbxx cbxx
                            LEFT JOIN NewtouchHIS_Base..V_C_Sys_ItemsDetail itmd ON itmd.CateCode = cbxx.cblb
                                                              AND itmd.Code = cbxx.kmcode
                                                              AND ( itmd.OrganizeId = @orgId
                                                              OR itmd.OrganizeId = '*'
                                                              )
                                inner JOIN dbo.jgss_srxx srxx ON srxx.Id = cbxx.srxxId
                                WHERE   srxx.zt = '1'
                                AND srxx.siteId = @orgId
                                AND srxx.[year] = @year
                                AND srxx.[month] = @month;;");

            inSqlParameterList.Add(new SqlParameter("@orgId", site));
            inSqlParameterList.Add(new SqlParameter("@year", year));
            inSqlParameterList.Add(new SqlParameter("@month", int.Parse(month) - 1 == 0 ? 12 : int.Parse(month) - 1));
            return this.FindList<jgssCostVO>(strsql.ToString(), inSqlParameterList.ToArray()).ToList();
        }

    }
}
