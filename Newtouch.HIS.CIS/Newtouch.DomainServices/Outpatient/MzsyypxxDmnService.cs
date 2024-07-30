using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Newtouch.Domain.DTO;
using Newtouch.Domain.ViewModels.Outpatient;
using Newtouch.Domain.ViewModels;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// 门诊输液药品信息
    /// </summary>
    public class MzsyypxxDmnService : DmnServiceBase, IMzsyypxxDmnService
    {
        public MzsyypxxDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据卡号获取输液信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="isEnd">true：已结束输液；false：未结束输液</param>
        /// <returns></returns>
        public IList<MzsyypxxVO> SelectMzsyypxxByKh(Pagination pagination, string kh,string mzh, string organizeId, bool isEnd)
        {
            var sql = new StringBuilder("");
            if (isEnd)
            {
                sql = new StringBuilder(@"
             SELECT syyp.[Id],syzx.[Id] syzxId,syyp.[xm],[kh],[fph],syyp.[cfmxId],[jsmxnm],[sfsj],syyp.[cfh],syyp.[ypCode],syyp.[ypmc],[ypgg],[seatNum]
 ,cfmx.ispsbz,gmxx.Remark psjg,cfmx.islgbz
,CONCAT([yl],'',[yldw]) ylStr,CONCAT(syyp.[sl],'',syyp.[dw]) slStr,CONCAT([jl],'',[jldw]) jlStr,syyp.[yfcode],yf.yfmc,[groupNo], syzx.remark
,syzx.dispenser, pys.Name dispenserName, syzx.executor, zxz.Name executorName,[sykssj],[syjssj],syyp.zcs,syyp.yzxcs
,syyp.[CreateTime],syyp.[CreatorCode],syyp.[LastModifyTime],syyp.[LastModifierCode]
FROM [dbo].[mz_syypxx](NOLOCK) syyp
join dbo.mz_syzxxx(NOLOCK) syzx ON syzx.syypxxId=syyp.Id AND syzx.zt='1' 
join xt_cf (NOLOCK) cf on cf.cfh=syyp.cfh and cf.OrganizeId=syyp.OrganizeId and cf.zt=1
join xt_cfmx (NOLOCK) cfmx on cfmx.cfId=cf.cfId and cfmx.ypCode=syyp.ypCode and cfmx.OrganizeId=cf.OrganizeId and cfmx.zt=1
left join xt_gmxx gmxx on gmxx.cfmxid=cfmx.cfmxId and gmxx.OrganizeId=cfmx.OrganizeId and gmxx.zt=1 and cancel=1
left join NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) pys ON pys.gh=syzx.dispenser AND pys.zt='1' AND pys.OrganizeId=syyp.OrganizeId
left join NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) zxz ON zxz.gh=syzx.executor AND zxz.zt='1' AND zxz.OrganizeId=syyp.OrganizeId
left join NewtouchHIS_Base..xt_ypyf (NOLOCK) yf on yf.yfCode=syyp.yfcode
            WHERE mzh=@mzh
            AND syyp.OrganizeId=@OrganizeId
            ");
            }
            else
            {
                sql = new StringBuilder(@"
SELECT syyp.[Id],cf.cfId,syyp.[xm],[kh],[fph],syyp.[cfmxId],[jsmxnm],[sfsj],syyp.[cfh],syyp.[ypCode],syyp.[ypmc],[ypgg]
,CONCAT([yl],'',[yldw]) ylStr,CONCAT(syyp.[sl],'',syyp.[dw]) slStr,CONCAT([jl],'',[jldw]) jlStr,syyp.[yfcode],yf.yfmc,[groupNo]
,syyp.zcs,syyp.yzxcs,cfmx.ispsbz,gmxx.Remark psjg,cfmx.islgbz,
syyp.[CreateTime],syyp.[CreatorCode],syyp.[LastModifyTime],syyp.[LastModifierCode]
FROM [dbo].[mz_syypxx](NOLOCK) syyp
join xt_cf (NOLOCK) cf on cf.cfh=syyp.cfh and cf.OrganizeId=syyp.OrganizeId and cf.zt=1
join xt_cfmx (NOLOCK) cfmx on cfmx.cfId=cf.cfId and cfmx.ypCode=syyp.ypCode and cfmx.OrganizeId=cf.OrganizeId and cfmx.zt=1
left join xt_gmxx gmxx on gmxx.cfmxid=cfmx.cfmxId and gmxx.OrganizeId=cfmx.OrganizeId and gmxx.zt=1 and cancel=1
LEFT JOIN NewtouchHIS_Base..xt_ypyf (NOLOCK) yf on yf.yfCode=syyp.yfcode
WHERE mzh=@mzh and syyp.zt=1
AND syyp.OrganizeId=@OrganizeId
");
            }
            var param = new DbParameter[] {
                //new SqlParameter("@kh", kh),
                 new SqlParameter("@mzh", mzh),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return QueryWithPage<MzsyypxxVO>(sql.ToString(), pagination, param);
        }

        /// <summary>
        /// 根据卡号获取输液信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public MzsyypxxVO SelectMzsyypxxById(long id, string organizeId)
        {
            var sql = new StringBuilder(@"
SELECT syyp.[Id],[xm],[kh],[fph],[cfmxId],[jsmxnm],[sfsj],[cfh],[ypCode],[ypmc],[ypgg],[seatNum]
,CONCAT([yl],'',[yldw]) ylStr,CONCAT([sl],'',[dw]) slStr,CONCAT([jl],'',[jldw]) jlStr,[groupNo]
,syzx.dispenser, pys.Name dispenserName, syzx.executor, zxz.Name executorName, syzx.remark
,[sykssj],[syjssj],syyp.[CreateTime],syyp.[CreatorCode],syyp.[LastModifyTime],syyp.[LastModifierCode]
FROM [dbo].[mz_syypxx](NOLOCK) syyp
LEFT JOIN dbo.mz_syzxxx(NOLOCK) syzx ON syzx.syypxxId=syyp.Id AND syzx.zt='1' 
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) pys ON pys.gh=syzx.dispenser AND pys.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) zxz ON zxz.gh=syzx.executor AND zxz.zt='1'
WHERE syyp.[Id]=@Id
AND syyp.OrganizeId=@OrganizeId ");
            var param = new DbParameter[] {
                new SqlParameter("@Id", id),
                new SqlParameter("@OrganizeId", organizeId)
            };
            return FirstOrDefault<MzsyypxxVO>(sql.ToString(), param);
        }

        /// <summary>
        /// 未结束的输液患者
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <param name="keyvalue">门诊号/姓名</param>
        /// <param name="fph"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <param name="isEnd">是否结束  false：输液未结束  true：输液已结束</param>
        /// <returns></returns>
        public IList<MzsyhzxxVO> SelectKhAndXm(Pagination pagination, string kh,string keyvalue, string fph, DateTime kssj, DateTime jssj, string organizeId, bool isEnd = false)
        {
            var sql = new StringBuilder(@"
SELECT DISTINCT kh, xm,mzh 
FROM dbo.mz_syypxx(NOLOCK) syyp
");
            if (isEnd)
            {
                sql.AppendLine(@" JOIN dbo.mz_syzxxx(NOLOCK) syzx ON syzx.syypxxId=syyp.Id AND syzx.zt='1' ");
            }
            sql.AppendLine(@" WHERE syyp.OrganizeId=@OrganizeId
AND syyp.sfsj BETWEEN @kssj AND @jssj
AND syyp.zt = '1'
AND syyp.kh LIKE '%' + @kh + '%'
AND(syyp.mzh LIKE '%' + @keyvalue + '%' or syyp.xm LIKE '%' + @keyvalue + '%')
AND syyp.fph LIKE '%' + @fph + '%'");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
                new SqlParameter("@kh", kh),
                 new SqlParameter("@keyvalue", keyvalue),
                new SqlParameter("@fph", fph)
            };
            return QueryWithPage<MzsyhzxxVO>(sql.ToString(), pagination, param);
        }

        /// <summary>
        /// 获取所有组号
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<string> SelectAllGroupNoByKh(string kh, string organizeId,string mzh)
        {
            const string sql = @"
SELECT DISTINCT groupNo FROM dbo.mz_syypxx(NOLOCK) 
WHERE OrganizeId=@OrganizeId
AND zt='1'
AND mzh=@mzh
--AND kh =@kh
";
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@mzh", mzh)
            };
            return FindList<string>(sql, param);
        }

		/// <summary>
		/// 已结算处方明细查询
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <param name="fph"></param>
		/// <param name="kh"></param>
		/// <param name="yfCode">用法代码</param>
		/// <returns></returns>
		public List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(string organizeId, DateTime kssj, DateTime jssj, string fph, string kh, string yfCode, string mzh)
		{
			const string sql = @"
SELECT distinct cf.cfnm, cfmx.cfmxId, js.jsnm, jsmx.jsmxnm, gh.xm, gh.kh,gh.mzh, js.CreateTime sfsj,cf.cfh, yp.ypCode, yp.ypmc, ypsx.ypgg,
 cfmx.yfCode,cfmx.pcCode,pc.zxcs,js.fph
,cfmx.yl,cfmx.yldw,CONCAT(cfmx.yl,'',cfmx.yldw) ylstr
,cfmx.sl,cfmx.dw,CONCAT(cfmx.sl,'',cfmx.dw) slstr
,cfmx.jl,cfmx.jldw,CONCAT(cfmx.jl,'',cfmx.jldw) jlstr
FROM NewtouchHIS_Sett.dbo.mz_cfmx(NOLOCK) cfmx 
INNER JOIN NewtouchHIS_Sett.dbo.mz_cf(NOLOCK) cf ON cf.cfnm=cfmx.cfnm AND cf.OrganizeId=cfmx.OrganizeId AND cf.zt='1'
INNER JOIN NewtouchHIS_Sett.dbo.mz_jsmx(NOLOCK) jsmx ON jsmx.cf_mxnm=cfmx.cfmxId AND jsmx.OrganizeId=cfmx.OrganizeId AND jsmx.zt='1' AND jsmx.sl>0
INNER JOIN NewtouchHIS_Sett.dbo.mz_js(NOLOCK) js ON js.jsnm=jsmx.jsnm AND js.OrganizeId=cfmx.OrganizeId AND js.zt='1' AND js.jszt=1 AND ISNULL(js.tbz, 0) = 0 AND js.cxjsnm=0
INNER JOIN NewtouchHIS_Sett.dbo.mz_gh(NOLOCK) gh ON gh.ghnm=js.ghnm AND gh.OrganizeId=cfmx.OrganizeId AND gh.zt='1' 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=cfmx.yp AND yp.OrganizeId=cfmx.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=cfmx.OrganizeId
Left join NewtouchHIS_Base.dbo.xt_yzpc pc on pc.yzpccode=cfmx.pcCode and pc.zt=1
WHERE cf.OrganizeId=@OrganizeId
AND js.CreateTime BETWEEN @kssj AND @jssj
AND (js.fph=@fph OR ''=@fph)
AND (gh.kh=@kh OR ''=@kh)
AND (gh.mzh=@mzh OR ''=@mzh)
AND cfmx.yfCode in (select col from f_split(@yfCode,',') where col>'') ";
			var param = new DbParameter[] {
				new SqlParameter("@OrganizeId", organizeId),
				new SqlParameter("@kssj", kssj),
				new SqlParameter("@jssj", jssj),
				new SqlParameter("@fph", fph??""),
				new SqlParameter("@kh", kh??""),
				new SqlParameter("@mzh", mzh??""),
				new SqlParameter("@yfCode", yfCode??"")
			};
			return FindList<OutpatientSettledRpQueryResponseDTO>(sql, param);
		}


		/// <summary>
		/// 插入新输液信息
		/// </summary>
		/// <param name="item"></param>
		/// <param name="organizeId"></param>
		/// <param name="creatorCode"></param>
		/// <returns></returns>
		public int InsertNewMzsyypxx(OutpatientSettledRpQueryResponseDTO item, string organizeId, string creatorCode)
        {
            if (item == null) return 0;
            var d = Math.Ceiling(Convert.ToDecimal(item.sl / item.zxcs));
            const string sql = @"
IF EXISTS(SELECT 1 FROM dbo.mz_syypxx(NOLOCK) WHERE jsmxnm=@jsmxnm AND cfmxId=@cfmxId)
BEGIN
	SELECT 0;
END
ELSE
BEGIN
	INSERT INTO dbo.mz_syypxx( OrganizeId ,xm ,kh ,mzh,fph ,cfmxId ,jsmxnm ,sfsj ,cfh ,ypCode ,ypmc ,ypgg ,yl ,yldw ,sl ,dw ,jl ,jldw,yfCode ,pcCode,zcs,yzxcs,groupNo ,zt ,CreateTime ,CreatorCode ,LastModifyTime ,LastModifierCode)
	VALUES  ( 
			  @OrganizeId , -- OrganizeId - varchar(50)
			  @xm , -- xm - varchar(50)
			  @kh , -- kh - varchar(30)
              @mzh , -- mzh - varchar(30)
			  @fph , -- fph - varchar(20)
			  @cfmxId , -- cfmxId - int
			  @jsmxnm , -- jsmxnm - int
			  @sfsj , -- sfsj - datetime
			  @cfh , -- cfh - varchar(100)
			  @ypCode , -- ypCode - varchar(50)
			  @ypmc , -- ypmc - varchar(256)
			  @ypgg , -- ypgg - varchar(100)
			  @yl , -- yl - numeric
			  @yldw , -- yldw - varchar(6)
			  @sl , -- sl - numeric
			  @dw , -- dw - varchar(20)
			  @jl , -- jl - numeric
			  @jldw , -- jldw - varchar(20)
              @yfCode , -- yfCode - varchar(20)
              @pcCode , -- pcCode - varchar(20)
              @zcs , -- zcs - int
              @yzxcs , -- yzxcs - int
			  '' , -- groupNo - varchar(50)
			  '1' , -- zt - char(1)
			  GETDATE() , -- CreateTime - datetime
			  @CreatorCode , -- CreatorCode - varchar(50)
			  NULL , -- LastModifyTime - datetime
			  ''  -- LastModifierCode - varchar(50)
			)
	SELECT 1;
END
";
            var param = new List<DbParameter>
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@xm", item.xm),
                new SqlParameter("@kh", item.kh),
                new SqlParameter("@mzh", item.mzh),
                new SqlParameter("@fph", item.fph ?? ""),
                new SqlParameter("@cfmxId", item.cfmxId),
                new SqlParameter("@jsmxnm", item.jsmxnm),
                new SqlParameter("@sfsj", item.sfsj),
                new SqlParameter("@cfh", item.cfh ?? ""),
                new SqlParameter("@ypCode", item.ypCode),
                new SqlParameter("@ypmc", item.ypmc),
                new SqlParameter("@ypgg", item.ypgg ?? ""),
                new SqlParameter("@yldw", item.yldw ?? ""),
                new SqlParameter("@sl", item.sl),
                new SqlParameter("@dw", item.dw ?? ""),
                new SqlParameter("@jldw", item.jldw ?? ""),
                new SqlParameter("@yfCode", item.yfCode ?? ""),
                new SqlParameter("@pcCode", item.pcCode ?? ""),
                new SqlParameter("@zcs", item.zxcs==null? 1 : Math.Ceiling(Convert.ToDecimal(item.sl/item.zxcs))),
                new SqlParameter("@yzxcs", "0"),
                new SqlParameter("@CreatorCode", creatorCode),
                item.yl == null ? new SqlParameter("@yl", DBNull.Value) : new SqlParameter("@yl", item.yl),
                item.jl == null ? new SqlParameter("@jl", DBNull.Value) : new SqlParameter("@jl", item.jl)
            };
            return FirstOrDefault<int>(sql, param.ToArray());
        }
    }
}
