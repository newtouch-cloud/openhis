using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices.Outpatient;
using Newtouch.Domain.ValueObjects.Outpatient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.DomainServices.Outpatient
{
    public class ElectronicPrescriptionDmnService : DmnServiceBase, IElectronicPrescriptionDmnService
    {
        public ElectronicPrescriptionDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
        public IList<ElectronicPrescriptionVO> GetGridJson(Pagination pagination, string organizeId,DateTime kssj, DateTime jssj, string xm)
        {
            var sql = new StringBuilder(@"
select a.*,
case when ysshyj is null then shzt1  when ysshyj='' then shzt1 else '未通过' end shzt
from (
select 
cf.cfId,cf.cfh,jz.mzh,gh.xm,gh.zjlx,gh.zjh,isnull(rxStasName,'无效') shzt1,isnull(rxUsedStasName,'未使用') qyzt,isnull(rxStasName,'未审核') cfzt
,jz.ghksmc ghks,dept.name kdks,jz.ghsj,cf.createtime cfklrq,cf.ysshyj
 from dbo.xt_cf cf 
 --left join dbo.xt_cfmx cfmx on cfmx.cfId=cf.cfId and cfmx.organizeId=cf.organizeId and cfmx.zt=cf.zt
 left join [dbo].[xt_jz] jz on cf.jzId=jz.jzId and cf.organizeId=jz.organizeId and cf.zt=jz.zt
 left join [NewtouchHIS_Sett].dbo.mz_gh gh on gh.mzh=jz.mzh and gh.organizeId=jz.organizeId and gh.zt=jz.zt
 left join [NewtouchHIS_Sett].[dbo].[Dzcf_D003_output] a on a.cfh=cf.cfh and a.OrganizeId=cf.OrganizeId
 left join [NewtouchHIS_Base].[dbo].[Sys_Department] dept on dept.code=cf.ks
 where cf.zt=1 and isdzcf=1 and cf.organizeId=@OrganizeId and cf.createtime between @kssj and @jssj+' 23:59:59' 
");
            if (!string.IsNullOrWhiteSpace(xm))
            {
                sql.AppendLine(" and (gh.xm like @xm or gh.mzh like @xm)");
            }

            sql.AppendLine(" ) a");
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
                new SqlParameter("@xm", "%"+xm+"%"),
            };
            return QueryWithPage<ElectronicPrescriptionVO>(sql.ToString(), pagination, param);
        }
    }
}
