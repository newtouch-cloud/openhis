using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 库房物资
    /// </summary>
    public class RelProductWarehouseRepo : RepositoryBase<RelProductWarehouseEntity>, IRelProductWarehouseRepo
    {
        public RelProductWarehouseRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int BatchDelete(List<RelProductWarehouseEntity> list)
        {
            if (list == null || list.Count <= 0) return 0;
            using (var db = new Newtouch.Infrastructure.EF.EFDbTransaction(_databaseFactory).BeginTrans())
            {
                list.ForEach(p =>
                {
                    db.Delete(p);
                });
                return db.Commit();
            }
        }
    }
}
