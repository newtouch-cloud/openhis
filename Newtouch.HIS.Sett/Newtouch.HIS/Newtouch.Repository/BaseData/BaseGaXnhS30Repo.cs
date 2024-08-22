using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.BaseData;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 新农合目录下载
    /// </summary>
    public class BaseGaXnhS30Repo : RepositoryBase<BaseGaXnhS30Entity>, IBaseGaXnhS30Repo
    {
        public BaseGaXnhS30Repo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}