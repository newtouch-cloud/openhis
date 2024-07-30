using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.V;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 结转
    /// </summary>
    public interface ISysMedicineStockCarryDownRepo : IRepositoryBase<SysMedicineStockCarryDownEntity>
    {
        /// <summary>
        /// 获取历史结转时间
        /// </summary>
        /// <returns></returns>
        List<KeyValueVEntity> GetlsjzDateTime(string yfbmCode, string organizeId);

        /// <summary>
        /// 获取最近一次结转信息
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        SysMedicineStockCarryDownEntity GetLastJzData(string yfbmCode, string organizeId);
    }
}
