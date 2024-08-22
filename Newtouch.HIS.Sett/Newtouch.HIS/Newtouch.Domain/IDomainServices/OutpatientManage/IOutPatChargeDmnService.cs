/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 
// Author：
// CreateDate： 2016/12/23 11:14:24 
//**********************************************************/
using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 门诊结算
    /// </summary>
    public interface IOutPatChargeDmnService
    {
        /// <summary>
        /// 根据卡号获得收费病人信息
        /// </summary>
        /// <param name="kh">卡号</param>
        /// <returns></returns>
        OutPatChargeInfoVO GetChargePatInfo(string kh, string brxz);

        /// <summary>
        /// 挂号科室类型
        /// </summary>
        /// <param name="patid">病人patid</param>
        /// <param name="brxz"></param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        List<OutPatChargeItemVO> getOutPatItem_ghlx(int patid, string brxz, string startDate, string endDate);

        /// <summary>
        /// 挂号科室类型
        /// </summary>
        /// <param name="patid">病人patid</param>
        /// <param name="brxz"></param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        List<OutPatChargeItemVO> GetOutPatGhlx(int patid, string brxz, string startDate, string endDate);

        /// <summary>
        /// 科室是否有效
        /// </summary>
        /// <param name="ks">科室</param>
        /// <param name="mjzbz">门急诊标志</param>
        /// <param name="parmJdrq">建档日期</param>
        /// <returns></returns>
        bool getActiveDuration(string ks, string mjzbz, object parmJdrq);

        /// <summary>
        /// 获取当前挂号的医生信息
        /// </summary>
        /// <param name="ghnm">挂号内码</param>
        /// <param name="parmKs">科室</param>
        /// <param name="type">3</param>
        /// <param name="mjzbz">门急诊标志</param>
        /// <param name="Isyzybzz">医保标志</param>
        /// <param name="Istxzzys">大病标志</param>
        /// <returns></returns>
        OutPatChargeDoctorVO getDoctorInfo(int ghnm, string parmKs, int type, string mjzbz, bool Isyzybzz, bool Istxzzys);

        /// <summary>
        /// 获取药品和收费项目
        /// </summary>
        /// <param name="keyword">查询关键字、py、mc</param>
        /// <returns></returns>
        List<ChargeItemDetailVO> getYpItemList(string keyword, string yfPz, string OrganizeId);

        /// <summary>
        /// 获取处方号
        /// </summary>
        /// <returns></returns>
        string getCflsh();

        /// <summary>
        /// 获取附加费金额
        /// </summary>
        /// <param name="parmBrxz">病人性质</param>
        /// <param name="parmDl">大类</param>
        /// <param name="parmSfxm">收费项目</param>
        /// <param name="parmDJ">单价</param>
        /// <returns></returns>
        decimal? Calcfwfjm(string parmBrxz, string parmDl, string parmSfxm, string parmDJ);

        /// <summary>
        /// 门诊收费结算 事务
        /// </summary>
        /// <param name="vo"></param>
        void OutPatChargeSettDB(OutPatChargeSettDataVo vo);

        /// <summary>
        /// 计算出门诊结算信息 getMzJs
        /// </summary>
        void GetMzJsInfo(SettlementEntityVO jsEntity, SysPatientNatureEntity brxzEntity, out OutpatientSettlementEntity mzjs, out List<OutpatientSettlementCategoryEntity> jsdl);


        /// <summary>
        /// 获取结算明细
        /// </summary>
        /// <param name="mzjs"></param>
        /// <param name="jsEntity"></param>
        /// <returns></returns>
        List<OutpatientSettlementDetailEntity> getJSMX(OutpatientSettlementEntity mzjs, SettlementEntityVO jsEntity);

        List<OutpatientSettlementDetailEntity> getJSMX(SettlementEntityVO jsEntity, string orgId);

        /// <summary>
        /// 门诊结算支付方式
        /// </summary>
        /// <param name="jsnm">结算内码</param>
        /// <param name="xjzffs">支付方式</param>
        /// <param name="xjzffsbh"></param>
        /// <param name="ysk">应收款</param>
        /// <returns></returns>
        OutpatientSettlementPaymentModelEntity GetMzJsZffs(int jsnm, string xjzffs, string xjzffsbh, decimal ysk);

        /// <summary>
        /// 门诊记账 获取病人基本信息(住院记账引用主意年龄修改)
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="blh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatAccInfoDto> GetChargePatInfoInAcc(string kh, string IsBlh, string mzh, string orgId);

        /// <summary>
        /// 根据门诊号查询病人详细信息（门诊收费右上方病人信息）
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatAccInfoDto> GetOutpatChargePatInfoInAcc(string mzh, string kh,string zjh, string cardType, string orgId);

        /// <summary>
        /// 根据卡号和挂号时间查询病人详细信息（门诊收费右上方病人信息）
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatAccInfoDto> GetpatientAccountList(DateTime kssj, DateTime jssj, string kh,string zjh, string cardType, string orgId);

        /// <summary>
        /// 根据卡号,病历号和证件号获取病人基本信息
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="blh"></param>
        /// <param name="zjh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OutpatAccInfoDto GetChargePatInfoInRegister(string kh, string blh, string zjh, string orgId, string cardType,string ly,string CardId);



        /// <summary>
        /// 门诊记账结算事务
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="jzjhEntity">记账计划</param>
        /// <param name="jzmxlist">记账计划明细</param>
        void OutPatChargeSettDBInAcc(OutPatChargeSettInAccDataVo vo, OutpatientAccountEntity jzjhEntity,
           List<OutpatientAccountDetailEntity> jzmxlist, Dictionary<string, string> optimaId);


        #region optima记账
        /// <summary>
        /// 根据门诊/住院号获取病人信息
        /// </summary>
        /// <param name="admsNum"></param>
        /// <param name="orgId"></param>
        /// <param name="type">病人类型：门诊/住院</param>
        /// <returns></returns>
        OutpatAccInfoDto GetPatInfoInOptima(string admsNum, string orgId, string type);

        /// <summary>
        /// 根据门诊号获取历史门诊记账项目
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OptimAccInfoDto> GetOutAccountInfo(string mzh, string orgId, string kssj, string jssj, string rygh);

        /// <summary>
        /// 根据住院号获取历史住院记账项目
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OptimAccInfoDto> GetintAccountInfo(string zyh, string orgId, string kssj, string jssj, string rygh);
        /// <summary>
        /// optima提交
        /// </summary>
        /// <param name="outaddvo">门诊新增记账</param>
        /// <param name="outupdatevo">门诊修改记账</param>
        /// <param name="outdelvo">门诊删除记账</param>
        /// <param name="inaddvo">住院新增记账</param>
        /// <param name="inupdatevo">住院修改记账</param>
        /// <param name="indelvo">住院删除记账</param>
        /// <param name="orgId"></param>
        void PatsettDBInOptima(SettInAccOutpatDataVo outaddvo,
           updateInAccOutpatVo outupdatevo,
           delInAccOutpatVo outdelvo,
           SettInAccHospatDataVo inaddvo,
           updateInAccHospatVo inupdatevo,
           delInAccHospatVo indelvo,
           string orgId,
           List<int> cfmxIds,
             Dictionary<string, string> optimaIds);
        #endregion

        #region 门诊收费 Vision-1.3 门诊记账的第三个版本
        /// <summary>
        ///  获取所有病人挂号信息
        /// </summary>
        IList<OutPatAccountingDto> GetRegisterGridJson(string begindate, string enddate, string orgId, string usercode);

        /// <summary>
        /// 根据门诊号获取历史收费项目
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OptimAccInfoDto> GetpatientAccountInfo(string mzh, string orgId);

        /// <summary>
        /// 保存记账，数据库保存
        /// </summary>
        /// <param name="vo">新增对象集合</param>
        /// <param name="updatevo">修改对象集合</param>
        /// <param name="jsnmList">修改和删除的结算内码集合</param>
        /// <param name="delvo">删除对象集合</param>
        /// <param name="orgId"></param>
        void OutPatsettDBInCharge(OutPatSettInAccDataVo vo, updatePatSettInAccDataVo updatevo, List<int> jsnmList, DelPatSettInAccDataVo delvo, string orgId);
        #endregion

        #region 门诊记账，第四个版本，增加单位治疗量逻辑
        /// <summary>
        /// 根据门诊号获取历史收费项目，不包含药品和已完成，已停止的记账计划
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OptimAccInfoDto> GetpatientAccountInfoV4(string mzh, string orgId);
        /// <summary>
        /// 保存记账，数据库保存
        /// </summary>
        /// <param name="vo">新增对象集合</param>
        /// <param name="updatevo">修改对象集合</param>
        /// <param name="jsnmList">修改结算内码集合</param>
        /// <param name="orgId"></param>
        void OutPatsettDBInCharge(OutPatSettInAccDataVo4 vo, List<OutpatientItemExeEntity> tempVo, string orgId);

        #endregion

        #region 收费2018
        object GetPatientGridJson(string orgId);

        /// <summary>
        /// 获取欠费预结的结算记录
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OutpatientSettArrearageVO> GetOutpatientSettArrearageVOList(string mzh, string orgId);

        /// <summary>
        /// 根据jsnm 获取结算的明细列表
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OutpatientSettDetailVO> GetOutpatientSettDetailList(int jsnm, string orgId);

        /// <summary>
        /// 根据门诊号获取未收费的处方
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ChargeLeftDto> GetPrescriptionBymzh(string mzh, string orgId);

        IList<ChargeRightDto> GetPrescriptionDetailBycfnm(IList<int> cfnmList, string orgId);
        IList<ChargeLeftDto> GetNewUnSettedPrescriptionByMzh(string mzh, string orgId);

        /// <summary>
        /// 获取待结
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ChargeRightDto> GetAllUnSettedListListByMzh(string mzh, string orgId);
        IList<ChargeRightDto> GetNewAllUnSettedListByMzh(string mzh, string cfnms, string orgId);
        /// <summary>
        /// 退费时再上传明细获取
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<GuiAnChargeRightDto> GetAllUnSettedListListByJsnm(string jsnm, string orgId);

        /// <summary>
        /// 获取待结项目（非处方）
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<ChargeRightDto> GetAllUnSettedSingleItemListByMzh(string mzh, string orgId);

        /// <summary>
        /// 获取指定患者新农合未结算的所有outpId
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<string> SelectNotSettedXnhOutpId(int patid, string organizeId);

        /// <summary>
        /// 贵安医保，获取门诊上传时数据（头信息）
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        GuiAnMzjsPatInfoDto GetGuiAnMzjsPatInfoDto(string mzh, string orgId);

		#endregion

		#region 重庆医保
	    IList<UploadPrescriptionsListInPut> GetCQYBUnSettedListByMzh(Pagination pagination, string mzh, string orgId);
	    IList<Input_2204> GetCQZFUnSettedListByMzh(string mzh, string cfnm, string orgId);
	    Input_2203A GetCQjzdjInfo(string mzh, string orgId);
		IList<Input_2204> GetChongQingGHMzjs(string ghxm, string zlxm, bool isCkf, bool isGbf, string orgId = null);
	    IList<UploadPrescriptionsListInPut> GetCQYBTfListByJsnm(string jsnm, string mzh,  string orgId);
	    IList<UploadPrescriptionsListInPut> GetCQZFTfListByJsnm(string jsnm, string mzh,  string orgId);
        IList<TbbzmlDto> GetMzbzml(string mllx);
        #endregion

        #region 结算查询

        /// <summary>
        /// 结算查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        IList<OutpatientSettlementVO> GetSettlementList(Pagination pagination, string orgId, string keyword, DateTime? kssj, DateTime? jssj, bool containsTF);

        #endregion

        /// <summary>
        /// 根据门诊号获取系统病人基本信息
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<SysPatientBasicInfoEntity> SelectSysPatientBasicInfoEntities(string mzh, string orgId);
    }
}
