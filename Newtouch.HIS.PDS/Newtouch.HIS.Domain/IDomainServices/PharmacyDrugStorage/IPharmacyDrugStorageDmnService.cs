using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.VO;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPharmacyDrugStorageDmnService
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
        /// 单据查询 主信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="qsrj"></param>
        /// <param name="jsrj"></param>
        /// <param name="pdh"></param>
        /// <param name="fph"></param>
        /// <param name="djlx"></param>
        /// <param name="shzt"></param>
        /// <param name="allUseableDjlx"></param>
        /// <param name="orgId"></param>
        /// <param name="curYfbmCode"></param>
        /// <returns></returns>
        IList<ReceiptQueryVO> SelectReceiptMainInfo(Pagination pagination, DateTime? qsrj, DateTime? jsrj, string pdh, string fph, int? djlx, string shzt, string allUseableDjlx, string orgId, string curYfbmCode);

        /// <summary>
        /// 单据查询 主信息
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        IList<ReceiptQueryVO> SelectReceiptMainInfo(ReceiptQueryParam para);

        /// <summary>
        /// 单据查询明细
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ReceiptQueryDetailVO> SelectReceipDetailInfo(string crkId, int djlx, string orgId);

        /// <summary>
        /// 获取盘点日期
        /// </summary>
        /// <returns></returns>
        List<InventoryDateDropDownVO> GetPdDateDropdownList();

        /// <summary>
        /// 获取未完成的盘点时间
        /// </summary>
        /// <returns></returns>
        List<InventoryDateDropDownVO> GetHangUpPdDates();

        /// <summary>
        /// 获取药品类别 
        /// </summary>
        /// <returns></returns>
        List<MedicineCategoryVO> GetMedicineCategoryList();

        /// <summary>
        /// 生成盘点信息
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string GenerateInventoryInfo(string yfbmCode, string organizeId, string userCode);

        /// <summary>
        /// 转化页面显示数量  XX盒XX片
        /// </summary>
        /// <returns></returns>
        string GetYfbmYpComplexYpSlandDw(int sl, string ypCode);

        /// <summary>
        /// 查询结转药品信息
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
        IList<DrupreparationVO> SelectDrupreparationInfo(SelectDrupreParam pagination);
        /// <summary>
        /// 查询盘点药品信息 单位独立
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="pdId"></param>
        /// <param name="pdsj"></param>
        /// <param name="srm"></param>
        /// <param name="ypzt"></param>
        /// <param name="yplb"></param>
        /// <param name="kcxs"></param>
        /// <returns></returns>
        IList<InventoryInfoVO> SelectInventoryDetailIndependentUnit(Pagination pagination, string pdId,
           string pdsj, string srm, string ypzt, string yplb, int kcxs);
        /// <summary>
        /// 结束盘点
        /// </summary>
        /// <returns></returns>
        string EndInventoryInfo(string pdId);

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
        /// 查询需要结转的药品信息
        /// </summary>
        /// <returns></returns>
        List<NeedCarryOverMedicineVO> SelectNeedCarryOverMedicineList();

        /// <summary>
        /// 结转
        /// </summary>
        /// <param name="list"></param>
        /// <param name="zq"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        void CarryOverMedicine(List<NeedCarryOverMedicineVO> list, string zq, DateTime kssj, DateTime jssj);

        /// <summary>
        /// 查询已结转药品
        /// </summary>
        /// <param name="zq"></param>
        /// <param name="inputCode"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        IList<CarryOverMedicineVO> SelectCarryOverMedicineList(Pagination pagination, string zq, string inputCode);

        /// <summary>
        /// 药品类别 （进销存）
        /// </summary>
        /// <returns></returns>
        List<MedicineCategoryVO> GetMedicineCategoryList2();

        /// <summary>
        /// 进销存统计
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kszq"></param>
        /// <param name="jszq"></param>
        /// <param name="ksjzsj"></param>
        /// <param name="jsjzsj"></param>
        /// <param name="inputCode"></param>
        /// <param name="deptCode"></param>
        /// <param name="drugType"></param>
        /// <param name="dosage"></param>
        /// <param name="drugState"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        IList<PSIStatisticsVO> PsiStatisticsInfoList(Pagination pagination, string kszq, string jszq, DateTime ksjzsj, DateTime jsjzsj, string inputCode, string deptCode, string drugType, string dosage, string drugState, string rate);

        /// <summary>
        /// 得到结转账期和账期对应的开始结束时间
        /// </summary>
        /// <returns></returns>
        List<AccountPeriodDropDownVO> GetAccountPeriodDropDownList();

        #region 通过药房代码获取药库

        /// <summary>
        /// 通过药房代码获取药库
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetTheUpperYkbmCodeList(string keyword, string yfbmCode, string orgId);
        #endregion

        #region  通过药库代码获取药房

        /// <summary>
        /// 通过药库代码获取发药药房
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="ykbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<SysPharmacyDepartmentVEntity> GetTheLowerYfbmCodeList(string keyword, string ykbmCode, string organizeId);
        #endregion

        #region 通过药房代码获取科室
        /// <summary>
        /// 通过药房代码获取科室
        /// </summary>
        /// <param name="ykbmCode"></param>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysDepartmentVEntity> GetTheLowerKsCodeList(string ykbmCode, string orgId, string keyword = "");
        #endregion

        #region 资源预定

        /// <summary>
        /// 资源预定 （冻结库存，写mz_cfmxph表）
        /// </summary>
        /// <param name="bookItemDo"></param>
        void BookItem(BookItemDo bookItemDo);

        /// <summary>
        /// 门诊处方预定（冻结库存）
        /// </summary>
        /// <param name="apiResp"></param>
        /// <param name="cfnm"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        string OutPatientBookItem(object apiResp, string cfnm, cdInfoVO vo);

        /// <summary>
        /// 门诊处方预定（冻结库存）
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        void OutPatientBookItem(BookItemDo bookItemDo);

        /// <summary>
        /// 住院医嘱执行（冻结库存）
        /// </summary>
        /// <param name="bookItemDo"></param>
        /// <returns></returns>
        void HospitalizationBookItem(BookItemDo bookItemDo);

        /// <summary>
        /// 医嘱执行 获取药品单价
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        MedicineDjandZhyzVO GetZYYPdj(string organizeId, string yfbmCode, string ypCode);
        #endregion
    }
}
