using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 出入库单据明细
    /// </summary>
    public interface IKfCrkmxRepo : IRepositoryBase<KfCrkmxEntity>
    {
        /// <summary>
        /// 修改发票号
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        int UpdateFph(long crkmxId, string fph);
    }
}