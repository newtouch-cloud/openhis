using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DmnServiceBase : EFDBBase
    {
        public DmnServiceBase(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}
