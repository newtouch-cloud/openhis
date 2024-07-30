using System.Collections.Generic;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// 推送中药信息
    /// </summary>
    public interface ICmmHis02RecordDmnService
    {

        /// <summary>
        /// 获取未同步的药品
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="dlType"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectNoSyncMedicines(string organizeId, string dlType = "TCM");

        /// <summary>
        /// 获取未同步的药品
        /// </summary>
        /// <param name="topSize">单次请求总条数</param>
        /// <param name="organizeId"></param>
        /// <param name="dlType"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectNoSyncMedicines(int topSize, string organizeId, string dlType = "TCM");

        /// <summary>
        /// 获取同步失败的药品
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SysMedicineExVEntity> SelectSyncFailedMedicines(string organizeId);
    }
}