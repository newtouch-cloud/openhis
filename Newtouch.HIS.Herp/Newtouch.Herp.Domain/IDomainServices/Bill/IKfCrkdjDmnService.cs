using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// 出入库单据
    /// </summary>
    public interface IKfCrkdjDmnService
    {
        /// <summary>
        /// 获取出入库单据主表信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="alldjlx">该用户所能拥有的所有单据类型</param>
        /// <param name="warehouseId">当前库房</param>
        /// <param name="ope">操作  query：查询  approval：审核</param>
        /// <returns></returns>
        IList<VCrkdjEntity> GetCrkdjMainList(Pagination pagination, CrkdjSearchParamDTO param, string[] alldjlx, string warehouseId, string ope = "");

        /// <summary>
        /// 获取出入库明细
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCrkdjmxEntity> GetCrkdjmxList(string crkId, string organizeId);

        /// <summary>
        /// 获取供应商和发票信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VFphAndGysEntity> GetFphAndGysInfo(string keyWord, string warehouseId, string organizeId);


        /// <summary>
        /// 删除单据主表和明细表 慎用
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int DeleteDjById(long crkId, string organizeId);

        /// <summary>
        /// 保存单据
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mxList"></param>
        /// <returns></returns>
        string SaveDj(KfCrkdjEntity dj, List<KfCrkmxEntity> mxList);

        /// <summary>
        /// 取消已成功插入明细
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="successList"></param>
        void CancelCrkmx(KfCrkdjEntity dj, List<KfCrkmxEntity> successList);

        /// <summary>
        /// 查询入库配送单号
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="warehouseId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeid"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        List<VInStorageDeliveryInfoEntity> SelectInStorageDeliveryInfo(string keyword, string warehouseId, string userCode, string organizeid, string shzt = "4");

        /// <summary>
        /// 通过配送单号获取出入库明细
        /// </summary>
        /// <param name="deliveryNo"></param>
        /// <param name="djh"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeid"></param>
        /// <param name="gysId"></param>
        /// <returns></returns>
        List<VCrkdjmxInfoEntity> SelectCrkmxByDeliveryNo(string deliveryNo, string djh, string warehouseId, string organizeid, string gysId = "");

        /// <summary>
        /// 删除指定出入库单据和明细
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        string DeleteCrkdj(long crkId);
    }
}