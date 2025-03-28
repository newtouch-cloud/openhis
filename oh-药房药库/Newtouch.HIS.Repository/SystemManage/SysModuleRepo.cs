using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysModuleRepo : RepositoryBase<SysModuleEntity>, ISysModuleRepo
    {
        public SysModuleRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        public IList<SysModuleEntity> GetValidList()
        {
            return this.IQueryable().Where(p => p.zt == "1").ToList();
        }

    }
}
