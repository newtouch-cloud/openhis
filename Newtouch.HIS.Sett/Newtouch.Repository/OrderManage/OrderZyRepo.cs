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
    public class OrderZyRepo : RepositoryBase<OrderZyEntity>, IOrderZyRepo
    {
        public OrderZyRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void SubmitInfo(OrderZyEntity entity, string keyValue = null)
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
