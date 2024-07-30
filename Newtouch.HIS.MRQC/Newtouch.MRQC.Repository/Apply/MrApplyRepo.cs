using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.MRQC.Domain.Entity.QcItemManage;
using Newtouch.MRQC.Domain.IRepository.Apply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Repository.Apply
{
    public class MrApplyRepo : RepositoryBase<MrApplyEntity>, IMrApplyRepo
    {
        public MrApplyRepo(IDefaultDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {
        }
    }
}
