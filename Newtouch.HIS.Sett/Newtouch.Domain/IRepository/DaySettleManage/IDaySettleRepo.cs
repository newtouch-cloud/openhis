using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDaySettleMxRepo : IRepositoryBase<DaySettleMxEntity>
    {
        /// <summary>
        /// 出院结算时检查是否有未完成且未终止的执行计划
        /// </summary>
        void CheckAccountingPlanDetailStatus(string zyh, string orgId);
    }
}
