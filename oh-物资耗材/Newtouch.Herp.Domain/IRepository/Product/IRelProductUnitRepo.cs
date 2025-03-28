using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 库存单位关联
    /// </summary>
    public interface IRelProductUnitRepo : IRepositoryBase<RelProductUnitEntity>
    {
        /// <summary>
        /// delete RelProductUnitEntity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteById(string id);
    }
}