using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity.SystemManage;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class jgss_EarningDetailInfoRepo : RepositoryBase<jgss_EarningDetailInfoEntity>, Ijgss_EarningDetailInfoRepo
    {
        public jgss_EarningDetailInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}


