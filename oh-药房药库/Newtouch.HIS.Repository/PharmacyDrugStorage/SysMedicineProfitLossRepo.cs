using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_syxx
    /// </summary>
    public class SysMedicineProfitLossRepo : RepositoryBase<SysMedicineProfitLossEntity>, ISysMedicineProfitLossRepo
    {
        public SysMedicineProfitLossRepo(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {
        }

    }
}
