using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_crkdj
    /// </summary>
    public class SysMedicineStorageIOReceiptRepo : RepositoryBase<SysMedicineStorageIOReceiptEntity>, ISysMedicineStorageIOReceiptRepo
    {
        public SysMedicineStorageIOReceiptRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
