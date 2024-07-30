using System;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Application.Interface;
using Newtouch.Common.Web;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.HIS.Sett.Request;
using Newtouch.Infrastructure;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// 患者入院诊断
    /// </summary>
    public class PatientRyDiagnosisApp : AppBase, IPatientRyDiagnosisApp
    {
        private readonly IInpatientPatientDiagnosisRepo _inpatientPatientDiagnosisRepo;
        private readonly IInpatientPatientInfoRepo _inpatientPatientInfoRepo;

        /// <summary>
        /// 获取入院诊断信息
        /// </summary>
        /// <param name="zhy"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<PatientRyDiagnosisDto> PatientRyDiagnosisQuery(string zhy, string organizeId)
        {
            var patInfoReq = new InPatientDetailQueryRequest
            {
                zyh = zhy,
                OrganizeId = organizeId
            };
            var apiRespzd = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<List<PatientRyDiagnosisDto>>>("/api/Patient/PatientRyDiagnosisQuery", patInfoReq);
            if (apiRespzd.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                throw new Exception(apiRespzd.sub_msg);
            }

            return apiRespzd.data;
        }

        /// <summary>
        /// 保存入院诊断
        /// </summary>
        /// <param name="patInAreaVo"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string SavaInPatDiagnosis(PatInAreaVO patInAreaVo, string organizeId, string userCode)
        {
            if (patInAreaVo == null) return "诊断内容不能为空！";
            if (string.IsNullOrWhiteSpace(patInAreaVo.zyh)) return "住院号不能为空！";
            if (string.IsNullOrWhiteSpace(patInAreaVo.ryzddm1) || string.IsNullOrWhiteSpace(patInAreaVo.ryzdmc1)) return "入院诊断1不能为空！";

            #region 同步至Sett
            var syncSettResult = SyncInPatDiagnosisToSett(patInAreaVo, organizeId, userCode);
            if (!string.IsNullOrWhiteSpace(syncSettResult)) return syncSettResult;
            #endregion

            var patOutDiaList = _inpatientPatientDiagnosisRepo.SelectData(patInAreaVo.zyh, organizeId, "1");//获取历史出院诊断
            var patInfo = _inpatientPatientInfoRepo.GetByZyh(organizeId, patInAreaVo.zyh);//获取患者信息
            var operateTime = DateTime.Now;
            try
            {

                using (var db = new EFDbTransaction(new DefaultDatabaseFactory()).BeginTrans())
                {
                    #region 修改病人信息库
                    if (patInfo != null)
                    {
                        patInfo.LastModifierCode = userCode;
                        patInfo.LastModifyTime = operateTime;
                        patInfo.zddm = patInAreaVo.ryzddm1;
                        patInfo.zdmc = patInAreaVo.ryzdmc1;
                        db.Update(patInfo);
                    }
                    #endregion

                    #region 修改住院病人诊断信息
                    if (patOutDiaList != null && patOutDiaList.Count > 0)
                    {
                        patOutDiaList.ForEach(p =>
                        {
                            p.zt = "0";
                            p.LastModifierCode = userCode;
                            p.LastModifyTime = operateTime;
                            db.Update(p);
                        });
                    }

                    var diaEntity = new InpatientPatientDiagnosisEntity
                    {
                        Id = Guid.NewGuid().ToString(),
                        OrganizeId = organizeId,
                        zyh = patInAreaVo.zyh,
                        zdlb = "1",
                        zdlx = "0",
                        zddm = patInAreaVo.ryzddm1,
                        zdmc = patInAreaVo.ryzdmc1,
                        zt = "1",
                        CreateTime = operateTime,
                        CreatorCode = userCode
                    };
                    db.Insert(diaEntity);
                    #endregion

                    db.Commit();
                    return "";
                }

            }
            catch (Exception)
            {
                return "修改入院诊断失败";
            }
        }

        /// <summary>
        /// 同步入院诊断至结算系统
        /// </summary>
        /// <param name="patInAreaVo"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        private string SyncInPatDiagnosisToSett(PatInAreaVO patInAreaVo, string organizeId, string userCode)
        {
            var zdxx = new ModifyRyDiagnosisRequestDTO
            {
                zyh = patInAreaVo.zyh,
                OrganizeId = organizeId,
                userCode = userCode,
                RyDiagnosisDetails = new List<RyDiagnosisDetail>
                {
                    new RyDiagnosisDetail
                    {
                        ryzdmc = patInAreaVo.ryzdmc1,
                        ryzdICD10 = patInAreaVo.ryzdICD101,
                        ryzddm = patInAreaVo.ryzddm1,
                        zdpx = "1"
                    },
                    new RyDiagnosisDetail
                    {
                        ryzddm = patInAreaVo.ryzddm2,
                        ryzdICD10 = patInAreaVo.ryzdICD102,
                        ryzdmc = patInAreaVo.ryzdmc2,
                        zdpx = "2"
                    },
                    new RyDiagnosisDetail
                    {
                        ryzddm = patInAreaVo.ryzddm3,
                        ryzdICD10 = patInAreaVo.ryzdICD103,
                        ryzdmc = patInAreaVo.ryzdmc3,
                        zdpx = "3"
                    }
                }
            };
            var apiRespzd = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/patient/ModifyRyDiagnosis", zdxx);
            return apiRespzd.code != APIRequestHelper.ResponseResultCode.SUCCESS ? apiRespzd.sub_msg : "";
        }
    }
}