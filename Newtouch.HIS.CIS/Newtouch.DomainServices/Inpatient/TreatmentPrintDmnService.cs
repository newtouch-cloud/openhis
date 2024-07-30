using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.DomainServices.Inpatient
{
    public class TreatmentPrintDmnService : DmnServiceBase, ITreatmentPrintDmnService
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public TreatmentPrintDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取病区发药患者树
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public IList<InpWardPatTreeVO> GetPatTree(string orgId)
        {
            string sql = @"select distinct b.zyh,b.xm hzxm,b.wardCode bqCode,c.bqmc from(select zyh from   zy_fyqqk where OrganizeId=@orgId
                union all
                select zyh from zy_tyssqqk where  OrganizeId=@orgId) aaa 
                inner join zy_brxxk b on aaa.zyh=b.zyh and b.OrganizeId=@orgId
                left join [NewtouchHIS_Base].[dbo].[V_S_xt_bq] c with(nolock) on c.bqcode=b.wardCode and c.OrganizeId=@orgId
                where b.zybz <>'9'--非取消入院
                and isnull(bqcode,'')<>''";
            return this.FindList<InpWardPatTreeVO>(sql, new[] { new SqlParameter("@orgId", orgId) });
        }

        public IList<TreatmentPrintVO> GetDetailGridJson(Pagination pagination, string orgId,string zyh,DateTime? kssj, DateTime? jssj,int? zyxz) {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
             sqlstr.Append(@"select aaa.*,isnull(b.yznr,c.yznr) yznr,staff.name lrz,'已执行' zt,d.bedcode,d.xm hzxm,case when zyxz=0 then '临' when zyxz=1 then '长' else '' end clbz  from 
                (select Id,zyh,yzxh,zxrq,createtime,mcsl,creatorcode,dw,zyxz,fzxh from zy_tyssqqk where organizeId=@orgId
                union all
                select Id,zyh,yzxh,zxrq,createtime,mcsl,creatorcode,ypdw dw,yzxz as zyxz,fzxh from zy_fyqqk where organizeId=@orgId) aaa
                left join zy_cqyz b on b.Id=aaa.yzxh and b.organizeId=@orgId
                left join zy_lsyz c on c.Id=aaa.yzxh and C.organizeId=@orgId
                LEFT JOIN NewtouchHIS_Base..V_C_Sys_UserStaff staff ON staff.Account = aaa.CreatorCode
                                                                              AND staff.OrganizeId = @orgId
                inner join zy_brxxk d on d.zyh=aaa.zyh and d.OrganizeId = @orgId
                where 1=1 ");
            if (zyxz != null)//长临标志：0：临时，1：长期
            {
                sqlstr.Append("  and zyxz=@zyxz");
                par.Add(new SqlParameter("@zyxz", zyxz));
            }
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sqlstr.Append("  and aaa.zyh IN ( SELECT * FROM dbo.f_split(@zyh, ','))");
                par.Add(new SqlParameter("@zyh", zyh));
            }
            if (kssj != DateTime.MaxValue && kssj != DateTime.MinValue &&
                jssj != DateTime.MaxValue && jssj != DateTime.MinValue&&kssj!=null&&jssj!=null)
            {
                sqlstr.Append("  and zxrq > @kssj AND zxrq < DATEADD(dd, 1, @jssj)");
                par.Add(new SqlParameter("@kssj", kssj));
                par.Add(new SqlParameter("@jssj", jssj));
            }
            par.Add(new SqlParameter("@orgId", orgId));
            return this.QueryWithPage<TreatmentPrintVO>(sqlstr.ToString(), pagination, par.ToArray());
        }
    }
}
