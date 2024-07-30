using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Domain.DTO;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Repository.SystemManage;
using Newtouch.Application.Interface;
using Newtouch.CIS.Proxy;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.AERequest;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.EMRRequest;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.HEALRequest;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.KRequest;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_02;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09;
using Newtouch.CIS.Proxy.Dapper.CMMPlatform;
using Newtouch.Domain.DTO;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure.Log;
using Newtouch.Repository;
using Newtouch.Tools;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// 门诊中医馆管理
    /// </summary>
    public class OutpatientCmmManagerApp : AppBase, IOutpatientCmmManagerApp
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ITreatmentRepo _treatmentRepo;
        private readonly IBaseDataDmnService _baseDataDmnService;
        private readonly ICmmHis01Repo _cmmHis01Repo;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly ICmmHis02RecordDmnService _comCmmHis02RecordDmnService;

        #region TCM_HIS_01

        /// <summary>
        /// 患者信息上传
        /// </summary>
        /// <param name="jzxx"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public Result TcmHis01(TreatEntityObj jzxx, string organizeId, string userCode)
        {
            var result = new Result
            {
                code = "1",
                desc = ""
            };
            try
            {
                if (CheckIsUpload(jzxx.mzh, organizeId)) return result;
                var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", organizeId);
                if (string.IsNullOrWhiteSpace(confInfo)) throw new Exception("请配置中医馆行政机构信息，编码为cmm_configuration");
                var objConfig = confInfo.ToObject<ConfigurationInfo>();
                if (objConfig == null) throw new Exception("中医馆行政机构信息配置有误，请注意结构！");
                if (string.IsNullOrWhiteSpace(objConfig.areaCode) || string.IsNullOrWhiteSpace(objConfig.jgdm)) throw new Exception("中医馆行政机构信息配置机构代码和区域代码不能为空！");
                var brxx = _baseDataDmnService.SelectXtBrjbxx(jzxx.blh, jzxx.mzh, organizeId);

                var patientInfo = new Patient
                {
                    id = jzxx.blh,
                    name = jzxx.xm,
                    genderCode = CIS.Proxy.Tools.GetGenderCode(jzxx.xb ?? ""),
                    gender = jzxx.xb ?? "",
                    birthday = jzxx.csny == null ? "" : ((DateTime)jzxx.csny).ToString("yyyy-MM-dd"),
                    cardTypeCode = CIS.Proxy.Tools.CardTypeCodeChange(jzxx.zjlx ?? 0) ?? "",
                    cardType = CIS.Proxy.Tools.GetCardType(jzxx.zjlx ?? 0) ?? "",
                    cardNo = jzxx.zjh ?? "",
                    outpatientNo = jzxx.mzh
                };
                if (brxx != null && brxx.Count > 0)
                {
                    var item = brxx.FirstOrDefault();
                    patientInfo.marriedCode = CIS.Proxy.Tools.ChangeMarriedCode(item.hf == null ? 9 : (int)item.hf);
                    patientInfo.married = CIS.Proxy.Tools.GetMarried(item.hf == null ? 9 : (int)item.hf);
                    patientInfo.patiType = item.brxzmc ?? "";
                    patientInfo.patiTypeCode = "";
                    patientInfo.telephone = item.phone ?? "";

                    if (new[] { "99", "" }.Contains(patientInfo.cardTypeCode) && string.IsNullOrWhiteSpace(patientInfo.cardNo))
                    {
                        patientInfo.cardNo = item.CardNo;
                    }
                }
                patientInfo.countryCode = "";
                patientInfo.country = "";
                patientInfo.nationalityCode = "";
                patientInfo.nationality = "";
                patientInfo.provinceCode = "";
                patientInfo.province = "";
                patientInfo.cityCode = "";
                patientInfo.city = "";
                patientInfo.area = "";
                patientInfo.ctRoleId = "";
                patientInfo.orgCode = objConfig.jgdm ?? "";
                patientInfo.areaCode = objConfig.areaCode;
                var dapper = new TcmHis01(patientInfo);
                var execResult = dapper.Execute();
                result = execResult as Result;
                InsertCmmHis01(dapper.Request, organizeId, userCode, result);
            }
            catch (Exception e)
            {
                result = new Result
                {
                    code = "0",
                    desc = e.Message + (e.InnerException == null ? "" : e.InnerException.Message)
                };
                LogCore.Error("OutpatientCmmManagerApp TcmHis01 error", e, result.ToJson());
            }
            return result;
        }

        /// <summary>
        /// 判断该患者是否已上传
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        private bool CheckIsUpload(string mzh, string organizeId)
        {
            var entity = _cmmHis01Repo.SelectDataByMzh(mzh, organizeId);
            return entity != null && !string.IsNullOrWhiteSpace(entity.Id);
        }

        /// <summary>
        /// 落地请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="result"></param>
        private void InsertCmmHis01(PushPatientRequestEntity request, string organizeId, string userCode, Result result)
        {
            result = result ?? new Result();
            var entity = new CmmHis01Entity
            {
                Id = Guid.NewGuid().ToString(),
                cmmId = request.Patient.id,
                name = request.Patient.name,
                gender = request.Patient.gender,
                genderCode = request.Patient.genderCode,
                birthday = request.Patient.birthday,
                cardTypeCode = request.Patient.cardTypeCode,
                cardType = request.Patient.cardType,
                cardNo = request.Patient.cardNo,
                outpatientNo = request.Patient.outpatientNo,
                occupation = request.Patient.occupation,
                occupationCode = request.Patient.occupationCode,
                marriedCode = request.Patient.marriedCode,
                married = request.Patient.married,
                countryCode = request.Patient.countryCode,
                country = request.Patient.country,
                nationality = request.Patient.nationality,
                nationalityCode = request.Patient.nationalityCode,
                province = request.Patient.province,
                provinceCode = request.Patient.provinceCode,
                city = request.Patient.city,
                cityCode = request.Patient.cityCode,
                areaCode = request.Patient.areaCode,
                area = request.Patient.area,
                streetInfo = request.Patient.streetInfo,
                postcode = request.Patient.postcode,
                ctRoleId = request.Patient.ctRoleId,
                ctAddr = request.Patient.ctAddr,
                ctName = request.Patient.ctName,
                ctTelephone = request.Patient.ctTelephone,
                patiTypeCode = request.Patient.patiTypeCode,
                patiType = request.Patient.patiType,
                telephone = request.Patient.telephone,
                orgCode = request.Patient.orgCode,
                sender = request.Header.sender,
                receiver = request.Header.receiver,
                sendTime = request.Header.sendTime,
                msgType = request.Header.msgType,
                msgID = request.Header.msgID,
                OrganizeId = organizeId,
                resultCode = result.code,
                resultDesc = result.desc,
                zt = "1",
                CreatorCode = userCode,
                CreateTime = DateTime.Now
            };
            _cmmHis01Repo.Insert(entity);
        }
        #endregion

        #region TCM_HIS_02

        /// <summary>
        /// 同步药品
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string PushMedicineInfo(string organizeId, string userCode)
        {
            var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", organizeId);
            if (string.IsNullOrWhiteSpace(confInfo)) throw new Exception("请配置中医馆行政机构信息，编码为cmm_configuration");
            var objConfig = confInfo.ToObject<ConfigurationInfo>();
            if (objConfig == null) throw new Exception("中医馆行政机构信息配置有误，请注意结构！");

            var record = _comCmmHis02RecordDmnService.SelectNoSyncMedicines(30, organizeId);
            if (record == null || record.Count <= 0) return "";

            var cts = new CancellationTokenSource();
            var parent = new Task(() =>
              {
                  var childFactory = new TaskFactory(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                  record.ForEach(p =>
                  {
                      childFactory.StartNew(() =>
                      {
                          SyncMedicines(p, organizeId, userCode, objConfig);
                      }, cts.Token);

                  });
              });
            parent.Start();
            parent.Wait(cts.Token);
            if (!cts.IsCancellationRequested) return "";
            LogCore.Fatal("PushMedicineInfo IsCancellationRequested", message: record.ToJson());
            return "推送中草药失败";
        }

        /// <summary>
        /// 同步药品，并保存同步记录表
        /// </summary>
        /// <param name="yp"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="config"></param>
        private void SyncMedicines(SysMedicineExVEntity yp, string organizeId, string userCode, ConfigurationInfo config)
        {
            var cmmHis02Entity = new CmmHis02RecordEntity
            {
                activeFlg = "Y",
                code = yp.ypCode,
                cmmId = yp.ypCode,
                manufacturer = yp.ycmc,
                name = yp.ypmc,
                orgCode = config.jgdm,
                specification = yp.gg,
                OrganizeId = organizeId,
                zt = "1",
                CreatorCode = userCode,
                CreateTime = DateTime.Now
            };
            var medicine = new Medicine
            {
                activeFlg = "Y",
                code = yp.ypCode,
                id = yp.ypCode,
                manufacturer = yp.ycmc,
                name = yp.ypmc,
                orgCode = config.jgdm,
                specification = yp.gg
            };
            var response = new TcmHis02(medicine).Execute();
            var result = response as Result;
            if (result != null)
            {
                cmmHis02Entity.resultCode = result.code;
                cmmHis02Entity.resultDesc = result.desc;
            }

            new CmmHis02RecordRepo(new DefaultDatabaseFactory()).Insert(cmmHis02Entity);
        }

        #endregion

        #region TCM_HIS_07

        /// <summary>
        /// 提取电子病历
        /// </summary>
        /// <param name="mzh">门诊号</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public Record ExtractIntegrateEmr(string mzh, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(mzh)) return null;
            var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", organizeId);
            if (string.IsNullOrWhiteSpace(confInfo)) throw new Exception("请配置中医馆行政机构信息，编码为cmm_configuration");
            var objConfig = confInfo.ToObject<ConfigurationInfo>();
            if (objConfig == null) throw new Exception("中医馆行政机构信息配置有误，请注意结构！");
            var request = new Receive
            {
                serialNo = mzh,
                orgCode = objConfig.jgdm
            };
            var response = new TcmHis07(request).Execute();
            var record = response as Record;
            return record;
        }
        #endregion

        #region TCM_HIS_08

        /// <summary>
        /// 提取处方
        /// </summary>
        /// <param name="mzh">门诊号</param>
        /// <param name="organizeId"></param>
        /// <param name="mzzybz">门诊住院标志 0-药库  1-门诊  2-住院  3-通用(取门诊单位) </param>
        /// <returns></returns>
        public List<DrugDataEx> ExtractIntegrateRp(string mzh, string organizeId, string mzzybz)
        {
            if (string.IsNullOrWhiteSpace(mzh)) return null;
            var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", organizeId);
            if (string.IsNullOrWhiteSpace(confInfo)) throw new Exception("请配置中医馆行政机构信息，编码为cmm_configuration");
            var objConfig = confInfo.ToObject<ConfigurationInfo>();
            if (objConfig == null) throw new Exception("中医馆行政机构信息配置有误，请注意结构！");
            var request = new Receive
            {
                serialNo = mzh,
                orgCode = objConfig.jgdm
            };
            var response = new TcmHis08(request).Execute();
            var prescription = response as Prescription;
            return FormatRpDetail(prescription, organizeId, mzzybz);
        }

        /// <summary>
        /// 规范处方信息
        /// </summary>
        /// <returns></returns>
        private List<DrugDataEx> FormatRpDetail(Prescription s, string organizeId, string mzzybz)
        {
            var result = new List<DrugDataEx>();
            if (s == null || s.drugDataList == null || s.drugDataList.Count == 0) return result;

            var errorMsg = "";
            var cts = new CancellationTokenSource();
            var parent = new Task(() =>
            {
                var cLocker = new object();
                var childFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                s.drugDataList.ForEach(p =>
                {
                    var childTask = childFactory.StartNew(() =>
                    {
                        if (cts.IsCancellationRequested)
                        {
                            return false;
                        }

                        try
                        {

                            var reqDto = new SelectSfxmYpFilterDTO
                            {
                                topCount = 50,
                                orgId = organizeId,
                                mzzybz = mzzybz,
                                dllb = "1",
                                sfdllx = "TCM",
                                keyword = p.drugCode,
                                dlCode = "",
                                isContansChildDl = true,
                                useypckflag = true,
                                ypyfbmCode = null,
                                containyp0ck = true,
                                onlyybflag = false,
                                isQyKssKZ = false,
                                qxjb = null
                            };
                            var yp = new FrameworkBase.MultiOrg.DmnService.SystemManage.BaseDataDmnService(new DefaultDatabaseFactory()).SelectSfxmYp(reqDto);
                            if (yp != null && yp.Count > 0)
                            {
                                var item = yp.FirstOrDefault().ToJson().ToObject<DrugDataEx>();
                                if (item == null)
                                {
                                    errorMsg = "药品信息转化失败";
                                    return false;
                                }

                                if (!"g".Equals((item.dw ?? "").Trim()))
                                {
                                    errorMsg = string.Format("【{0}】缺少单位g", item.sfxmmc);
                                    cts.Cancel();
                                    return false;
                                }
                                item.drugQuantity = p.drugQuantity;
                                item.drugUnit = p.drugUnit;
                                item.note = p.note;
                                item.drugName = p.drugName;
                                item.drugCode = p.drugCode;
                                lock (cLocker)
                                {
                                    result.Add(item);
                                }

                                return true;
                            }
                            errorMsg = "匹对本地药品信息失败";
                            cts.Cancel();
                            return false;

                        }
                        catch (Exception e)
                        {
                            errorMsg = e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
                            cts.Cancel();
                            return false;
                        }
                    }, cts.Token);

                });
            });
            parent.Start();
            parent.Wait(cts.Token);
            if (cts.IsCancellationRequested)
            {
                LogCore.Fatal("FormatRpDetail  fail", message: s.ToJson());
                throw new Exception(errorMsg);
            }
            return result;
        }
        #endregion

        #region TCM_HIS_09

        /// <summary>
        /// 提取诊断信息
        /// </summary>
        /// <param name="mzh">门诊号</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public DiagInfo ExtractIntegrateDiagnosis(string mzh, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(mzh)) return null;
            var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", organizeId);
            if (string.IsNullOrWhiteSpace(confInfo)) throw new Exception("请配置中医馆行政机构信息，编码为cmm_configuration");
            var objConfig = confInfo.ToObject<ConfigurationInfo>();
            if (objConfig == null) throw new Exception("中医馆行政机构信息配置有误，请注意结构！");
            var request = new Receive
            {
                serialNo = mzh,
                orgCode = objConfig.jgdm
            };
            var response = new TcmHis09(request).Execute();
            var diagInfo = response as DiagInfo;
            AddLocalZddm(diagInfo, organizeId);
            return diagInfo;
        }

        /// <summary>
        /// 附加本地诊断代码
        /// </summary>
        /// <param name="diagInfo"></param>
        /// <param name="organizeId"></param>
        private void AddLocalZddm(DiagInfo diagInfo, string organizeId)
        {
            if (diagInfo == null || diagInfo.diagnosislist == null || diagInfo.diagnosislist.Count <= 0) return;
            var cts = new CancellationTokenSource();
            var parent = new Task(() =>
            {
                var childFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                diagInfo.diagnosislist.ForEach(p =>
                {
                    var childTask = childFactory.StartNew(() =>
                    {
                        var zdRepo =
                            new SysDiagnosisRepo(new DefaultDatabaseFactory()).SelectDataByICD10(organizeId, p.code);
                        if (zdRepo == null) return false;
                        p.localCode = zdRepo.zdCode;
                        return true;
                    }, cts.Token);
                });
            });
            parent.Start();
            parent.Wait();
            if (cts.IsCancellationRequested)
            {
                LogCore.Fatal("AddLocalZddm CancellationRequested", message: diagInfo.ToJson());
            }
        }
        #endregion

        #region Integrated Module

        /// <summary>
        /// 电子病历集成模块
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="jzys"></param>
        /// <param name="organizeId"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public string GetIntegrateEmrUrl(string mzh, string jzys, string organizeId, out string response)
        {
            response = "";
            var entity = _cmmHis01Repo.SelectDataByMzh(mzh, organizeId);
            if (entity == null || string.IsNullOrWhiteSpace(entity.Id)) return "尚未推送患者信息至中医馆，请先选择患者";
            var emrUrlRequest = new EMRRequestEntity
            {
                resource = "EMR_WRITE",
                doctorId = jzys,
                patientId = entity.cmmId
            };
            response = CmmProxyCenter<EMRRequestEntity, string>.Execute(emrUrlRequest);
            return "";
        }

        /// <summary>
        /// 辩证论治
        /// </summary>
        /// <param name="aeRequest"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public string GetIntegrateAeUrl(GetAeRequestDto aeRequest, out string response)
        {
            response = "";
            if (aeRequest == null || string.IsNullOrWhiteSpace(aeRequest.mzh)) return "门诊号不能为空";
            if (string.IsNullOrWhiteSpace(aeRequest.organizeId)) return "组织机构不能为空";
            if (string.IsNullOrWhiteSpace(aeRequest.jzys)) return "就诊医生工号不能为空";
            var entity = _cmmHis01Repo.SelectDataByMzh(aeRequest.mzh, aeRequest.organizeId);
            if (entity == null || string.IsNullOrWhiteSpace(entity.Id)) return "尚未推送患者信息至中医馆，请先选择患者";

            var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", aeRequest.organizeId);
            if (string.IsNullOrWhiteSpace(confInfo)) return "请配置中医馆行政机构信息，编码为cmm_configuration";
            var objConfig = confInfo.ToObject<ConfigurationInfo>();
            if (objConfig == null) return "中医馆行政机构信息配置有误，请注意结构！";
            var staff = _sysStaffRepo.GetValidStaffByGh(aeRequest.jzys, aeRequest.organizeId);

            var returnHisUrl = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("returnHisUrl");
            if (string.IsNullOrWhiteSpace(returnHisUrl)) return "请配置返回数据给 HIS 的代理页面地址——key：returnHisUrl";
            var aeUrlRequest = new AERequestEntity
            {
                resource = "AE_EDIT",
                orgCode = objConfig.jgdm,
                chisZggh = aeRequest.jzys,
                chisEmpName = staff.Name,
                clinicId = aeRequest.mzh,
                brxm = entity.name,
                sex = entity.genderCode,
                patientid = entity.cmmId,
                returnHisUrl = returnHisUrl
            };
            response = CmmProxyCenter<AERequestEntity, string>.Execute(aeUrlRequest);
            return "";
        }

        /// <summary>
        /// 知识库
        /// </summary>
        /// <returns></returns>
        public string GetIntegrateKUrl(out string response)
        {
            var kRequest = new KRequestEntity
            {
                resource = "K_LOOK"
            };
            response = CmmProxyCenter<KRequestEntity, string>.Execute(kRequest);
            return "";
        }

        /// <summary>
        /// 集成治未病
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <param name="organizeId"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public string GetIntegrateHEALUrl(string blh, string mzh, string zjlx, string zjh, string organizeId, out string response)
        {
            response = "";
            if (string.IsNullOrWhiteSpace(mzh)) return "信息有误，门诊号不能为空";
            var entity = _cmmHis01Repo.SelectDataByMzh(mzh, organizeId);
            if (entity == null || string.IsNullOrWhiteSpace(entity.cardTypeCode) ||
                string.IsNullOrWhiteSpace(entity.cardNo))
            {
                return "信息不全，推送患者时缺少证件类型和证件号";
            }

            int tzjlx = Convert.ToInt32(entity.cardTypeCode);
            var request = new HEALRequestEntity
            {
                resource = "HEAL_CHECKBODY",
                CertificatesType = tzjlx.ToString(),
                CertificatesNumber = entity.cardNo
            };
            response = CmmProxyCenter<HEALRequestEntity, string>.Execute(request);
            return "";
        }

        /// <summary>
        /// 组装远程医疗参数
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="mzh"></param>
        /// <param name="jzys"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IntegrateRcDto GetIntegrateRCRequsetParam(string blh, string mzh, string jzys, string organizeId)
        {
            var confInfo = _sysConfigRepo.GetValueByCode("cmm_configuration", organizeId);
            if (string.IsNullOrWhiteSpace(confInfo)) throw new Exception("请配置中医馆行政机构信息，编码为cmm_configuration");
            var objConfig = confInfo.ToObject<ConfigurationInfo>();
            if (objConfig == null) throw new Exception("中医馆行政机构信息配置有误，请注意结构！");
            if (string.IsNullOrWhiteSpace(objConfig.areaCode) || string.IsNullOrWhiteSpace(objConfig.jgdm)) throw new Exception("中医馆行政机构信息配置机构代码和区域代码不能为空！");

            if (string.IsNullOrWhiteSpace(blh)) throw new Exception("请选择患者");
            if (string.IsNullOrWhiteSpace(mzh)) throw new Exception("数据异常，门诊号丢失");
            var brxx = _baseDataDmnService.SelectXtBrjbxx(blh, mzh, organizeId);
            if (brxx == null || brxx.Count <= 0) throw new Exception(string.Format("根据病历号【{0}】获取病人基本信息失败", blh));
            var patientBaseInfo = brxx.FirstOrDefault();
            var staff = _sysStaffRepo.GetValidStaffByGh(jzys, organizeId);

            var result = new IntegrateRcDto
            {
                organCode = objConfig.jgdm,
                doctorId = jzys,
                doctorName = staff.Name,
                patientId = blh,
                patientName = patientBaseInfo.xm,
                mobile = patientBaseInfo.phone,
                gender = patientBaseInfo.xb,
                birthday = patientBaseInfo.csny != null ? ((DateTime)patientBaseInfo.csny).ToString("yyyy-MM-dd") : "",
                patientType = CIS.Proxy.Tools.ChangeBrxzCode(patientBaseInfo.brxz ?? ""),
                series = patientBaseInfo.CardNo,
                certid = "",
            };
            if ("1".Equals((patientBaseInfo.zjlx ?? "").Trim()))
            {
                result.certid = patientBaseInfo.zjh;
            }
            return result;
        }
        #endregion
    }
}