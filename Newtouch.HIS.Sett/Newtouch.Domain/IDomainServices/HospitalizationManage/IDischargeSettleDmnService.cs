using Newtouch.Core.Common;
using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Proxy.guian.DTO;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.HospitalizationManage
{
    public interface IDischargeSettleDmnService
    {

        #region 出院结算
        /// <summary>
        /// 根据住院号获取病人基本信息
        /// </summary>
        List<InpatientSettPatInfoVO> SelectInpatientSettPatInfo(string zyh, string orgId);
	    
		/// <summary>
		/// 根据住院号获取病人 医保相关信息
		/// </summary>
		/// <param name="zyh"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		InpatientSettYbPatInfoVO GetInpatientSettYbPatInfo(string zyh, string orgId);

		/// <summary>
		/// 治疗项目计费表（zy_xmjfb）
		/// </summary>
		/// <param name="zyh"></param>
		/// <param name="OrganizeId"></param>
		/// <returns></returns>
		List<TreatmentItemFeeDetailVO> SelectTreatmentItemList(string zyh, string orgId, DateTime? kssj = null, DateTime? jssj = null,string ks=null);

        decimal GetInpatSettFeeSum(string zyh, string orgId, string sczt, string xmmc, DateTime? kssj = null, DateTime? jssj = null);
        /// <summary>
        /// 药品计费表
        /// </summary>
        List<DrugFeeDetailVO> SelectDrugList(string zyh, string orgId, DateTime? kssj = null, DateTime? jssj = null, string ks = null);

        List<DrugFeeDetailVO> SelectWsfDrugList(string zyh, string orgId, DateTime? kssj = null, DateTime? jssj = null, string ks = null);
        List<DrugFeeDetailVO> SelectWsfDrugList(string zyh, string orgId, DateTime? kssj = null, DateTime? jssj = null, string ks = null, string xmlb = null, string xmmc = null);
        List<TreatmentItemFeeDetailVO> SelectWsfItemList(string zyh, string orgId, DateTime? kssj, DateTime? jssj, string ks = null, string xmlb = null, string xmmc = null);
        /// <summary>
        /// 非治疗项目计费表
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<NonTreatmentItemFeeDetailVO> SelectNonTreatmentItemList(string zyh, string orgId);

        /// <summary>
        /// 保存结算
        /// </summary>
        /// <param name="settpatInfo"></param>
        /// <param name="settleItemsBo"></param>
        /// <param name="expectedcyrq"></param>
        /// <param name="orgId"></param>
        /// <param name="fph"></param>
        /// <param name="feeRelated">金额及支付信息</param>
        /// <param name="ybfeeRelated">医保相关费用信息</param>
        void SaveSett(InpatientSettPatInfoVO settpatInfo, InpatientSettleItemBO settleItemsBo, DateTime expectedcyrq, string orgId, string fph, InpatientSettFeeRelatedDTO feeRelated
            , CQZyjs05Dto ybfeeRelated, S14OutResponseDTO xnhfeeRelated
            , string outTradeNo,string jslx, out int jsnm);

        /// <summary>
        /// 获取按大类分组费用明细
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="sfdl"></param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupDetailVO> GetHospGroupFeeDetailVOList(string zyh, string orgId, string sfdl);
        /// <summary>
        /// 获取按大类分组费用
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupVO> GetHospGroupFeeVOList(string zyh, string orgId, string ver);
        /// <summary>
        /// 获取按大类分组费用-明细费用分页
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="sfdl"></param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupDetailVO> GetHospGroupFeeVOList(Pagination pagination, string zyh, string orgId, string sfdl);
        /// <summary>
        /// 获取按大类分组费用-带模糊查询
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="sfdl"></param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupDetailVO> GetHospGroupFeeVOData(Pagination pagination, string zyh, string orgId,string keyword);
        /// <summary>
        /// 获取收费明细
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="sfxmCode"></param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupDetailVO> GetDetailedQuery(Pagination pagination, string zyh, string orgId, string sfxmCode, string zfbz,decimal? dj);
        /// <summary>
        /// 住院患者中心获取费用账单
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="sfxmCode"></param>
        /// <returns></returns>
        IList<HospFeeChargeCategoryGroupDetailVO> GetFyzdDetailedQuery(Pagination pagination, string zyh, string orgId, string sfxmCode);
        

        IList<HospFeeUploadDetailVO> GetHospXmYpFeeVOList(Pagination pagination,string zyh, string orgId,string sczt, string xmmc,DateTime? kssj,DateTime jssj,string isnewyb);
        #endregion

        #region 取消出院结算

        /// <summary>
        /// 取消出院结算
        /// </summary>
        /// <param name="preSettlEntity"></param>
        /// <param name="settlDetailList"></param>
        /// <param name="cancelReason"></param>
        /// <param name="orgId"></param>
        void DoCancel(HospSettlementEntity preSettlEntity, List<HospSettlementDetailEntity> settlDetailList, string cancelReason, string cancelyblsh,string orgId);

        #endregion

        #region 模拟结算（GA）

        /// <summary>
        /// 保存医保预结算数据
        /// </summary>
        void SaveSett2303(CqybSett2303Entity YuJieSuan);

        #endregion

        #region 医保上传明细
        InZyfymxxrDto GetUploadFeeDetails(Pagination pagination, string zyh,string orgId,string usrName, out decimal ybzje);
        #endregion

        #region 新农合结算

        InpatientSettXnhPatInfoVO GetInpatientSettXnhPatInfo(string zyh, string orgId);
        S10RequestDTO GetUploadXnhFeeDetails(string zyh, string orgId, out decimal nhfyzje);
        S07RequestDTO GetXnhS07RequestDTO(string zyh, string orgId);
        string GetZyhByGrbm(string xnhgrbm,string sfjs, string orgId);

		#endregion

		#region 重庆医保

	    UploadPrescriptionsInPut GetCQUploadFeeDetails(Pagination pagination, string mzh, string orgId, string usercode, out decimal ybzje,
		    out decimal zfzje);
        string Getgjybdm(string mzzyh, string cfnms, string cflx,string orgId);
        decimal GetCQAlreadyUploadFeeDetails(string mzh, string orgId);
        HosCqybJsPatInfoVO GetCQInpatientSettYbPatInfo(string zyh, string orgId);

        /// <summary>
        /// 获取未审批明细
        /// </summary>
        /// <param name="cfmxlshstr">交易流水号字符串</param>
        /// <param name="orgId"></param>
        /// <param name="FinalType">审批类型</param>
        /// <param name="highPrices">高收费价格</param>
        /// <returns></returns>
        IList<RequestApprovalVO> sweep_expired_approvals(string cfmxlshstr, string orgId, string zyh);

        void updatespbz(string cfhstr, string orgId, string usercode);

        void commitPartialSettle(string zyh, string orgId, DateTime startTime, DateTime endTime, InpatientSettFeeRelatedDTO feeRelated, CQZyjs05Dto ybfeeRelated, string jslx, InpatientSettleItemBO ypjfbList, out int jsnm, int? expectedCount = null);
        /// <summary>
        /// 转出院结算更新病人基本信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        void UpdatePatientInfo(string zyh, string orgId);

        /// <summary>
        /// 最后一次交易流水号
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        int GetCQLastJsnm(string zyh, string orgId);

        /// <summary>
        /// 最后一次交易流水号
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetCQLastLsh(int jsnm, string orgId);

        CancelSettInfo GetCancelSettInfo(int jsnm, string orgId);

        #endregion

        #region 医保数据传输
        /// <summary>
        /// 查询HIS费用总额
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        InpatIentFeeObj GetHospPreInfo(string zyh, string orgId,DateTime jzsj);
        /// <summary>
        /// 是否存在未上传明细
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string ValialUploadData(string zyh, string orgId, DateTime jssj);
        string ValialUploadDataShyb(string zyh, string orgId, DateTime jssj);
        string ValialPartialUploadData(string zyh, string orgId, DateTime jssj);
        InpatIentFeeInfo GetCQAlreadyUploadFeeDetailsV2(string zyh, string orgId, DateTime jssj);
        #endregion
    }
}
