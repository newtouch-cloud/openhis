using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.Medicine;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 药品
    /// </summary>
    public interface IMedicineDmnService
    {
        /// <summary>
        /// 查询损益药品list
        /// </summary>
        /// <param name="inputCode">关键字</param>
        /// <returns></returns>
        List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode);

        /// <summary>
        /// 查询药品的kcsl和jj
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="yxq"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        void SelectKcslAndJj(string ypCode, string ph, string pc, DateTime? yxq, out int kcsl, out decimal jj);

        ///// <summary>
        ///// 表更库存数量
        ///// </summary>
        ///// <param name="ypCode"></param>
        ///// <param name="ph"></param>
        ///// <param name="pc"></param>
        ///// <param name="yxq"></param>
        //void UpdateStockQuantity(string ypCode, string pc, int sysl);

        #region 报损报益

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        void SaveReportLossAndProfit(SysMedicineProfitLossEntity entity, int kcsl, decimal jj);

        /// <summary>
        /// 报损报溢查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        IList<LossAndProditInfoVO> SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk);

        /// <summary>
        /// 批发价总金额、零售价总金额查询
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="syyy">损益原因</param>
        /// <param name="inputCode">关键字</param>
        /// <param name="syqk">损益情况</param>
        /// <returns></returns>
        LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk);

        /// <summary>
        /// 报损报溢 保存
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        string SaveReportLossAndProfit(List<YpSyxxVo> profitLossEntityList);

        #endregion

        #region 调价盈亏
        /// <summary>
        /// 调价盈亏查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="Srm"></param>
        /// <param name="Yfykmc"></param>
        /// <param name="Lkc"></param>
        /// <returns></returns>
        IList<SysMedicinePriceAdjustmentProfitLossVO> SelectPriceAdjustmentProfitLossList(Pagination pagination, DateTime? startTime, DateTime? endTime, string Srm, string yfbmCode, bool Lkc, string allUseableYfbmCodes = null);

        #endregion
        /// <summary>
        /// 报损报溢查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        IList<InventoryQureyinfoVO> GetInventoryQureyInfo(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk);

        #region 药品发药统计
        //药品分类查询
        IList<MedicineClassificationVO> GetMedicineClassificationList(string keyword = null);
        #endregion
    }
}