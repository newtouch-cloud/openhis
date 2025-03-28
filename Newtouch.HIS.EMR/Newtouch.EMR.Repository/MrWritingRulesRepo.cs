using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Repository
{
    public class MrWritingRulesRepo : RepositoryBase<MrWritingRulesEntity>, IMrWritingRulesRepo
    {
        public MrWritingRulesRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public void DeleteForm(string Id, string orgId)
        {
            this.Delete(a => a.Id == Id && a.OrganizeId == orgId);
        }
    }
}
