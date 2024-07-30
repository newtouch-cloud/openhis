using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderMzRepo : RepositoryBase<OrderMzEntity>, IOrderMzRepo
    {
        public OrderMzRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void SubmitInfo(OrderMzEntity entity, string keyValue = null)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                var ety = FindEntity(keyValue);
                if (ety != null)
                {
                    this.Update(entity);
                }

            }
            else
            {
                entity.Create(true);
                this.Insert(entity);

            }
        } 
    }
}
