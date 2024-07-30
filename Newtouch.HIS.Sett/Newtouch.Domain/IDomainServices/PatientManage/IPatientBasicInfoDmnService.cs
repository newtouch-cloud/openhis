using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using Newtouch.HIS.Domain.ValueObjects.YibaoInterfaceManage;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.HIS.Sett.Request.Patient;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IPatientBasicInfoDmnService
    {

        /// <summary>
        /// 根据病人内码找病人基本信息
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
         OutpatAccInfoDto GetPatInfoByPatid(int patid, string orgId);
        /// <summary>
        /// 同时保存病人和卡信息
        /// </summary>
        /// <returns></returns>
        void SavePatBasicCardInfo(SysHosBasicInfoVO vo, string orgId, string CreatorCode);

        DateTime? IFCQRQISJZSJ(string zyh, string orgId);

        /// <summary>
        /// 根据patid获取住院登记的病人基本信息
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="result"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<SysHosBasicInfoVO> GetPatBasicCardInfo(string patid, out bool result, string OrganizeId);

        /// <summary>
        /// 病人管理获取form窗体
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        SysHosBasicInfoVO GetSyspatientFormJson(string patid, string OrganizeId,string zjh);

        /// <summary>
        /// 同时保存住院基本信息和预交账户信息
        /// </summary>
        /// <param name="SysHosBasicInfoVO"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="res"></param>
        string SaveSysBasicAccountInfo(SysHosBasicInfoVO SysHosBasicInfoVO, string OrganizeId, out string res);

        /// <summary>
        /// check zyh是否存在有效计费
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        bool CheckHasFee(string zyh, string orgId);
        /// <summary>
        /// 病人是否已入区
        /// </summary>
        /// <typeparam name="SysPatientManageSelectVO"></typeparam>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        bool BoolRuQu(string zyh, string orgid);

        #region 病人管理

        /// <summary>
        /// 病人管理查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        /// <param name="xlbrbz"></param>
        /// <returns></returns>
        List<SysPatientManageSelectVO> GetList(Pagination pagination, string orgId,string zjh);
        #endregion

        #region 重庆医保异常交易
        List<YibaoDataVo> GetYbCancelList(Pagination pagination, string isjs, string lx, string keyword);
        #endregion

        /// <summary>
        /// 根据住院号查询有效住院病人基本信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        List<SysHosBasicInfoVO> GetSysBasicByZHY(string kh, string zyh, string orgid);

        /// <summary>
        /// 费用一日清 住院号浮层搜索用
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgid"></param>
        /// <returns></returns>
        List<SysHosBasicInfoVO> GetSysBasicVagueByZHY(string zyh, string orgid);

        /// <summary>
        /// 门诊病人挂号查询
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <param name="lastUpdateTime"></param>
        /// <param name="outpatientNumber"></param>
        /// <returns></returns>
        List<OutPatientRegistrationInfoDTO> OutPatientRegistrationQuery(
            string orgId, ref string flag, ref string msg
            , DateTime? lastUpdateTime = null, string outpatientNumber = null
            , string ksCode = null, string ysgh = null
            , string mjzbz = null
            , string jiuzhenbz = null
            , string keyword = null
            , string zzhz = null
            , Pagination pagination = null);

        /// <summary>
        /// 住院患者查询（接口用）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <param name="lastUpdateTime"></param>
        /// <param name="zyh"></param>
        /// <param name="bqCode"></param>
        /// <param name="zybz"></param>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        List<InPatientInfoDTO> InPatientQuery(
            string orgId, ref string flag, ref string msg
            , DateTime? lastUpdateTime = null, string zyh = null
            , string bqCode = null, string zybz = null
            , string keyword = null
            , Pagination pagination = null);

        /// <summary>
        /// 住院患者查询（患者查询用）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <param name="lastUpdateTime"></param>
        /// <param name="zyh"></param>
        /// <param name="bqCode"></param>
        /// <param name="zybz"></param>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <param name="ryrqkssj"></param>
        /// <param name="ryrqjssj"></param>
        /// <returns></returns>
        IList<InPatientInfoVO> GetInPatientList(
            string orgId, ref string flag, ref string msg
            , DateTime? lastUpdateTime = null, string zyh = null
            , string bqCode = null, string zybz = null
            , string keyword = null
            , Pagination pagination = null, DateTime? ryrqkssj = null, DateTime? ryrqjssj = null,string ksmc=null,string bqmc=null);

        /// <summary>
        /// 更新住院患者多诊断之主诊断
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="zddm"></param>
        void UpdatePatInpMultiDiagnosis(string orgId, string zyh, string zddm,string user);

        /// <summary>
        /// 住院病人信息 分页数据
        /// </summary>
        /// <param name="pagination">分页信息</param>
        /// <param name="zyh">住院号</param>
        /// <param name="xm">姓名</param>
        /// <returns></returns>
        IList<HospPatientBasicInfoEntity> GetPatSearchList(Pagination pagination, string orgId, string zyh, string xm, string brzybzType = null);

        /// <summary>
        /// 取消入院
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        void CancelAdmission(string zyh, string orgId);

        /// <summary>
        /// 自费转医保
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="patid"></param>
        /// <param name="cardInfo"></param>
        /// <param name="ryblInfo"></param>
        void InpatientZFchangetoYB(string orgId, string zyh, int patid, GACardReadInfoDTO cardInfo, GuianRybl21OutInfoEntity ryblInfo);

        /// <summary>
        /// 自费转新农合
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="patid"></param>
        /// <param name="cardInfo"></param>
        /// <param name="ryblInfo"></param>
        bool InpatientZFchangetoXNH(string orgId, string zyh, HosPatZFToXNHVO cardInfo,
            GuianXnhS04InfoEntity ryblInfo, out string msg);
        /// <summary>
        /// 医保转自费
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="patid"></param>
        void InpatientYBchangetoZF(string orgId, string zyh, int patid);

        #region 新农合

        SysHosBasicInfoVO GetZfToXnhPatInfo(string zyh, string orgId);
         S04RequestDTO ComposeS04par(SysHosBasicInfoVO vo, string orgId);
         Response<S04ResponseDTO> S04submit(S04RequestDTO request, string orgId);
         bool InpatXnhInsertS04data(GuianXnhS04InfoEntity ryblInfo, out string msg);
         Response<string> S05submit(S05RequestDTO request, string orgId);
         Response<string> S06submit(S06RequestDTO request, string orgId);
         S06RequestDTO ComposeS06par(SysHosBasicInfoVO vo, string orgId);
        /// <summary>
        /// 验证新农合接口对照
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
         bool validateTTentity(string orgId, string code);
		#endregion

		#region 重庆医保

	    Input_Bbrxx GetCQjzdjInfo(string zyh, string orgId);
	    void InPatZFchangetoYB(string orgId, string zyh, int patid, ZYToYBDto patInfo, CqybMedicalReg02Entity ryblInfo);
        void OutPatZFchangetoYB(string orgId, string mzh, int patid, ZYToYBDto patInfo);
        void UpdateCqybOut02(string zyh,string orgId);
        #endregion

        #region 医保业务
        /// <summary>
        /// 是否首次就诊
        /// </summary>
        /// <param name="sfzh"></param>
        /// <param name="xm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysPatBasicInfoVo ValidateFirstVisit(string sfzh, string xm, string orgId,string kh= null, string jzpzlx = null);
        #endregion
        bool SignInStateUpdate(string mzh, string calledstu,string yhcode,string orgId);
        bool PatientAppointment(string mzh, string orgId);

        IList<InPatientDTO> InPatientInfoQuery(InPatientInfoQueryRequest dto);
        IList<OutPatientRegistrationInfoDTO> OutPatientConsultationQuery(OutPatientConsultationQueryRequest dto);
        IList<InpatientDayFeeDTO> InpatientDayFee(InpatientDayFeeRequest dto);
        /// <summary>
        /// 根据住院号获取入院诊断信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="HospitalID"></param>
        /// <returns></returns>
        IList<ryzd> getRyzdByZyh(string zyh, string HospitalID);
        /// <summary>
        /// 根据住院号获取出院诊断信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="HospitalID"></param>
        /// <returns></returns>
        IList<cyzd> getCyzdByZyh(string zyh, string HospitalID);

        /// <summary>
        /// 门诊历史账单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<MZhistorybillDTO> MZhistorybill(MZhistorybillRequest req);

        /// <summary>
        /// 账单明细
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<MZhistorybillMXDTO> MZhistorybillMX(MZhistorybillMXRequest req);

        /// <summary>
        /// 更新报警额
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <param name="bje"></param>
        /// <returns></returns>
        bool updateZybrxxkExpandBje(string zyh, string organizeId, decimal? bje);


        bool updateZybrxxkExpandZhye(string zyh, string organizeId, decimal? zhye);
        List<HisKsZdVO> GetksZzdList(string orgid);
    }
}
