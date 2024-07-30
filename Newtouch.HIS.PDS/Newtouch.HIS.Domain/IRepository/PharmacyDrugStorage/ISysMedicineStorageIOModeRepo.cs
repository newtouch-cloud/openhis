using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineStorageIOModeRepo : IRepositoryBase<SysMedicineStorageIOModeEntity>
    {
        /// <summary>
        /// 获取出入库方式
        /// </summary>
        /// <returns></returns>
        object GetStorageIOMode();
    }
}
