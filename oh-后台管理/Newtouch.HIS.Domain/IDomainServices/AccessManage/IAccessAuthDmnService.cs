using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IAccessAuthDmnService
    {
        bool Authorized(AccessIdentity ident, out string message);
    }
}
