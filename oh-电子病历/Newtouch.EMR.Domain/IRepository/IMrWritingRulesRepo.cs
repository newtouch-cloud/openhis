using Newtouch.EMR.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.IRepository
{
    public interface IMrWritingRulesRepo : IRepositoryBase<MrWritingRulesEntity>
    {
        void DeleteForm(string Id, string orgId);
    }
}
