using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Infrastructure;

namespace FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// DB上下文Factory
    /// </summary>
    public sealed class DefaultDatabaseFactory : DatabaseFactory<DefaultDbContext>, IDefaultDatabaseFactory
    {

    }
}
