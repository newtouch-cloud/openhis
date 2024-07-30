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
    public class SysRoleRepo : RepositoryBase<SysRoleEntity>, ISysRoleRepo
    {
        public SysRoleRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        public IList<SysRoleEntity> GetList()
        {
            var topOrgId = Constants.TopOrganizeId;
            return this.IQueryable(p => p.TopOrganizeId == topOrgId && p.zt == "1").ToList();
        }


    }
}


