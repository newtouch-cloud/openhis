using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Core.Common.Interface;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysCashPaymentModelRepo : RepositoryBase<SysCashPaymentModelEntity>, ISysCashPaymentModelRepo
    {
        private readonly ICache _cache;

        public SysCashPaymentModelRepo(IDefaultDatabaseFactory databaseFactory, ICache cache)
            : base(databaseFactory)
        {
            this._cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<SysCashPaymentModelEntity> GetLazyList()
        {
            return _cache.Get<IList<SysCashPaymentModelEntity>>(Infrastructure.CacheKey.ValidXTXJZFFSListSetKey, () =>
            {
                return this.IQueryable().Where(p => p.zt == "1").ToList();
            });
        }

    }
}
