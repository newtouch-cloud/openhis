using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.IRepository;
using System.Collections.Generic;

namespace Newtouch.Repository
{
    public class SysMedicalOrderFrequencyRepo : RepositoryBase<SysMedicalOrderFrequencyVEntity>, ISysMedicalOrderFrequencyRepo
    {
        public SysMedicalOrderFrequencyRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
}
