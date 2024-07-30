using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 申领单
    /// </summary>
    public interface IKfApplyOrderRepo : IRepositoryBase<KfApplyOrderEntity>
    {

        /// <summary>
        /// 查询申领单住信息
        /// </summary>
        /// <param name="sldh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        KfApplyOrderEntity SelectData(string sldh, string organizeId);
    }
}