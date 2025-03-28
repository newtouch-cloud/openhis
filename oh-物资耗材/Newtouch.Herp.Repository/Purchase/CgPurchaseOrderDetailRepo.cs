using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 采购计划明细
    /// </summary>
    public class CgPurchaseOrderDetailRepo : RepositoryBase<CgPurchaseOrderDetailEntity>, ICgPurchaseOrderDetailRepo
    {
        public CgPurchaseOrderDetailRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
