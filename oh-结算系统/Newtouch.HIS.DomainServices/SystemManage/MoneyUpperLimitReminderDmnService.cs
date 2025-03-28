using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public class MoneyUpperLimitReminderDmnService : DmnServiceBase, IMoneyUpperLimitReminderDmnService
    {
        public MoneyUpperLimitReminderDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 金额上限查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="reminderType"></param>
        /// <returns></returns>
        public IList<MoneyUpperLimitReminderSelectVO> GetAllList(Pagination pagination, string orgId, string keyword, string reminderType, string sxtxId = null)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            var parlist = new List<SqlParameter>();
            sqlStr.Append(@"
SELECT sxtxId,
        reminderType,
        jesxtx.ks AS ksCode,
        Department.Name AS ks,
        jesxtx.ys AS ysGh,
        Staff.Name AS ys,
        jesxtx.jesx,
		jesxtx.CreateTime,
		jesxtx.LastModifyTime,
        jesxtx.zt
FROM xt_jesxtx jesxtx
LEFT JOIN NewtouchHIS_Base..V_S_Sys_Department Department
    ON Department.Code=jesxtx.ks
        AND Department.OrganizeId=jesxtx.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff Staff
    ON Staff.gh=jesxtx.ys
        AND Staff.OrganizeId=jesxtx.OrganizeId
WHERE jesxtx.OrganizeId=@orgId
        
                        ");
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlStr.Append(" AND ( Department.Code like @keyword or Department.Name like @keyword or Staff.gh like @keyword or Staff.Name like @keyword)");
                parlist.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%" ));
            }
            if (!string.IsNullOrEmpty(reminderType))
            {
                sqlStr.Append(" AND jesxtx.reminderType=@reminderType");
                parlist.Add(new SqlParameter("@reminderType", reminderType.Trim()));
            }
            if (!string.IsNullOrEmpty(sxtxId))
            {
                sqlStr.Append(" and jesxtx.sxtxId=@sxtxId");
                parlist.Add(new SqlParameter("@sxtxId", sxtxId.Trim()));
            }
            parlist.Add(new SqlParameter("@orgId", orgId.Trim()));

            var list = this.QueryWithPage<MoneyUpperLimitReminderSelectVO>(sqlStr.ToString(), pagination, parlist.ToArray()).ToList();
            return list;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="gh"></param>
        /// <param name="deptCode"></param>
        /// <param name="jfjesx"></param>
        /// <param name="yjfzje"></param>
        public void GetStaffjfjeInfo(string orgId, string gh, string deptCode, out decimal jfjesx, out decimal yjfzje)
        {
            var pars = new List<SqlParameter>() {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@ksrq", Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"))),
                new SqlParameter("@jsrq", DateTime.Now),
                new SqlParameter("@therapist", gh),
                new SqlParameter("@therapistDeptCode", deptCode),
            };
            var outpar1 = new SqlParameter("@jfjesx", System.Data.SqlDbType.Decimal);
            outpar1.Direction = System.Data.ParameterDirection.Output;
            var outpar2 = new SqlParameter("@yjfzje", System.Data.SqlDbType.Decimal);
            outpar2.Direction = System.Data.ParameterDirection.Output;
            pars.Add(outpar1);
            pars.Add(outpar2);

            this.ExecuteSqlCommand("exec [spTotalAmountByTherapist] @orgId, @ksrq, @jsrq, @therapist, @therapistDeptCode, @jfjesx output, @yjfzje output", pars.ToArray());

            jfjesx = Convert.ToDecimal(outpar1.Value);
            yjfzje = Convert.ToDecimal(outpar2.Value);
        }

    }
}
