using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 门诊药品操作记录
    /// </summary>
    public class MzCfypczjlRepo : RepositoryBase<MzCfypczjlEntity>, IMzCfypczjlRepo
    {
        public MzCfypczjlRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
