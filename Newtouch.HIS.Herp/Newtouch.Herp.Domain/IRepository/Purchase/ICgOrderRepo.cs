using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 采购单
    /// </summary>
    public interface ICgOrderRepo : IRepositoryBase<CgOrderEntity>
    {

        /// <summary>
        /// 获取采购单信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        CgOrderEntity SelectData(string orderNo, string organizeId);

        /// <summary>
        /// 审核采购单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orderType"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        int AuditPurchasingOrder(string orderNo, int orderType, string userCode,string organizeId, string remark);


    }
}