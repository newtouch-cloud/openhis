using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_ls_syxx
    /// </summary>
    public class SysMedicineTemporaryProfitLossRepo : RepositoryBase<SysMedicineTemporaryProfitLossEntity>, ISysMedicineTemporaryProfitLossRepo
    {
        public SysMedicineTemporaryProfitLossRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
