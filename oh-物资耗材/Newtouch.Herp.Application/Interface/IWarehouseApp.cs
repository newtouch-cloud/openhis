using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// ���ʲ���
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
        /// ͬ���ⷿ����
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="opereateType"></param>
        /// <param name="organizeId"></param>
        /// <param name="warehouseId">Ŀ��ⷿ</param>
        /// <returns></returns>
        bool FreshWhAndwzRelList(List<string> productIds, int opereateType, string organizeId, string warehouseId);
    }
}