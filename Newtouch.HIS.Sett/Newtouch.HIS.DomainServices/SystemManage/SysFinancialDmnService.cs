using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    public class SysFinancialDmnService : DmnServiceBase, ISysFinancialDmnService
    {

        public SysFinancialDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IList<FinanceReceiptVO> GetFinancialInvoiceList(string keyValue, string OrganizeId, string zt = "1")
        {
            var sqlStr3 = $@"
                SELECT  a.cwsjId ,
                        a.szm ,
                        a.qssjh ,
                        a.dqsjh ,
                        a.jssjh ,
                        staff.Name ry
                FROM    dbo.cw_sj a
                        LEFT JOIN NewtouchHIS_Base..V_C_Sys_UserStaff staff ON staff.Account = a.ry AND a.OrganizeId = staff.OrganizeId
                        WHERE   a.OrganizeId = @orgId {(string.IsNullOrWhiteSpace(zt) ? "" : $"AND a.zt = @zt ")}
                        AND ((a.ry = @Id or @Id='') or (staff.Name = @Id or @Id=''))
                        order by a.zt desc
            ";

            return this.FindList<FinanceReceiptVO>(sqlStr3, new[] {
                    new SqlParameter("@orgId", OrganizeId),
                    new SqlParameter("@Id", keyValue??""),
                    new SqlParameter("@zt", zt??"")
            }); ;
        }

        public IList<FinanceInvoiceVO> GetCwfpList(string keyValue, string lyry, string OrganizeId)
        {
            var sqlStr3 = $@"
SELECT  a.fpdm ,
        a.szm ,
        a.lyry ,
        a.dqfph ,
        a.qsfph ,a.jsfph,a.zt,a.CreateTime,a.CreatorCode,
        staff.Name ry ,
        a.zt ,
        isnull(a.is_del, 0) is_del
FROM    dbo.cw_fp a
        LEFT JOIN NewtouchHIS_Base..V_C_Sys_UserStaff staff ON staff.Account = a.lyry AND a.OrganizeId = staff.OrganizeId
        WHERE  a.OrganizeId = @orgId 
        AND ((a.lyry = @Id or @Id='') or (staff.Name = @Id or @Id='')) {(string.IsNullOrWhiteSpace(lyry) ? "" : "AND a.lyry = @lyry ")}
        order by a.is_del asc, a.zt desc, a.CreateTime desc ";

            return this.FindList<FinanceInvoiceVO>(sqlStr3.ToString(), new[] {
                        new SqlParameter("@orgId", OrganizeId),
                        new SqlParameter("@lyry", lyry??""),
                        new SqlParameter("@Id", keyValue??"")
                }); ;
        }


        public IList<InvoiceDetailVO> InvoiceQueryList(DateTime kssj, DateTime jssj, string orgId, string creatorCode)
        {
            var sqlStr3 = $@"
SELECT * 
FROM (
	SELECT '门诊' jzlx, gh.mzh jzh, js.fph, brxx.xm, gh.kh, dept.Name ks, js.zje, js.jzsj sfrq  
	FROM mz_js(nolock) js 
	INNER JOIN mz_gh(nolock) gh on gh.ghnm=js.ghnm and gh.OrganizeId=js.OrganizeId AND gh.zt='1'
	INNER JOIN dbo.xt_brjbxx(nolock) brxx on brxx.patid=js.patid and brxx.OrganizeId=brxx.OrganizeId AND brxx.zt='1'
	LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(nolock) dept on dept.Code=gh.ks and dept.OrganizeId=brxx.OrganizeId AND dept.zt='1'
	WHERE ISNULL(js.fph, '')<>'' and js.OrganizeId=@orgId and js.zt='1' 
	and js.CreatorCode=@creatorCode AND js.jzsj BETWEEN @kssj AND @jssj

	UNION ALL

	SELECT '住院' jzlx, js.zyh jzh, js.fph, brxx.xm, brxx.kh, dept.Name ks , js.zje, js.CreateTime sfrq
	FROM zy_js(nolock) js 
	INNER JOIN zy_brjbxx(nolock) brxx on brxx.zyh=js.zyh and brxx.OrganizeId=js.OrganizeId AND brxx.zt='1'
	LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(nolock) dept on dept.Code=brxx.ks and dept.OrganizeId=brxx.OrganizeId AND dept.zt='1'
	WHERE ISNULL(js.fph, '')<>'' and js.OrganizeId=@orgId and js.zt='1' 
	and js.CreatorCode=@creatorCode AND js.CreateTime BETWEEN @kssj AND @jssj
) a
ORDER BY a.fph, a.sfrq DESC ";
            return FindList<InvoiceDetailVO>(sqlStr3.ToString(), new[] {
                    new SqlParameter("@orgId", orgId),
                    new SqlParameter("@kssj", kssj<=Constants.MinDate?Constants.MinDate:kssj),
                    new SqlParameter("@jssj", jssj>DateTime.Now?DateTime.Now:jssj),
                    new SqlParameter("@creatorCode", creatorCode??"")
            }); ;
        }
    }
}
