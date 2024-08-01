using FrameworkBase.Infrastructure;
using Newtouch.Infrastructure;

namespace FrameworkBase.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// DB上下文Factory
    /// </summary>
    public sealed class DefaultDatabaseFactory : DatabaseFactory<DefaultDbContext>, IDefaultDatabaseFactory
    {

    }
}
