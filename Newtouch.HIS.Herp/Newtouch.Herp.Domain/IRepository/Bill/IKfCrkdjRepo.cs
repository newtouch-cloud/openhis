using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 出入库单据操作
    /// </summary>
    public interface IKfCrkdjRepo : IRepositoryBase<KfCrkdjEntity>
    {
        /// <summary>
        /// 打回申请 从待处理变成暂存，只针对外部入库
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        string BackToTemporary(long crkId);
    }
}