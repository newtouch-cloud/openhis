using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 住院结算-医保费用-贵安
    /// </summary>
    public class HospSettlementGAXNHFeeRepo : RepositoryBase<HospSettlementGAXNHFeeEntity>, IHospSettlementGAXNHFeeRepo
    {
        public HospSettlementGAXNHFeeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}