using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class OrganizeRepo : RepositoryBase<OrganizeEntity>, IOrganizeRepo
    {
        public OrganizeRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取当前组织下的所有组织
        /// </summary>
        /// <returns></returns>
        public List<OrganizeEntity> GetListByTopOrg(string topOrganizeId)
        {
            return this.IQueryable().Where(p => p.TopOrganizeId == topOrganizeId).ToList();
        }

        /// <summary>
        /// 获取当前组织下的所有组织
        /// </summary>
        /// <returns></returns>
        public List<OrganizeEntity> GetValidListByTopOrg(string topOrganizeId)
        {
            return this.IQueryable().Where(p => p.TopOrganizeId == topOrganizeId && p.zt == "1").ToList();
        }

    }
}
