using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationPharmacy;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 发药
    /// </summary>
    public interface IDispenseIndexInfoDmnService
    {
        #region 发药
        /// <summary>
        /// 配药完成，修改医嘱执行状态
        /// </summary>
        /// <param name="tdrq"></param>
        /// <param name="zxId"></param>
        /// <param name="fybz"></param>
        /// <param name="ypdj"></param>
        /// <param name="ypje"></param>
        /// <param name="zfbl"></param>
        /// <param name="zfxz"></param>
        /// <returns></returns>
        string UPBQPYyzzxZT(string tdrq, string zxId, string fybz, string ypdj, string ypje, string zfbl, string zfxz);


        #endregion

        #region 配药
        string GetBQPYXXList(DispenseBQInfoVO model);

        //执行配药操作
        string GetBQTYXXList(DispenseBQInfoVO model);
        #endregion

        #region 退药

        /// <summary>
        /// 获取退药明细表，关联医嘱表ID
        /// </summary>
        /// <returns></returns>
        List<ModelZyTyYszyzIdInfoVO> YpTyyszyzidList();
        /// <summary>
        ///  获取退药明细以及医嘱批号表 可退药信息
        /// </summary>
        /// <returns></returns>
        List<ModelBQBRYZZXTYInfoVO> YpTymxxxList(List<ModelZyTyYszyzIdInfoVO> list);

        /// <summary>
        /// 病区退药操作
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string ZYBQTYOperate(List<ModelBQBRYZZXTYInfoVO> list, string zyyf,string OrganizeId);

        /// <summary>
        /// 获取住院退药的病人
        /// </summary>
        /// <param name="zybrbq"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<YPFYPatientInfoVO> GetTyPatient(string zybrbq, string organizeId);
        #endregion

        #region 住院发药
        /// <summary>
        /// 获取发药明细
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<YPFYPatientInfoVO> SelectDispenseDrugDetail(HospitalizationDispenseDrugParam req);

        /// <summary>
        /// 删除医嘱信息
        /// </summary>
        /// <param name="yzId">医嘱ID</param>
        /// <param name="zxId">医嘱执行ID</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string DeleteYzxx(string yzId, string zxId, string organizeId, string userCode);

        /// <summary>
        /// 获取需要发药的病区（去重）
        /// </summary>
        /// <returns></returns>
        IList<ZYFYBQModelVO> GetFyBq(string orgId);

        /// <summary>
        /// 获取退药病人和病区
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<YPFYPatientInfoVO> GetDispensePatientInfo(string yfbmCode, string organizeId);

        /// <summary>
        /// 获取需要发药的病人
        /// </summary>
        /// <returns></returns>
        IList<YPFYPatientInfoVO> GetFyPatient(string zybrbq);

        /// <summary>
        /// 发药退回操作
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fybz"></param>
        /// <returns></returns>
        string ZYBQFYReturn(List<DispenseBQRYXXIndexVO> list, string fybz);

        /// <summary>
        /// 住院发药按钮操作
        /// </summary>
        /// <param name="list"></param>
        /// <param name="zyyf"></param>
        /// <returns></returns>
        string ZYBQFYOperate(List<DispenseBQRYXXIndexVO> list, string zyyf);

        /// <summary>
        /// 住院发、退药查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<HospitalizationDispenseDetail> GetZyDrugQueryList(Pagination pagination, QueryZYFYInfoReqVO req);

        /// <summary>
        /// 住院发药查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<HospitalizationDispenseDetail> GetZyfyQueryList(Pagination pagination, QueryZYFYInfoReqVO req);

        /// <summary>
        /// 住院退药查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<HospitalizationDispenseDetail> GetZytyQueryList(Pagination pagination, QueryZYFYInfoReqVO req);
        /// <summary>
        /// 根据医嘱操作日志
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<HospitalizationDispenseDetail> GetYzCzjlList(Pagination pagination, QueryZYFYInfoReqVO req);

        /// <summary>
        /// 插入退药申请单
        /// </summary>
        /// <param name="applyBillDetail"></param>
        /// <param name="applyBill"></param>
        /// <returns></returns>
        string InsertReturnDispensingMedicine(List<ZyReturnDrugApplyBillDetailEntity> applyBillDetail,
           List<ZyReturnDrugApplyBillEntity> applyBill);

        /// <summary>
        /// 获取退药病人和病区
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<YPFYPatientInfoVO> GetReturnDispensePatientInfo(string yfbmCode, string organizeId);

        /// <summary>
        /// 获取退药明细
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetail(HospitalizationReturnDrugParam req);

        /// <summary>
        /// 获取退药明细
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetailNoBatch(HospitalizationReturnDrugParam req);

        List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetailNoBatchV2(HospitalizationReturnDrugParam req);

        List<ZyTyApplyNoVO> GetZyTyApplyNo(HospitalizationReturnDrugParam req);

        List<HospitalizationReturnDispenseDetail> SelectReturnDrugDetailV2(HospitalizationReturnDrugParam req);

        IList<HospitalizationDispenseDetail> GetYzCzjlListV2(Pagination pagination, QueryZYFYInfoReqVO req);

        List<YPFYPatientInfoVO> GetFybdBrxxTree(string yfbmCode, string organizeId);

        IList<HospitalizationDispenseDetailV2> GetFybdList(Pagination pagination, QueryZYFYInfoReqVOV2 req);

        IList<FybdComboboxList> GetFybdComboboxList( QueryZYFYInfoReqVO req);

        #endregion

        #region 科室备药

        /// <summary>
        /// 获取科室申请单待发药/备药库存退回
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<KSBYSQDInfoVO> GetDeptApplySendInfo(string yfbmCode, string organizeId,string bylx);

        /// <summary>
        /// 申请单列表
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="OrgId"></param>
        /// <param name="Yfbm"></param>
        /// <returns></returns>
        List<DeptMedicineApplySendVo> SelectDeptApplySendList(string SqdArr, string OrgId, string Yfbm);

        /// <summary>
        /// 发药
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="userCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string ApplyNoSendDrugs(string SqdArr, string userCode, string yfbmCode, string organizeId);

        /// <summary>
        /// 科室备药库存退回列表
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="OrgId"></param>
        /// <param name="Yfbm"></param>
        /// <returns></returns>
        List<DeptMedicineApplySendVo> SelectDeptApplyReturnList(string SqdArr, string OrgId, string Yfbm);

        /// <summary>
        /// 库存退还
        /// </summary>
        /// <param name="SqdArr"></param>
        /// <param name="userCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string KcApplyNoReturnDrugs(string SqdArr, string userCode, string yfbmCode, string organizeId);
        #endregion


        #region  住院医嘱处方
        IList<ZycfcxList> GetZycfList(Pagination pagination, ZycfcxVo req);
        IList<ZycfcxDetailList> GetZycfDetailList(Pagination pagination, ZycfcxVo req);
        #endregion
    }
}
