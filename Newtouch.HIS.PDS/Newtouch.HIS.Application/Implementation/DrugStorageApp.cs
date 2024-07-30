using System;
using System.Collections;
using System.Collections.Generic;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.DomainServices.DrugStorage;
using Newtouch.PDS.Requset;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.DomainServices;
using Newtouch.PDS.Requset.Stock;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 库存应用
    /// </summary>
    public class DrugStorageApp : AppBase, IDrugStorageApp
    {
        private readonly ISysMedicineRepo _sysMedicineRepo;
        private readonly IDrugStorageDmnService _drugStorageDmnService;
        private readonly ISysMedicinePriceAdjustmentRepo _sysMedicinePriceAdjustmentRepo;
        private readonly IPharmacyDrugStorageDmnService _pharmacyDrugStorageDmnService;
        private readonly ISysMedicineStockCarryDownDmnService _sysMedicineStockCarryDownDmnService;

        /// <summary>
        /// 查询当前部门药品(入库)
        /// </summary>
        /// <param name="keyword">ypmc、py、spm、ypCode</param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        public List<DepartmentMedicineVO> SelectDepartmentMedicineList(string keyword, string yfbmCode, string organizeId)
        {
            return _drugStorageDmnService.SelectDepartmentMedicineList(keyword, yfbmCode, organizeId);
        }

        /// <summary>
        /// 查询当前部门药品(出库)
        /// </summary>
        /// <param name="keyword">ypmc、py、spm、ypCode</param>
        /// <param name="fph"></param>
        /// <param name="gyscode"></param>
        /// <returns></returns>
        public List<DepartmentMedicineVO> SelectDepartmentMedicineList2(string keyword, string fph, string gyscode)
        {
            return _drugStorageDmnService.SelectDepartmentMedicineList2(keyword, fph, gyscode);
        }

        /// <summary>
        /// 药品发票信息
        /// </summary>
        /// <param name="fph">fph</param>
        /// <returns></returns>
        public List<MedicineInvoiceInfoVO> SelectMedicineListByFPH(string fph)
        {
            return _drugStorageDmnService.SelectMedicineListByFPH(fph);
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="ioReceiptEntity"></param>
        /// <param name="ioReceiptDetailList"></param>
        public void SaveInStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity, List<SysMedicineStorageIOReceiptDetailEntity> ioReceiptDetailList)
        {
            _drugStorageDmnService.SaveInStorageInfo(ioReceiptEntity, ioReceiptDetailList);
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="ioReceiptEntity"></param>
        /// <param name="ioReceiptDetailList"></param>
        public void SaveOutStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity, List<SysMedicineStorageIOReceiptDetailVO> ioReceiptDetailList)
        {
            _drugStorageDmnService.SaveOutStorageInfo(ioReceiptEntity, ioReceiptDetailList);
        }

        #region 药品调价
        /// <summary>
        /// 调价申请 查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        public IList<AdjustPriceMedicineInfoVO> SelectAdjustPriceMedicineInfoList(Pagination pagination, string inputCode)
        {
            return _drugStorageDmnService.SelectAdjustPriceMedicineInfoList(pagination, inputCode);
        }

        /// <summary>
        /// 调价审核查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public IList<AdjustPriceMedicineInfoVO> SelectMedicineAdjustPriceApprovalInfoList(Pagination pagination, string inputCode, string shzt)
        {
            return _drugStorageDmnService.SelectMedicineAdjustPriceApprovalInfoList(pagination, inputCode, shzt);
        }

        /// <summary>
        /// 药品调价执行
        /// </summary>
        /// <param name="ypCodeList"></param>
        public string ExecteAdjustPrice(ArrayList ypCodeList)
        {
            var result = _sysMedicinePriceAdjustmentRepo.CheckStatus(ypCodeList);//检查是否有未处理的数据
            return string.IsNullOrEmpty(result) ? _drugStorageDmnService.ExecteAdjustPrice(ypCodeList) : result;
        }

        /// <summary>
        /// 调价历史查询
        /// </summary>
        /// <param name="inputCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public IList<AdjustPriceHistoryInfoVO> SelectMedicineAdjustPriceHistoryInfoList(Pagination pagination, string inputCode, string startTime, string endTime)
        {
            return _drugStorageDmnService.SelectMedicineAdjustPriceHistoryInfoList(pagination, inputCode, startTime, endTime);
        }
        #endregion

        #region 结转

        /// <summary>
        /// 结转
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        public void CarryOverMedicine(string yfbmCode, string organizeId)
        {
            //var needCarryOverMedicineList = _pharmacyDrugStorageDmnService.SelectNeedCarryOverMedicineList();//先查询需要结转的药品信息
            //if (needCarryOverMedicineList.Count > 0)
            //{
            //    var result = _sysMedicineStockCarryDownDmnService.CarryOverMedicine(needCarryOverMedicineList, yfbmCode, organizeId, DateTime.Now, UserIdentity.UserCode);
            //    if (!string.IsNullOrWhiteSpace(result)) throw new FailedException(result);
            //}
            //else
            //{
            //    throw new FailedCodeException("OUT_OF_STOCK");
            //}
            var result = _sysMedicineStockCarryDownDmnService.CarryOverMedicine(yfbmCode, organizeId, DateTime.Now, UserIdentity.UserCode);
            if (!string.IsNullOrWhiteSpace(result)) throw new FailedException(result);
        }
        #endregion

        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="yplist"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public StockQueryResponseDTO StockQuery(List<DrugInfo> yplist, string organizeId)
        {
            if (yplist == null || yplist.Count == 0) return null;
            var result = new StockQueryResponseDTO
            {
                drugStockInfos = new List<DrugStockInfo>()
            };
            for(int i = 0; i < yplist.Count; i ++)
            {
                DrugInfo p = yplist[i];
                if (string.IsNullOrEmpty(p.lyyf))
                {
                    p.lyyf = "";
                }
                result.drugStockInfos.AddRange(_drugStorageDmnService.SelectStock(p.ypCode, p.lyyf, organizeId));
            }
            /*
            yplist.ForEach(p => { result.drugStockInfos.AddRange(_drugStorageDmnService.SelectStock(p.ypCode, p.lyyf, organizeId)); });
            */
            return result;
        }
        public string PrepareMedicine(BYDjInfoDTO yplist, string organizeId,string yhgh)
        {
            var excResult = new DispensingDmnService(new DefaultDatabaseFactory(), false)
                                  .PrepareMedicine(yplist, organizeId,yhgh);
            return excResult;
        }
public string PrepareMedicineReturn (BythDjInfoDTO yplist, string organizeId, string yhgh)
        {
            var excResult = new DispensingDmnService(new DefaultDatabaseFactory(), false)
                                  .PrepareMedicineReturn(yplist, organizeId, yhgh);
            return excResult;
        }
        public string WithdrawalPreparation(string Djh, string organizeId, string yhgh,string shzt)
        {
            var excResult = new DispensingDmnService(new DefaultDatabaseFactory(), false)
                                  .WithdrawalPreparation(Djh, organizeId, yhgh,shzt);
            return excResult;
        }

        /// <summary>
        /// 提交报损报溢
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        public string SubmitReportLossAndProfit(SysMedicineProfitLossEntity[] syxx)
        {
            if (syxx == null || syxx.Length <= 0) return "损益明细不能为空";
            var errorMsg = "";
            var cts = new CancellationTokenSource();
            try
            {
                var parent = new Task(() =>
                {
                    var taskFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                    foreach (var item in syxx)
                    {
                        taskFactory.StartNew(() =>
                        {
                            if (cts.IsCancellationRequested)
                            {
                                return false;
                            }

                            var tmpResult = new SysMedicineStockInfoDmnService(new DefaultDatabaseFactory(), false).SubmitReportLossAndProfit(item);
                            if (string.IsNullOrWhiteSpace(tmpResult)) return true;
                            cts.Cancel();
                            errorMsg = tmpResult;
                            return false;

                        }, cts.Token);
                    }
                });
                parent.Start();
                parent.Wait();
                if (cts.IsCancellationRequested || !string.IsNullOrWhiteSpace(errorMsg))
                {
                    throw new Exception(errorMsg);
                }

                return "";
            }
            catch (Exception e)
            {
                return e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }
        }

        public string WithdrawalPreparationReturn(string Djh, string organizeId, string yhgh, string thzt)
        {
            var excResult = new DispensingDmnService(new DefaultDatabaseFactory(), false)
                                  .WithdrawalPreparationReturn(Djh, organizeId, yhgh, thzt);
            return excResult;
        }
    }
}
