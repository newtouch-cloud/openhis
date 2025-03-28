using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.Herp.DomainServices
{
    /// <summary>
    /// DmnService Base
    /// </summary>
    public abstract class DmnServiceBase : FrameworkBase.MultiOrg.DmnService.DmnServiceBase
    {
        public DmnServiceBase(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
