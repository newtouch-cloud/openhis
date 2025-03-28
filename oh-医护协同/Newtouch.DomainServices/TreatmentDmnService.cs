using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Domain.ViewModels;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// 就诊信息
    /// </summary>
    public class TreatmentDmnService : DmnServiceBase, ITreatmentDmnService
    {
        public TreatmentDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取就诊信息
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TreatmentVo> SelectTreatmentEntities(string mzh, string orgId)
        {

            const string sql = @"
SELECT DISTINCT xyzd.zdCode xyzdCode, xyzd.zdmc xyzdmc, zyzd.zdCode zyzdCode, zyzd.zdmc zyzdmc, jz.* 
FROM dbo.xt_jz(NOLOCK) jz
LEFT JOIN dbo.xt_xyzd(NOLOCK) xyzd ON xyzd.jzId=jz.jzId AND xyzd.zt='1' AND xyzd.OrganizeId=jz.OrganizeId
LEFT JOIN dbo.xt_zyzd(NOLOCK) zyzd ON zyzd.jzId=jz.jzId AND zyzd.zt='1' AND zyzd.OrganizeId=jz.OrganizeId
WHERE jz.mzh=@mzh AND jz.OrganizeId=@orgId AND jz.zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@orgId", orgId)
            };
            return FindList<TreatmentVo>(sql, param);
        }

        /// <summary>
        /// 获取就诊信息
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public TreatEntityObj SelectTreatmentEntitie(string mzh, string orgId)
        {

            const string sql = @"
                SELECT gh.mzh                                                         AS mzh
     , gh.blh                                                         as blh
     , cast(isnull(gh.mjzbz, '1') as int)                                            mjzbz
     , gh.xm                                                          AS xm
     , (CASE gh.xb WHEN '1' THEN '男' WHEN '2' THEN '女' ELSE '不详' END) as xb
     , xx.csny                                                        AS csny
     , xz.brxz                                                           brxzCode
     , xz.brxzmc                                                         brxzmc
     , cast(gh.zjlx as int)                                              zjlx
     , gh.zjh                                                            zjh
     , ksmcgl.Name                                                       ghksmc
     , ysxmgl.Name                                                       ghys
     , isnull(gh.ghrq, gh.CreateTime)                                 AS ghsj
     , gh.CreateTime                                                  as ghczsj
     , 1                                                                 jzzt
     , gh.ys                                                             jzys
     , gh.ks                                                             jzks
     , gh.ybjsh
     , case when gh.fzbz = 1 then '1' else '' end                        cfzbz
     , gh.kh                                                             sbbh
     , kh.cbdbm
     , xx.py
     , gh.kh
     , xx.dh                                                          AS ContactNum
     , xx.xian_sheng                                                  AS province
     , xx.xian_shi                                                    AS city
     , xx.xian_xian                                                   AS county
     , xx.xian_dz                                                     AS [ADDRESS]
     , gh.nlshow
     , kh.grbh
     , '0'                                                                 ghlybz
from NewtouchHIS_Sett..mz_gh gh
         left join NewtouchHIS_Sett..xt_brjbxx xx
                   on gh.patId = xx.patId and gh.OrganizeId = xx.OrganizeId
         left join NewtouchHIS_Sett..xt_card kh
                   on kh.cardno = gh.kh and kh.CardType = gh.CardType and kh.OrganizeId = gh.OrganizeId
         LEFT JOIN NewtouchHIS_Sett..xt_brxz xz
                   ON xz.brxz = gh.brxz AND xz.OrganizeId = gh.OrganizeId
         LEFT JOIN NewtouchHIS_Base..V_C_Sys_ItemsDetail Citem ON (Citem.OrganizeId = gh.OrganizeId
    OR citem.OrganizeId = '*'
                                                                      )
    AND citem.Code = gh.lxrgx
    AND citem.CateCode = 'RelativeType'
         LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff ysxmgl
                   on ysxmgl.gh = gh.ys and ysxmgl.OrganizeId = gh.OrganizeId
         LEFT JOIN NewtouchHIS_Base..V_S_Sys_Department ksmcgl
                   on ksmcgl.Code = gh.ks and ksmcgl.OrganizeId = gh.OrganizeId
		WHERE gh.zt = '1' and isnull(gh.ghzt,'') <> '2' AND xx.zt = '1' AND xz.zt = '1' and isnull(gh.mzh, '') <> '' and gh.OrganizeId = @orgId and gh.mzh = @mzh
                ";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@orgId", orgId)
            };
            return FirstOrDefault<TreatEntityObj>(sql.ToString(), param);
        }

        public IList<PatientVisitInfoVO> GetPatientVisitList(Pagination pagination, string kh, string xm, string jzlx, string jszt, DateTime? kssj, DateTime? jssj, string orgId)
        {
            var strMzSql = new StringBuilder(@"
                SELECT '0' jzlx,'门诊' jzlxmc,jz.xm,jz.kh,jz.jzId,jz.mzh mzzyh,jz.ghksmc ksmc,jz.zlkssj sj, cast(c.jszt as char ) jszt,case c.jszt when 0 then '未结算' when 1 then '已结算' when 2 then '已退费' else '' end jsztmc
                FROM dbo.xt_jz(NOLOCK) jz
                left join NewtouchHIS_Sett..mz_gh b on jz.mzh = b.mzh and jz.OrganizeId=b.OrganizeId
                left join NewtouchHIS_Sett..mz_js c on b.ghnm = c.ghnm and b.OrganizeId=c.OrganizeId and c.jslx='0'
                WHERE jz.OrganizeId=@orgId AND jz.zt='1'
                "); 
            var inSqlParameterList = new List<DbParameter>();
            var strZySql = new StringBuilder(@"
                select '1' jzlx,'住院' jzlxmc,
       a.xm,
       a.kh,
       ''     jzId,
       a.zyh mzzyh,
       c.Name ksmc,
       ryrq   sj,js.jszt,case js.jszt when '1' then '已结算' when '2' then '已退费' else '未结算' end jsztmc
from NewtouchHIS_Sett..zy_brjbxx a
left join NewtouchHIS_Sett..zy_js js on a.zyh=js.zyh and a.OrganizeId = js.OrganizeId
         --left join zy_cwsyjlk b on a.zyh = b.zyh and b.zt = '1' and a.OrganizeId = b.OrganizeId
         left join NewtouchHIS_Base..Sys_Department c on a.ks = c.Code and a.OrganizeId = c.OrganizeId
where a.OrganizeId = @orgId
  and a.zt = '1'
                ");
            inSqlParameterList.Add(new SqlParameter("@orgId", orgId));
            if (!string.IsNullOrEmpty(kh))
            {
                strMzSql.Append(@" AND jz.kh like @kh ");
                strZySql.Append(@" AND a.kh like @kh ");
                inSqlParameterList.Add(new SqlParameter("@kh", "%" + kh + "%"));
            }
            if (!string.IsNullOrEmpty(xm))
            {
                strMzSql.Append(@" AND jz.xm like @xm ");
                strZySql.Append(@" AND a.xm like @xm ");
                inSqlParameterList.Add(new SqlParameter("@xm", "%" + xm + "%"));
            }
            if (kssj != null)
            {
                strMzSql.Append(@" AND jz.ghsj >= @kssj ");
                strZySql.Append(@" AND a.ryrq >= @kssj ");
                inSqlParameterList.Add(new SqlParameter("@kssj", kssj));
            }
            if (jssj != null)
            {
                strMzSql.Append(@" AND jz.ghsj <= @jssj ");
                strZySql.Append(@" AND a.ryrq <= @jssj ");
                inSqlParameterList.Add(new SqlParameter("@jssj", jssj));
            }
            if (!string.IsNullOrEmpty(jszt))
            {
                strMzSql.Append(@" AND cast(c.jszt as char ) = @jszt ");
				if (jszt == "1")
                {
                    strZySql.Append(@" AND js.jszt = @jszt ");
                }
				else
				{
                    strZySql.Append(@" AND (js.jszt = @jszt or js.jszt is null)");
                }
                inSqlParameterList.Add(new SqlParameter("@jszt", jszt));
            }
            var strSql = "";
            if (jzlx == "0")
            {
                strSql = strMzSql.ToString();
            }
            else if (jzlx == "1")
			{
                strSql = strZySql.ToString();
            }
			else
			{
                strSql = strMzSql.ToString() + " union all "+ strZySql.ToString();
            }
            return QueryWithPage<PatientVisitInfoVO>(strSql, pagination, inSqlParameterList.ToArray());
        }
        public InPatientInfoVO GetInPatientInfo(string zyh, string orgId)
        {
            var inSqlParameterList = new List<DbParameter>();
            var strZySql = new StringBuilder(@"
                select a.zyh,
       c.Name                                                                                              ksmc,
       (select top 1 ysmc from zy_PatDocInfo where zyh = a.zyh and OrganizeId = a.OrganizeId and Type = 0) zyys,
       (select top 1 ysmc from zy_PatDocInfo where zyh = a.zyh and OrganizeId = a.OrganizeId and Type = 1) zzys,
       (select top 1 ysmc from zy_PatDocInfo where zyh = a.zyh and OrganizeId = a.OrganizeId and Type = 2) zrys,
       --STUFF((
       --          SELECT ', ' + zdmc
       --          FROM NewtouchHIS_Sett..zy_rydzd i
       --          WHERE i.zyh = a.zyh
       --          FOR XML PATH (''), TYPE
       --      ).value('.', 'nvarchar(max)'), 1, 2, '') AS                                                   
                                                                                                            ryzd,
       --STUFF((
       --         SELECT ', ' + zdmc
       --          FROM zy_PatDxInfo i
       --          WHERE i.zyh = a.zyh
       --          FOR XML PATH (''), TYPE
       --      ).value('.', 'nvarchar(max)'), 1, 2, '') AS                                                  
                                                                                                            cyzd,
       ryrq,
       cyrq,
       case zybz
           when '0' then '入院登记'
           when '1' then '病区中'
           when '2' then '已出区'
           when '3' then '已出院'
           when '7' then '转区'
           when '9' then '作废记录'
           else '' end                                                                                     zybz,
        d.brxzmc                                                                                           brxz
from NewtouchHIS_Sett..zy_brjbxx a
         --left join zy_cwsyjlk b on a.zyh = b.zyh and b.zt = '1' and a.OrganizeId = b.OrganizeId
         left join NewtouchHIS_Base..Sys_Department c on a.ks = c.Code and a.OrganizeId = c.OrganizeId
         left join NewtouchHIS_Sett..xt_brxz d on a.brxz = d.brxz and a.OrganizeId = d.OrganizeId
where a.OrganizeId = @orgId and a.zt='1' and a.zyh = @zyh
                ");
            inSqlParameterList.Add(new SqlParameter("@orgId", orgId));
            inSqlParameterList.Add(new SqlParameter("@zyh", zyh));
            return FirstOrDefault<InPatientInfoVO>(strZySql.ToString(), inSqlParameterList.ToArray());
        }
    }
}