using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderInfoRepo : RepositoryBase<OrderInfoEntity>, IOrderInfoRepo
    {
        public OrderInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void SubmitInfo(OrderInfoEntity entity, string keyValue = null)
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
