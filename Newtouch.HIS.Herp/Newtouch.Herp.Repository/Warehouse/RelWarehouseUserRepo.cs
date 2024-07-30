using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 库房人员
    /// </summary>
    public class RelWarehouseUserRepo : RepositoryBase<RelWarehouseUserEntity>, IRelWarehouseUserRepo
    {
        public RelWarehouseUserRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// get RelWarehouseUserEntity list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public List<RelWarehouseUserEntity> GetListById(string id, string organizeId, string zt = "1")
        {
            return IQueryable(p => p.Id == id && p.OrganizeId == organizeId && p.zt == zt).ToList();
        }

        /// <summary>
        /// get RelWarehouseUserEntity list
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public List<RelWarehouseUserEntity> GetListByWarehouseId(string warehouseId, string organizeId, string zt = "1")
        {
            return IQueryable(p => p.warehouseId == warehouseId && p.OrganizeId == organizeId && p.zt == zt).ToList();
        }
    }
}
