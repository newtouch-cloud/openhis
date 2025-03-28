using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderPayInfoRepo : IRepositoryBase<OrderPayInfoEntity>
    {
        void SubmitInfo(OrderPayInfoEntity entity, string keyValue = null);

        OrderPayInfoEntity GetSuccessRecordByOutTradeNo(string outTradeNo);

    }
}
