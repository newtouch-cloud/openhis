using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.SystemManage
{
    public class SysConsultDmnService : DmnServiceBase, ISysConsultDmnService
    {


        public SysConsultDmnService(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IList<SysExpertVO> GetExpertListByDept(string orgId, string ksCode)
        {

            var sql = @"select distinct
c.gh,c.Name name,c.DepartmentCode 
from [dbo].[Sys_StaffDuty] a
left join [dbo].[Sys_Duty] b on a.DutyId=b.id and a.zt=b.zt 
left join [dbo].[Sys_Staff] c on a.StaffId=c.Id and a.zt=c.zt
where a.zt=1 and c.OrganizeId=@orgId
and (code='zrys' or code='fzrys')";
            if (!string.IsNullOrWhiteSpace(ksCode))
            {
                sql += " and c.departmentCode=@ksCode";
            }

            return this.FindList<SysExpertVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId", orgId ?? ""),
                new SqlParameter("@ksCode", ksCode ?? ""),
            });
        }

        /// <summary>
        /// 根据专家工号获取专家姓名
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="gh"></param>
        /// <returns></returns>

        public string GetNameByEpxert(string orgId, string gh)
        {

            var sql = @"select name from [dbo].[Sys_Staff]
where zt=1 and organizeId=@orgId and gh=@gh";

            return this.FirstOrDefault<string>(sql, new SqlParameter[] {
                new SqlParameter("@orgId", orgId ?? ""),
                new SqlParameter("@gh", gh ?? ""),
            });
        }

    }
}
