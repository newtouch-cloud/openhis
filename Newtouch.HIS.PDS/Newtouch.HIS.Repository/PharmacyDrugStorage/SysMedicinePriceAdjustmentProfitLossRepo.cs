using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_tjsy
    /// </summary>
    public class SysMedicinePriceAdjustmentProfitLossRepo : RepositoryBase<SysMedicinePriceAdjustmentProfitLossEntity>, ISysMedicinePriceAdjustmentProfitLossRepo
    {
        public SysMedicinePriceAdjustmentProfitLossRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
