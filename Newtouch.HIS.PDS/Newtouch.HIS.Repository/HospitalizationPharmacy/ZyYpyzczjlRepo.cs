using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 住院药品医嘱操作记录
    /// </summary>
    public class ZyYpyzczjlRepo : RepositoryBase<ZyYpyzczjlEntity>, IZyYpyzczjlRepo
    {
        public ZyYpyzczjlRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
