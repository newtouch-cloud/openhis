using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    public class CommonDmnService : DmnServiceBase, ICommonDmnService
    {
        public CommonDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取就诊人数（门诊和住院）
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public VisitNumBO GetVisitNum(string configmethod, string orgId = null)
        {

            /*************************************门诊部门，暂不考虑全退*****************************************************/
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
                (
	                select groupDate, count(1) num1 from 
	                (
	                select distinct CONVERT(varchar(6), groupDate, 112) groupDate, patid
	                from 
	                (");
            if (configmethod.ToLower() == "reg")
            {
                sqlStr.Append(@"   SELECT    CONVERT(VARCHAR(6), ISNULL(gh.ghrq,
                                                              CreateTime), 112) AS groupDate,
                                                    mzh patid
                                          FROM      mz_gh gh
                                          WHERE gh.zt = '1'
                                                    AND ISNULL(gh.ghzt, '') <> '2'
                                                    AND OrganizeId = @orgId
                                                    AND CONVERT(VARCHAR(6), ISNULL(gh.ghrq,
                                                              CreateTime), 112) >= @currYearMoth");
            }
            else if (configmethod.ToLower() == "exce")
            {
                sqlStr.Append(@"  SELECT  CONVERT(VARCHAR(6), zx.zxsj, 112) AS groupDate ,
                        jh.patid
                FROM    dbo.mz_jzjhmxzx (NOLOCK) zx
                        LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = zx.jzjhmxId
                                                      AND mx.OrganizeId = @orgId
                        INNER JOIN dbo.mz_jzjh jh ON jh.jzjhId = mx.jzjhId
                                                     AND jh.OrganizeId = @orgId
                WHERE   zx.zt = '1'
                        AND zx.OrganizeId = @orgId
                        AND CONVERT(VARCHAR(6), zx.zxsj, 112) >= @currYearMoth");
            }
            else
            {
                sqlStr.Append(@" select convert(varchar(6),
			                isnull(ssrq, sfrq),
			                112) AS groupDate , patid
	                from mz_xm  where zt = '1' and OrganizeId=@orgId and convert(varchar(6),isnull(ssrq, sfrq), 112) >=@currYearMoth");
            }

            sqlStr.Append(@")aaa
	                ) bbb
	                group by bbb.groupDate
                    ) AS a
                    ON a.groupDate=col
                    ORDER BY col");

            SqlParameter[] param =
            {
                    new SqlParameter("@orgId",orgId)
                };
            outpatientlist = this.FindList<OutpatientVisitNumVO>(sqlStr.ToString(), param).ToList();


            /**********************************************住院部门,已去掉全退的人*****************************************************/

            var inpatientList = new List<InpatientVisitNumVO>();

            var sqlStr3 = new StringBuilder();
            List<InpatientVisitBasicVO> zyList = null;

            sqlStr3.Append(@"select zyh, zybz, ryrq, cyrq
                                    from zy_brjbxx
                                    where zt = '1' and zybz <> 9
                                    and OrganizeId = @orgId
                                     AND ( zybz <> 3 --已出院
                                      AND ( ryrq >= @startTime
                                            AND ryrq < @entTime
                                          )
                                      OR ( cyrq >= @startTime
                                           AND cyrq < @entTime
                                         ))");
            zyList = this.FindList<InpatientVisitBasicVO>(sqlStr3.ToString(), new[] {
                    new SqlParameter("@orgId", orgId)
                    ,new SqlParameter("@startTime", new DateTime(DateTime.Now.Year, 1,1))
                    ,new SqlParameter("@entTime", new DateTime(DateTime.Now.Year + 1, 1,1))
                });

            var nowYear = DateTime.Now.Year;
            var nowMonth = DateTime.Now.Month;
            string ycy = ((int)EnumZYBZ.Ycy).ToString();
            for (var i = 0; i < 12; i++)
            {
                var num = 0;
                if (i < nowMonth)
                {
                    //1.当月入院或当月出院
                    //2.在院状态<>已出院 and 入院日期<当月
                    num = zyList.Count(p =>
                    (p.ryrq >= new DateTime(nowYear, (i + 1), 1)
                    && p.ryrq < new DateTime(nowYear, (i + 1), 1).AddMonths(1)
                    || (p.cyrq >= new DateTime(nowYear, (i + 1), 1)
                    && p.cyrq < new DateTime(nowYear, (i + 1), 1).AddMonths(1)))
                    || (p.zybz != ycy && p.ryrq < new DateTime(nowYear, (i + 1), 1)));
                }
                inpatientList.Add(new InpatientVisitNumVO()
                {
                    num = num,
                });
            }

            //放在一个对象，返回到页面
            var visitNumBO = new VisitNumBO
            {
                OutpatientList = outpatientlist,
                InpatientList = inpatientList,
                zyrs = zyList.Count,
            };
            return visitNumBO;
        }

        /// <summary>
        /// 获取今天之前10周就诊人次详情
        /// </summary>
        /// <param name="month"></param>
        /// <param name="isAdministrator"></param>
        /// <param name="orgId"></param>
        /// <param name="topOrgId"></param>
        public MonthVisitNumBO GetWeekNum(string configmethod, string orgId)
        {
            /*************************************门诊部门，暂不考虑全退*****************************************************/
            List<OutpatientWeekVisitNumVO> outpatientlist = null;
            var basic = @"WITH    cteTree
                              AS ( SELECT   DATEADD(wk, DATEDIFF(wk, 0, GETDATE()), 0) wbegin ,
                                            GETDATE() wend
                                   UNION ALL
                                   SELECT   DATEADD(d, -7, wbegin) wbegin ,
                                            DATEADD(d, -1, wbegin) wend
                                   FROM     cteTree
                                   WHERE    wbegin > DATEADD(wk, -9,
                                         DATEADD(wk,
                                                 DATEDIFF(wk, 0, GETDATE()), 0)))";
            StringBuilder sqlStr = new StringBuilder(basic);
            if (configmethod.ToLower() == "reg")
            {
                sqlStr.Append(@"    SELECT DISTINCT
                                    CONVERT(VARCHAR(10), aaa.wbegin, 25) wbegin ,
                                    CONVERT(VARCHAR(10), aaa.wend, 25) wend ,
                                    COUNT(1) num
                            FROM    ( SELECT  DISTINCT  cte.wbegin ,
                                                cte.wend ,
                                                CONVERT(VARCHAR(10), ISNULL(ghrq, CreateTime), 25) AS vDate ,
                                                mzh
                                      FROM      dbo.mz_gh
                                                INNER JOIN cteTree cte ON CONVERT(VARCHAR(10), ISNULL(ghrq,
                                                                                      CreateTime), 25) >= cte.wbegin
                                                                          AND CONVERT(VARCHAR(10), ISNULL(ghrq,
                                                                                      CreateTime), 25) < cte.wend
                                      WHERE     zt = '1' and ISNULL(ghzt, '') <> '2'
                                                AND OrganizeId =@orgId
                                    ) aaa
                            GROUP BY aaa.wbegin ,
                                    aaa.wend;");
            }
            else
            {
                sqlStr.Append(@"    SELECT DISTINCT
                                    CONVERT(VARCHAR(10), aaa.wbegin, 25) wbegin,
                                    CONVERT(VARCHAR(10), aaa.wend, 25) wend ,
                                    COUNT(1) num
                            FROM    ( SELECT  DISTINCT  cte.wbegin ,
                                                cte.wend ,
                                                CONVERT(VARCHAR(10), ISNULL(ssrq, sfrq), 25) AS vDate ,
                                                patid
                                      FROM      mz_xm
                                                INNER JOIN cteTree cte ON CONVERT(VARCHAR(10), ISNULL(ssrq,
                                                                                      CreateTime), 25) >= cte.wbegin
                                                                          AND CONVERT(VARCHAR(10), ISNULL(ssrq,
                                                                                      CreateTime), 25) < cte.wend
                                      WHERE     zt = '1'
                                                AND OrganizeId = @orgId
                                    ) aaa
                            GROUP BY aaa.wbegin ,
                                    aaa.wend;");
            }
            SqlParameter[] param =
                         {
                    new SqlParameter("@orgId",orgId)
                };
            outpatientlist = this.FindList<OutpatientWeekVisitNumVO>(sqlStr.ToString(), param).ToList();

            /****************住院部门,已去掉全退的人**********************/
            StringBuilder sqlStr3 = new StringBuilder(basic);
            var inpatientList = new List<OutpatientWeekVisitNumVO>();
            List<InpatientVisitBasicVO> zyList = null;
            //如果是admin登录，则统计该组织机构下所有医院的就诊人数
            sqlStr3.Append(@"     SELECT  CONVERT(VARCHAR(10), cte.wbegin, 25) wbegin,
                                    CONVERT(VARCHAR(10), cte.wend, 25)  wend,
                                    zyh ,
                                    zybz ,
                                    ryrq ,
                                    cyrq
                            FROM    zy_brjbxx
                                    INNER JOIN cteTree cte ON CONVERT(VARCHAR(10), ryrq, 25) >= cte.wbegin
                                                              AND CONVERT(VARCHAR(10), ryrq, 25) < cte.wend
                            WHERE   zt = '1'
                                    AND zybz <> 9
                                    AND OrganizeId = @orgId");
            zyList = this.FindList<InpatientVisitBasicVO>(sqlStr3.ToString(), new[] {
                    new SqlParameter("@orgId", orgId)});
            string ycy = ((int)EnumZYBZ.Ycy).ToString();
            var LastWeekInfo = GetLastWeek();//获取最后10周日期list
            if (LastWeekInfo != null && LastWeekInfo.Count() > 0)
            {
                for (int i = 0; i < LastWeekInfo.Count(); i++)
                {
                    //1.本周入院或本周出院
                    //2.在院状态<>已出院 and 入院日期<本周
                    var Num = zyList.Count(p =>
                      (p.ryrq >= DateTime.Parse(LastWeekInfo[i].wbegin)
                      && p.ryrq < DateTime.Parse(LastWeekInfo[i].wend)
                      || (p.cyrq >= DateTime.Parse(LastWeekInfo[i].wbegin)
                      && p.cyrq < DateTime.Parse(LastWeekInfo[i].wend)))
                      || (p.zybz != ycy && p.ryrq < DateTime.Parse(LastWeekInfo[i].wbegin)));
                    inpatientList.Add(new OutpatientWeekVisitNumVO()
                    {
                        num = Num,
                        wbegin = LastWeekInfo[i].wbegin.ToString(),
                        wend = LastWeekInfo[i].wend.ToString()
                    });
                }
            }
            //放在一个对象，返回到页面
            var visitNumBO = new MonthVisitNumBO
            {
                OutpatientList = outpatientlist,
                InpatientList = inpatientList
            };
            return visitNumBO;
        }

        /// <summary>
        /// 获取最后10周日期list
        /// </summary>
        /// <returns></returns>
        public List<LastWeekInfo> GetLastWeek()
        {
            var LastWeekInfo = this.FindList<LastWeekInfo>(@"WITH    cteTree
                              AS(SELECT    DATEADD(wk, DATEDIFF(wk, 0, GETDATE()), 0) wbegin ,
                                            GETDATE() wend
                                   UNION ALL
                                   SELECT   DATEADD(d, -7, wbegin) wbegin,
                                            DATEADD(d, -1, wbegin) wend
                                   FROM     cteTree
                                   WHERE    wbegin > DATEADD(wk, -9,
                                         DATEADD(wk,
                                                 DATEDIFF(wk, 0, GETDATE()), 0))) 
                                     SELECT  CONVERT(VARCHAR(10), wbegin, 25) wbegin ,
                                                CONVERT(VARCHAR(10), wend, 25) wend
                                        FROM    cteTree ORDER BY wbegin");
            return LastWeekInfo;
        }

        /// <summary>
        /// 贵州医保,通过交易流水号获取交易验证码
        /// </summary>
        /// <param name="jylsh"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public string Gzyb_GetJyyzmByJylsh(string jylsh, string OrgId)
        {
            const string sqlStr = " SELECT TOP 1 jyyzm FROM V_Gzyb_Trancation WHERE jylsh=@jylsh AND OrganizeId=@orgId ";
            SqlParameter[] param =
            {
                new SqlParameter("@jylsh",jylsh),
                new SqlParameter("@orgId",OrgId)
            };
            List<string> results = this.FindList<string>(sqlStr, param).ToList();
            if (results != null && results.Count > 0)
                return results[0];
            else
                return string.Empty;
        }

        /// <summary>
        /// 获取药品分类list（非收费大类）
        /// </summary>
        /// <returns></returns>
        public IList<SysMedicineClassificationVEntity> GetMedicineClassificationList()
        {
            var sql = "select * from [NewtouchHIS_Base]..V_S_xt_ypfl";
            return this.FindList<SysMedicineClassificationVEntity>(sql);
        }

        /// <summary>
        /// 获取项目分类list
        /// </summary>
        /// <returns></returns>
        public IList<SysChargeCategoryVEntity> GetChargetItemClassificationList(string orgId)
        {
            //2项目
            var sql = "select * from [NewtouchHIS_Base]..V_S_xt_sfdl where dllb = 2 and OrganizeId = @orgId and zt = '1'";
            return this.FindList<SysChargeCategoryVEntity>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }


        #region base字典等公共数据
        /// <summary>
        /// 获取科室绑定的病区
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysDepartmentWardRelationVO> GetWardbyDept(string orgId,string ks,string keyword)
        {
            string sql = @"select a.bqCode bq,a.bqmc,c.Code ks,c.Name ksmc
from NewtouchHIS_Base.dbo.xt_bq a with(nolock)
left join NewtouchHIS_Base.dbo.Sys_DepartmentWardRelation b with(nolock) on a.OrganizeId=b.OrganizeId and a.bqCode=b.bqCode and b.zt='1' 
left join NewtouchHIS_Base.dbo.Sys_Department c  with(nolock) on b.OrganizeId=c.OrganizeId and b.DepartmentId=c.Id and c.zt='1'
where a.OrganizeId=@orgId and a.zt='1'";
            if (!string.IsNullOrWhiteSpace(ks))
            {
                sql += " and b.bqCode=@ks ";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and a.bqmc like @keyword ";
            }
            return this.FindList<SysDepartmentWardRelationVO>(sql, new SqlParameter[] {
                new SqlParameter("orgId",orgId),
                new SqlParameter("ks",ks??""),
                new SqlParameter("keyword","%"+keyword??""+"%"),
            });

        }

        #endregion
    }
}

