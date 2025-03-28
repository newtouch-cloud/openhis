using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices.Clinic;
using Newtouch.Domain.ValueObjects.Clinic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.DomainServices.Clinic
{
    public class ClinicDmnService : DmnServiceBase, IClinicDmnService
    {

        public ClinicDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }



        public IList<ClinicApplyInfoVO> GetClinicApplyGridJson(Pagination pagination, string orgId, string xm, string zjh, string kssj, string jssj, string ksCode, string ysgh, int sqzt,string userCode)
        {
            string sql = @" 
select a.*,b.Name deptName,c.Name ysxm,d.brxzmc from zl_zlsq a
left join [NewtouchHIS_Base].dbo.Sys_Department b on a.ks=b.Code and a.organizeId=b.organizeId and a.zt=b.zt
left join [NewtouchHIS_Base].dbo.[Sys_Staff] c on a.ysgh=c.gh and a.organizeId=c.organizeId and a.zt=c.zt
left join [NewtouchHIS_Sett].dbo.[xt_brxz] d on a.brxz=d.brxz and a.organizeId=d.organizeId and a.zt=d.zt 
where a.zt=1 and a.OrganizeId=@orgId 
";

            if (!string.IsNullOrWhiteSpace(xm)) {
                sql += " and a.xm like @xm";
            }
            if (!string.IsNullOrWhiteSpace(zjh))
            {
                sql += " and a.zjh like @zjh";
            }
            if (!string.IsNullOrWhiteSpace(ksCode))
            {
                sql += " and a.ks = @ksCode";
            }
            if (!string.IsNullOrWhiteSpace(ysgh))
            {
                sql += " and a.ysgh = @ysgh";
            }
            if (sqzt!=0)
            {
                sql += " and a.sqzt = @sqzt";
            }
            sql += " and a.sqsj between @kssj and @jssj";
            sql += " and a.ysgh=@userCode or a.sqrgh=@userCode";
            
            IList<SqlParameter> parlist = new List<SqlParameter>();
            parlist.Add(new SqlParameter("@orgId", orgId));
            parlist.Add(new SqlParameter("@xm", "'%"+xm+"%'"));
            parlist.Add(new SqlParameter("@zjh", "'%" + zjh + "%'"));
            parlist.Add(new SqlParameter("@ksCode", ksCode));
            parlist.Add(new SqlParameter("@ysgh", ysgh));
            parlist.Add(new SqlParameter("@sqzt", sqzt));
            parlist.Add(new SqlParameter("@kssj", kssj + " 00:00:00"));
            parlist.Add(new SqlParameter("@jssj", jssj + " 23:59:59"));
            parlist.Add(new SqlParameter("@userCode", userCode));
            return this.QueryWithPage<ClinicApplyInfoVO>(sql, pagination, parlist.ToArray(), false);
        }

        public OutBookScheduleVO getClinicScheduleId(string orgId, string czks)
        {
            string sql = @" 
select * from  [NewtouchHIS_Sett].dbo.mz_ghpb_schedule a
where a.zt=1 and a.OrganizeId=@orgId 
and czks=@czks and regFee=0 and outDate = Convert (date,getDate(),120)
";

            IList<SqlParameter> parlist = new List<SqlParameter>();
            parlist.Add(new SqlParameter("@orgId", orgId));
            parlist.Add(new SqlParameter("@czks", czks));
            return this.FirstOrDefault<OutBookScheduleVO>(sql, parlist.ToArray());
        }

        public ClinicPatVO getClinicPatInfo(string orgId, string Id)
        {
            string sql = @" 

select a.[Id]
      ,a.[OrganizeId]
      ,a.[sqsj]
      ,a.[jzsj]
      ,a.[ks]
      ,a.[ysgh]
      ,a.[sqzt]
      ,a.[patid]
      ,a.[xm]
      ,a.[nl]
      ,a.[zjh]
      ,a.[birth]
      ,a.[brxz]
      ,a.[mettingId]
      ,a.[sqr]
      ,a.[sqlsh]
      ,a.[CreateTime]
      ,a.[CreatorCode]
      ,a.[LastModifyTime]
      ,a.[LastModifierCode]
      ,a.[zt]
      ,a.[kh] 
	  ,mzjbz=1
,(CASE a.xb WHEN '1' THEN '男' WHEN '2' THEN '女' ELSE '不详' END ) as xb
,xz.brxzmc brxzmc
,a.nl nlshow
,a.mzh
,gh.blh
,xz.brxz brxzCode 
,xx.csny
,gh.zjlx
,'远程诊疗' ghksmc
,ysxmgl.Name ysxm
,CONVERT(varchar(100), isnull(gh.ghrq, gh.CreateTime), 20) AS ghsj
,bl.jzId
--,mzh=(select top 1 mzh from [NewtouchHIS_Sett].dbo.mz_gh gh  where patId=a.patId and  a.OrganizeId=OrganizeId and a.zt=zt and gh.ks='00000080' order by createTime desc)
from zl_zlsq a
LEFT JOIN NewtouchHIS_Sett..xt_brxz xz ON xz.brxz = a.brxz	AND xz.OrganizeId = a.OrganizeId AND xz.zt=a.zt
LEFT JOIN [NewtouchHIS_Sett].dbo.mz_gh gh on a.patId=gh.patid and a.OrganizeId=gh.OrganizeId and a.zt=gh.zt and gh.ks='00000080'
left join NewtouchHIS_Sett..xt_brjbxx xx on a.patId = xx.patId and a.OrganizeId = xx.OrganizeId and a.zt=xx.zt
LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff ysxmgl on ysxmgl.gh = gh.ys and ysxmgl.OrganizeId = gh.OrganizeId and ysxmgl.zt=gh.zt
left join xt_bl bl on bl.blh=gh.blh and bl.OrganizeId=gh.OrganizeId and bl.zt=gh.zt
where a.zt=1 and a.OrganizeId=@orgId  and a.Id=@Id
order by gh.createTime desc
";

            IList<SqlParameter> parlist = new List<SqlParameter>();
            parlist.Add(new SqlParameter("@orgId", orgId));
            parlist.Add(new SqlParameter("@Id", Id));
            return this.FirstOrDefault<ClinicPatVO>(sql, parlist.ToArray());
        }


    }
}
