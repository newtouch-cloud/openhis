using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.ValueObjects;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// 物资操作
    /// </summary>
    public interface IWzProductDmnService
    {
        /// <summary>
        /// get product list
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        IList<VWzProductEntity> GetList(Pagination pagination, string organizeId, string lb, string zt, string keyWord);

        /// <summary>
        /// 根据ID删除物资信息
        /// </summary>
        /// <param name="id"></param>
        void DeleteProduct(string id);

        /// <summary>
        /// 根据物资单位关联关系ID获取物资和单位信息
        /// </summary>
        /// <param name="relId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VProductUnitEntity> GetProductAndUnitByProId(string relId, string organizeId);

        /// <summary>
        /// 获取本部门物资类别
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        List<VBmwzlbEntity> GetBmWzLb(string warehouseId, string organizeId, string zt = "1");

        /// <summary>
        /// 修改物资价格
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lsj"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        int UpdateProductPrice(string id, decimal lsj, string userCode, string organizeId, string zt = "1");

        /// <summary>
        /// 获取物资信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        VProductInfoEntity GetProductInfo(string id, string organizeId);

        /// <summary>
        /// 获取待处理事件总数
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        NeedDealCountVO GetNeedDealCountByZkf(string warehouseId, string organizeId);

        /// <summary>
        /// 获取待处理事件总数
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        NeedDealCountVO GetNeedDealCountByKskf(string warehouseId, string organizeId);

        /// <summary>
        /// 获取物资代码
        /// </summary>
        /// <returns></returns>
        string GetProductCode();

        /// <summary>
        /// 检查物资代码是否已存在
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        string CheckProductCode(string productCode, string keyValue);

        /// <summary>
        /// 插入物资数据并同步到收费项目中
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int InsertProductToSfxm(WzProductEntity entity);


		int InsertTOSfxm(string orgid, string wzcode);

        /// <summary>
        /// 更新物资数据并同步到收费项目中
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int updateProductToSfxm(WzProductEntity entity);

        /// <summary>
        /// 获取产品明细
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <param name="topCount">默认 top 500</param>
        /// <returns></returns>
        List<VProductInfoDo> SelectProductDetail(string keyword, string organizeId, int topCount = 500);

        /// <summary>
        /// 查询物资所有单位
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<ProductUnit> SelectProductUnits(string productId, string organizeId);
    }
}