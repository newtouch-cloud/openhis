using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DO;

namespace Newtouch.HIS.Application.Interface
{
    public interface IMedicineApp
    {
        /// <summary>
        /// 根据损益类型获取损益原因
        /// </summary>
        /// <param name="sylx"></param>
        /// <returns></returns>
        List<SysMedicineProfitLossReasonEntity> GetLossProfitReasonListByType(string sylx);

        /// <summary>
        /// 查询损益药品list
        /// </summary>
        /// <param name="inputCode">关键字</param>
        /// <returns></returns>
        List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode);

        /// <summary>
        /// 报损报溢 保存
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        void SaveReportLossAndProfit(List<SysMedicineProfitLossEntity> profitLossEntityList);

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
        /// 获取住院月处方发药次
        /// </summary>
        /// <returns></returns>
        MonthlySummaryDO GetFyCountVoByYfbm(List<FyCountVoByYfbm> val);
        
        /// <summary>
        /// 获取今年医嘱次|处方药品次
        /// </summary>
        /// <returns></returns>
        List<FyClassificationStatistics> GetFyCountBydl();
        /// <summary>
        /// 盘点明细查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        IList<InventoryQureyinfoVO> InventoryQureyInfo(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk);
    }
}
