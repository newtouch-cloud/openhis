using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.DomainServices
{
    public class MrDeptDmnService : DmnServiceBase, IMrDeptDmnService
    {
        public MrDeptDmnService(IDefaultDatabaseFactory databaseFactory)
               : base(databaseFactory)
        {

        }

        /// <summary>
        /// 分页获取his科室 病案科室列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<MrDeptVO> GetPaginationList(Pagination pagination, string orgId)
        {
            string sql = @"select a.Id, a.organizeId,baksId,c.ksmc as baksName,a.code as hisdept,a.name as hisdeptname from [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] a
left join [Newtouch_MRMS].[dbo].[mr_rel_dept] b 
on a.Code = b.hisdept 
left join [Newtouch_MRMS].[dbo].[mr_dic_dept] c 
on b.baksId=c.ksbm 
where a.zt=1";
            var para = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(orgId))
            {
                sql += " and a.organizeId=@orgId";
            }
            para.Add(new SqlParameter("@orgId", orgId));

            return this.QueryWithPage<MrDeptVO>(sql.ToString(), pagination, para == null ? null : para.ToArray());

        }
    }
}
