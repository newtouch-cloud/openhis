using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 物资调价
    /// </summary>
    public class WzPriceAdjustmentRepo : RepositoryBase<WzPriceAdjustmentEntity>, IWzPriceAdjustmentRepo
    {
        public WzPriceAdjustmentRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取未执行列表
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<WzPriceAdjustmentEntity> GetUnexecutedList(string productId, string organizeId)
        {
            return IQueryable(p => p.productId == productId
                                   && p.OrganizeId == organizeId
                                   && p.zxbz == ((int)EnumTjZxbz.Not).ToString()
                                   && (p.shzt == ((int)EnumTjShzt.Audited).ToString() || p.shzt == ((int)EnumTjShzt.Waiting).ToString())).ToList();
        }

    }
}
