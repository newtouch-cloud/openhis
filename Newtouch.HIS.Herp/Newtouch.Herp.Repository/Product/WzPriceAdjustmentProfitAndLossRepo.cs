using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 物资调价损益
    /// </summary>
    public class WzPriceAdjustmentProfitAndLossRepo : RepositoryBase<WzPriceAdjustmentProfitAndLossEntity>, IWzPriceAdjustmentProfitAndLossRepo
    {
        public WzPriceAdjustmentProfitAndLossRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
