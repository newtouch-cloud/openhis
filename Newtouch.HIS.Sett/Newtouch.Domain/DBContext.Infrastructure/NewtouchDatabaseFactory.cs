using Newtouch.Infrastructure;

namespace Newtouch.HIS.Domain.DBContext.Infrastructure
{
    /// <summary>
    /// NewoutchHIS库DB上下文Factory
    /// </summary>
    public class NewtouchDatabaseFactory : DatabaseFactory<NewtouchDbContext>, INewtouchDatabaseFactory
    {

    }
}
