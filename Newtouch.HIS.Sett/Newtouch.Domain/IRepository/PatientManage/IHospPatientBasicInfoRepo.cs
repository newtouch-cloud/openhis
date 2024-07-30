using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHospPatientBasicInfoRepo : IRepositoryBase<HospPatientBasicInfoEntity>
    {
        /// <summary>
        /// 根据住院号获取病人住院基本信息
        /// add by HLF
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="kh">卡号</param> 
        /// <param name="xm">姓名</param> 
        /// <returns></returns>
        List<HospPatientBasicInfoEntity> GetHosPatInfoList(string zyh, string kh, string xm, string OrganizeId);

        /// <summary>
        /// 根据病人内码获取有效的住院基本信息
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        HospPatientBasicInfoEntity GetFirstPatiInfoByPatid(int? patid);

        /// <summary>
        /// 根据zyh判断是否存在病人基本信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetBlhByZyh(string zyh, string orgId);

        /// <summary>
        /// 获取机构病人列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<PendingExecutionPatientVO> GetWardPatientVOListByOrg(string orgId, string bqCode = null);

        /// <summary>
        /// 根据zyh获取病人信息
        /// </summary>
        HospPatientBasicInfoEntity GetInpatientInfoByZyh(string zyh, string orgId);

        /// <summary>
        /// 筛选住院病人
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="zybz"></param>
        /// <returns></returns>
        List<HospPatientBasicInfoEntity> GetInpatientList(string orgId, string keyword = null, string zybz = "zaiyuanbr");

        /// <summary>
        /// 更新住院病人在院标志
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="zybz"></param>
        void UpdateInpatientStatus(string orgId, string zyh, string zybz);
        /// <summary>
        /// 通过出区、转区更新患者基本信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="bq"></param>
        /// <param name="cw"></param>
        /// <param name="cyrq"></param>
        /// <param name="cyzd"></param>
        /// <param name="cybq"></param>
        /// <param name="doctor"></param>
        /// <param name="ryzd"></param>
        /// <param name="rybq"></param>
        void UpdateInpatientOutInfoRequest(string orgId, string zyh, string zybz, string bq, string cw, DateTime? cyrq, string doctor, string cyzd);
        void UpdateInpatientOutRecallInfoRequest(string orgId, string zyh, string zybz, string bq, string cw, DateTime? cyrq, string doctor, string cyzd);
    }
}
