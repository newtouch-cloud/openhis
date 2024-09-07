using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// xt_yp_pdxxmx
    /// </summary>
    public interface ISysMedicineInventoryDetailRepo : IRepositoryBase<SysMedicineInventoryDetailEntity>
    {
        /// <summary>
        /// 盘点保存时 变更数量
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        void UpdateSlBySaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList);

        /// <summary>
        /// 变更库存追溯码信息
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        void UpdateZsmBySaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList);
    }
}
