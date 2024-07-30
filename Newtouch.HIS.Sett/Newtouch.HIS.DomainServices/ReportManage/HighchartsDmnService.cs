using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IDomainServices.ReportManage;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.HIS.DomainServices.ReportManage
{
    public class HighchartsDmnService : DmnServiceBase, IHighchartsDmnService
    {
        public HighchartsDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #region 门诊

        /// <summary>
        /// 获取门诊就诊人数
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<OutpatientVisitNumVO> GetOutpatientVisitNum(string orgId)
        {
            List<OutpatientVisitNumVO> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"--当前年份
                            DECLARE @currYear varchar(30)=(Convert(varchar, year(getdate()),10))
                            --当前年月
                            DECLARE @currYearMoth varchar(30)
                            SET @currYearMoth=@currYear+'01';
                            --select @currYearMoth
                            --门诊
                            SELECT col, isnull(num1,0) num FROM [dbo].[f_split](''+@currYear+'01,'+@currYear+'02,'+@currYear+'03,'+@currYear+'04,'+@currYear+'05,'+@currYear+'06,'+@currYear+'07,'+@currYear+'08,'+@currYear+'09,'+@currYear+'10,'+@currYear+'11,'+@currYear+'12',',') 
                            LEFT JOIN
                            (select groupDate, count(1) num1 from 
	                        (select distinct CONVERT(varchar(6), groupDate, 112) groupDate, patid
	                        from 
	                        (SELECT    CONVERT(VARCHAR(6), jhmx.jzsj, 112) AS groupDate ,
                                                    patid
                                          FROM      mz_xm
										    LEFT JOIN dbo.mz_jzjhmx jhmx ON jhmx.jzjhmxId = mz_xm.jzjhmxId
                                                              AND mz_xm.OrganizeId = jhmx.OrganizeId
                                          WHERE     mz_xm.zt = '1'
                                                    AND mz_xm.OrganizeId = @orgId
                                                    AND CONVERT(VARCHAR(6), jhmx.jzsj, 112) >= @currYearMoth)aaa) bbb group by bbb.groupDate) AS a ON a.groupDate=col");
            SqlParameter[] param =
            {
                    new SqlParameter("@orgId",orgId)
                };
            outpatientlist = FindList<OutpatientVisitNumVO>(sqlStr.ToString(), param);

            return outpatientlist;
        }

        /// <summary>
        /// 获取门诊就诊人次
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<OutpatientVisitNumVO> GetOutpatientVisitPerNum(string orgId)
        {
            List<OutpatientVisitNumVO> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"--当前年份
                            DECLARE @currYear varchar(30)=(Convert(varchar, year(getdate()),10))
                            --当前年月
                            DECLARE @currYearMoth varchar(30)
                            SET @currYearMoth=@currYear+'01';
                            --select @currYearMoth
                            --门诊
                            SELECT col, isnull(num1,0) num FROM [dbo].[f_split](''+@currYear+'01,'+@currYear+'02,'+@currYear+'03,'+@currYear+'04,'+@currYear+'05,'+@currYear+'06,'+@currYear+'07,'+@currYear+'08,'+@currYear+'09,'+@currYear+'10,'+@currYear+'11,'+@currYear+'12',',') 
                            LEFT JOIN
                            (select groupDate, count(1) num1 from 
	                        (SELECT DISTINCT
                                        CONVERT(VARCHAR(6), groupDate, 112) groupDate ,
                                        ghnm
                              FROM      ( SELECT    CONVERT(VARCHAR(6), jhmx.jzsj, 112) AS groupDate ,
                                                    ghnm
                                          FROM      mz_xm
                                          LEFT JOIN dbo.mz_jzjhmx jhmx ON jhmx.jzjhmxId = mz_xm.jzjhmxId
                                                              AND mz_xm.OrganizeId = jhmx.OrganizeId
                                          WHERE     mz_xm.zt = '1'
                                                    AND mz_xm.OrganizeId = @orgId
                                                    AND CONVERT(VARCHAR(6), jhmx.jzsj, 112) >= @currYearMoth)aaa) bbb group by bbb.groupDate) AS a ON a.groupDate=col");
            SqlParameter[] param =
            {
                    new SqlParameter("@orgId",orgId)
                };
            outpatientlist = FindList<OutpatientVisitNumVO>(sqlStr.ToString(), param);

            return outpatientlist;
        }

        /// <summary>
        /// 获取门诊收入统计
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<OutpatientSCNumVO> GetOutpatientSalaryNum(string orgId)
        {
            List<OutpatientSCNumVO> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"--当前年份
                DECLARE @currYear VARCHAR(30)= ( CONVERT(VARCHAR, YEAR(GETDATE()), 10) )
                                            --当前年月
                DECLARE @currYearMoth VARCHAR(30)
                SET @currYearMoth = @currYear + '01';
                                            --select @currYearMoth
                                            --门诊
                SELECT  col ,
                        ISNULL(num1, 0) num,
                        '门诊' name
                FROM    [dbo].[f_split]('' + @currYear + '01,' + @currYear + '02,' + @currYear
                        + '03,' + @currYear + '04,' + @currYear + '05,'
                        + @currYear + '06,' + @currYear + '07,' + @currYear
                        + '08,' + @currYear + '09,' + @currYear + '10,'
                        + @currYear + '11,' + @currYear + '12', ',')
        LEFT JOIN ( SELECT  groupDate ,
                            SUM(JE) num1
                    FROM    ( SELECT DISTINCT
                                        CONVERT(VARCHAR(6), groupDate, 112) groupDate ,
                                        JE
                              FROM      (--收费项目 
                                        SELECT  DISTINCT  mz_xm.xmnm cfmxId,  CAST(mz_xm.dj * mz_jsmx.sl AS DECIMAL(18,
                                                              2)) AS JE ,
                                                    mx.jzsj groupDate
                                          FROM      mz_xm
                                                    LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = mz_xm.jzjhmxId
                                                              AND mx.OrganizeId = @orgId
                                                    LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm xt_sfxm ON mx.sfxmCode = xt_sfxm.sfxmCode
                                                              AND xt_sfxm.OrganizeId = @orgId
                                                    LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl ON mz_xm.dl = sfdl.dlcode
                                                              AND sfdl.OrganizeId = @orgId
                                                    INNER JOIN mz_jsmx ON mz_jsmx.mxnm = mz_xm.xmnm
                                                              AND mz_jsmx.jslx != '0'
                                                              AND mz_jsmx.OrganizeId = @orgId
                                                    INNER JOIN mz_js js ON js.jsnm = mz_jsmx.jsnm
                                                               AND js.OrganizeId = @orgId
                                                               AND js.jszt <> '2'
                                          WHERE     mz_xm.OrganizeId = @orgId
                                                    AND CONVERT(VARCHAR(6), mx.jzsj, 112) >= @currYearMoth
                                                     AND NOT EXISTS ( SELECT 1
                                                     FROM   mz_js b
                                                     WHERE  jszt = '2'
                                                        AND b.cxjsnm = js.jsnm
                                                        AND b.OrganizeId = js.OrganizeId )
                                          union
                                         --处方
                                        SELECT DISTINCT
                                                mz_cfmx.cfmxId ,
                                                CAST(mz_cfmx.dj * mz_jsmx.sl AS DECIMAL(18, 2)) AS JE ,
                                                mx.jzsj groupDate
                                        FROM    mz_cfmx
                                                LEFT JOIN mz_cf ON mz_cfmx.cfnm = mz_cf.cfnm
                                                                   AND mz_cf.OrganizeId = @orgId
                                                LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = mz_cfmx.jzjhmxId
                                                LEFT JOIN NewtouchHIS_Base..V_S_xt_yp xt_yp ON mz_cfmx.yp = xt_yp.ypCode
                                                                                               AND xt_yp.OrganizeId = @orgId
                                                LEFT JOIN NewtouchHIS_Base..V_S_xt_ypsx xt_ypsx ON mz_cfmx.yp = xt_ypsx.ypCode
                                                                                                   AND xt_ypsx.OrganizeId = @orgId
                                                LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl xt_sfdl ON mz_cfmx.dl = xt_sfdl.dlCode
                                                                                                   AND xt_sfdl.OrganizeId = @orgId
                                                INNER JOIN mz_jsmx ON mz_jsmx.jslx != '0'
                                                                      AND mz_jsmx.OrganizeId = @orgId
                                                                      --AND mz_jsmx.mxbm = mz_cfmx.yp
                                                                      AND mz_jsmx.cf_mxnm = mz_cfmx.cfnm
                                                INNER JOIN mz_js js ON js.jsnm = mz_jsmx.jsnm
                                                   AND js.OrganizeId = @orgId
                                                   AND js.jszt <> '2'
                                        WHERE   mz_cf.OrganizeId = @orgId
                                                AND CONVERT(VARCHAR(6), mx.jzsj, 112) >= @currYearMoth
                                                 AND NOT EXISTS ( SELECT 1
                                                 FROM   mz_js b
                                                 WHERE  jszt = '2'
                                                        AND b.cxjsnm = js.jsnm
                                                        AND b.OrganizeId = js.OrganizeId )
                                        ) aaa
                            ) bbb
                    GROUP BY bbb.groupDate
                  ) AS a ON a.groupDate = col");
            SqlParameter[] param =
            {
                    new SqlParameter("@orgId",orgId)
                };
            outpatientlist = FindList<OutpatientSCNumVO>(sqlStr.ToString(), param);
            return outpatientlist;
        }

        #region 门诊记账1.2 and 1.3版本，不适用多个治疗师和医院的记账逻辑
        /// <summary>
        /// 获取治疗师治疗总费用
        /// </summary>
        public List<TherapistVisit> GetTherapistDischarge(string year, string orgId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"   DECLARE @cnt INT;
               SELECT   @cnt = cnt
               FROM     [dbo].[CntByOrgId](@orgId);
               IF ( @cnt > 0 )--存在执行表记录，按照执行逻辑走,否则按照mz_xm逻辑走
                BEGIN
                    SELECT  'Rev.per Therapist' title ,
                --zx.zlsgh gh ,
                cuser.Name name ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '01' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Jan ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '02' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Feb ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '03' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Mar ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '04' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Apr ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '05' THEN xm.dj * zx.sl
                         ELSE 0
                    END) May ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '06' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Jun ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '07' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Jul ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '08' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Aug ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '09' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Sep ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '10' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Oct ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '11' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Nov ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '12' THEN xm.dj * zx.sl
                         ELSE 0
                    END) [Dec]
        FROM    dbo.mz_jzjhmxzx (NOLOCK) zx
                LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                              AND mx.OrganizeId = @orgId
                INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                             AND jh.OrganizeId = @orgId
                LEFT JOIN mz_xm xm ON xm.jzjhmxId = mx.jzjhmxId
                                      AND xm.OrganizeId = @orgId
                LEFT JOIN dbo.mz_gh ON xm.ghnm = mz_gh.ghnm
                                       AND mz_gh.OrganizeId = xm.OrganizeId
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = zx.zlsgh
                                                              AND cuser.OrganizeId = @orgId
        WHERE   xm.zt = 1
                AND mz_gh.zt = 1
                AND xm.OrganizeId = @orgId
                AND YEAR(zx.zxsj) = @year
                and zx.zt='1'
        GROUP BY --zx.zlsgh ,
                cuser.Name;
    END;
   ELSE
    BEGIN
        SELECT  'Rev.per Therapist' title ,
                cuser.gh gh ,
                cuser.Name name ,
                SUM(CASE WHEN MONTH(ISNULL(ssrq, sfrq)) = '01'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Jan ,
                ISNULL(SUM(CASE WHEN MONTH(ISNULL(ssrq, sfrq)) = '02'
                                THEN xm.dj * xm.sl
                                ELSE 0
                           END), 0.00) Feb ,
                SUM(CASE WHEN MONTH(ISNULL(ssrq, sfrq)) = '03'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Mar ,
                SUM(CASE WHEN MONTH(ISNULL(ssrq, sfrq)) = '04'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Apr ,
                SUM(CASE WHEN MONTH(ISNULL(ssrq, sfrq)) = '05'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) May ,
                SUM(CASE WHEN MONTH(ISNULL(ssrq, sfrq)) = '06'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Jun ,
                SUM(CASE WHEN MONTH(ISNULL(ssrq, sfrq)) = '07'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Jul ,
                SUM(CASE WHEN MONTH(ISNULL(ssrq, sfrq)) = '08'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Aug ,
                SUM(CASE WHEN MONTH(ISNULL(ssrq, sfrq)) = '09'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Sep ,
                SUM(CASE WHEN MONTH(ISNULL(ssrq, sfrq)) = '10'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Oct ,
                SUM(CASE WHEN MONTH(ISNULL(ssrq, sfrq)) = '11'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Nov ,
                SUM(CASE WHEN MONTH(ISNULL(ssrq, sfrq)) = '12'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) [Dec]
        INTO    #temp
        FROM    mz_xm xm
                LEFT JOIN dbo.mz_gh ON xm.ghnm = mz_gh.ghnm
                                       AND mz_gh.OrganizeId = xm.OrganizeId
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ys
                                                              AND cuser.OrganizeId = @orgId
        WHERE   xm.zt = 1
                AND mz_gh.zt = 1
                AND xm.OrganizeId = @orgId
                AND YEAR(ISNULL(ssrq, sfrq)) = @year
        GROUP BY cuser.gh ,
                cuser.Name;
        SELECT  'Rev.per Therapist' title ,
                cuser.gh gh ,
                cuser.Name name ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '01'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Jan ,
                ISNULL(SUM(CASE WHEN MONTH(mz_cf.jsrq) = '02'
                                THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                                ELSE 0
                           END), 0.00) Feb ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '03'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Mar ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '04'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Apr ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '05'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) May ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '06'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Jun ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '07'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Jul ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '08'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Aug ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '09'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Sep ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '10'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Oct ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '11'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Nov ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '12'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) [Dec]
        INTO    #temp2
        FROM    dbo.mz_cfmx
                INNER JOIN dbo.mz_cf ON dbo.mz_cf.cfnm = dbo.mz_cfmx.cfnm
                                        AND dbo.mz_cf.OrganizeId = dbo.mz_cfmx.OrganizeId
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = mz_cf.ys
                                                              AND cuser.OrganizeId = @orgId
                LEFT JOIN dbo.mz_gh ON mz_gh.ghnm = mz_cf.ghnm
        WHERE   mz_cfmx.OrganizeId = @orgId
                AND mz_cfmx.zt = 1
                AND YEAR(mz_cf.jsrq) = @year
                AND dbo.mz_cf.zt = 1
                AND dbo.mz_cfmx.zt = 1
                AND dbo.mz_gh.zt = 1
        GROUP BY cuser.gh ,
                cuser.Name;
        SELECT  'Rev.per Therapist' title ,
                a.name ,
                a.gh ,
                ISNULL(a.Jan, 0.0000) + ISNULL(b.Jan, 0.0000) Jan ,
                ISNULL(a.Feb, 0.0000) + ISNULL(b.Feb, 0.0000) Feb ,
                ISNULL(a.Mar, 0.0000) + ISNULL(b.Mar, 0.0000) Mar ,
                ISNULL(a.Apr, 0.0000) + ISNULL(b.Apr, 0.0000) Apr ,
                ISNULL(a.May, 0.0000) + ISNULL(b.May, 0.0000) May ,
                ISNULL(a.Jun, 0.0000) + ISNULL(b.Jun, 0.0000) Jun ,
                ISNULL(a.Jul, 0.0000) + ISNULL(b.Jul, 0.0000) Jul ,
                ISNULL(a.Aug, 0.0000) + ISNULL(b.Aug, 0.0000) Aug ,
                ISNULL(a.Sep, 0.0000) + ISNULL(b.Sep, 0.0000) Sep ,
                ISNULL(a.Oct, 0.0000) + ISNULL(b.Oct, 0.0000) Oct ,
                ISNULL(a.Nov, 0.0000) + ISNULL(b.Nov, 0.0000) Nov ,
                ISNULL(a.[Dec], 0.0000) + ISNULL(b.[Dec], 0.0000) [Dec]
        FROM    #temp a
                LEFT JOIN #temp2 b ON a.gh = b.gh
        ORDER BY a.gh;

        DROP TABLE #temp,#temp2;
    END;");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                    new SqlParameter("@year",year)
                };
            return FindList<TherapistVisit>(sqlStr.ToString(), param);
        }

        /// <summary>
        /// 获取治疗师治疗人数
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TherapistVisit> GetTherapistVisit(string year, string orgId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"   DECLARE @cnt INT;
           SELECT   @cnt = cnt
           FROM     [dbo].[CntByOrgId](@orgId);
           IF ( @cnt > 0 )--存在执行表记录，按照执行逻辑走,否则按照mz_xm逻辑走
            BEGIN
	        SELECT  'Patient Visit' title ,
        xm.Name ,
        xm.gh ,
        CAST(SUM(CASE WHEN MONTH(ssrq) = '01' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jan ,
        CAST(SUM(CASE WHEN MONTH(ssrq) = '02' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Feb ,
        CAST(SUM(CASE WHEN MONTH(ssrq) = '03' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Mar ,
        CAST(SUM(CASE WHEN MONTH(ssrq) = '04' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Apr ,
        CAST(SUM(CASE WHEN MONTH(ssrq) = '05' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) May ,
        CAST(SUM(CASE WHEN MONTH(ssrq) = '06' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jun ,
        CAST(SUM(CASE WHEN MONTH(ssrq) = '07' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jul ,
        CAST(SUM(CASE WHEN MONTH(ssrq) = '08' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Aug ,
        CAST(SUM(CASE WHEN MONTH(ssrq) = '09' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Sep ,
        CAST(SUM(CASE WHEN MONTH(ssrq) = '10' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Oct ,
        CAST(SUM(CASE WHEN MONTH(ssrq) = '11' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Nov ,
        CAST(SUM(CASE WHEN MONTH(ssrq) = '12' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) [Dec]
FROM    ( SELECT    aaa.* ,
                    cuser.Name name
          FROM      ( SELECT DISTINCT
                                jh.ghnm ,
                                CONVERT(VARCHAR(10), zx.zxsj, 120) ssrq ,
                                zx.zlsgh gh
                      FROM      dbo.mz_jzjhmxzx (NOLOCK) zx
                                LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                              AND mx.OrganizeId = @orgId
                                INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                             AND jh.OrganizeId = @orgId
                                LEFT JOIN dbo.mz_gh ON jh.ghnm = mz_gh.ghnm
                                                       AND mz_gh.OrganizeId = @orgId
                      WHERE     zx.OrganizeId = @orgId
                                AND zx.zt = '1'
                                AND mz_gh.zt = '1'
                                AND YEAR(zx.zxsj) = @year
                      GROUP BY  jh.ghnm ,
                                zx.zxsj ,
                                zx.zlsgh
                    ) aaa
                    LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = aaa.gh
                                                              AND cuser.OrganizeId = @orgId
        ) xm
GROUP BY xm.name ,
        xm.gh
ORDER BY xm.gh;
    END;
   ELSE
    BEGIN
	SELECT  'Patient Visit' title ,
                        cuser.name ,
                        cuser.gh ,
                        CAST(SUM(CASE WHEN MONTH(ssrq) = '01' THEN 1
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) Jan ,
                        CAST(SUM(CASE WHEN MONTH(ssrq) = '02' THEN 1
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) Feb ,
                        CAST(SUM(CASE WHEN MONTH(ssrq) = '03' THEN 1
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) Mar ,
                        CAST(SUM(CASE WHEN MONTH(ssrq) = '04' THEN 1
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) Apr ,
                        CAST(SUM(CASE WHEN MONTH(ssrq) = '05' THEN 1
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) May ,
                        CAST(SUM(CASE WHEN MONTH(ssrq) = '06' THEN 1
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) Jun ,
                        CAST(SUM(CASE WHEN MONTH(ssrq) = '07' THEN 1
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) Jul ,
                        CAST(SUM(CASE WHEN MONTH(ssrq) = '08' THEN 1
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) Aug ,
                        CAST(SUM(CASE WHEN MONTH(ssrq) = '09' THEN 1
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) Sep ,
                        CAST(SUM(CASE WHEN MONTH(ssrq) = '10' THEN 1
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) Oct ,
                        CAST(SUM(CASE WHEN MONTH(ssrq) = '11' THEN 1
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) Nov ,
                        CAST(SUM(CASE WHEN MONTH(ssrq) = '12' THEN 1
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) [Dec]
                FROM    ( SELECT DISTINCT
                    aaaaaaa.*
          FROM      ( SELECT    ghnm ,
                                CONVERT(VARCHAR(10), ISNULL(ssrq,sfrq), 120) ssrq ,
                                ys
                      FROM      dbo.mz_xm
                      WHERE     OrganizeId = @orgId
                                AND zt = 1
                                AND YEAR(ISNULL(ssrq,sfrq)) = @year
                      GROUP BY  ghnm ,
                                ISNULL(ssrq,sfrq) ,
                                ys
                      UNION ALL
                      SELECT    ghnm ,
                                CONVERT(VARCHAR(10), jsrq, 120) ssrq ,
                                ys
                      FROM      dbo.mz_cfmx
                                LEFT JOIN dbo.mz_cf ON dbo.mz_cf.cfnm = dbo.mz_cfmx.cfnm
                                                       AND dbo.mz_cf.OrganizeId = dbo.mz_cfmx.OrganizeId
                      WHERE     mz_cfmx.OrganizeId = @orgId
                                AND mz_cfmx.zt = 1
                                AND YEAR(jsrq) = @year
                                AND dbo.mz_cf.zt = 1
                                AND dbo.mz_cfmx.zt = 1
                      GROUP BY  ghnm ,
                                jsrq ,
                                ys
                    ) aaaaaaa
                    LEFT JOIN dbo.mz_gh ON aaaaaaa.ghnm = mz_gh.ghnm
          WHERE      dbo.mz_gh.zt = 1
        ) xm
        LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ys
                                                              AND cuser.OrganizeId = @orgId
GROUP BY cuser.Name ,
        cuser.gh ORDER BY  cuser.gh
    END;");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                     new SqlParameter("@year",year)
                };
            return FindList<TherapistVisit>(sqlStr.ToString(), param);
        }

        /// <summary>
        /// 获取平均费用（总费用/总人次）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TherapistVisit> GetTherapistavgDischarge(string year, string orgId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"   DECLARE @cnt INT;
   SELECT   @cnt = cnt
   FROM     [dbo].[CntByOrgId]( @orgId);
   IF ( @cnt > 0 )--存在执行表记录，按照执行逻辑走,否则按照mz_xm逻辑走
    BEGIN
        SELECT  'Rev.per Therapist' title ,
                zx.zlsgh gh ,
                cuser.Name name ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '01' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Jan ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '02' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Feb ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '03' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Mar ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '04' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Apr ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '05' THEN xm.dj * zx.sl
                         ELSE 0
                    END) May ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '06' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Jun ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '07' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Jul ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '08' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Aug ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '09' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Sep ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '10' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Oct ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '11' THEN xm.dj * zx.sl
                         ELSE 0
                    END) Nov ,
                SUM(CASE WHEN MONTH(zx.zxsj) = '12' THEN xm.dj * zx.sl
                         ELSE 0
                    END) [Dec]
        INTO    #temp
        FROM    dbo.mz_jzjhmxzx (NOLOCK) zx
                LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                              AND mx.OrganizeId = @orgId
                INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                             AND jh.OrganizeId = @orgId
                LEFT JOIN mz_xm xm ON xm.jzjhmxId = mx.jzjhmxId
                                      AND xm.OrganizeId = @orgId
                LEFT JOIN dbo.mz_gh ON xm.ghnm = mz_gh.ghnm
                                       AND mz_gh.OrganizeId = xm.OrganizeId
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = zx.zlsgh
                                                              AND cuser.OrganizeId = @orgId
        WHERE   xm.zt = 1
                AND mz_gh.zt = 1
                AND xm.OrganizeId = @orgId
                AND YEAR(zx.zxsj) = @year
                and zx.zt='1'
        GROUP BY zx.zlsgh ,
                cuser.Name;


        --总人次
        SELECT  'Patient Visit' title ,
                xm.name ,
                xm.gh ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '01' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Jan ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '02' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Feb ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '03' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Mar ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '04' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Apr ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '05' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) May ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '06' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Jun ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '07' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Jul ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '08' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Aug ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '09' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Sep ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '10' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Oct ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '11' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Nov ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '12' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) [Dec]
        INTO    #temp2
        FROM    ( SELECT    jh.ghnm ,
                            CONVERT(VARCHAR(10), zx.zxsj, 120) ssrq ,
                            zx.zlsgh gh ,
                            cuser.Name name
                  FROM      dbo.mz_jzjhmxzx (NOLOCK) zx
                            LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                          AND mx.OrganizeId = @orgId
                            INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                         AND jh.OrganizeId = @orgId
                            LEFT JOIN dbo.mz_gh ON jh.ghnm = mz_gh.ghnm
                                                   AND mz_gh.OrganizeId = @orgId
                            LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = zx.zlsgh
                                                              AND cuser.OrganizeId = @orgId
                  WHERE     zx.OrganizeId = @orgId
                            AND zx.zt = '1'
                            AND mz_gh.zt = '1'
                            AND YEAR(zx.zxsj) = @year
                  GROUP BY  jh.ghnm ,
                            zx.zxsj ,
                            zx.zlsgh ,
                            cuser.Name
                ) xm
        GROUP BY xm.name ,
                xm.gh
        ORDER BY xm.gh;


--平均费用
        SELECT  'Avg Rev.per Therapist' title ,
                a.name ,
                a.gh ,
                CAST(ROUND(( CASE ISNULL(b.Jan, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Jan)
                                    / CONVERT(FLOAT, b.Jan)
                             END ), 2) AS DECIMAL(18, 2)) Jan ,
                CAST(ROUND(( CASE ISNULL(b.Feb, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Feb)
                                    / CONVERT(FLOAT, b.Feb)
                             END ), 2) AS DECIMAL(18, 2)) Feb ,
                CAST(ROUND(( CASE ISNULL(b.Mar, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Mar)
                                    / CONVERT(FLOAT, b.Mar)
                             END ), 2) AS DECIMAL(18, 2)) Mar ,
                CAST(ROUND(( CASE ISNULL(b.Apr, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Apr)
                                    / CONVERT(FLOAT, b.Apr)
                             END ), 2) AS DECIMAL(18, 2)) Apr ,
                CAST(ROUND(( CASE ISNULL(b.May, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.May)
                                    / CONVERT(FLOAT, b.May)
                             END ), 2) AS DECIMAL(18, 2)) May ,
                CAST(ROUND(( CASE ISNULL(b.Jun, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Jun)
                                    / CONVERT(FLOAT, b.Jun)
                             END ), 2) AS DECIMAL(18, 2)) Jun ,
                CAST(ROUND(( CASE ISNULL(b.Jul, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Jul)
                                    / CONVERT(FLOAT, b.Jul)
                             END ), 2) AS DECIMAL(18, 2)) Jul ,
                CAST(ROUND(( CASE ISNULL(b.Aug, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Aug)
                                    / CONVERT(FLOAT, b.Aug)
                             END ), 2) AS DECIMAL(18, 2)) Aug ,
                CAST(ROUND(( CASE ISNULL(b.Sep, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Sep)
                                    / CONVERT(FLOAT, b.Sep)
                             END ), 2) AS DECIMAL(18, 2)) Sep ,
                CAST(ROUND(( CASE ISNULL(b.Oct, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Oct)
                                    / CONVERT(FLOAT, b.Oct)
                             END ), 2) AS DECIMAL(18, 2)) Oct ,
                CAST(ROUND(( CASE ISNULL(b.Nov, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Nov)
                                    / CONVERT(FLOAT, b.Nov)
                             END ), 2) AS DECIMAL(18, 2)) Nov ,
                CAST(ROUND(( CASE ISNULL(b.[Dec], 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.[Dec])
                                    / CONVERT(FLOAT, b.[Dec])
                             END ), 2) AS DECIMAL(18, 2)) [Dec]
        FROM    #temp a
                LEFT JOIN #temp2 b ON a.gh = b.gh
        ORDER BY a.gh;
        DROP TABLE #temp,#temp2;
    END;
   ELSE
    BEGIN
        SELECT  'Rev.per Therapist' title ,
                cuser.gh gh ,
                cuser.Name name ,
                SUM(CASE WHEN MONTH(ISNULL(xm.ssrq, xm.sfrq)) = '01'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Jan ,
                ISNULL(SUM(CASE WHEN MONTH(ISNULL(xm.ssrq, xm.sfrq)) = '02'
                                THEN xm.dj * xm.sl
                                ELSE 0
                           END), 0.00) Feb ,
                SUM(CASE WHEN MONTH(ISNULL(xm.ssrq, xm.sfrq)) = '03'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Mar ,
                SUM(CASE WHEN MONTH(ISNULL(xm.ssrq, xm.sfrq)) = '04'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Apr ,
                SUM(CASE WHEN MONTH(ISNULL(xm.ssrq, xm.sfrq)) = '05'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) May ,
                SUM(CASE WHEN MONTH(ISNULL(xm.ssrq, xm.sfrq)) = '06'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Jun ,
                SUM(CASE WHEN MONTH(ISNULL(xm.ssrq, xm.sfrq)) = '07'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Jul ,
                SUM(CASE WHEN MONTH(ISNULL(xm.ssrq, xm.sfrq)) = '08'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Aug ,
                SUM(CASE WHEN MONTH(ISNULL(xm.ssrq, xm.sfrq)) = '09'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Sep ,
                SUM(CASE WHEN MONTH(ISNULL(xm.ssrq, xm.sfrq)) = '10'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Oct ,
                SUM(CASE WHEN MONTH(ISNULL(xm.ssrq, xm.sfrq)) = '11'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) Nov ,
                SUM(CASE WHEN MONTH(ISNULL(xm.ssrq, xm.sfrq)) = '12'
                         THEN xm.dj * xm.sl
                         ELSE 0
                    END) [Dec]
        INTO    #temp3
        FROM    mz_xm xm
                LEFT JOIN dbo.mz_gh ON xm.ghnm = mz_gh.ghnm
                                       AND mz_gh.OrganizeId = xm.OrganizeId
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ys
                                                              AND cuser.OrganizeId = @orgId
        WHERE   xm.zt = 1
                AND mz_gh.zt = 1
                AND xm.OrganizeId = @orgId
                AND YEAR(ISNULL(xm.ssrq, xm.sfrq)) = @year
        GROUP BY cuser.gh ,
                cuser.Name;
        SELECT  'Rev.per Therapist' title ,
                cuser.gh gh ,
                cuser.Name name ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '01'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Jan ,
                ISNULL(SUM(CASE WHEN MONTH(mz_cf.jsrq) = '02'
                                THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                                ELSE 0
                           END), 0.00) Feb ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '03'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Mar ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '04'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Apr ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '05'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) May ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '06'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Jun ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '07'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Jul ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '08'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Aug ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '09'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Sep ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '10'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Oct ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '11'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) Nov ,
                SUM(CASE WHEN MONTH(mz_cf.jsrq) = '12'
                         THEN dbo.mz_cfmx.dj * mz_cfmx.sl
                         ELSE 0
                    END) [Dec]
        INTO    #temp4
        FROM    dbo.mz_cfmx
                INNER JOIN dbo.mz_cf ON dbo.mz_cf.cfnm = dbo.mz_cfmx.cfnm
                                        AND dbo.mz_cf.OrganizeId = dbo.mz_cfmx.OrganizeId
                LEFT JOIN dbo.mz_gh ON mz_gh.ghnm = mz_cf.ghnm
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = mz_cf.ys
                                                              AND cuser.OrganizeId = @orgId
        WHERE   mz_cfmx.OrganizeId = @orgId
                AND mz_cfmx.zt = 1
                AND YEAR(mz_cf.jsrq) = @year
                AND dbo.mz_cf.zt = 1
                AND dbo.mz_cfmx.zt = 1
                AND dbo.mz_gh.zt = 1
        GROUP BY cuser.gh ,
                cuser.Name;
        SELECT  'Rev.per Therapist' title ,
                a.name ,
                a.gh ,
                ISNULL(a.Jan, 0.0000) + ISNULL(b.Jan, 0.0000) Jan ,
                ISNULL(a.Feb, 0.0000) + ISNULL(b.Feb, 0.0000) Feb ,
                ISNULL(a.Mar, 0.0000) + ISNULL(b.Mar, 0.0000) Mar ,
                ISNULL(a.Apr, 0.0000) + ISNULL(b.Apr, 0.0000) Apr ,
                ISNULL(a.May, 0.0000) + ISNULL(b.May, 0.0000) May ,
                ISNULL(a.Jun, 0.0000) + ISNULL(b.Jun, 0.0000) Jun ,
                ISNULL(a.Jul, 0.0000) + ISNULL(b.Jul, 0.0000) Jul ,
                ISNULL(a.Aug, 0.0000) + ISNULL(b.Aug, 0.0000) Aug ,
                ISNULL(a.Sep, 0.0000) + ISNULL(b.Sep, 0.0000) Sep ,
                ISNULL(a.Oct, 0.0000) + ISNULL(b.Oct, 0.0000) Oct ,
                ISNULL(a.Nov, 0.0000) + ISNULL(b.Nov, 0.0000) Nov ,
                ISNULL(a.[Dec], 0.0000) + ISNULL(b.[Dec], 0.0000) [Dec]
        INTO    #temp5
        FROM    #temp a
                LEFT JOIN #temp2 b ON a.gh = b.gh;


		 --总人次
        SELECT  'Patient Visit' title ,
                cuser.Name ,
                cuser.gh ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '01' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Jan ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '02' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Feb ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '03' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Mar ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '04' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Apr ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '05' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) May ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '06' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Jun ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '07' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Jul ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '08' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Aug ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '09' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Sep ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '10' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Oct ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '11' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Nov ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '12' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) [Dec]
        INTO    #temp6
        FROM    ( SELECT DISTINCT
                            aaaaaaa.*
                  FROM      ( SELECT    ghnm ,
                                        CONVERT(VARCHAR(10), ISNULL(ssrq, sfrq), 120) ssrq ,
                                        ys
                              FROM      dbo.mz_xm
                              WHERE     OrganizeId = @orgId
                                        AND zt = 1
                                        AND YEAR(ISNULL(ssrq, sfrq)) = @year
                              GROUP BY  ghnm ,
                                        ISNULL(ssrq, sfrq) ,
                                        ys
                              UNION ALL
                              SELECT    ghnm ,
                                        CONVERT(VARCHAR(10), jsrq, 120) ssrq ,
                                        ys
                              FROM      dbo.mz_cfmx
                                        LEFT JOIN dbo.mz_cf ON dbo.mz_cf.cfnm = dbo.mz_cfmx.cfnm
                                                              AND dbo.mz_cf.OrganizeId = dbo.mz_cfmx.OrganizeId
                              WHERE     mz_cfmx.OrganizeId = @orgId
                                        AND mz_cfmx.zt = 1
                                        AND YEAR(jsrq) = @year
                                        AND dbo.mz_cf.zt = 1
                                        AND dbo.mz_cfmx.zt = 1
                              GROUP BY  ghnm ,
                                        jsrq ,
                                        ys
                            ) aaaaaaa
                            LEFT JOIN dbo.mz_gh ON aaaaaaa.ghnm = mz_gh.ghnm
                                                   AND mz_gh.OrganizeId = @orgId
                  WHERE     dbo.mz_gh.zt = 1
                ) xm
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ys
                                                              AND cuser.OrganizeId = @orgId
        GROUP BY cuser.Name ,
                cuser.gh;


--平均费用
        SELECT  'Avg Rev.per Therapist' title ,
                a.name ,
                a.gh ,
                CAST(ROUND(( CASE ISNULL(b.Jan, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Jan)
                                    / CONVERT(FLOAT, b.Jan)
                             END ), 2) AS DECIMAL(18, 2)) Jan ,
                CAST(ROUND(( CASE ISNULL(b.Feb, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Feb)
                                    / CONVERT(FLOAT, b.Feb)
                             END ), 2) AS DECIMAL(18, 2)) Feb ,
                CAST(ROUND(( CASE ISNULL(b.Mar, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Mar)
                                    / CONVERT(FLOAT, b.Mar)
                             END ), 2) AS DECIMAL(18, 2)) Mar ,
                CAST(ROUND(( CASE ISNULL(b.Apr, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Apr)
                                    / CONVERT(FLOAT, b.Apr)
                             END ), 2) AS DECIMAL(18, 2)) Apr ,
                CAST(ROUND(( CASE ISNULL(b.May, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.May)
                                    / CONVERT(FLOAT, b.May)
                             END ), 2) AS DECIMAL(18, 2)) May ,
                CAST(ROUND(( CASE ISNULL(b.Jun, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Jun)
                                    / CONVERT(FLOAT, b.Jun)
                             END ), 2) AS DECIMAL(18, 2)) Jun ,
                CAST(ROUND(( CASE ISNULL(b.Jul, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Jul)
                                    / CONVERT(FLOAT, b.Jul)
                             END ), 2) AS DECIMAL(18, 2)) Jul ,
                CAST(ROUND(( CASE ISNULL(b.Aug, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Aug)
                                    / CONVERT(FLOAT, b.Aug)
                             END ), 2) AS DECIMAL(18, 2)) Aug ,
                CAST(ROUND(( CASE ISNULL(b.Sep, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Sep)
                                    / CONVERT(FLOAT, b.Sep)
                             END ), 2) AS DECIMAL(18, 2)) Sep ,
                CAST(ROUND(( CASE ISNULL(b.Oct, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Oct)
                                    / CONVERT(FLOAT, b.Oct)
                             END ), 2) AS DECIMAL(18, 2)) Oct ,
                CAST(ROUND(( CASE ISNULL(b.Nov, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Nov)
                                    / CONVERT(FLOAT, b.Nov)
                             END ), 2) AS DECIMAL(18, 2)) Nov ,
                CAST(ROUND(( CASE ISNULL(b.[Dec], 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.[Dec])
                                    / CONVERT(FLOAT, b.[Dec])
                             END ), 2) AS DECIMAL(18, 2)) [Dec]
        FROM    #temp5 a
                LEFT JOIN #temp6 b ON a.gh = b.gh
        ORDER BY a.gh;
        DROP TABLE #temp5,#temp6,#temp3,#temp4;
    END; ");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                    new SqlParameter("@year",year)
                };
            return FindList<TherapistVisit>(sqlStr.ToString(), param);
        }

        /// <summary>
        /// 获取治疗师平均治疗人次（总人次/总天数）
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TherapistVisit> GetTherapistavgVisit(string year, string orgId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@" DECLARE @cnt INT;
 SELECT @cnt = cnt
 FROM   [dbo].[CntByOrgId](@orgId);
 IF ( @cnt > 0 )--存在执行表记录，按照执行逻辑走,否则按照mz_xm逻辑走
    BEGIN
  --总人次
        SELECT  'Patient Visit' title ,
                xm.name ,
                xm.gh ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '01' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Jan ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '02' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Feb ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '03' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Mar ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '04' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Apr ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '05' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) May ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '06' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Jun ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '07' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Jul ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '08' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Aug ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '09' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Sep ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '10' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Oct ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '11' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Nov ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '12' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) [Dec]
        INTO    #temp
        FROM    ( SELECT    jh.ghnm ,
                            CONVERT(VARCHAR(10), zx.zxsj, 120) ssrq ,
                            zx.zlsgh gh ,
                            cuser.Name name
                  FROM      dbo.mz_jzjhmxzx (NOLOCK) zx
                            LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                          AND mx.OrganizeId = @orgId
                            INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                         AND jh.OrganizeId = @orgId
                            LEFT JOIN dbo.mz_gh ON jh.ghnm = mz_gh.ghnm
                                                   AND mz_gh.OrganizeId = @orgId
                            LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = zx.zlsgh
                                                              AND cuser.OrganizeId = @orgId
                  WHERE     zx.OrganizeId = @orgId
                            AND zx.zt = '1'
                            AND mz_gh.zt = '1'
                            AND YEAR(zx.zxsj) = @year
                  GROUP BY  jh.ghnm ,
                            zx.zxsj ,
                            zx.zlsgh ,
                            cuser.Name
                ) xm
        GROUP BY xm.name ,
                xm.gh
        ORDER BY xm.gh;

  --总天数
        SELECT  Name ,
                gh ,
                SUM(CASE WHEN MONTH(ssrq) = '01' THEN 1
                         ELSE 0
                    END) Jan ,
                ISNULL(SUM(CASE WHEN MONTH(ssrq) = '02' THEN 1
                                ELSE 0
                           END), 0.00) Feb ,
                SUM(CASE WHEN MONTH(ssrq) = '03' THEN 1
                         ELSE 0
                    END) Mar ,
                SUM(CASE WHEN MONTH(ssrq) = '04' THEN 1
                         ELSE 0
                    END) Apr ,
                SUM(CASE WHEN MONTH(ssrq) = '05' THEN 1
                         ELSE 0
                    END) May ,
                SUM(CASE WHEN MONTH(ssrq) = '06' THEN 1
                         ELSE 0
                    END) Jun ,
                SUM(CASE WHEN MONTH(ssrq) = '07' THEN 1
                         ELSE 0
                    END) Jul ,
                SUM(CASE WHEN MONTH(ssrq) = '08' THEN 1
                         ELSE 0
                    END) Aug ,
                SUM(CASE WHEN MONTH(ssrq) = '09' THEN 1
                         ELSE 0
                    END) Sep ,
                SUM(CASE WHEN MONTH(ssrq) = '10' THEN 1
                         ELSE 0
                    END) Oct ,
                SUM(CASE WHEN MONTH(ssrq) = '11' THEN 1
                         ELSE 0
                    END) Nov ,
                SUM(CASE WHEN MONTH(ssrq) = '12' THEN 1
                         ELSE 0
                    END) [Dec]
        INTO    #temp2
        FROM    ( SELECT    zx.zxsj ssrq ,
                            zx.zlsgh gh ,
                            cuser.Name
                  FROM      dbo.mz_jzjhmxzx (NOLOCK) zx
                            LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                          AND mx.OrganizeId = @orgId
                            INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                         AND jh.OrganizeId = @orgId
                            LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = zx.zlsgh
                                                              AND cuser.OrganizeId = @orgId
                  WHERE     zx.OrganizeId = @orgId
                            AND zx.zt = 1
                            AND YEAR(zx.zxsj) = @year
                            AND EXISTS ( SELECT ghnm
                                         FROM   mz_gh
                                         WHERE  ghnm = jh.ghnm
                                                AND zt = 1 )
                  GROUP BY  zx.zxsj ,
                            zx.zlsgh ,
                            cuser.Name
                ) ccc
        GROUP BY Name ,
                gh;

        SELECT  'Avg Patient Visit' title ,
                a.name ,
                a.gh ,
                CAST(ROUND(( CASE ISNULL(b.Jan, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Jan)
                                    / CONVERT(FLOAT, b.Jan)
                             END ), 2) AS DECIMAL(18, 2)) Jan ,
                CAST(ROUND(( CASE ISNULL(b.Feb, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Feb)
                                    / CONVERT(FLOAT, b.Feb)
                             END ), 2) AS DECIMAL(18, 2)) Feb ,
                CAST(ROUND(( CASE ISNULL(b.Mar, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Mar)
                                    / CONVERT(FLOAT, b.Mar)
                             END ), 2) AS DECIMAL(18, 2)) Mar ,
                CAST(ROUND(( CASE ISNULL(b.Apr, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Apr)
                                    / CONVERT(FLOAT, b.Apr)
                             END ), 2) AS DECIMAL(18, 2)) Apr ,
                CAST(ROUND(( CASE ISNULL(b.May, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.May)
                                    / CONVERT(FLOAT, b.May)
                             END ), 2) AS DECIMAL(18, 2)) May ,
                CAST(ROUND(( CASE ISNULL(b.Jun, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Jun)
                                    / CONVERT(FLOAT, b.Jun)
                             END ), 2) AS DECIMAL(18, 2)) Jun ,
                CAST(ROUND(( CASE ISNULL(b.Jul, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Jul)
                                    / CONVERT(FLOAT, b.Jul)
                             END ), 2) AS DECIMAL(18, 2)) Jul ,
                CAST(ROUND(( CASE ISNULL(b.Aug, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Aug)
                                    / CONVERT(FLOAT, b.Aug)
                             END ), 2) AS DECIMAL(18, 2)) Aug ,
                CAST(ROUND(( CASE ISNULL(b.Sep, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Sep)
                                    / CONVERT(FLOAT, b.Sep)
                             END ), 2) AS DECIMAL(18, 2)) Sep ,
                CAST(ROUND(( CASE ISNULL(b.Oct, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Oct)
                                    / CONVERT(FLOAT, b.Oct)
                             END ), 2) AS DECIMAL(18, 2)) Oct ,
                CAST(ROUND(( CASE ISNULL(b.Nov, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Nov)
                                    / CONVERT(FLOAT, b.Nov)
                             END ), 2) AS DECIMAL(18, 2)) Nov ,
                CAST(ROUND(( CASE ISNULL(b.[Dec], 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.[Dec])
                                    / CONVERT(FLOAT, b.[Dec])
                             END ), 2) AS DECIMAL(18, 2)) [Dec]
        FROM    #temp a
                LEFT JOIN #temp2 b ON a.gh = b.gh
        ORDER BY a.gh;
        DROP TABLE  #temp,#temp2;
    END;
 ELSE
    BEGIN
        SELECT  'Patient Visit' title ,
                cuser.Name ,
                cuser.gh ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '01' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Jan ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '02' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Feb ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '03' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Mar ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '04' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Apr ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '05' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) May ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '06' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Jun ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '07' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Jul ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '08' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Aug ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '09' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Sep ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '10' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Oct ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '11' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) Nov ,
                CAST(SUM(CASE WHEN MONTH(ssrq) = '12' THEN 1
                              ELSE 0
                         END) AS DECIMAL(18, 2)) [Dec]
        INTO    #temp3
        FROM    ( SELECT DISTINCT
                            aaaaaaa.*
                  FROM      ( SELECT    ghnm ,
                                        CONVERT(VARCHAR(10), ISNULL(ssrq, sfrq), 120) ssrq ,
                                        ys
                              FROM      dbo.mz_xm
                              WHERE     OrganizeId = @orgId
                                        AND zt = 1
                                        AND YEAR(ISNULL(ssrq, sfrq)) = @year
                              GROUP BY  ghnm ,
                                        ISNULL(ssrq, sfrq) ,
                                        ys
                              UNION ALL
                              SELECT    ghnm ,
                                        CONVERT(VARCHAR(10), jsrq, 120) ssrq ,
                                        ys
                              FROM      dbo.mz_cfmx
                                        LEFT JOIN dbo.mz_cf ON dbo.mz_cf.cfnm = dbo.mz_cfmx.cfnm
                                                              AND dbo.mz_cf.OrganizeId = dbo.mz_cfmx.OrganizeId
                              WHERE     mz_cfmx.OrganizeId = @orgId
                                        AND mz_cfmx.zt = 1
                                        AND YEAR(jsrq) = @year
                                        AND dbo.mz_cf.zt = 1
                                        AND dbo.mz_cfmx.zt = 1
                              GROUP BY  ghnm ,
                                        jsrq ,
                                        ys
                            ) aaaaaaa
                            LEFT JOIN dbo.mz_gh ON aaaaaaa.ghnm = mz_gh.ghnm
                  WHERE     dbo.mz_gh.zt = 1
                ) xm
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ys
                                                              AND cuser.OrganizeId = @orgId
        GROUP BY cuser.Name ,
                cuser.gh;

  --总天数
        SELECT  cuser.Name ,
                cuser.gh ,
                SUM(CASE WHEN MONTH(ssrq) = '01' THEN 1
                         ELSE 0
                    END) Jan ,
                ISNULL(SUM(CASE WHEN MONTH(ssrq) = '02' THEN 1
                                ELSE 0
                           END), 0.00) Feb ,
                SUM(CASE WHEN MONTH(ssrq) = '03' THEN 1
                         ELSE 0
                    END) Mar ,
                SUM(CASE WHEN MONTH(ssrq) = '04' THEN 1
                         ELSE 0
                    END) Apr ,
                SUM(CASE WHEN MONTH(ssrq) = '05' THEN 1
                         ELSE 0
                    END) May ,
                SUM(CASE WHEN MONTH(ssrq) = '06' THEN 1
                         ELSE 0
                    END) Jun ,
                SUM(CASE WHEN MONTH(ssrq) = '07' THEN 1
                         ELSE 0
                    END) Jul ,
                SUM(CASE WHEN MONTH(ssrq) = '08' THEN 1
                         ELSE 0
                    END) Aug ,
                SUM(CASE WHEN MONTH(ssrq) = '09' THEN 1
                         ELSE 0
                    END) Sep ,
                SUM(CASE WHEN MONTH(ssrq) = '10' THEN 1
                         ELSE 0
                    END) Oct ,
                SUM(CASE WHEN MONTH(ssrq) = '11' THEN 1
                         ELSE 0
                    END) Nov ,
                SUM(CASE WHEN MONTH(ssrq) = '12' THEN 1
                         ELSE 0
                    END) [Dec]
        INTO    #temp4
        FROM    ( SELECT DISTINCT
                            aaaaaaa.*
                  FROM      ( SELECT    ISNULL(ssrq, sfrq) ssrq ,
                                        ys
                              FROM      dbo.mz_xm
                              WHERE     OrganizeId = @orgId
                                        AND zt = 1
                                        AND YEAR(ISNULL(ssrq, sfrq)) = @year
                                        AND EXISTS ( SELECT ghnm
                                                     FROM   mz_gh
                                                     WHERE  ghnm = mz_xm.ghnm
                                                            AND zt = 1 )
                              GROUP BY  ISNULL(ssrq, sfrq) ,
                                        ys
                              UNION ALL
                              SELECT    jsrq ssrq ,
                                        ys
                              FROM      dbo.mz_cfmx
                                        LEFT JOIN dbo.mz_cf ON dbo.mz_cf.cfnm = dbo.mz_cfmx.cfnm
                                                              AND dbo.mz_cf.OrganizeId = dbo.mz_cfmx.OrganizeId
                              WHERE     mz_cfmx.OrganizeId = @orgId
                                        AND mz_cfmx.zt = 1
                                        AND YEAR(jsrq) = @year
                                        AND dbo.mz_cf.zt = 1
                                        AND dbo.mz_cfmx.zt = 1
                                        AND EXISTS ( SELECT ghnm
                                                     FROM   mz_gh
                                                     WHERE  ghnm = mz_cf.ghnm
                                                            AND zt = 1 )
                              GROUP BY  jsrq ,
                                        ys
                            ) aaaaaaa
                ) ccc
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = ccc.ys
                                                              AND OrganizeId = @orgId
        GROUP BY cuser.Name ,
                cuser.gh;

        SELECT  'Avg Patient Visit' title ,
                a.Name ,
                a.gh ,
                CAST(ROUND(( CASE ISNULL(b.Jan, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Jan)
                                    / CONVERT(FLOAT, b.Jan)
                             END ), 2) AS DECIMAL(18, 2)) Jan ,
                CAST(ROUND(( CASE ISNULL(b.Feb, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Feb)
                                    / CONVERT(FLOAT, b.Feb)
                             END ), 2) AS DECIMAL(18, 2)) Feb ,
                CAST(ROUND(( CASE ISNULL(b.Mar, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Mar)
                                    / CONVERT(FLOAT, b.Mar)
                             END ), 2) AS DECIMAL(18, 2)) Mar ,
                CAST(ROUND(( CASE ISNULL(b.Apr, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Apr)
                                    / CONVERT(FLOAT, b.Apr)
                             END ), 2) AS DECIMAL(18, 2)) Apr ,
                CAST(ROUND(( CASE ISNULL(b.May, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.May)
                                    / CONVERT(FLOAT, b.May)
                             END ), 2) AS DECIMAL(18, 2)) May ,
                CAST(ROUND(( CASE ISNULL(b.Jun, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Jun)
                                    / CONVERT(FLOAT, b.Jun)
                             END ), 2) AS DECIMAL(18, 2)) Jun ,
                CAST(ROUND(( CASE ISNULL(b.Jul, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Jul)
                                    / CONVERT(FLOAT, b.Jul)
                             END ), 2) AS DECIMAL(18, 2)) Jul ,
                CAST(ROUND(( CASE ISNULL(b.Aug, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Aug)
                                    / CONVERT(FLOAT, b.Aug)
                             END ), 2) AS DECIMAL(18, 2)) Aug ,
                CAST(ROUND(( CASE ISNULL(b.Sep, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Sep)
                                    / CONVERT(FLOAT, b.Sep)
                             END ), 2) AS DECIMAL(18, 2)) Sep ,
                CAST(ROUND(( CASE ISNULL(b.Oct, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Oct)
                                    / CONVERT(FLOAT, b.Oct)
                             END ), 2) AS DECIMAL(18, 2)) Oct ,
                CAST(ROUND(( CASE ISNULL(b.Nov, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.Nov)
                                    / CONVERT(FLOAT, b.Nov)
                             END ), 2) AS DECIMAL(18, 2)) Nov ,
                CAST(ROUND(( CASE ISNULL(b.[Dec], 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.[Dec])
                                    / CONVERT(FLOAT, b.[Dec])
                             END ), 2) AS DECIMAL(18, 2)) [Dec]
        FROM    #temp3 a
                LEFT JOIN #temp4 b ON a.gh = b.gh
        ORDER BY a.gh;
        DROP TABLE  #temp3,#temp4;
    END;");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                     new SqlParameter("@year",year)
                };
            return FindList<TherapistVisit>(sqlStr.ToString(), param);
        }

        /// <summary>
        /// 获取治疗师月度平均治疗人次（总人次/总天数）
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TherapistMonthVisit> GetMonthTherapistavgVisit(string year, string orgId,string month)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"DECLARE @cnt INT;
SELECT  @cnt = cnt
FROM    [dbo].[CntByOrgId](@orgId);
IF ( @cnt > 0 )--存在执行表记录，按照执行逻辑走,否则按照mz_xm逻辑走
    BEGIN
		--总人次
        SELECT  'Patient Visit' title ,
                xm.gh ,
                xm.name name ,
                CONVERT(VARCHAR(10), xm.ssrq, 25) ssrq ,
                COUNT(1) zrc
        INTO    #temp
        FROM    ( SELECT    dbo.mz_gh.ghnm ,
                            CONVERT(VARCHAR(10), zx.zxsj, 120) ssrq ,
                            zx.zlsgh gh ,
                            cuser.Name name
                  FROM      dbo.mz_jzjhmxzx (NOLOCK) zx
                            LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                          AND mx.OrganizeId = @orgId
                            INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                         AND jh.OrganizeId = @orgId
                            LEFT JOIN dbo.mz_gh ON jh.ghnm = mz_gh.ghnm
                                                   AND mz_gh.OrganizeId = @orgId
                            LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = zx.zlsgh
                                                              AND cuser.OrganizeId = @orgId
                  WHERE     zx.OrganizeId = @orgId
                            AND zx.zt = 1
                            AND dbo.mz_gh.zt = '1'
                            AND YEAR(zx.zxsj) = @year
                            AND MONTH(zx.zxsj) = @month
                  GROUP BY  dbo.mz_gh.ghnm ,
                            zx.zxsj ,
                            zx.zlsgh ,
                            cuser.Name
                ) xm
        WHERE   ISNULL(gh, '') <> ''
        GROUP BY xm.ssrq ,
                xm.gh ,
                xm.name;

        --总天数
        SELECT  COUNT(1) zrs ,
                xm.gh
        INTO    #temp2
        FROM    ( SELECT    MONTH(zx.zxsj) ssrq ,
                            zx.zlsgh gh
                  FROM      dbo.mz_jzjhmxzx (NOLOCK) zx
                            LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                          AND mx.OrganizeId = @orgId
                            INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                         AND jh.OrganizeId = @orgId
                            LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = zx.zlsgh
                                                              AND cuser.OrganizeId = @orgId
                  WHERE     zx.OrganizeId = @orgId
                            AND zx.zt = 1
                            AND YEAR(zx.zxsj) = @year
                            AND EXISTS ( SELECT ghnm
                                         FROM   mz_gh
                                         WHERE  ghnm = jh.ghnm
                                                AND zt = 1 )
                  GROUP BY  zx.zxsj ,
                            zx.zlsgh
                ) xm
        GROUP BY xm.gh;

        SELECT  'Avg Patient Visit' title ,
                cuser.Name name ,
                a.gh gh ,
                a.ssrq ,
                CAST(ROUND(( CASE ISNULL(b.zrs, 0)
                               WHEN 0 THEN 0
                               ELSE CONVERT(FLOAT, a.zrc)
                                    / CONVERT(FLOAT, b.zrs)
                             END ), 2) AS DECIMAL(18, 2)) yxris
        FROM    #temp a
                LEFT JOIN #temp2 b ON a.gh = b.gh
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = a.gh
                                                              AND OrganizeId = @orgId
        ORDER BY a.gh;
        DROP TABLE  #temp,#temp2;
    END;
ELSE
    BEGIN
        SELECT 'Patient Visit' title ,
        xm.ys ,
        CONVERT(VARCHAR(10), xm.ssrq, 25) ssrq ,
        COUNT(1) zrc
 INTO   #temp3
 FROM   ( SELECT DISTINCT
                    aaaaaaa.*
          FROM      ( SELECT    ghnm ,
                                CONVERT(VARCHAR(10), ISNULL(ssrq, sfrq), 120) ssrq ,
                                ys
                      FROM      dbo.mz_xm
                      WHERE     OrganizeId = @orgId
                                AND zt = 1
                                AND YEAR(ISNULL(ssrq, sfrq)) = @year
                                AND MONTH(ISNULL(ssrq, sfrq)) = @month
                      GROUP BY  ghnm ,
                                ISNULL(ssrq, sfrq) ,
                                ys
                      UNION ALL
                      SELECT    ghnm ,
                                CONVERT(VARCHAR(10), jsrq, 120) ssrq ,
                                ys
                      FROM      dbo.mz_cfmx
                                LEFT JOIN dbo.mz_cf ON dbo.mz_cf.cfnm = dbo.mz_cfmx.cfnm
                                                       AND dbo.mz_cf.OrganizeId = dbo.mz_cfmx.OrganizeId
                      WHERE     mz_cfmx.OrganizeId = @orgId
                                AND mz_cfmx.zt = 1
                                AND YEAR(jsrq) = @year
                                AND MONTH(jsrq) = @month
                                AND dbo.mz_cf.zt = 1
                                AND dbo.mz_cfmx.zt = 1
                      GROUP BY  ghnm ,
                                jsrq ,
                                ys
                    ) aaaaaaa
                    LEFT JOIN dbo.mz_gh ON aaaaaaa.ghnm = mz_gh.ghnm
          WHERE     dbo.mz_gh.zt = 1
        ) xm
 GROUP BY xm.ys ,
        xm.ssrq;

--总天数
 SELECT COUNT(1) zrs ,
        xm.ys
 INTO   #temp4
 FROM   ( SELECT DISTINCT
                    aaaaaaa.*
          FROM      ( SELECT    CONVERT(VARCHAR(10), ISNULL(ssrq, sfrq), 120) ssrq ,
                                ys
                      FROM      dbo.mz_xm
                      WHERE     OrganizeId = @orgId
                                AND zt = 1
                                AND YEAR(ISNULL(ssrq, sfrq)) = @year
                                AND MONTH(ISNULL(ssrq, sfrq)) = @month
                                AND EXISTS ( SELECT ghnm
                                             FROM   mz_gh
                                             WHERE  ghnm = mz_xm.ghnm
                                                    AND zt = 1 )
                      GROUP BY  ISNULL(ssrq, sfrq) ,
                                ys
                      UNION ALL
                      SELECT    CONVERT(VARCHAR(10), jsrq, 120) ssrq ,
                                ys
                      FROM      dbo.mz_cfmx
                                LEFT JOIN dbo.mz_cf ON dbo.mz_cf.cfnm = dbo.mz_cfmx.cfnm
                                                       AND dbo.mz_cf.OrganizeId = dbo.mz_cfmx.OrganizeId
                      WHERE     mz_cfmx.OrganizeId = @orgId
                                AND mz_cfmx.zt = 1
                                AND YEAR(jsrq) = @year
                                AND MONTH(jsrq) = @month
                                AND dbo.mz_cf.zt = 1
                                AND dbo.mz_cfmx.zt = 1
                                AND EXISTS ( SELECT ghnm
                                             FROM   mz_gh
                                             WHERE  ghnm = mz_cf.ghnm
                                                    AND zt = 1 )
                      GROUP BY  jsrq ,
                                ys
                    ) aaaaaaa
        ) xm
 GROUP BY xm.ys; 

 SELECT 'Avg Patient Visit' title ,
        cuser.Name name ,
        a.ys gh ,
        a.ssrq ,
        CAST(ROUND(( CASE ISNULL(b.zrs, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.zrc) / CONVERT(FLOAT, b.zrs)
                     END ), 2) AS DECIMAL(18, 2)) yxris
 FROM   #temp3 a
        LEFT JOIN #temp4 b ON a.ys = b.ys
        LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = a.ys
                                                              AND OrganizeId = @orgId
 ORDER BY a.ys;
 DROP TABLE  #temp3,#temp4;
    END;");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                     new SqlParameter("@year",year),
                     new SqlParameter("@month",month)
                };
            return FindList<TherapistMonthVisit>(sqlStr.ToString(), param);
        }

        /// <summary>
        /// 获取治疗师治疗总费用
        /// </summary>
        public List<TherapistMonthCharge> GetMonthTherapistDischarge(string year, string orgId, string month)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"   DECLARE @cnt INT;
               SELECT   @cnt = cnt
               FROM     [dbo].[CntByOrgId](@orgId);
               IF ( @cnt > 0 )--存在执行表记录，按照执行逻辑走,否则按照mz_xm逻辑走
                BEGIN
                SELECT  aaa.title ,
                aaa.gh ,
                aaa.name ,
                aaa.ssrq ,
                SUM(aaa.zje) zje
        FROM    ( SELECT    'Rev.per Therapist' title ,
                            zx.zlsgh gh ,
                            cuser.Name name ,
                            CONVERT(VARCHAR(10), zx.zxsj, 25) ssrq ,
                            SUM(xm.dj * zx.sl) zje
                  FROM      dbo.mz_jzjhmxzx (NOLOCK) zx
                            LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                          AND mx.OrganizeId = @orgId
                            INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                         AND jh.OrganizeId = @orgId
                            LEFT JOIN mz_xm xm ON xm.jzjhmxId = mx.jzjhmxId
                                                  AND xm.OrganizeId = @orgId
                            LEFT JOIN dbo.mz_gh ON jh.ghnm = mz_gh.ghnm
                                                   AND mz_gh.OrganizeId = xm.OrganizeId
                            LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = zx.zlsgh
                                                              AND cuser.OrganizeId = @orgId
                  WHERE     xm.zt = 1
                            AND mz_gh.zt = 1
                            AND xm.OrganizeId = @orgId
                            AND YEAR(zx.zxsj) = @year
                            AND MONTH(zx.zxsj) = @month
                            AND ISNULL(cuser.gh, '') <> ''
                  GROUP BY  zx.zxsj ,
                            zx.zlsgh ,
                            cuser.Name
                ) aaa
        GROUP BY aaa.gh ,
                aaa.name ,
                aaa.ssrq ,
                aaa.title;
    END;
   ELSE
    BEGIN
         --项目总费用
        SELECT  'Rev.per Therapist' title ,
                cuser.gh gh ,
                cuser.Name name ,
                CONVERT(VARCHAR(10), ISNULL(xm.ssrq, xm.sfrq), 25) ssrq ,
                SUM(xm.dj * xm.sl) zje
        INTO    #temp
        FROM    mz_xm xm
                LEFT JOIN dbo.mz_gh ON xm.ghnm = mz_gh.ghnm
                                       AND mz_gh.OrganizeId = xm.OrganizeId
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ys
                                                              AND cuser.OrganizeId = @orgId
        WHERE   xm.zt = 1
                AND mz_gh.zt = 1
                AND xm.OrganizeId = @orgId
                AND YEAR(ISNULL(xm.ssrq, xm.sfrq)) = @year
                AND MONTH(ISNULL(xm.ssrq, xm.sfrq)) = @month
                AND ISNULL(cuser.gh, '') <> ''
        GROUP BY ISNULL(xm.ssrq, xm.sfrq) ,
                cuser.gh ,
                cuser.Name;

        --处方总费用
        SELECT  'Rev.per Therapist' title ,
                cuser.gh gh ,
                cuser.Name name ,
                CONVERT(VARCHAR(10), mz_cf.jsrq, 25) ssrq ,
                SUM(mz_cfmx.dj * mz_cfmx.sl) zje
        INTO    #temp2
        FROM    dbo.mz_cfmx
                INNER JOIN dbo.mz_cf ON dbo.mz_cf.cfnm = dbo.mz_cfmx.cfnm
                                        AND dbo.mz_cf.OrganizeId = dbo.mz_cfmx.OrganizeId
                LEFT JOIN dbo.mz_gh ON mz_gh.ghnm = mz_cf.ghnm
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = mz_cf.ys
                                                              AND cuser.OrganizeId = @orgId
        WHERE   mz_cfmx.OrganizeId = @orgId
                AND mz_cfmx.zt = 1
                AND YEAR(mz_cf.jsrq) = @year
                AND MONTH(mz_cf.jsrq) = @month
                AND dbo.mz_cf.zt = 1
                AND dbo.mz_cfmx.zt = 1
                AND dbo.mz_gh.zt = 1
        GROUP BY CONVERT(VARCHAR(10), mz_cf.jsrq, 25) ,
                cuser.gh ,
                cuser.Name;
		
        --门诊项目+处方合计费用
        SELECT  title ,
                aaa.name ,
                ssrq ,
                aaa.gh ,
                SUM(zje) zje
        FROM    ( SELECT    *
                  FROM      #temp
                  UNION ALL
                  SELECT    *
                  FROM      #temp2
                ) aaa
        GROUP BY aaa.gh ,
                aaa.name ,
                aaa.ssrq ,
                aaa.title;
        DROP TABLE #temp,#temp2; END;");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                    new SqlParameter("@year",year),
                    new SqlParameter("@month",month)
                };
            return FindList<TherapistMonthCharge>(sqlStr.ToString(), param);
        }


        /// <summary>
        /// 获取月度平均费用（总费用/总人次）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TherapistMonthCharge> GetMonthTherapistavgDischarge(string year, string orgId, string month)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"   DECLARE @cnt INT;
        SELECT  @cnt = cnt
        FROM    [dbo].[CntByOrgId](@orgId);
        IF ( @cnt > 0 )--存在执行表记录，按照执行逻辑走,否则按照mz_xm逻辑走
            BEGIN
			--项目总费用
SELECT  aaa.title ,
        aaa.gh ,
        aaa.name ,
        aaa.ssrq ,
        SUM(aaa.zje) zje
INTO    #temp
FROM    ( SELECT    'Rev.per Therapist' title ,
                    zx.zlsgh gh ,
                    cuser.Name name ,
                    CONVERT(VARCHAR(10), zx.zxsj, 25) ssrq ,
                    SUM(xm.dj * zx.sl) zje
          FROM      dbo.mz_jzjhmxzx (NOLOCK) zx
                    LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                  AND mx.OrganizeId = @orgId
                    INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                 AND jh.OrganizeId = @orgId
                    LEFT JOIN mz_xm xm ON xm.jzjhmxId = mx.jzjhmxId
                                          AND xm.OrganizeId = @orgId
                    LEFT JOIN dbo.mz_gh ON jh.ghnm = mz_gh.ghnm
                                           AND mz_gh.OrganizeId = xm.OrganizeId
                    LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = zx.zlsgh
                                                              AND cuser.OrganizeId = @orgId
          WHERE     xm.zt = 1
                    and zx.zt=1
                    AND mz_gh.zt = 1
                    AND xm.OrganizeId = @orgId
                    AND YEAR(zx.zxsj) = @year
                    AND MONTH(zx.zxsj) = @month
                    AND ISNULL(cuser.gh, '') <> ''
          GROUP BY  zx.zxsj ,
                    zx.zlsgh ,
                    cuser.Name
        ) aaa
GROUP BY aaa.gh ,
        aaa.name ,
        aaa.ssrq ,
        aaa.title;

 --总人次
SELECT  'Patient Visit' title ,
        xm.gh ,
        xm.name name ,
        CONVERT(VARCHAR(10), xm.ssrq, 25) ssrq ,
       COUNT(1) zrc
INTO    #temp2
FROM    ( SELECT    dbo.mz_gh.ghnm ,
                    CONVERT(VARCHAR(10), zx.zxsj, 120) ssrq ,
                    zx.zlsgh gh ,
                    cuser.Name name
          FROM      dbo.mz_jzjhmxzx (NOLOCK) zx
                    LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                  AND mx.OrganizeId = @orgId
                    INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                 AND jh.OrganizeId = @orgId
                    LEFT JOIN dbo.mz_gh ON jh.ghnm = mz_gh.ghnm
                                           AND mz_gh.OrganizeId = @orgId
                    LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = zx.zlsgh
                                                              AND cuser.OrganizeId = @orgId
          WHERE     zx.OrganizeId = @orgId
                    AND zx.zt = 1
                    AND dbo.mz_gh.zt = '1'
                    AND YEAR(zx.zxsj) = @year
                    AND MONTH(zx.zxsj) = @month
          GROUP BY  dbo.mz_gh.ghnm ,
                    zx.zxsj ,
                    zx.zlsgh ,
                    cuser.Name
        ) xm
WHERE   ISNULL(gh, '') <> ''
GROUP BY xm.ssrq ,
        xm.gh ,
        xm.name;

--平均费用= 总费用/总人次
SELECT DISTINCT
        'Avg Rev.per Therapist' title ,
        a.Name ,
        a.gh ,
        a.ssrq ,
        CAST(ROUND(( CASE ISNULL(b.zrc, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.zje) / CONVERT(FLOAT, b.zrc)
                     END ), 2) AS DECIMAL(18, 2)) zje
FROM    #temp a
        LEFT JOIN #temp2 b ON a.gh = b.gh
                              AND a.ssrq = b.ssrq
ORDER BY a.gh;
DROP TABLE #temp,#temp2;
            END;
        ELSE
		BEGIN
		    --项目总费用
                        SELECT  'Rev.per Therapist' title ,
                                cuser.gh gh ,
                                cuser.Name name ,
                                CONVERT(VARCHAR(10), ISNULL(xm.ssrq,xm.sfrq), 25) ssrq ,
                                SUM(xm.dj * xm.sl) zje
                        INTO    #temp3
                        FROM    mz_xm xm
                                LEFT JOIN dbo.mz_gh ON xm.ghnm = mz_gh.ghnm
                                                       AND mz_gh.OrganizeId = xm.OrganizeId
                                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ys
                          AND cuser.OrganizeId = @orgId
                        WHERE   xm.zt = 1
                                AND mz_gh.zt = 1
                                AND xm.OrganizeId = @orgId
                                AND YEAR(ISNULL(ssrq,sfrq)) = @year
                                AND MONTH(ISNULL(ssrq,sfrq)) = @month
                        GROUP BY ISNULL(ssrq, sfrq) ,
                                cuser.gh ,
                                cuser.Name;

                        --处方总费用
                        SELECT  'Rev.per Therapist' title ,
                                cuser.gh gh ,
                                cuser.Name name ,
                                CONVERT(VARCHAR(10), mz_cf.jsrq, 25) ssrq ,
                                SUM(mz_cfmx.dj * mz_cfmx.sl) zje
                        INTO    #temp4
                        FROM    dbo.mz_cfmx
                                INNER JOIN dbo.mz_cf ON dbo.mz_cf.cfnm = dbo.mz_cfmx.cfnm
                                                        AND dbo.mz_cf.OrganizeId = dbo.mz_cfmx.OrganizeId
                                LEFT JOIN dbo.mz_gh ON mz_gh.ghnm = mz_cf.ghnm
                                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = mz_cf.ys
                                                                                      AND cuser.OrganizeId = @orgId
                        WHERE   mz_cfmx.OrganizeId = @orgId
                                AND mz_cfmx.zt = 1
                                AND YEAR(mz_cf.jsrq) = @year
                                AND MONTH(mz_cf.jsrq) = @month
                                AND dbo.mz_cf.zt = 1
                                AND dbo.mz_cfmx.zt = 1
                                AND dbo.mz_gh.zt = 1
                        GROUP BY CONVERT(VARCHAR(10), mz_cf.jsrq, 25) ,
                                cuser.gh ,
                                cuser.Name;
		
                        --门诊项目+处方合计费用
                        SELECT  title ,
                                name ,
                                ssrq ,
                                gh ,
                                SUM(zje) zje
                        INTO    #temp5
                        FROM    ( SELECT    *
                                  FROM      #temp3
                                  UNION ALL
                                  SELECT    *
                                  FROM      #temp4
                                ) aaa
                        GROUP BY aaa.gh ,
                                aaa.name ,
                                aaa.ssrq ,
                                aaa.title;


                        --总人次
                        SELECT  'Patient Visit' title ,
                                cuser.Name ,
                                cuser.gh ,
                                CONVERT(VARCHAR(10), ssrq, 25) ssrq ,
                                COUNT(1) zrc
                        INTO    #temp6
                        FROM    ( SELECT DISTINCT
                                            aaaaaaa.*
                                  FROM      ( SELECT    ghnm ,
                                                        CONVERT(VARCHAR(10), ISNULL(ssrq,sfrq), 120) ssrq ,
                                                        ys
                                              FROM      dbo.mz_xm
                                              WHERE     OrganizeId = @orgId
                                                        AND zt = 1
                                                        AND YEAR(ISNULL(ssrq,sfrq)) = @year
                                                        AND MONTH(ISNULL(ssrq,sfrq)) = @month
                                              GROUP BY  ghnm ,
                                                        ISNULL(ssrq, sfrq) ,
                                                        ys
                                              UNION ALL
                                              SELECT    ghnm ,
                                                        CONVERT(VARCHAR(10), jsrq, 120) ssrq ,
                                                        ys
                                              FROM      dbo.mz_cfmx
                                                        LEFT JOIN dbo.mz_cf ON dbo.mz_cf.cfnm = dbo.mz_cfmx.cfnm
                                                                               AND dbo.mz_cf.OrganizeId = dbo.mz_cfmx.OrganizeId
                                              WHERE     mz_cfmx.OrganizeId = @orgId
                                                        AND mz_cfmx.zt = 1
                                                        AND YEAR(jsrq) = @year
                                                        AND MONTH(jsrq) = @month
                                                        AND dbo.mz_cf.zt = 1
                                                        AND dbo.mz_cfmx.zt = 1
                                              GROUP BY  ghnm ,
                                                        jsrq ,
                                                        ys
                                            ) aaaaaaa
                                            LEFT JOIN dbo.mz_gh ON aaaaaaa.ghnm = mz_gh.ghnm
                                                                   AND mz_gh.OrganizeId = @orgId
                                  WHERE     dbo.mz_gh.zt = 1
                                ) xm
                                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ys
                                                                                      AND cuser.OrganizeId = @orgId
                        GROUP BY cuser.Name ,
                                cuser.gh ,
                                xm.ssrq;

                    --平均费用= 总费用/总人次
                    SELECT DISTINCT
                    'Avg Rev.per Therapist' title ,
                    a.name ,
                    a.gh ,
                    a.ssrq ,
                    CAST(ROUND(( CASE ISNULL(b.zrc, 0)
                                   WHEN 0 THEN 0
                                   ELSE CONVERT(FLOAT, a.zje) / CONVERT(FLOAT, b.zrc)
                                 END ), 2) AS DECIMAL(18, 2)) zje
            FROM    #temp5 a
                    LEFT JOIN #temp6 b ON a.gh = b.gh
                                          AND a.ssrq = b.ssrq
            ORDER BY a.gh;
            DROP TABLE #temp,#temp2,#temp3,#temp4,#temp5,#temp6;
            END;");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                    new SqlParameter("@year",year),
                    new SqlParameter("@month",month)
                };
            return FindList<TherapistMonthCharge>(sqlStr.ToString(), param);
        }


        /// <summary>
        /// 获取治疗师月度治疗人数
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TherapistMonthVisit> GetMonthTherapistVisit(string year, string orgId, string month)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"   DECLARE @cnt INT;
                           SELECT   @cnt = cnt
                           FROM     [dbo].[CntByOrgId](@orgId);
                           IF ( @cnt > 0 )--存在执行表记录，按照执行逻辑走,否则按照mz_xm逻辑走
                            BEGIN
                            SELECT  'Patient Visit' title ,
                            xm.gh ,
                            xm.name name ,
                            CONVERT(VARCHAR(10), xm.ssrq, 25) ssrq ,
                            CAST(COUNT(1) AS DECIMAL(18, 2)) yxris
                    FROM    ( SELECT  aaa.* ,
            cuser.Name name
            FROM    ( SELECT    dbo.mz_gh.ghnm ,
                                CONVERT(VARCHAR(10), zx.zxsj, 120) ssrq ,
                                zx.zlsgh gh
                      FROM      dbo.mz_jzjhmxzx (NOLOCK) zx
                                LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                              AND mx.OrganizeId = @orgId
                                INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                             AND jh.OrganizeId = @orgId
                                LEFT JOIN dbo.mz_gh ON jh.ghnm = mz_gh.ghnm
                                                       AND mz_gh.OrganizeId = @orgId
                      WHERE     zx.OrganizeId = @orgId
                                AND zx.zt = 1
                                AND dbo.mz_gh.zt = '1'
                                AND YEAR(zx.zxsj) = @year
                                AND MONTH(zx.zxsj) = @month
                      GROUP BY  dbo.mz_gh.ghnm ,
                                zx.zxsj ,
                                zx.zlsgh
                    ) aaa
                    LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = aaa.gh
                                                                          AND cuser.OrganizeId = @orgId
                ) xm
        WHERE   ISNULL(gh, '') <> ''
        GROUP BY xm.ssrq ,
                xm.gh ,
                xm.name;
    END;
   ELSE
    BEGIN
        SELECT  'Patient Visit' title ,
                cuser.gh ,
                cuser.Name name ,
                CONVERT(VARCHAR(10), xm.ssrq, 25) ssrq ,
                CAST(COUNT(1) AS DECIMAL(18, 2)) yxris
        FROM    ( SELECT DISTINCT
                            aaaaaaa.*
                  FROM      ( SELECT    ghnm ,
                                        CONVERT(VARCHAR(10), ISNULL(ssrq, sfrq), 120) ssrq ,
                                        ys
                              FROM      dbo.mz_xm
                              WHERE     OrganizeId = @orgId
                                        AND zt = 1
                                        AND YEAR(ISNULL(ssrq, sfrq)) = @year
                                        AND MONTH(ISNULL(ssrq, sfrq)) = @month
                              GROUP BY  ghnm ,
                                        ISNULL(ssrq, sfrq) ,
                                        ys
                              UNION ALL
                              SELECT    ghnm ,
                                        CONVERT(VARCHAR(10), jsrq, 120) ssrq ,
                                        ys
                              FROM      dbo.mz_cfmx
                                        LEFT JOIN dbo.mz_cf ON dbo.mz_cf.cfnm = dbo.mz_cfmx.cfnm
                                                              AND dbo.mz_cf.OrganizeId = dbo.mz_cfmx.OrganizeId
                              WHERE     mz_cfmx.OrganizeId = @orgId
                                        AND mz_cfmx.zt = 1
                                        AND YEAR(jsrq) = @year
                                        AND MONTH(jsrq) = @month
                                        AND dbo.mz_cf.zt = 1
                                        AND dbo.mz_cfmx.zt = 1
                              GROUP BY  ghnm ,
                                        jsrq ,
                                        ys
                            ) aaaaaaa
                            LEFT JOIN dbo.mz_gh ON aaaaaaa.ghnm = mz_gh.ghnm
                  WHERE     dbo.mz_gh.zt = 1
                ) xm
                LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ys
                                                              AND cuser.OrganizeId = @orgId
        WHERE   ISNULL(gh, '') <> ''
        GROUP BY xm.ssrq ,
                cuser.gh ,
                cuser.Name;
    END; ");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                     new SqlParameter("@year",year),
                     new SqlParameter("@month",month)
                };
            return FindList<TherapistMonthVisit>(sqlStr.ToString(), param);
        }
        #endregion

        #endregion

        #region 住院
        /// <summary>
        /// 获取住院新增人数
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<OutpatientVisitNumVO> GetInpatientVisitNum(string orgId)
        {
            List<OutpatientVisitNumVO> inpatientList = null;
            StringBuilder sqlStr2 = new StringBuilder();
            sqlStr2.Append(@"   --当前年份
                                DECLARE @currYear VARCHAR(30) = ( CONVERT(VARCHAR, YEAR(GETDATE()), 10) )
                                 --当前年月
                                DECLARE @currYearMoth VARCHAR(30)
                                SET @currYearMoth = @currYear + '01';
                                SELECT  col ,
                                        ISNULL(num1, 0) num ,
                                        '住院' name
                                FROM    [dbo].[f_split]('' + @currYear + '01,' + @currYear + '02,' + @currYear
                                                        + '03,' + @currYear + '04,' + @currYear + '05,'
                                                        + @currYear + '06,' + @currYear + '07,' + @currYear
                                                        + '08,' + @currYear + '09,' + @currYear + '10,'
                                                        + @currYear + '11,' + @currYear + '12', ',')
                                        LEFT JOIN ( SELECT  ddd.ryrq groupDate ,
                                                            COUNT(1) num1
                                                    FROM    ( SELECT DISTINCT
                                                                        syxh ,
                                                                        zyh ,
                                                                        CONVERT(VARCHAR(6), ryrq, 112) ryrq
                                                              FROM      dbo.zy_brjbxx
                                                              WHERE     zt = 1
                                                                        AND OrganizeId = @orgId
                                                                        AND CONVERT(VARCHAR(6), ryrq, 112) >= @currYearMoth
                                                            ) ddd
                                                    GROUP BY ddd.ryrq
                                                  ) AS a ON a.groupDate = col");

            SqlParameter[] param =
            {
                    new SqlParameter("@orgId",orgId)
                };
            inpatientList = FindList<OutpatientVisitNumVO>(sqlStr2.ToString(), param);

            return inpatientList;
        }

        /// <summary>
        /// 获取出院人数
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<InpatientVisitNumVO> GetInpatientDischargeNum(string orgId)
        {
            List<InpatientVisitNumVO> inpatientList = null;
            StringBuilder sqlStr2 = new StringBuilder();
            sqlStr2.Append(@"--当前年份
                    DECLARE @currYear VARCHAR(30) = ( CONVERT(VARCHAR, YEAR(GETDATE()), 10) )
                    --当前年月
                    DECLARE @currYearMoth VARCHAR(30)
                    SET @currYearMoth = @currYear + '01';
                    SELECT  col ,
                            ISNULL(num1, 0) num ,
                            '住院' name
                    FROM    [dbo].[f_split]('' + @currYear + '01,' + @currYear + '02,' + @currYear
                        + '03,' + @currYear + '04,' + @currYear + '05,'
                        + @currYear + '06,' + @currYear + '07,' + @currYear
                        + '08,' + @currYear + '09,' + @currYear + '10,'
                        + @currYear + '11,' + @currYear + '12', ',')
                     LEFT JOIN ( SELECT  ddd.cyrq groupDate ,
                            COUNT(1) num1
                    FROM    ( SELECT DISTINCT
                                        syxh ,
                                        zyh ,
                                        CONVERT(VARCHAR(6), cyrq, 112) cyrq
                              FROM      dbo.zy_brjbxx
                              WHERE     zt = 1
                                        AND OrganizeId = @orgId
                                        AND CONVERT(VARCHAR(6), cyrq, 112) >= @currYearMoth
                            ) ddd
                    GROUP BY ddd.cyrq
                  ) AS a ON a.groupDate = col");

            SqlParameter[] param =
            {
                    new SqlParameter("@orgId",orgId)
                };
            inpatientList = this.FindList<InpatientVisitNumVO>(sqlStr2.ToString(), param);

            return inpatientList;
        }

        /// <summary>
        /// 获取住院收入统计
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<InpatientSCNumVO> GetInpatientSalaryNum(string orgId)
        {
            List<InpatientSCNumVO> outpatientlist = null;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"--当前年份
            DECLARE @currYear VARCHAR(30) = ( CONVERT(VARCHAR, YEAR(GETDATE()), 10) )
                                                                --当前年月
            DECLARE @currYearMoth VARCHAR(30)
            SET @currYearMoth = @currYear + '01';
                                                --select @currYearMoth
                                                --住院
            SELECT  col ,
                    ISNULL(num1, 0) num,'住院' name
            FROM    [dbo].[f_split]('' + @currYear + '01,' + @currYear + '02,' + @currYear
                                    + '03,' + @currYear + '04,' + @currYear + '05,'
                                    + @currYear + '06,' + @currYear + '07,' + @currYear
                                    + '08,' + @currYear + '09,' + @currYear + '10,'
                                    + @currYear + '11,' + @currYear + '12', ',')
                    LEFT JOIN ( SELECT  ddd.tdrq groupDate ,
                            SUM(JE) num1
                    FROM    (      
                                    SELECT DISTINCT jfbbh,
                                        ccc.zyh ,
                                        bbb.tdrq ,
                                        sl * dj JE
                              FROM      ( --收费项目
                                                    SELECT DISTINCT jfbbh,
                                                    CONVERT(VARCHAR(6), tdrq, 112) tdrq ,
                                                    zyh ,
                                                    sl ,
                                                    dj
                                          FROM      ( SELECT  dbo.zy_xmjfb.jfbbh,tdrq ,
                                                              zyh ,
                                                              sl ,
                                                              dj
                                                      FROM    zy_xmjfb
                                                      WHERE   zt = '1'
                                                              AND OrganizeId =@orgId
                                                              AND CONVERT(VARCHAR(6), tdrq, 112) >= @currYearMoth
                                                    ) aaa
                                          GROUP BY  aaa.zyh ,
                                                    aaa.jfbbh,
                                                    aaa.tdrq ,
                                                    aaa.sl ,
                                                    aaa.dj
                                          UNION
                                         --药品
                                          SELECT DISTINCT jfbbh,
                                                    CONVERT(VARCHAR(6), tdrq, 112) tdrq ,
                                                    zyh ,
                                                    sl ,
                                                    dj
                                          FROM      ( SELECT  dbo.zy_ypjfb.jfbbh,tdrq ,
                                                              zyh ,
                                                              sl ,
                                                              dj
                                                      FROM    zy_ypjfb
                                                      WHERE   zt = '1'
                                                              AND OrganizeId =@orgId
                                                              AND CONVERT(VARCHAR(6), tdrq, 112) >= @currYearMoth
                                                    ) aaa
                                          GROUP BY  aaa.zyh ,aaa.jfbbh,
                                                    aaa.tdrq ,
                                                    aaa.sl ,
                                                    aaa.dj
                                        ) bbb
                                        LEFT JOIN zy_brjbxx ccc ON bbb.zyh = ccc.zyh
                                                              AND OrganizeId =@orgId
                            ) ddd
                    GROUP BY ddd.tdrq
                  ) AS a ON a.groupDate = col");
            SqlParameter[] param =
            {
                    new SqlParameter("@orgId",orgId)
                };
            outpatientlist = FindList<InpatientSCNumVO>(sqlStr.ToString(), param);
            return outpatientlist;
        }


        /// <summary>
        /// 获取治疗师住院治疗总费用
        /// </summary>
        public List<TherapistVisit> GetInTherapistDischarge(string year, string orgId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@" SELECT  'Rev.per Therapist' title ,
        cuser.gh gh ,
        cuser.Name name ,
        SUM(CASE WHEN MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = '01'
                 THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Jan ,
        ISNULL(SUM(CASE WHEN MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = '02'
                        THEN jfb.dj * jfb.sl
                        ELSE 0
                   END), 0.00) Feb ,
        SUM(CASE WHEN MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = '03'
                 THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Mar ,
        SUM(CASE WHEN MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = '04'
                 THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Apr ,
        SUM(CASE WHEN MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = '05'
                 THEN jfb.dj * jfb.sl
                 ELSE 0
            END) May ,
        SUM(CASE WHEN MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = '06'
                 THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Jun ,
        SUM(CASE WHEN MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = '07'
                 THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Jul ,
        SUM(CASE WHEN MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = '08'
                 THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Aug ,
        SUM(CASE WHEN MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = '09'
                 THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Sep ,
        SUM(CASE WHEN MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = '10'
                 THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Oct ,
        SUM(CASE WHEN MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = '11'
                 THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Nov ,
        SUM(CASE WHEN MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = '12'
                 THEN jfb.dj * jfb.sl
                 ELSE 0
            END) [Dec]
INTO    #temp
FROM    dbo.zy_brjbxx zyxx
        LEFT JOIN dbo.zy_xmjfb jfb ON jfb.zyh = zyxx.zyh
        LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = jfb.ssry
                                                              AND cuser.OrganizeId = @orgId
WHERE   jfb.zt = 1
        AND zyxx.zt = 1
        AND zyxx.OrganizeId = @orgId
        AND jfb.OrganizeId = @orgId
        AND YEAR(ISNULL(jfb.tdrq, jfb.ssrq)) = @year
GROUP BY cuser.gh ,
        cuser.Name; 

SELECT  'Rev.per Therapist' title ,
        cuser.gh gh ,
        cuser.Name name ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '01' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Jan ,
        ISNULL(SUM(CASE WHEN MONTH(jfb.tdrq) = '02' THEN jfb.dj * jfb.sl
                        ELSE 0
                   END), 0.00) Feb ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '03' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Mar ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '04' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Apr ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '05' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) May ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '06' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Jun ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '07' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Jul ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '08' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Aug ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '09' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Sep ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '10' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Oct ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '11' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Nov ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '12' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) [Dec]
INTO    #temp2
FROM    dbo.zy_brjbxx zyxx
        LEFT JOIN dbo.zy_ypjfb jfb ON jfb.zyh = zyxx.zyh
        LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = jfb.ys
                                                              AND cuser.OrganizeId = @orgId
WHERE   jfb.OrganizeId = @orgId
        AND jfb.zt = 1
        AND YEAR(jfb.tdrq) = @year
        AND zyxx.zt = 1
GROUP BY cuser.gh ,
        cuser.Name;

SELECT  'Rev.per Therapist' title ,
        a.name ,
        a.gh ,
        ISNULL(a.Jan, 0.0000) + ISNULL(b.Jan, 0.0000) Jan ,
        ISNULL(a.Feb, 0.0000) + ISNULL(b.Feb, 0.0000) Feb ,
        ISNULL(a.Mar, 0.0000) + ISNULL(b.Mar, 0.0000) Mar ,
        ISNULL(a.Apr, 0.0000) + ISNULL(b.Apr, 0.0000) Apr ,
        ISNULL(a.May, 0.0000) + ISNULL(b.May, 0.0000) May ,
        ISNULL(a.Jun, 0.0000) + ISNULL(b.Jun, 0.0000) Jun ,
        ISNULL(a.Jul, 0.0000) + ISNULL(b.Jul, 0.0000) Jul ,
        ISNULL(a.Aug, 0.0000) + ISNULL(b.Aug, 0.0000) Aug ,
        ISNULL(a.Sep, 0.0000) + ISNULL(b.Sep, 0.0000) Sep ,
        ISNULL(a.Oct, 0.0000) + ISNULL(b.Oct, 0.0000) Oct ,
        ISNULL(a.Nov, 0.0000) + ISNULL(b.Nov, 0.0000) Nov ,
        ISNULL(a.[Dec], 0.0000) + ISNULL(b.[Dec], 0.0000) [Dec]
FROM    #temp a
        LEFT JOIN #temp2 b ON a.gh = b.gh
        order by a.gh
DROP TABLE #temp,#temp2;");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                    new SqlParameter("@year",year)
                };
            return FindList<TherapistVisit>(sqlStr.ToString(), param);
        }

        /// <summary>
        /// 获取治疗师住院治疗人次
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TherapistVisit> GetInTherapistVisit(string year, string orgId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"SELECT  'Patient Visit' title ,
        cuser.name ,
        cuser.gh ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '01' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jan ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '02' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Feb ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '03' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Mar ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '04' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Apr ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '05' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) May ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '06' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jun ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '07' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jul ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '08' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Aug ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '09' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Sep ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '10' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Oct ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '11' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Nov ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '12' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) [Dec]
        FROM    ( SELECT DISTINCT
                    aaaaaaa.*
          FROM      ( SELECT    zyh ,
                                CONVERT(VARCHAR(10), ISNULL(jfb.tdrq, jfb.ssrq), 120) tdrq ,
                                jfb.ssry ys
                      FROM      dbo.zy_xmjfb jfb
                      WHERE     OrganizeId =@orgId
                                AND jfb.zt = 1
                                AND YEAR(ISNULL(jfb.tdrq, jfb.ssrq)) = @year
                      GROUP BY  zyh ,
                                ISNULL(jfb.tdrq, jfb.ssrq) ,
                                jfb.ssry
                      UNION ALL
                      SELECT    zyh ,
                                CONVERT(VARCHAR(10), tdrq, 120) tdrq ,
                                ys
                      FROM      dbo.zy_ypjfb jfb
                      WHERE     jfb.OrganizeId =@orgId
                                AND jfb.zt = 1
                                AND YEAR(tdrq) = @year
                      GROUP BY  zyh ,
                                tdrq ,
                                ys
                    ) aaaaaaa
                    left JOIN dbo.zy_brjbxx zyxx ON aaaaaaa.zyh = zyxx.zyh
          WHERE     zyxx.zt = 1
                    --AND zyxx.zybz = 3
        ) xm
        LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ys
                                                              AND cuser.OrganizeId =@orgId
GROUP BY cuser.Name ,
        cuser.gh order by cuser.gh");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                     new SqlParameter("@year",year)
                };
            return FindList<TherapistVisit>(sqlStr.ToString(), param);
        }

        /// <summary>
        /// 获取治疗师住院治疗平均人次 总人次/总天数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TherapistVisit> GetInTherapistavgVisit(string year, string orgId) {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"--总人次
    SELECT  'Patient Visit' title ,
        cuser.Name ,
        cuser.gh ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '01' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jan ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '02' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Feb ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '03' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Mar ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '04' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Apr ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '05' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) May ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '06' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jun ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '07' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jul ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '08' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Aug ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '09' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Sep ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '10' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Oct ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '11' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Nov ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '12' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) [Dec]
INTO    #temp
FROM    ( SELECT DISTINCT
                    aaaaaaa.*
          FROM      ( SELECT    zyh ,
                                CONVERT(VARCHAR(10), ISNULL(jfb.tdrq, jfb.ssrq), 120) tdrq ,
                                jfb.ssry ys
                      FROM      dbo.zy_xmjfb jfb
                      WHERE     OrganizeId = @orgId
                                AND jfb.zt = 1
                                AND YEAR(ISNULL(jfb.tdrq, jfb.ssrq)) = @year
                      GROUP BY  zyh ,
                                ISNULL(jfb.tdrq, jfb.ssrq) ,
                                jfb.ssry
                      UNION ALL
                      SELECT    zyh ,
                                CONVERT(VARCHAR(10), tdrq, 120) tdrq ,
                                ys
                      FROM      dbo.zy_ypjfb jfb
                      WHERE     jfb.OrganizeId = @orgId
                                AND jfb.zt = 1
                                AND YEAR(tdrq) = @year
                      GROUP BY  zyh ,
                                tdrq ,
                                ys
                    ) aaaaaaa
                    RIGHT JOIN dbo.zy_brjbxx zyxx ON aaaaaaa.zyh = zyxx.zyh
          WHERE     zyxx.zt = 1
        ) xm
        LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ys
                                                              AND cuser.OrganizeId = @orgId
GROUP BY cuser.Name ,
        cuser.gh;

--总天数
SELECT  xm.ys ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '01' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jan ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '02' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Feb ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '03' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Mar ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '04' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Apr ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '05' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) May ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '06' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jun ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '07' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jul ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '08' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Aug ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '09' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Sep ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '10' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Oct ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '11' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Nov ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '12' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) [Dec]
INTO    #temp2
FROM    ( SELECT    jfb.ssry ys ,
                    CONVERT(VARCHAR(10), ISNULL(jfb.tdrq, jfb.ssrq), 120) tdrq
          FROM      dbo.zy_xmjfb jfb
          WHERE     OrganizeId = @orgId
                    AND jfb.zt = 1
                    AND YEAR(ISNULL(jfb.tdrq, jfb.ssrq)) = @year
          GROUP BY  ISNULL(jfb.tdrq, jfb.ssrq) ,
                    jfb.ssry
          UNION ALL
          SELECT    jfb.ys ,
                    CONVERT(VARCHAR(10), tdrq, 120) tdrq
          FROM      dbo.zy_ypjfb jfb
          WHERE     jfb.OrganizeId = @orgId
                    AND jfb.zt = 1
                    AND YEAR(tdrq) = @year
          GROUP BY  tdrq ,
                    jfb.ys
        ) xm
GROUP BY xm.ys;

--总人次/总天数
SELECT  'Avg Patient Visit' title ,
        a.Name ,
        a.gh ,
        CAST(ROUND(( CASE ISNULL(b.Jan, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Jan) / CONVERT(FLOAT, b.Jan)
                     END ), 2) AS DECIMAL(18, 2)) Jan ,
        CAST(ROUND(( CASE ISNULL(b.Feb, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Feb) / CONVERT(FLOAT, b.Feb)
                     END ), 2) AS DECIMAL(18, 2)) Feb ,
        CAST(ROUND(( CASE ISNULL(b.Mar, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Mar) / CONVERT(FLOAT, b.Mar)
                     END ), 2) AS DECIMAL(18, 2)) Mar ,
        CAST(ROUND(( CASE ISNULL(b.Apr, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Apr) / CONVERT(FLOAT, b.Apr)
                     END ), 2) AS DECIMAL(18, 2)) Apr ,
        CAST(ROUND(( CASE ISNULL(b.May, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.May) / CONVERT(FLOAT, b.May)
                     END ), 2) AS DECIMAL(18, 2)) May ,
        CAST(ROUND(( CASE ISNULL(b.Jun, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Jun) / CONVERT(FLOAT, b.Jun)
                     END ), 2) AS DECIMAL(18, 2)) Jun ,
        CAST(ROUND(( CASE ISNULL(b.Jul, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Jul) / CONVERT(FLOAT, b.Jul)
                     END ), 2) AS DECIMAL(18, 2)) Jul ,
        CAST(ROUND(( CASE ISNULL(b.Aug, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Aug) / CONVERT(FLOAT, b.Aug)
                     END ), 2) AS DECIMAL(18, 2)) Aug ,
        CAST(ROUND(( CASE ISNULL(b.Sep, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Sep) / CONVERT(FLOAT, b.Sep)
                     END ), 2) AS DECIMAL(18, 2)) Sep ,
        CAST(ROUND(( CASE ISNULL(b.Oct, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Oct) / CONVERT(FLOAT, b.Oct)
                     END ), 2) AS DECIMAL(18, 2)) Oct ,
        CAST(ROUND(( CASE ISNULL(b.Nov, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Nov) / CONVERT(FLOAT, b.Nov)
                     END ), 2) AS DECIMAL(18, 2)) Nov ,
        CAST(ROUND(( CASE ISNULL(b.[Dec], 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.[Dec]) / CONVERT(FLOAT, b.[Dec])
                     END ), 2) AS DECIMAL(18, 2)) [Dec]
FROM    #temp a
        JOIN #temp2 b ON a.gh = b.ys;
DROP TABLE  #temp,#temp2;");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                     new SqlParameter("@year",year)
                };
            return FindList<TherapistVisit>(sqlStr.ToString(), param);
        }

        /// <summary>
        /// 获取治疗之住院平均费用
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TherapistVisit> GetInTherapistavgDischarge(string year, string orgId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"SELECT  'Rev.per Therapist' title ,
        cuser.gh gh ,
        cuser.name name ,
        SUM(CASE WHEN MONTH(tdrq) = '01' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Jan ,
        ISNULL(SUM(CASE WHEN MONTH(tdrq) = '02' THEN jfb.dj * jfb.sl
                        ELSE 0
                   END), 0.00) Feb ,
        SUM(CASE WHEN MONTH(tdrq) = '03' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Mar ,
        SUM(CASE WHEN MONTH(tdrq) = '04' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Apr ,
        SUM(CASE WHEN MONTH(tdrq) = '05' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) May ,
        SUM(CASE WHEN MONTH(tdrq) = '06' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Jun ,
        SUM(CASE WHEN MONTH(tdrq) = '07' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Jul ,
        SUM(CASE WHEN MONTH(tdrq) = '08' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Aug ,
        SUM(CASE WHEN MONTH(tdrq) = '09' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Sep ,
        SUM(CASE WHEN MONTH(tdrq) = '10' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Oct ,
        SUM(CASE WHEN MONTH(tdrq) = '11' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Nov ,
        SUM(CASE WHEN MONTH(tdrq) = '12' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) [Dec]
INTO    #temp
FROM    dbo.zy_brjbxx zyxx
        LEFT JOIN dbo.zy_xmjfb jfb ON jfb.zyh = zyxx.zyh
        LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = jfb.ssry
                                                              AND cuser.OrganizeId = @orgId
WHERE   --zyxx.zybz = 3
        --AND 
        jfb.zt = 1
        AND zyxx.zt = 1
        AND zyxx.OrganizeId = @orgId
        AND jfb.OrganizeId = @orgId
        AND YEAR(jfb.tdrq) = @year
GROUP BY cuser.gh ,
        cuser.name 

SELECT  'Rev.per Therapist' title ,
        cuser.gh gh ,
        cuser.name name ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '01' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Jan ,
        ISNULL(SUM(CASE WHEN MONTH(jfb.tdrq) = '02' THEN jfb.dj * jfb.sl
                        ELSE 0
                   END), 0.00) Feb ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '03' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Mar ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '04' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Apr ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '05' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) May ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '06' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Jun ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '07' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Jul ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '08' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Aug ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '09' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Sep ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '10' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Oct ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '11' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) Nov ,
        SUM(CASE WHEN MONTH(jfb.tdrq) = '12' THEN jfb.dj * jfb.sl
                 ELSE 0
            END) [Dec]
INTO    #temp2
FROM    dbo.zy_brjbxx zyxx
        LEFT JOIN dbo.zy_ypjfb jfb ON jfb.zyh = zyxx.zyh
        LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = jfb.ys
                                                              AND cuser.OrganizeId = @orgId
WHERE   jfb.OrganizeId = @orgId
        AND jfb.zt = 1
        AND YEAR(jfb.tdrq) = @year
        AND zyxx.zt = 1
        --AND zyxx.zybz = 3
        --AND jfb.ys IS NOT NULL
GROUP BY cuser.gh ,
        cuser.name

SELECT  'Rev.per Therapist' title ,
        a.name ,
        a.gh ,
        ISNULL(a.Jan, 0.0000) + ISNULL(b.Jan, 0.0000) Jan ,
        ISNULL(a.Feb, 0.0000) + ISNULL(b.Feb, 0.0000) Feb ,
        ISNULL(a.Mar, 0.0000) + ISNULL(b.Mar, 0.0000) Mar ,
        ISNULL(a.Apr, 0.0000) + ISNULL(b.Apr, 0.0000) Apr ,
        ISNULL(a.May, 0.0000) + ISNULL(b.May, 0.0000) May ,
        ISNULL(a.Jun, 0.0000) + ISNULL(b.Jun, 0.0000) Jun ,
        ISNULL(a.Jul, 0.0000) + ISNULL(b.Jul, 0.0000) Jul ,
        ISNULL(a.Aug, 0.0000) + ISNULL(b.Aug, 0.0000) Aug ,
        ISNULL(a.Sep, 0.0000) + ISNULL(b.Sep, 0.0000) Sep ,
        ISNULL(a.Oct, 0.0000) + ISNULL(b.Oct, 0.0000) Oct ,
        ISNULL(a.Nov, 0.0000) + ISNULL(b.Nov, 0.0000) Nov ,
        ISNULL(a.[Dec], 0.0000) + ISNULL(b.[Dec], 0.0000) [Dec]
INTO #temp3
FROM    #temp a
        LEFT JOIN #temp2 b ON a.gh = b.gh

SELECT  'Patient Visit' title ,
        xm.ys gh ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '01' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jan ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '02' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Feb ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '03' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Mar ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '04' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Apr ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '05' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) May ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '06' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jun ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '07' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Jul ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '08' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Aug ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '09' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Sep ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '10' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Oct ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '11' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) Nov ,
        CAST(SUM(CASE WHEN MONTH(tdrq) = '12' THEN 1
                      ELSE 0
                 END) AS DECIMAL(18, 2)) [Dec]
INTO    #temp4
FROM    ( SELECT DISTINCT
                    aaaaaaa.*
          FROM      ( SELECT    zyh ,
                                CONVERT(VARCHAR(10), ISNULL(jfb.tdrq, jfb.ssrq), 120) tdrq ,
                                jfb.ssry ys
                      FROM      dbo.zy_xmjfb jfb
                      WHERE     OrganizeId = @orgId
                                AND jfb.zt = 1
                                AND YEAR(ISNULL(jfb.tdrq, jfb.ssrq)) = @year
                      GROUP BY  zyh ,
                                ISNULL(jfb.tdrq, jfb.ssrq) ,
                                jfb.ssry
                      UNION ALL
                      SELECT    zyh ,
                                CONVERT(VARCHAR(10), tdrq, 120) tdrq ,
                                ys
                      FROM      dbo.zy_ypjfb jfb
                      WHERE     jfb.OrganizeId = @orgId
                                AND jfb.zt = 1
                                AND YEAR(tdrq) = @year
                      GROUP BY  zyh ,
                                tdrq ,
                                ys
                    ) aaaaaaa
                    LEFT JOIN dbo.zy_brjbxx zyxx ON aaaaaaa.zyh = zyxx.zyh
          WHERE     zyxx.zt = 1
        ) xm
GROUP BY xm.ys

SELECT  'Avg Rev.per Therapist' title ,
        a.name ,
        a.gh ,
        CAST(ROUND(( CASE ISNULL(b.Jan, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Jan) / CONVERT(FLOAT, b.Jan)
                     END ), 2) AS DECIMAL(18, 2)) Jan ,
        CAST(ROUND(( CASE ISNULL(b.Feb, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Feb) / CONVERT(FLOAT, b.Feb)
                     END ), 2) AS DECIMAL(18, 2)) Feb ,
        CAST(ROUND(( CASE ISNULL(b.Mar, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Mar) / CONVERT(FLOAT, b.Mar)
                     END ), 2) AS DECIMAL(18, 2)) Mar ,
        CAST(ROUND(( CASE ISNULL(b.Apr, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Apr) / CONVERT(FLOAT, b.Apr)
                     END ), 2) AS DECIMAL(18, 2)) Apr ,
        CAST(ROUND(( CASE ISNULL(b.May, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.May) / CONVERT(FLOAT, b.May)
                     END ), 2) AS DECIMAL(18, 2)) May ,
        CAST(ROUND(( CASE ISNULL(b.Jun, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Jun) / CONVERT(FLOAT, b.Jun)
                     END ), 2) AS DECIMAL(18, 2)) Jun ,
        CAST(ROUND(( CASE ISNULL(b.Jul, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Jul) / CONVERT(FLOAT, b.Jul)
                     END ), 2) AS DECIMAL(18, 2)) Jul ,
        CAST(ROUND(( CASE ISNULL(b.Aug, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Aug) / CONVERT(FLOAT, b.Aug)
                     END ), 2) AS DECIMAL(18, 2)) Aug ,
        CAST(ROUND(( CASE ISNULL(b.Sep, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Sep) / CONVERT(FLOAT, b.Sep)
                     END ), 2) AS DECIMAL(18, 2)) Sep ,
        CAST(ROUND(( CASE ISNULL(b.Oct, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Oct) / CONVERT(FLOAT, b.Oct)
                     END ), 2) AS DECIMAL(18, 2)) Oct ,
        CAST(ROUND(( CASE ISNULL(b.Nov, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.Nov) / CONVERT(FLOAT, b.Nov)
                     END ), 2) AS DECIMAL(18, 2)) Nov ,
        CAST(ROUND(( CASE ISNULL(b.[Dec], 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.[Dec]) / CONVERT(FLOAT, b.[Dec])
                     END ), 2) AS DECIMAL(18, 2)) [Dec]
                    FROM    #temp3 a
                            LEFT JOIN #temp4 b ON a.gh = b.gh
                    ORDER BY a.gh
                    DROP TABLE #temp,#temp2,#temp3,#temp4");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                    new SqlParameter("@year",year)
                };
            return FindList<TherapistVisit>(sqlStr.ToString(), param);
        }

        /// <summary>
        /// 住院月份总费用
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <param name="month"></param>
        public List<TherapistMonthCharge> GetMonthInTherapistDischarge(string year, string orgId,string month) {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@" SELECT 'Rev.per Therapist' title , jfb.jfbbh ,
                            cuser.gh gh ,
                            cuser.Name name ,
                            jfb.dj * jfb.sl zje ,
                            ISNULL(jfb.tdrq, jfb.ssrq) ssrq
                     INTO   #temp
                     FROM   dbo.zy_brjbxx zyxx
                            LEFT JOIN dbo.zy_xmjfb jfb ON jfb.zyh = zyxx.zyh
                            LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = jfb.ssry
                                                                                  AND cuser.OrganizeId =@orgId
                     WHERE  jfb.zt = 1
                            AND zyxx.zt = 1
                            AND zyxx.OrganizeId =@orgId
                            AND jfb.OrganizeId =@orgId
                            AND YEAR(ISNULL(jfb.tdrq, jfb.ssrq)) = @year
                            AND MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = @month
                     GROUP BY  jfb.jfbbh ,cuser.gh ,
                            cuser.Name ,
                            ISNULL(jfb.tdrq, jfb.ssrq) ,
                            jfb.dj ,
                            jfb.sl;

                     SELECT 'Rev.per Therapist' title , jfb.jfbbh ,
                            cuser.gh gh ,
                            cuser.Name name ,
                            jfb.dj * jfb.sl zje ,
                            jfb.tdrq ssrq
                     INTO   #temp2
                     FROM   dbo.zy_brjbxx zyxx
                            LEFT JOIN dbo.zy_ypjfb jfb ON jfb.zyh = zyxx.zyh
                            LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = jfb.fyry
                                                                                  AND cuser.OrganizeId =@orgId
                     WHERE  jfb.OrganizeId =@orgId
                            AND jfb.zt = 1
                            AND YEAR(jfb.tdrq) = @year
		                    AND MONTH(jfb.tdrq) = @month
                            AND zyxx.zt = 1
                     GROUP BY  jfb.jfbbh ,cuser.gh ,
                            cuser.Name ,
                            jfb.tdrq ,
                            jfb.dj ,
                            jfb.sl;

                     SELECT 'Rev.per Therapist' title ,
                            a.name ,
                            a.gh ,
                            SUM(ISNULL(a.zje, 0.0000) + ISNULL(b.zje, 0.0000)) zje,
		                    CONVERT(VARCHAR(10),a.ssrq, 120) ssrq
                     FROM   #temp a
                            LEFT JOIN #temp2 b ON a.gh = b.gh 
                                AND CONVERT(VARCHAR(10), a.ssrq, 120) = CONVERT(VARCHAR(10), b.ssrq, 120)
                                GROUP BY a.gh ,
                                CONVERT(VARCHAR(10), a.ssrq, 120),
                                a.name;
                    DROP TABLE #temp,#temp2;");
            SqlParameter[] param =
              {
                    new SqlParameter("@orgId",orgId),
                    new SqlParameter("@year",year),
                    new SqlParameter("@month",month),
            };
            return FindList<TherapistMonthCharge>(sqlStr.ToString(), param);
        }

        /// <summary>
        /// 住院月份平均费用
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<TherapistMonthCharge> GetMonthInTherapistavgDischarge(string year, string orgId, string month)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"SELECT  'Rev.per Therapist' title ,
                            cuser.gh gh ,
                            cuser.Name name ,
                            jfb.dj * jfb.sl zje ,
                            ISNULL(jfb.tdrq, jfb.ssrq) ssrq
                    INTO    #temp
                    FROM    dbo.zy_brjbxx zyxx
                            LEFT JOIN dbo.zy_xmjfb jfb ON jfb.zyh = zyxx.zyh
                            LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = jfb.ssry
                                                                                  AND cuser.OrganizeId = @orgId
                    WHERE   --zyxx.zybz = 3
                            --AND 
                            jfb.zt = 1
                            AND zyxx.zt = 1
                            AND zyxx.OrganizeId = @orgId
                            AND jfb.OrganizeId = @orgId
                            AND YEAR(ISNULL(jfb.tdrq, jfb.ssrq)) = @year
                            AND MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = @month
                    GROUP BY cuser.gh ,
                            cuser.Name ,
                            jfb.dj ,
                            jfb.sl ,
                            ISNULL(jfb.tdrq, jfb.ssrq);

                    SELECT  'Rev.per Therapist' title ,
                            cuser.gh gh ,
                            cuser.Name name ,
                            jfb.dj * jfb.sl zje ,
                            jfb.tdrq ssrq
                    INTO    #temp2
                    FROM    dbo.zy_brjbxx zyxx
                            LEFT JOIN dbo.zy_ypjfb jfb ON jfb.zyh = zyxx.zyh
                            LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = jfb.ys
                                                                                  AND cuser.OrganizeId = @orgId
                    WHERE   jfb.OrganizeId = @orgId
                            AND jfb.zt = 1
                            AND YEAR(jfb.tdrq) = @year
                            AND MONTH(jfb.tdrq) = @month
                            AND zyxx.zt = 1
                            --AND zyxx.zybz = 3
                    GROUP BY cuser.gh ,
                            cuser.Name ,
                            jfb.dj ,
                            jfb.sl ,
                            jfb.tdrq;

                    SELECT  'Rev.per Therapist' title ,
                            a.name ,
                            a.gh ,
                            ISNULL(a.zje, 0.0000) + ISNULL(b.zje, 0.0000) zje ,
                            a.ssrq
                    INTO    #temp3
                    FROM    #temp a
                            LEFT JOIN #temp2 b ON a.gh = b.gh
                                                  AND a.ssrq = b.ssrq;

                    SELECT  'Patient Visit' title ,
                            xm.ssry gh ,
                            COUNT(1) rc ,
                            xm.ssrq
                    INTO    #temp4
                    FROM    ( SELECT DISTINCT
                                        aaaaaaa.*
                              FROM      ( SELECT    zyh ,
                                                    CONVERT(VARCHAR(10), ISNULL(tdrq, ssrq), 120) ssrq ,
                                                    jfb.ssry ssry
                                          FROM      dbo.zy_xmjfb jfb
                                          WHERE     OrganizeId = @orgId
                                                    AND jfb.zt = 1
                                                    AND YEAR(ISNULL(tdrq, ssrq)) = @year
                                                    AND MONTH(ISNULL(tdrq, ssrq)) = @month
                                          GROUP BY  zyh ,
                                                    ISNULL(tdrq, ssrq) ,
                                                    jfb.ssry
                                          UNION ALL
                                          SELECT    zyh ,
                                                    CONVERT(VARCHAR(10), tdrq, 120) tdrq ,
                                                    jfb.ys ssry
                                          FROM      dbo.zy_ypjfb jfb
                                          WHERE     jfb.OrganizeId = @orgId
                                                    AND jfb.zt = 1
                                                    AND YEAR(tdrq) = @year
                                                    AND MONTH(tdrq) = @month
                                          GROUP BY  zyh ,
                                                    tdrq ,
                                                    jfb.ys
                                        ) aaaaaaa
                                        LEFT  JOIN dbo.zy_brjbxx zyxx ON aaaaaaa.zyh = zyxx.zyh
                              WHERE     zyxx.zt = 1
                                        --AND zyxx.zybz = 3
                            ) xm
                    GROUP BY xm.ssry ,
                            xm.ssrq;

                    SELECT  'Avg Rev.per Therapist' title ,
                            a.name ,
                            a.gh ,
                            CONVERT(VARCHAR(10),a.ssrq, 120) ssrq
                            CAST(ROUND(( CASE ISNULL(b.rc, 0)
                                           WHEN 0 THEN 0
                                           ELSE CONVERT(FLOAT, a.zje) / CONVERT(FLOAT, b.rc)
                                         END ), 2) AS DECIMAL(18, 2)) zje
                    FROM    #temp3 a
                            LEFT JOIN #temp4 b ON a.gh = b.gh
                                                  AND a.ssrq = b.ssrq
                    ORDER BY a.gh;
                    DROP TABLE #temp,#temp2,#temp3,#temp4;");
            SqlParameter[] param =
              {
                    new SqlParameter("@orgId",orgId),
                    new SqlParameter("@year",year),
                    new SqlParameter("@month",month),
            };
            return FindList<TherapistMonthCharge>(sqlStr.ToString(), param);

        }

        /// <summary>
        /// 获取治疗师住院月份治疗总人次
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TherapistMonthVisit> GetMonthInTherapistVisit(string year,string month, string orgId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"SELECT  'Patient Visit' title ,
        xm.ssry gh ,
        cuser.Name name,
        CAST(COUNT(1) AS DECIMAL(18, 2)) yxris ,
        CONVERT(VARCHAR(10),xm.ssrq, 120) ssrq
FROM    ( SELECT DISTINCT
                    aaaaaaa.*
          FROM      ( SELECT    zyh ,
                                CONVERT(VARCHAR(10), ISNULL(tdrq, ssrq), 120) ssrq ,
                                jfb.ssry ssry
                      FROM      dbo.zy_xmjfb jfb
                      WHERE     OrganizeId = @orgId
                                AND jfb.zt = 1
                                AND YEAR(ISNULL(tdrq, ssrq)) = @year
                                AND MONTH(ISNULL(tdrq, ssrq)) = @month
                      GROUP BY  zyh ,
                                ISNULL(tdrq, ssrq) ,
                                jfb.ssry
                      UNION ALL
                      SELECT    zyh ,
                                CONVERT(VARCHAR(10), tdrq, 120) tdrq ,
                                jfb.ys ssry
                      FROM      dbo.zy_ypjfb jfb
                      WHERE     jfb.OrganizeId = @orgId
                                AND jfb.zt = 1
                                AND YEAR(tdrq) = @year
                                AND MONTH(tdrq) = @month
                      GROUP BY  zyh ,
                                tdrq ,
                                jfb.ys
                    ) aaaaaaa
                    LEFT JOIN dbo.zy_brjbxx zyxx ON aaaaaaa.zyh = zyxx.zyh
          WHERE     zyxx.zt = 1
                    --AND zyxx.zybz = 3
        ) xm
        LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ssry
                                                              AND cuser.OrganizeId = @orgId
GROUP BY xm.ssry ,
        xm.ssrq,
		cuser.Name;");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                     new SqlParameter("@year",year),
                     new SqlParameter("@month",month)
                };
            return FindList<TherapistMonthVisit>(sqlStr.ToString(), param);
        }

        /// <summary>
        /// 获取治疗师住院治疗月度平均人次 总人次/总天数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<TherapistMonthVisit> GetMonthInTherapistavgVisit(string year, string month, string orgId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(@"--总人次
SELECT  'Patient Visit' title ,
        cuser.Name ,
        cuser.gh ,
        COUNT(1) zrc
INTO    #temp
FROM    ( SELECT DISTINCT
                    aaaaaaa.*
          FROM      ( SELECT    zyh ,
                                CONVERT(VARCHAR(10), ISNULL(jfb.tdrq, jfb.ssrq), 120) tdrq ,
                                jfb.ssry ys
                      FROM      dbo.zy_xmjfb jfb
                      WHERE     OrganizeId = @orgId
                                AND jfb.zt = 1
                                AND YEAR(ISNULL(jfb.tdrq, jfb.ssrq)) = @year
                                AND MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = @month
                      GROUP BY  zyh ,
                                ISNULL(jfb.tdrq, jfb.ssrq) ,
                                jfb.ssry
                      UNION ALL
                      SELECT    zyh ,
                                CONVERT(VARCHAR(10), tdrq, 120) tdrq ,
                                ys
                      FROM      dbo.zy_ypjfb jfb
                      WHERE     jfb.OrganizeId = @orgId
                                AND jfb.zt = 1
                                AND YEAR(tdrq) = @year
                                AND MONTH(tdrq) = @month
                      GROUP BY  zyh ,
                                tdrq ,
                                ys
                    ) aaaaaaa
                    INNER JOIN dbo.zy_brjbxx zyxx ON aaaaaaa.zyh = zyxx.zyh
          WHERE     zyxx.zt = 1
        ) xm
        LEFT JOIN NewtouchHIS_Base..[V_C_Sys_UserStaff] cuser ON cuser.gh = xm.ys
                                                              AND cuser.OrganizeId = @orgId
GROUP BY cuser.Name ,
        cuser.gh;

 --总天数
SELECT  ys ,
        COUNT(1) zts
INTO    #temp2
FROM    ( SELECT    jfb.ssry ys ,
                    CONVERT(VARCHAR(10), ISNULL(jfb.tdrq, jfb.ssrq), 120) tdrq
          FROM      dbo.zy_xmjfb jfb
          WHERE     OrganizeId = @orgId
                    AND jfb.zt = 1
                    AND YEAR(ISNULL(jfb.tdrq, jfb.ssrq)) = @year
                    AND MONTH(ISNULL(jfb.tdrq, jfb.ssrq)) = @month
          GROUP BY  ISNULL(jfb.tdrq, jfb.ssrq) ,
                    jfb.ssry
          UNION ALL
          SELECT    jfb.ys ,
                    CONVERT(VARCHAR(10), tdrq, 120) tdrq
          FROM      dbo.zy_ypjfb jfb
          WHERE     jfb.OrganizeId = @orgId
                    AND jfb.zt = 1
                    AND YEAR(tdrq) = @year
                    AND MONTH(tdrq) = @month
          GROUP BY  tdrq ,
                    jfb.ys
        ) xm
GROUP BY xm.ys;


        --总人次/总天数
SELECT  'Avg Patient Visit' title ,
        a.Name ,
        a.gh ,
        CAST(ROUND(( CASE ISNULL(b.zts, 0)
                       WHEN 0 THEN 0
                       ELSE CONVERT(FLOAT, a.zrc) / CONVERT(FLOAT, b.zts)
                     END ), 2) AS DECIMAL(18, 2)) yxris
FROM    #temp a
        LEFT JOIN #temp2 b ON a.gh = b.ys;
DROP TABLE  #temp,#temp2;");
            SqlParameter[] param =
               {
                    new SqlParameter("@orgId",orgId),
                     new SqlParameter("@year",year),
                     new SqlParameter("@month",month)
                };
            return FindList<TherapistMonthVisit>(sqlStr.ToString(), param);
        }
        #endregion

        #region Common

        /// <summary>
        /// 门诊住院收入统计对比
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public SCNumBO GetSalaryNumCompared(string orgId)
        {
            List<OutpatientSCNumVO> outpatientlist = GetOutpatientSalaryNum(orgId);
            List<InpatientSCNumVO> inpatientList = GetInpatientSalaryNum(orgId);
            //放在一个对象，返回到页面
            var visitNumBO = new SCNumBO
            {
                OutpatientList = outpatientlist,
                InpatientList = inpatientList
            };
            return visitNumBO;
        }

        #endregion

    }
}
