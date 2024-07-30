using Newtouch.Infrastructure;

namespace Newtouch.DomainServices
{
    public abstract class DmnServiceBase : EFDBBase
    {
        public DmnServiceBase(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}
