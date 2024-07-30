using Newtouch.Infrastructure;

namespace Newtouch.HIS.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// DB上下文Factory NewtouchHIS_Base
    /// </summary>
    public sealed class BaseDatabaseFactory : DatabaseFactory<BaseDbContext>, IBaseDatabaseFactory
    {
    }
}