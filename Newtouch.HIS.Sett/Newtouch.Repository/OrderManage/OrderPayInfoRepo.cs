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
    public class OrderPayInfoRepo : RepositoryBase<OrderPayInfoEntity>, IOrderPayInfoRepo
    {
        public OrderPayInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void SubmitInfo(OrderPayInfoEntity entity, string keyValue = null)
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

        public OrderPayInfoEntity GetSuccessRecordByOutTradeNo(string outTradeNo)
        {
            var success = (int)EnumPayStatus.Success;
            return this.IQueryable().Where(p => p.OutTradeNo == outTradeNo && p.PayStatus == success).FirstOrDefault();
        }

    }
}
