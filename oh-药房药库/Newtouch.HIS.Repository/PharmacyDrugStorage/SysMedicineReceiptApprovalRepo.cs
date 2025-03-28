using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_djsh
    /// </summary>
    public class SysMedicineReceiptApprovalRepo : RepositoryBase<SysMedicineReceiptApprovalEntity>, ISysMedicineReceiptApprovalRepo
    {
        public SysMedicineReceiptApprovalRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
