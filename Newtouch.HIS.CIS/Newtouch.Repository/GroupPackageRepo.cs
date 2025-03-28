using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupPackageRepo : RepositoryBase<GroupPackageEntity>, IGroupPackageRepo
    {
        public GroupPackageRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 组套列表
        /// </summary>
        /// <returns></returns>
        public List<GroupPackageEntity> GetList(int type, string orgId)
        {
            var list = this.IQueryable().Where(a => a.OrganizeId == orgId && a.Type == type).ToList();
            return list;
        }
        public List<GroupPackageEntity> GetList(int type, string orgId, string keyword)
        {
            var list = this.IQueryable().Where(a => a.OrganizeId == orgId && a.Type == type && a.ztmc.Contains(keyword)).ToList();
            return list;
        }
    }
}
