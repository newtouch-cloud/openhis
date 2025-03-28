using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 住院模拟结算-医保费用-贵安
    /// </summary>
    public class HospSimulateSettlementGAYBFeeRepo : RepositoryBase<HospSimulateSettlementGAYBFeeEntity>, IHospSimulateSettlementGAYBFeeRepo
    {
        public HospSimulateSettlementGAYBFeeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}