using System.Collections;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.Stock;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 库存应用
    /// </summary>
    public interface IDrugStorageApp
    {
        /// <summary>
        /// 查询当前部门药品(入库)
        /// </summary>
        /// <param name="keyword">ypmc、py、spm、ypCode</param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        List<DepartmentMedicineVO> SelectDepartmentMedicineList(string keyword, string yfbmCode, string organizeId);
        string PrepareMedicine(BYDjInfoDTO yplist, string organizeId,string yhgh);
        string PrepareMedicineReturn(BythDjInfoDTO yplist, string organizeId, string yhgh);
        /// <summary>
        /// 药品发票信息
        /// </summary>
        /// <param name="fph">fph</param>
        /// <returns></returns>
        List<MedicineInvoiceInfoVO> SelectMedicineListByFPH(string fph);

        /// <summary>
        /// 查询当前部门药品(出库)
        /// </summary>
        /// <param name="keyword">ypmc、py、spm、ypCode</param>
        /// <param name="fph"></param>
        /// <param name="gyscode"></param>
        /// <returns></returns>
        List<DepartmentMedicineVO> SelectDepartmentMedicineList2(string keyword, string fph, string gyscode);

        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="ioReceiptEntity"></param>
        /// <param name="ioReceiptDetailEntityList"></param>
        void SaveInStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity, List<SysMedicineStorageIOReceiptDetailEntity> ioReceiptDetailEntityList);

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="ioReceiptEntity"></param>
        /// <param name="ioReceiptDetailList"></param>
        void SaveOutStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity, List<SysMedicineStorageIOReceiptDetailVO> ioReceiptDetailList);

        /// <summary>
        /// 调价申请 查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        IList<AdjustPriceMedicineInfoVO> SelectAdjustPriceMedicineInfoList(Pagination pagination, string inputCode);

        /// <summary>
        /// 调价审核查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        IList<AdjustPriceMedicineInfoVO> SelectMedicineAdjustPriceApprovalInfoList(Pagination pagination, string inputCode, string shzt);

        /// <summary>
        /// 药品调价执行
        /// </summary>
        /// <param name="ypCodeList"></param>
        string ExecteAdjustPrice(ArrayList ypCodeList);

        /// <summary>
        /// 调价历史查询
        /// </summary>
        /// <param name="inputCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        IList<AdjustPriceHistoryInfoVO> SelectMedicineAdjustPriceHistoryInfoList(Pagination pagination, string inputCode, string startTime, string endTime);

        /// <summary>
        /// 结转
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        void CarryOverMedicine(string yfbmCode, string organizeId);

        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="yplist">药品信息</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        StockQueryResponseDTO StockQuery(List<DrugInfo> yplist, string organizeId);

        /// <summary>
        /// 提交报损报溢
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        string SubmitReportLossAndProfit(SysMedicineProfitLossEntity[] syxx);
        string WithdrawalPreparation(string Djh, string organizeId, string yhgh,string shzt);
        string WithdrawalPreparationReturn(string Djh, string organizeId, string yhgh, string thzt);
    }
}
