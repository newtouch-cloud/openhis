
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    public class OutpatientItemRepo : RepositoryBase<OutpatientItemEntity>, IOutpatientItemRepo
    {
        public OutpatientItemRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
       
    }
}
