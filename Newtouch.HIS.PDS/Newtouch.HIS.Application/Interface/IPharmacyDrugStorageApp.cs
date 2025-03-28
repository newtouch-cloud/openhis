using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPharmacyDrugStorageApp
    {
        /// <summary>
        /// 库存量查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="tybz"></param>
        /// <param name="zt"></param>
        /// <param name="noShow0Kc"></param>
        /// <returns></returns>
        IList<MedicineStockQueryVO> SelectStockTotal(Pagination pagination, string inputCode, string tybz, string zt, bool noShow0Kc);

        /// <summary>
        /// 库存量查询明细
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        IList<MedicineStockQueryDetailVO> SelectStockDetail(string ypCode, string yfbmCode);

        /// <summary>
        /// 单据查询明细
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <returns></returns>
        IList<ReceiptQueryDetailVO> SelectReceipDetailInfo(string crkId, int djlx);

        /// <summary>
        /// 获取盘点日期
        /// </summary>
        /// <returns></returns>
        List<InventoryDateDropDownVO> GetPdDateDropdownList();

        /// <summary>
        /// 获取药品类别 （盘点）
        /// </summary>
        /// <returns></returns>
        List<MedicineCategoryVO> GetMedicineCategoryList();

        /// <summary>
        /// 开始盘点
        /// </summary>
        InventoryDateDropDownVO StartInventory();

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
        IList<InventoryInfoVO> SelectInventoryInfoList(Pagination pagination, string pdId, string pdsj, string srm, string ypzt, string yplb, int kcxs);

        /// <summary>
        /// 盘点保存
        /// </summary>
        /// <param name="inventoryInfoList"></param>
        /// <param name="pdId"></param>
        void SaveInventoryInfo(List<SaveInventoryInfoVO> inventoryInfoList, string pdId);


        /// <summary>
        /// 保存库存追溯码信息
        /// </summary>
        /// <param name="SaveInventoryZsmInfo"></param>
        void SaveInventoryZsmInfo(List<SaveInventoryInfoVO> inventoryInfoList);

        /// <summary>
        /// 结束盘点
        /// </summary>
        /// <param name="pdId"></param>
        void EndInventory(string pdId);

        /// <summary>
        /// 取消盘点
        /// </summary>
        /// <param name="pdId"></param>
        void CancelInventory(string pdId);

        /// <summary>
        /// 获取药品特殊属性
        /// </summary>
        /// <returns></returns>
        List<DrugSpecialPropertiesVO> GetDrugSpecialPropertiesList();

        /// <summary>
        /// 结转
        /// </summary>
        /// <param name="zq"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        void CarryOverMedicine(string zq, DateTime kssj, DateTime jssj);

        /// <summary>
        /// 查询已结转药品
        /// </summary>
        /// <param name="zq"></param>
        /// <param name="inputCode"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        IList<CarryOverMedicineVO> SelectCarryOverMedicineList(Pagination pagination, string zq, string inputCode);

        /// <summary>
        /// 返回药品剂型list
        /// </summary>
        /// <returns></returns>
        List<MedicineFormulationVO> GetMedicineFormulationList();

        /// <summary>
        /// 药品类别 （进销存）
        /// </summary>
        /// <returns></returns>
        List<MedicineCategoryVO> GetMedicineCategoryList2();


    }
}
