using System.Collections.Generic;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// 库存明细
    /// </summary>
    public interface IKcPdxxmxRepo : IRepositoryBase<KcPdxxmxEntity>
    {
        /// <summary>
        /// 盘点保存时 变更数量
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        void UpdateSlBySaveInventoryInfo(List<SaveInventoryDTO> inventoryInfoList);
    }
}