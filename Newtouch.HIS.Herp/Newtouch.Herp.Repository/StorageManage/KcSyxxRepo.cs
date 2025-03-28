using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.EF;
using System.Linq;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 损益
    /// </summary>
    public class KcSyxxRepo : RepositoryBase<KcSyxxEntity>, IKcSyxxRepo
    {
        public KcSyxxRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
