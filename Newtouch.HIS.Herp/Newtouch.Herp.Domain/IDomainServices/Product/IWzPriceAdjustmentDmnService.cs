using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// 调价
    /// </summary>
    public interface IWzPriceAdjustmentDmnService
    {
        /// <summary>
        /// 获取调价信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="shzt"></param>
        /// <param name="organizeId"></param>
        /// <param name="zxbz"></param>
        /// <returns></returns>
        IList<VPriceAdjustmentInfoEntity> GetPriceAdjustmentList(Pagination pagination, string keyWord, string shzt, string organizeId, string zxbz = "");

        /// <summary>
        /// 审核调价
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="operationType"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string Approval(List<string> idList, string operationType, string userCode, string organizeId);

        /// <summary>
        /// 获取调价损益信息
        /// </summary>
        /// <param name="wztjId"></param>
        /// <param name="productId"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        List<VPriceAdjustmentProfitLossEntity> GetPriceAdjustmentProfitLoss(string wztjId, string productId, string organizeid);

        /// <summary>
        /// 获取调价损益信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IList<VPriceAdjustmentProfitLossEntity> GetPriceAdjustmentProfitLoss(Pagination pagination, PriceAdjustmentProfitLossDTO param);

        /// <summary>
        /// 调价执行
        /// </summary>
        /// <param name="tjInfo"></param>
        /// <returns></returns>
        string AdjustPriceExecute(WzPriceAdjustmentEntity tjInfo);

        /// <summary>
        /// 获取调价历史信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeId"></param>
        /// <param name="bt">开始时间</param>
        /// <param name="et">结束时间</param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        IList<VPriceAdjustmentInfoEntity> GetPriceAdjustmentHistoryList(Pagination pagination, string keyWord, string organizeId, DateTime bt, DateTime et, string warehouseId);
    }
}