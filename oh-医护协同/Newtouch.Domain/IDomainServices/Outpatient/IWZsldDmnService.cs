using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.Domain.DTO;
using Newtouch.Domain.ValueObjects;


namespace Newtouch.Domain.IDomainServices
{
    public interface IWZsldDmnService
    {

        /// <summary>
        /// 下拉列表物资信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<VSelProductInfoVO> DepartmentStockListQuery(DepartmentStockListQueryParamDTO param);

        /// <summary>
        /// 获取物资批号批次信息  top 20
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="gysId"></param>
        /// <param name="deliveryNo"></param>
        /// <returns></returns>
        List<VProductBatchInfoVO> ProductBatchQuery(string productId, string warehouseId, string organizeId,  string keyword = "");

        List<RelWarehouseVO> GetList(string organizeId, string keyword);
        List<RelWarehouseVO> GetDeptList(string organizeId, string keyword);

    }
}
