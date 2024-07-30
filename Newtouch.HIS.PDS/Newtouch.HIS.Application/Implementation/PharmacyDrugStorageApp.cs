using System;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Application;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class PharmacyDrugStorageApp : AppBase, IPharmacyDrugStorageApp
    {
        private readonly IPharmacyDrugStorageDmnService _pharmacyDrugStorageDmnService;
        private readonly ISysMedicineInventoryRepo _sysMedicineInventoryRepo;
        private readonly ISysMedicineInventoryDetailRepo _sysMedicineInventoryDetailRepo;
        private readonly ISysMedicineFormulationRepo _sysMedicineFormulationRepo;
        private readonly ISysMedicineStockCarryDownRepo _sysMedicineStockCarryDownRepo;

        /// <summary>
        /// 库存量查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="tybz"></param>
        /// <param name="zt"></param>
        /// <param name="noShow0Kc"></param>
        /// <returns></returns>
        public IList<MedicineStockQueryVO> SelectStockTotal(Pagination pagination, string inputCode, string tybz, string zt, bool noShow0Kc)
        {
            return _pharmacyDrugStorageDmnService.SelectStockTotal(pagination, inputCode, tybz, zt, noShow0Kc);
        }

        /// <summary>
        /// 库存量查询明细
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        public IList<MedicineStockQueryDetailVO> SelectStockDetail(string ypCode, string yfbmCode)
        {
            return _pharmacyDrugStorageDmnService.SelectStockDetail(ypCode, yfbmCode);
        }

        /// <summary>
        /// 单据查询明细
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <returns></returns>
        public IList<ReceiptQueryDetailVO> SelectReceipDetailInfo(string crkId, int djlx)
        {
            if (string.IsNullOrEmpty(crkId))
            {
                throw new FailedException("CrkId_is_empty");
            }
            if (djlx < 0)
            {
                throw new FailedCodeException("RECEIPTNO_IS_EMPTY");
            }
            return _pharmacyDrugStorageDmnService.SelectReceipDetailInfo(crkId, djlx, OperatorProvider.GetCurrent().OrganizeId);
        }


        #region 库存盘点

        /// <summary>
        /// 获取盘点日期
        /// </summary>
        /// <returns></returns>
        public List<InventoryDateDropDownVO> GetPdDateDropdownList()
        {
            return _pharmacyDrugStorageDmnService.GetPdDateDropdownList();
        }

        /// <summary>
        /// 获取药品类别 （盘点）
        /// </summary>
        /// <returns></returns>
        public List<MedicineCategoryVO> GetMedicineCategoryList()
        {
            return _pharmacyDrugStorageDmnService.GetMedicineCategoryList();
        }

        /// <summary>
        /// 开始盘点
        /// </summary>
        public InventoryDateDropDownVO StartInventory()
        {
            //判断是否有未完成的盘点
            var pdDate = _pharmacyDrugStorageDmnService.GetHangUpPdDates();
            if (pdDate != null && pdDate.Count > 0)
            {
                return null;
            }
            //生成盘点信息
            var resultMessage = _pharmacyDrugStorageDmnService.GenerateInventoryInfo(Constants.CurrentYfbm.yfbmCode, OperatorProvider.GetCurrent().OrganizeId, OperatorProvider.GetCurrent().UserCode);
            if (!string.IsNullOrEmpty(resultMessage))
            {
                throw new FailedException(resultMessage);
            }
            //获取盘点的开始时间
            var inventoryDateDropDown = GetPdDateDropdownList().FirstOrDefault();
            if (inventoryDateDropDown == null)
            {
                throw new FailedCodeException("GET_INVENTORY_START_TIME_FAILED");
            }
            return inventoryDateDropDown;
        }

        /// <summary>
        /// 查询需盘点的药品信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="pdId"></param>
        /// <param name="pdsj"></param>
        /// <param name="srm"></param>
        /// <param name="ypzt"></param>
        /// <param name="yplb"></param>
        /// <param name="kcxs"></param>
        /// <returns></returns>
        public IList<InventoryInfoVO> SelectInventoryInfoList(Pagination pagination, string pdId, string pdsj, string srm, string ypzt, string yplb, int kcxs)
        {
            return _pharmacyDrugStorageDmnService.SelectInventoryInfoList(pagination, pdId, pdsj, srm, ypzt, yplb, kcxs);
        }

        /// <summary>
        /// 盘点保存
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        /// <param name="pdId"></param>
        public void SaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList, string pdId)
        {
            var pdInfo = _sysMedicineInventoryRepo.SelectPdInfByPdId(pdId, OperatorProvider.GetCurrent().OrganizeId);
            if (pdInfo == null)
            {
                throw new FailedException("未找到指定盘点信息");
            }
            if (pdInfo.Jssj != null || pdInfo.Jssj >= Constants.MinDateTime)
            {
                throw new FailedException("该盘点已结束");
            }
            _sysMedicineInventoryDetailRepo.UpdateSlBySaveInventoryInfo(inventoryInfoList);
        }

        /// <summary>
        /// 结束盘点
        /// </summary>
        /// <returns></returns>
        public void EndInventory(string pdId)
        {
            var pdInfo = _sysMedicineInventoryRepo.SelectPdInfByPdId(pdId, OperatorProvider.GetCurrent().OrganizeId);
            if (pdInfo == null)
            {
                throw new FailedException("未找到指定盘点信息");
            }
            if (pdInfo.Jssj != null || pdInfo.Jssj >= Constants.MinDateTime)
            {
                throw new FailedException("该盘点已结束");
            }
            //生成盘点信息
            var resultMessage = _pharmacyDrugStorageDmnService.EndInventoryInfo(pdId);
            if (!string.IsNullOrEmpty(resultMessage) && !resultMessage.Contains("成功"))
            {
                throw new FailedException(resultMessage);
            }
        }

        /// <summary>
        /// 取消盘点
        /// </summary>
        /// <param name="pdId"></param>
        public void CancelInventory(string pdId)
        {
            //判断是否有未完成的盘点
            var count = _sysMedicineInventoryRepo.SelectUnfinishedInventoryByPdId(pdId);
            if (count != 1)
            {
                throw new FailedCodeException("THE_INVENTORY_HAS_ENDED_AND_CAN_NOT_BE_CANCELED");
            }
            _pharmacyDrugStorageDmnService.CancelInventory(pdId);
        }

        #endregion

        #region 库存结转
        /// <summary>
        /// 获取药品特殊属性
        /// </summary>
        /// <returns></returns>
        public List<DrugSpecialPropertiesVO> GetDrugSpecialPropertiesList()
        {
            return _pharmacyDrugStorageDmnService.GetDrugSpecialPropertiesList();
        }

        /// <summary>
        /// 结转
        /// </summary>
        public void CarryOverMedicine(string zq, DateTime kssj, DateTime jssj)
        {
            var needCarryOverMedicineList = _pharmacyDrugStorageDmnService.SelectNeedCarryOverMedicineList();//先查询需要结转的药品信息
            if (needCarryOverMedicineList.Count > 0)
            {
                _pharmacyDrugStorageDmnService.CarryOverMedicine(needCarryOverMedicineList, zq, kssj, jssj);
            }
            else
            {
                throw new FailedCodeException("OUT_OF_STOCK");
            }
        }

        /// <summary>
        /// 查询已结转药品
        /// </summary>
        /// <param name="zq"></param>
        /// <param name="inputCode"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IList<CarryOverMedicineVO> SelectCarryOverMedicineList(Pagination pagination, string zq, string inputCode)
        {
            return _pharmacyDrugStorageDmnService.SelectCarryOverMedicineList(pagination, zq, inputCode);
        }

        #endregion

        #region 进销存统计
        /// <summary>
        /// 返回药品剂型list
        /// </summary>
        /// <returns></returns>
        public List<MedicineFormulationVO> GetMedicineFormulationList()
        {
            return _sysMedicineFormulationRepo.GetMedicineFormulationList();
        }

        /// <summary>
        /// 药品类别 （进销存）
        /// </summary>
        /// <returns></returns>
        public List<MedicineCategoryVO> GetMedicineCategoryList2()
        {
            return _pharmacyDrugStorageDmnService.GetMedicineCategoryList2();
        }

        #endregion


    }
}
