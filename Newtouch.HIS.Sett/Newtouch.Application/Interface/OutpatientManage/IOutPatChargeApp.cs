/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 门诊挂号收费相关业务
// Author：HLF
// CreateDate： 2016/12/26 12:42:17 
//**********************************************************/
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Proxy.guian.DTO.S25;
using Newtouch.Core.Common;
using System;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutPatChargeApp
    {
        /// <summary>
        /// 根据卡号获取基本信息
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="brxz"></param>
        /// <returns></returns>
        OutPatChargeInfoVO GetOutPatChargeInfo(string kh, string brxz);


        /// <summary>
        /// 获取挂号科室医生
        /// </summary>
        /// <param name="patid">病人patid</param>
        /// <param name="brxz">病人性质编号</param>
        /// <param name="docVo"></param>
        /// <returns></returns>
        List<OutPatChargeItemVO> GetOutGhInfo(int patid, string brxz, out OutPatChargeDoctorVO docVo);

        /// <summary>
        /// 获取药品和收费项目
        /// </summary>
        /// <param name="keyword">查询关键字</param>
        /// <returns></returns>
        List<ChargeItemDetailVO> GetYpItemInfo(string keyword);

        /// <summary>
        /// 获取处方号和服务费
        /// </summary> 
        /// <param name="sfxmtype">收费项目类别</param> 
        ///  <param name="lscfh">临时处方号</param>
        /// <param name="brxz">病人性质</param>
        /// <param name="dl">大类</param>
        /// <param name="sfxm">收费项目</param>
        /// <param name="dj">单价</param>
        /// <param name="sl">数量</param>
        /// <returns></returns> 
        OutPatChargeCFDto GetCfhAndFjfMoney(string sfxmtype, string lscfh, string brxz, string dl, string sfxm, string dj, string sl);

        /// <summary>
        /// 获取处方号
        /// </summary>
        /// <param name="sfxmtype">收费项目类别</param>
        /// <returns></returns>
        string GetCfh(string sfxmtype);

        /// <summary>
        /// 获取收费项目模版
        /// </summary>
        /// <param name="ks">科室</param>
        /// <returns></returns>
        OutPatChargeItemTempVO GetChargeTemplateAll(string ks);


        /// <summary>
        /// 门诊收费添加处方和结算
        /// </summary>
        /// <param name="gridDto">处方列表</param>
        /// <param name="kh">卡号</param>
        /// <param name="fph">发票号</param> 
        /// <param name="xjzffs">现金支付方式</param> 
        /// <param name="xjzffsbh">现金支付方式编号</param>  
        /// <param name="ysk">预收款</param> 
        /// <param name="ssk">实收款</param>
        /// <param name="zl">找零</param>
        /// <returns></returns>
        SettSaveSuccessResultDto OutPatChargeSett(List<OutpatChargeDto> gridDto, string kh, string fph, string xjzffs, string xjzffsbh, decimal ysk, decimal ssk, decimal zl);

        /// <summary>
        /// 保存结算实体赋值
        /// </summary>
        /// <param name="gridDto">要保存、结算的处方列表</param>
        /// <param name="kh"></param>
        /// <param name="fph">发票号</param>
        /// <param name="xjzffs">现金支付方式</param>
        /// <param name="xjzffsbh"></param>
        /// <param name="ysk">预收款</param>
        /// <param name="ssk">实收款</param>
        /// <param name="zl"></param>
        /// <param name="jsEntity"></param>
        /// <returns></returns>
        OutPatChargeSettDataVo GetOutPatChargeSettDbvo(List<OutpatChargeDto> gridDto, string kh, string fph,
           string xjzffs, string xjzffsbh, decimal ysk, decimal ssk, decimal zl, out SettlementEntityVO jsEntity);

        /// <summary>
        /// 门诊记账 根据门诊号获取基本信息
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="blh"></param>
        /// <returns></returns>
        List<OutpatAccInfoDto> GetOutPatBasicInfoInAcc(string IsBlh, string mzh);

        /// <summary>
        /// 门诊登记和住院登记 根据病历号 或 卡号 查询病人基本信息
        /// </summary>
        /// <param name="blh">病历号</param>
        /// <param name="kh">卡号</param>
        /// <param name="zjh">证件号</param>
        /// <param name="cardType">卡类型</param>
        /// <returns></returns>
        OutpatAccInfoDto GetOutPatBasicInfoInRegister(string blh, string kh, string zjh, string cardType,string ly,string CardId);


        #region 门诊收费 Vision-1.2 门诊记账的第三个版本
        /// <summary>
        /// 门诊收费，获取病人基本信息和记账信息
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        IList<OptimAccInfoDto> GetInfo(string mzh);
        #endregion

        #region 门诊收费

        /// <summary>
        /// 门诊收费2018 提交门诊收费
        /// </summary>
        /// <param name="bacDto"></param>
        /// <param name="feeRelated"></param>
        /// <param name="ybfeeRelated"></param>
        /// <param name="accDto"></param>
        /// <param name="orgId"></param>
        /// <param name="cfnmList"></param>
        /// <param name="jsnmList"></param>
        /// <param name="extxmnmList">历史已提交（库中已有）</param>
        /// <returns></returns>
        bool submitOutpatCharge(BasicInfoDto2018 bacDto
            , OutpatientSettFeeRelatedDTO feeRelated
            , CQMzjs05Dto ybfeeRelated
            , S25ResponseDTO xnhybfeeRelated
            , OutServiceInfoDto2018 accDto, string orgId
            , IList<int> cfnmList, out IList<int> jsnmList, IList<int> extxmnmList
            , string outTradeNo);

        /// <summary>
        /// 补计费提交
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="bacDto"></param>
        /// <param name="xmList"></param>
        /// <returns></returns>
        bool submitOutpatAdditional(string orgId, BasicInfoDto2018 bacDto,
            OutServiceInfoDto2018 accDto);
        /// <summary>
        /// 项目未收费，单独收费
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ghnm"></param>
        /// <param name="fph"></param>
        /// <param name="sfrq"></param>
        /// <param name="feeRelated"></param>
        /// <param name="ghxmnmList"></param>
        int submitOutpatGhCharge(string orgId, string fph, DateTime? sfrq, OutpatientSettFeeRelatedDTO feeRelated, IList<int> ghxmnmList);
        /// <summary>
        /// 整合前台的收费数据成后台的理想状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        OutServiceInfoDto2018 togetherData(List<OutGridInfoDto2018> dto);
        #endregion

        /// <summary>
        /// 明细上传到医保
        /// </summary>
        /// <param name="mzh"></param>
        void DetailsUploadYb(string mzh);

        guianMainAllOfMzjs GetGuianMainOfMzjs(string mzh);

        guianMainAllOfMzjs GetGuiAnDetailsMzjsYbTfh(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh);

        

        #region 重庆医保
        void GetChongQingMainOfMzjs(string mzh, string cfnm, out decimal ybzje, out decimal zfzje);
	    UploadPrescriptionsInPut GetCQDetailsMzjsYbTfh(string mzh, string jsnm, Dictionary<string, decimal> tjsxmDict, out decimal ybzje, out decimal zfzje, out decimal tfzje);

	    void SaveCqybUploadInPres(List<UploadPrescriptionsListInPut> cflist, string zymzh, string jytype);
        void SaveCqybUploadInPres(string zymzh, string jytype, string cfh, string pch);
        void UpdateCqyb04InPres(string zymzh, string cfh, string orgId);

        void UpDateUploadPch(string zymzh, string cfh, string pch, string orgId);

        List<RefundPrescriptionsInPut> GetCqyb10Data(string zymzh, string type);

        List<Input_2205> GetCqybMxCxData(string zymzh, string type, string ybver);
        object ZFToYB_Step_1(string mzh);

	    object ZFToYB_Step_3(string mzh, string sbbh, string xm);

	    object ZFToYB_Step_6(string mzh, int patid,ZYToYBDto patInfo);

	    object ZFToYB_Step_8(string mzh);
        #endregion
    }
}
