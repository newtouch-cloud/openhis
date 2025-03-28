using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Herp.Domain.Entity.Product;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 物资收费项目对照表
    /// </summary>
    public class RelProductAndsfxmRepo : RepositoryBase<RelProductAndsfxmEntity>, IRelProductAndsfxmRepo
    {
        public RelProductAndsfxmRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
