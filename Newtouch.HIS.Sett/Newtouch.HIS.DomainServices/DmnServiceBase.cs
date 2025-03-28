using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Newtouch.HIS.DomainServices
{
    public abstract class DmnServiceBase : EFDBBase
    {
        public DmnServiceBase(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}
