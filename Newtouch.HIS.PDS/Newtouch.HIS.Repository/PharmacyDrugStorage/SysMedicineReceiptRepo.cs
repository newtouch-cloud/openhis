using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_dj
    /// </summary>
    public class SysMedicineReceiptRepo : RepositoryBase<SysMedicineReceiptEntity>, ISysMedicineReceiptRepo
    {
        public SysMedicineReceiptRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
