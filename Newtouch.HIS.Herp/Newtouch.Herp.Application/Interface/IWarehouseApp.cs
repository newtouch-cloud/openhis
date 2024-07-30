using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// 物资操作
    /// </summary>
    public interface IWarehouseApp
    {
        /// <summary>
        /// submit warehouse maintenance form
        /// </summary>
        /// <param name="wzEntity"></param>
        /// <param name="staffghs"></param>
        /// <param name="departmentIds"></param>
        /// <param name="keyWord"></param>
        void SubmitForm(KfWarehouseEntity wzEntity, string[] staffghs, string[] departmentIds, string keyWord);

        /// <summary>
        /// 同步库房物资
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="opereateType"></param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId">目标库房</param>
        /// <returns></returns>
        bool FreshWhAndwzRelList(List<string> productIds, int opereateType, string organizeId, string warehouseId);
    }
}