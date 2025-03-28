using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{

    /// <summary>
    /// xt_yp_pdxx
    /// </summary>
    public interface ISysMedicineInventoryRepo : IRepositoryBase<SysMedicineInventoryEntity>
    {

        /// <summary>
        /// 判断是否有未完成的盘点  （开始）
        /// </summary>
        /// <returns></returns>
        int SelectUnfinishedInventory();

        /// <summary>
        /// 判断是否有未完成的盘点  （结束）
        /// </summary>
        /// <returns></returns>
        int SelectUnfinishedInventoryByPdId(string pdId);

        /// <summary>
        /// 根据盘点ID获取盘点信息
        /// </summary>
        /// <param name="pdId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        SysMedicineInventoryEntity SelectPdInfByPdId(string pdId, string organizeId);
    }


}
