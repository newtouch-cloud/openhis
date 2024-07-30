using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 采购计划
    /// </summary>
    public interface ICgPurchaseOrderRepo : IRepositoryBase<CgPurchaseOrderEntity>
    {
        /// <summary>
        /// 撤销采购计划
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int CancelPurchasingPlan(string cgdh, string organizeId, string userCode);

        /// <summary>
        /// 查询采购计划
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        CgPurchaseOrderEntity SelectData(string cgdh, string organizeId);
    }
}