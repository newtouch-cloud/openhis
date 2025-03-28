using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.OutpatientManage
{
    public class OutBookScheduleService : DmnServiceBase, IOutBookScheduleService
    {
        public OutBookScheduleService(IDefaultDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {
        }

        //分页获取Schedule列表
        public IList<OutBookScheduleVO> GetPagintionList(Pagination pagination, string orgId, DateTime time)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            var parlist = new List<SqlParameter>();
            sqlStr.Append(@"select a.*,b.sfxmmc,c.sfxmmc as zlxmmc from  mz_ghpb_schedule  a
			left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] b
			on b.sfxmCode=a.ghlx and b.OrganizeId=a.OrganizeId
			left join NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] c 
			on c.sfxmCode=a.zlxm and c.OrganizeId=a.OrganizeId 
            where 1=1 and zt='1' ");

            sqlStr.Append(" and createTime >= @time ");
            parlist.Add(new SqlParameter("@time", Convert.ToDateTime(time).ToString("yyyy-MM-dd HH:mm:ss")));
            if (!string.IsNullOrEmpty(orgId))
            {
                sqlStr.Append(" AND OrganizeId = @OrganizeId");
                parlist.Add(new SqlParameter("@OrganizeId", orgId));
            }
            var list = this.QueryWithPage<OutBookScheduleVO>(sqlStr.ToString(), pagination, parlist.ToArray()).ToList();
            return list;
        }

    }
}
