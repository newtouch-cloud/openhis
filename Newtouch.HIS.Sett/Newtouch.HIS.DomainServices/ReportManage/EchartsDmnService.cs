using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices.ReportManage;
using Newtouch.HIS.Domain.ValueObjects.ReportManage;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices.ReportManage
{
    public class EchartsDmnService : DmnServiceBase, IEchartsDmnService
    {
        public EchartsDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 门诊人次图表
        /// </summary>
        public List<PerformanceIndicatorVO> SelectPerformanceIndicator(string orgId, string year)
        {
            StringBuilder sqlStr = new StringBuilder();
            IList<SqlParameter> parlist = new List<SqlParameter>();
            sqlStr.Append(@" 
--定义年份
DECLARE @currYear varchar(30)=(Convert(varchar, year(getdate()),10))
                        ");
            if (string.IsNullOrEmpty(year))
            {
                sqlStr.Append(@"
--当前年份
SET @currYear=(Convert(varchar, year(getdate()),10))
                            ");
            }
            else
            {
                sqlStr.Append(@"
--选中的年份
SET @currYear=@year
                            ");
                parlist.Add(new SqlParameter("@year", year));
            }
            sqlStr.Append(@"

--当前年月
DECLARE @currYearMoth varchar(30)
SET @currYearMoth=@currYear+'01';
--select @currYearMoth

 DECLARE @cnt INT;
 SELECT @cnt = cnt
 FROM   [dbo].[CntByOrgId](@orgId);
 IF ( @cnt > 0 )--存在执行表记录，按照执行逻辑走,否则按照mz_xm逻辑走
    BEGIN
       SELECT  col ,
        COUNT(DISTINCT patid) zrs ,
        COUNT(DISTINCT rc) zrc ,
        CONVERT(DECIMAL(9, 2), CASE WHEN COUNT(DISTINCT patid) = 0 THEN 0
                                    ELSE CONVERT(DECIMAL(9, 2), COUNT(DISTINCT rc))
                                         / COUNT(DISTINCT patid)
                               END) AS pjrc --平均人次
FROM    [dbo].[f_split]('' + @currYear + '01,' + @currYear + '02,' + @currYear
                        + '03,' + @currYear + '04,' + @currYear + '05,'
                        + @currYear + '06,' + @currYear + '07,' + @currYear
                        + '08,' + @currYear + '09,' + @currYear + '10,'
                        + @currYear + '11,' + @currYear + '12', ',')
        LEFT JOIN ( SELECT  jh.patid ,
                            CONVERT(VARCHAR(6), zx.zxsj, 112) rq ,
                            CONVERT(VARCHAR(20), jh.ghnm)
                            + CONVERT(VARCHAR(8), zx.zxsj, 112) rc
                    FROM    dbo.mz_jzjhmxzx (NOLOCK) zx
                            LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                          AND mx.OrganizeId = @orgId
                            INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                         AND jh.OrganizeId = @orgId
                    WHERE   zx.zt = '1'
                            AND mx.zt = '1'
                            AND mx.OrganizeId = @orgId
                            AND CONVERT(VARCHAR(4), zx.zxsj, 112) = @currYear
                  ) aaaaa ON col = aaaaa.rq
GROUP BY col
ORDER BY col;
    END;
 ELSE
    BEGIN
    SELECT col, COUNT(distinct patid) zrs, COUNT(distinct rc) zrc ,
        Convert(decimal(9, 2), 
		CASE WHEN count(distinct patid) = 0 THEN 0
			 ELSE Convert(decimal(9,2),COUNT(distinct rc))/COUNT(distinct patid) end) AS pjrc --平均人次
FROM [dbo].[f_split](''+@currYear+'01,'+@currYear+'02,'+@currYear+'03,'+@currYear+'04,'+@currYear+'05,'+@currYear+'06,'+@currYear+'07,'+@currYear+'08,'+@currYear+'09,'+@currYear+'10,'+@currYear+'11,'+@currYear+'12',',')
LEFT JOIN 
    (SELECT b.patid,
         Convert(varchar(6),ISNULL(a.ssrq,a.sfrq),112) rq,
         Convert(varchar(20),b.ghnm) + Convert(varchar(8), ISNULL(a.ssrq,a.sfrq),112) rc
    FROM mz_xm a
    LEFT JOIN mz_gh b
        ON a.ghnm = b.ghnm
            AND a.OrganizeId = b.OrganizeId
    WHERE a.zt = '1'
            AND b.zt = '1'
            AND a.OrganizeId = @orgId
            AND Convert(varchar(4), ISNULL(a.ssrq,a.sfrq),112) = @currYear
    UNION
    SELECT b.patid,
         Convert(varchar(6),
         a2.jsrq,
        112) rq,
         Convert(varchar(20),
        b.ghnm) + Convert(varchar(8),
         a2.jsrq,
        112) rc
    FROM mz_cfmx a
    LEFT JOIN mz_cf a2
        ON a.cfnm = a2.cfnm
            AND a.OrganizeId = a2.OrganizeId
    LEFT JOIN mz_gh b
        ON a2.ghnm = b.ghnm
            AND a.OrganizeId = b.OrganizeId
    WHERE a.zt = '1'
            AND a2.zt = '1'
            AND b.zt = '1'
            AND a2.jsrq is NOT null
            AND a.OrganizeId = @orgId
            AND Convert(varchar(4), a2.jsrq,112) = @currYear 
			) aaaaa
    ON col = aaaaa.rq
GROUP BY  col
ORDER BY col
    END;");
            parlist.Add(new SqlParameter("@orgId", orgId));
            var list = this.FindList<PerformanceIndicatorVO>(sqlStr.ToString(), parlist.ToArray()).ToList();
            return list;
        }

        /// <summary>
        /// 门诊人次月份图表
        /// </summary>
        public List<PerformanceIndicatorVO> PerformanceMonthIndicator(string orgId, string year,string month)
        {
            StringBuilder sqlStr = new StringBuilder();
            IList<SqlParameter> parlist = new List<SqlParameter>();
            sqlStr.Append(@" --定义年份
                            DECLARE @currYear varchar(30)=(Convert(varchar, year(getdate()),10))");
            if (string.IsNullOrEmpty(year))
            {
                sqlStr.Append(@"--当前年份
                            SET @currYear=(Convert(varchar, year(getdate()),10))");
            }
            else
            {
                sqlStr.Append(@"--选中的年份
                            SET @currYear=@year");
                parlist.Add(new SqlParameter("@year", year));
            }
            sqlStr.Append(@"--当前年月
                            DECLARE @currYearMoth VARCHAR(30);
                            SET @currYearMoth = @currYear + @month;

     DECLARE @cnt INT;
     SELECT @cnt = cnt
     FROM   [dbo].[CntByOrgId](@orgId);
 IF ( @cnt > 0 )--存在执行表记录，按照执行逻辑走,否则按照mz_xm逻辑走
    BEGIN
        SELECT  CONVERT(VARCHAR(10), aaaaa.rq, 25) col ,
                COUNT(DISTINCT patid) zrs ,
                COUNT(DISTINCT rc) zrc ,
                CONVERT(DECIMAL(9, 2), CASE WHEN COUNT(DISTINCT patid) = 0
                                            THEN 0
                                            ELSE CONVERT(DECIMAL(9, 2), COUNT(DISTINCT rc))
                                                 / COUNT(DISTINCT patid)
                                       END) AS pjrc --平均人次
        FROM    ( SELECT    jh.patid ,
                            CONVERT(VARCHAR(10), zx.zxsj, 25) rq ,
                            CONVERT(VARCHAR(20), jh.ghnm)
                            + CONVERT(VARCHAR(8), zx.zxsj, 112) rc
                  FROM      dbo.mz_jzjhmxzx (NOLOCK) zx
                            LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                          AND mx.OrganizeId = @orgId
                            INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                         AND jh.OrganizeId = @orgId
                  WHERE     zx.zt = '1'
                            AND mx.zt = '1'
                            AND jh.zt = '1'
                            AND zx.OrganizeId = @orgId
                            AND CONVERT(VARCHAR(6), zx.zxsj, 112) = @currYearMoth
                ) aaaaa
        GROUP BY aaaaa.rq;       
    END;
 ELSE
    BEGIN
        SELECT  CONVERT(VARCHAR(10), aaaaa.rq, 25) col ,
                COUNT(DISTINCT patid) zrs ,
                COUNT(DISTINCT rc) zrc ,
                CONVERT(DECIMAL(9, 2), CASE WHEN COUNT(DISTINCT patid) = 0
                                            THEN 0
                                            ELSE CONVERT(DECIMAL(9, 2), COUNT(DISTINCT rc))
                                                 / COUNT(DISTINCT patid)
                                       END) AS pjrc --平均人次
        FROM    ( SELECT    b.patid ,
                            CONVERT(VARCHAR(10), ISNULL(a.ssrq, a.sfrq), 25) rq ,
                            CONVERT(VARCHAR(20), b.ghnm)
                            + CONVERT(VARCHAR(8), ISNULL(a.ssrq, a.sfrq), 112) rc
                  FROM      mz_xm a
                            LEFT JOIN mz_gh b ON a.ghnm = b.ghnm
                                                 AND a.OrganizeId = b.OrganizeId
                  WHERE     a.zt = '1'
                            AND b.zt = '1'
                            AND a.OrganizeId = @orgId
                            AND CONVERT(VARCHAR(6), ISNULL(a.ssrq, a.sfrq), 112) = @currYearMoth
                  UNION
                  SELECT    b.patid ,
                            CONVERT(VARCHAR(10), a2.jsrq, 25) rq ,
                            CONVERT(VARCHAR(20), b.ghnm)
                            + CONVERT(VARCHAR(8), a2.jsrq, 112) rc
                  FROM      mz_cfmx a
                            LEFT JOIN mz_cf a2 ON a.cfnm = a2.cfnm
                                                  AND a.OrganizeId = a2.OrganizeId
                            LEFT JOIN mz_gh b ON a2.ghnm = b.ghnm
                                                 AND a.OrganizeId = b.OrganizeId
                  WHERE     a.zt = '1'
                            AND a2.zt = '1'
                            AND b.zt = '1'
                            AND a2.jsrq IS NOT NULL
                            AND a.OrganizeId = @orgId
                            AND CONVERT(VARCHAR(6), a2.jsrq, 112) = @currYearMoth
                ) aaaaa
        GROUP BY aaaaa.rq;        
        END;");
            parlist.Add(new SqlParameter("@orgId", orgId));
            parlist.Add(new SqlParameter("@month", month));
            var list = this.FindList<PerformanceIndicatorVO>(sqlStr.ToString(), parlist.ToArray()).ToList();
            return list;
        }
    }
}
