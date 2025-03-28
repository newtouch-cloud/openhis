using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class DaySettleRepo : RepositoryBase<DaySettleEntity>, IDaySettleRepo
    {
        public DaySettleRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 出院结算时检查是否有未完成且未终止的执行计划
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        public void CheckAccountingPlanDetailStatus(string zyh,string orgId)
        {
            if(string.IsNullOrEmpty(orgId))
            {
                throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
            }
            var list = this.IQueryable().Where(a => a.Id == zyh && a.OrganizeId == orgId && a.zt == "1").ToList();
            if (list != null && list.Count > 0)
            {
                throw new FailedCodeException("HOPS_THERE_IS_AN_UNSETTLED_ACCOUNTING_PLAN");
            }
        }
    }
}
