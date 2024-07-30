using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderRefundInfoRepo : IRepositoryBase<OrderRefundInfoEntity>
    {
        void SubmitInfo(OrderRefundInfoEntity entity, string keyValue);

        /// <summary>
        /// 获取已退金额（包括未知状态的）
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
        decimal GetRefundedSumByOutTradeNo(string outTradeNo);
    }
}
