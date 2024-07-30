using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderRefundInfoRepo : RepositoryBase<OrderRefundInfoEntity>, IOrderRefundInfoRepo
    {
        public OrderRefundInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void SubmitInfo(OrderRefundInfoEntity entity, string keyValue)
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

        /// <summary>
        /// 获取已退金额（包括未知状态的）
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
        public decimal GetRefundedSumByOutTradeNo(string outTradeNo)
        {
            var success = (int)EnumRefundStatus.Success;
            var unknowm = (int)EnumRefundStatus.UnKnown;
            return this.IQueryable().Where(p => p.OutTradeNo == outTradeNo && (p.RefundStatus == success || p.RefundStatus == unknowm)).Select(p => p.Amount).ToList().Sum();
        }

    }
}
