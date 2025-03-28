using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 物资类别
    /// </summary>
    public interface IWzTypeRepo : IRepositoryBase<WzTypeEntity>
    {
        /// <summary>
        /// delete unit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteUnitById(string id);
    }
}
