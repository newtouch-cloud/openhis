using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface IAccessAuthRepo
    {
        IList<AccessAuthEntity> FindList(Pagination pagination, string keyword, string orgId);
        IList<AccessAuthEntity> FindList(string code, string orgId);

        AccessAuthEntity FindEntity(string keyValue);
        AccessAuthEntity FindEntity(string keyValue, string valid);
        void RegistProcess(AccessAuthEntity entity,string keyValue);
    }
}
