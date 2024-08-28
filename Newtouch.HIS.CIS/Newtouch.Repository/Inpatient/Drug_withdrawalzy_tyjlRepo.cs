using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.IRepository.Inpatient;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.Repository.Inpatient
{
    public class Drug_withdrawalzy_tyjlRepo : RepositoryBase<Drug_withdrawalzy_tyjlEntity>, IDrug_withdrawalzy_tyjlRepo
    {
        public Drug_withdrawalzy_tyjlRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public IList<DrugwithdrawalTreeVO> Griddata(string patInfo, string keyword, string kssj, string jssj, string orgId)
        {
            //药品名称不为空时
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var sql = string.Format(@"select a.tydh,a.Id,a.hzxm,b.yzxz,b.ypmc,a.tysl,a.ypdw,CONVERT(varchar(16), b.CreateTime, 20) as zxrq,CONVERT(varchar(16), a.CreateTime, 20) as CreateTime,a.zt,cw.cwmc,s.name ysxm,a.zyh
 from Newtouch_CIS.dbo.zy_tyjl a 
 left join  Newtouch_CIS.[dbo].[zy_fyqqk] b on a.zyh=b.zyh  and a.OrganizeId=b.OrganizeId and b.zt=1 and a.yzxh=b.yzxh and a.fyqqxh=b.lyxh
 left join newtouch_cis..zy_cqyz cq on cq.id=a.yzxh and cq.zt='1'
 left join newtouch_cis..zy_lsyz ls on ls.id=a.yzxh and ls.zt='1'
 left join newtouch_cis..zy_brxxk zb on zb.zyh=a.zyh and zb.zt='1'
 left join NewtouchHIS_Base.dbo.V_S_xt_cw cw with(nolock) on zb.BedCode=cw.cwCode and zb.OrganizeId=cw.OrganizeId and cw.sfzy=1
 left join NewtouchHIS_Base.dbo.V_C_Sys_UserStaff s on a.OrganizeId=s.OrganizeId and a.CreatorCode=s.account and s.zt='1'
 where
 a.zyh='" + patInfo + "' and a.OrganizeId='" + orgId + "' and a.CreateTime>='" + kssj + "' and a.CreateTime<='" + jssj + "' and a.zt=1 and b.ypmc like '%" + keyword+"%' " +
 " group by a.tydh,a.Id,a.hzxm,b.yzxz,b.ypmc,a.tysl,a.ypdw,a.zt,cw.cwmc,s.name,a.zyh,CONVERT(varchar(16), b.CreateTime, 20),CONVERT(varchar(16), a.CreateTime, 20)" +
 " order by a.tydh");

                return this.FindList<DrugwithdrawalTreeVO>(sql);
            }
            else
            {
                var sql = string.Format(@"select a.tydh,a.Id,a.hzxm,b.yzxz,b.ypmc,a.tysl,a.ypdw,CONVERT(varchar(16), b.CreateTime, 20) as zxrq,CONVERT(varchar(16), a.CreateTime, 20) as CreateTime,a.zt,cw.cwmc,s.name ysxm,a.zyh
 from Newtouch_CIS.dbo.zy_tyjl a 
 left join  Newtouch_CIS.[dbo].[zy_fyqqk] b on a.zyh=b.zyh  and a.OrganizeId=b.OrganizeId and b.zt=1 and a.yzxh=b.yzxh and a.fyqqxh=b.lyxh
 left join newtouch_cis..zy_cqyz cq on cq.id=a.yzxh and cq.zt='1'
 left join newtouch_cis..zy_lsyz ls on ls.id=a.yzxh and ls.zt='1'
 left join newtouch_cis..zy_brxxk zb on zb.zyh=a.zyh and zb.zt='1'
 left join NewtouchHIS_Base.dbo.V_S_xt_cw cw with(nolock) on zb.BedCode=cw.cwCode and zb.OrganizeId=cw.OrganizeId and cw.sfzy=1
 left join NewtouchHIS_Base.dbo.V_C_Sys_UserStaff s on a.OrganizeId=s.OrganizeId and a.CreatorCode=s.account and s.zt='1'
 where
 a.zyh='" + patInfo+ "' and a.OrganizeId='" + orgId + "' and a.CreateTime>='" + kssj+"' and a.CreateTime<='"+jssj+ "' and a.zt=1 " +
 " group by a.tydh,a.Id,a.hzxm,b.yzxz,b.ypmc,a.tysl,a.ypdw,a.zt,cw.cwmc,s.name,a.zyh,CONVERT(varchar(16), b.CreateTime, 20),CONVERT(varchar(16), a.CreateTime, 20)" +
 " order by a.tydh");

                return this.FindList<DrugwithdrawalTreeVO>(sql);
            }
        }

        public IList<GrugTreezsVO> treecx(string keyword, string staffId, string orgId)
        {
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@staffId", staffId));

            string sqlText = "select distinct a.zyh,a.hzxm,bq.bqCode,bq.bqmc " +
                ", cw.BedNo,e.sex,CAST(FLOOR(DATEDIFF(DY, e.birth, GETDATE()) / 365.25) AS VARCHAR(5)) nl"+
                ",CONVERT(VARCHAR(25),CASE DATEDIFF(DAY, e.ryrq,GETDATE()) WHEN 0 THEN 1 else  DATEDIFF(DAY, e.ryrq,GETDATE())END ) inHosDays " +
                             "from[NewtouchHIS_Base].[dbo].[Sys_StaffWard] sw " +
                              "inner join[NewtouchHIS_Base].[dbo].[xt_bq] bq on sw.OrganizeId=bq.OrganizeId and sw.bqcode=bq.bqcode " +
                              "inner join Newtouch_CIS.dbo.zy_tyjl a on bq.OrganizeId= a.OrganizeId and bq.bqCode= a.WardCode " +
                              "inner join zy_brxxk e on e.zyh=a.zyh and e.organizeid=a.organizeid " +
                              "left join zy_cwsyjlk cw with(nolock) on cw.zyh=a.zyh and cw.OrganizeId=a.OrganizeId " +
                              "where sw.OrganizeId=@orgId and sw.StaffId=@staffId";
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sqlText += " and (a.zyh like @keyword or a.hzxm like @keyword)";
                pars.Add(new SqlParameter("@keyword", keyword + "%"));
            }
            return this.FindList<GrugTreezsVO>(sqlText, pars.ToArray());
        }

    }
}
