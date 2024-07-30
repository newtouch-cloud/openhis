using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Attributes;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Sett.Request;
using Newtouch.HIS.Sett.Request.Patient;

namespace Newtouch.HIS.Sett.API.Controllers
{
    /// <summary>
    /// 患者相关
    /// </summary>
    [RoutePrefix("api/Patient")]
    [DefaultAuthorize]
    public class PatientController : ApiControllerBase<PatientController>
    {
        public PatientController(IComponentContext com)
            : base(com)
        {
        }

        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly ISysPatientMedicalRecordDmnService _sysPatientMedicalRecordDmnService;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly IHospMultiDiagnosisRepo _hospMultiDiagnosisRepo;
        private readonly ISysPatBasicInfoApp _sysPatBasicInfoApp;

        /// <summary>
        /// 门诊挂号信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutPatientRegistrationQuery")]
        public HttpResponseMessage OutPatientRegistrationQuery(OutPatientRegistrationQueryRequest par)
        {
            Action<OutPatientRegistrationQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                string flag = null;
                string msg = null;
                var list = _patientBasicInfoDmnService.OutPatientRegistrationQuery(this.UserIdentity.OrganizeId
                    , ref flag, ref msg, par.lastUpdateTime, par.outpatientNo
                    , par.ksCode, par.ysgh
                    , par.mjzbz
                    , par.jiuzhenbz
                    , par.keyword
                    , par.zzhz
                    , par.pagination);

                //这是分页查询
                resp.data = new
                {
                    pagination = par.pagination,
                    list = list
                };

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 更新门诊病人（门诊挂号）就诊状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutPatientUpdateConsultationStatus")]
        public HttpResponseMessage OutPatientUpdateConsultationStatus(OutPatientUpdateConsultationStatusRequest par)
        {
            Action<OutPatientUpdateConsultationStatusRequest, DefaultResponse> ac = (req, resp) =>
            {
                _outpatientRegistRepo.UpdateConsultationStatus(this.UserIdentity.OrganizeId
                    , par.outpatientNo, par.jiuzhenbz, par.jzys);

                resp.data = null;

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 更新门诊病人（门诊挂号）诊断信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutPatientUpdateDiagnosis")]
        public HttpResponseMessage OutPatientUpdateDiagnosis(OutPatientUpdateDiagnosisRequest par)
        {
            Action<OutPatientUpdateDiagnosisRequest, DefaultResponse> ac = (req, resp) =>
            {
                _outpatientRegistRepo.UpdateDiagnosis(this.UserIdentity.OrganizeId
                    , par.outpatientNo, par.zdicd10, par.zdmc);

                resp.data = null;

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 病历查询（历史病历）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientMedicalRecordQuery")]
        public HttpResponseMessage PatientMedicalRecordQuery(PatientMedicalRecordQueryRequest par)
        {
            Action<PatientMedicalRecordQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                var list = _sysPatientMedicalRecordDmnService.GetMedicalRecordList(this.UserIdentity.OrganizeId, req.blh, req.blId);

                resp.data = list;

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 住院患者查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InPatientQuery")]
        public HttpResponseMessage InPatientQuery(InPatientQueryRequest par)
        {
            Action<InPatientQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                string flag = null;
                string msg = null;
                var list = _patientBasicInfoDmnService.InPatientQuery(this.UserIdentity.OrganizeId
                    , ref flag, ref msg, par.lastUpdateTime, par.zyh
                    , par.bqCode, par.zybz
                    , par.keyword
                    , par.pagination);

                //这是分页查询
                resp.data = new
                {
                    pagination = par.pagination,
                    list = list
                };

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 住院患者详情查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InPatientDetailQuery")]
        public HttpResponseMessage InPatientDetailQuery(InPatientDetailQueryRequest par)
        {
            Action<InPatientDetailQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                string flag = null;
                string msg = null;
                var list = _patientBasicInfoDmnService.InPatientQuery(this.UserIdentity.OrganizeId
                    , ref flag, ref msg, null, par.zyh);

                //这是分页查询
                resp.data = list == null && list.Count > 0 ? null : list[0];

                if (resp.data != null)
                {
                    resp.code = ResponseResultCode.SUCCESS;
                }
                else
                {
                    resp.code = ResponseResultCode.ERROR;
                    resp.sub_code = "NOT_FOUND";
                    resp.sub_msg = "NOT_FOUND";
                }
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 更新住院病人在院标志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateInpatientStatus")]
        public HttpResponseMessage UpdateInpatientStatus(UpdateInpatientStatusRequest par)
        {
            Action<UpdateInpatientStatusRequest, DefaultResponse> ac = (req, resp) =>
            {
                _hospPatientBasicInfoRepo.UpdateInpatientStatus(this.UserIdentity.OrganizeId, req.zyh, req.zybz);

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        [HttpPost]
        [Route("UpdateInpatientOutInfo")]
        public HttpResponseMessage UpdateInpatientOutInfo(UpdateInpatientOutInfoRequest pat)
        {
            Action<UpdateInpatientOutInfoRequest, DefaultResponse> ac = (req, resp) =>
            {

                _hospPatientBasicInfoRepo.UpdateInpatientOutInfoRequest(this.UserIdentity.OrganizeId, req.zyh, req.zybz, req.bq, req.cw, req.cyrq, req.doctor, req.cyzd);

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, pat);

            return base.CreateResponse(response);
        }

        [HttpPost]
        [Route("UpdateInpatientOutRecallInfo")]
        public HttpResponseMessage UpdateInpatientOutRecallInfo(UpdateInpatientOutInfoRequest pat)
        {
            Action<UpdateInpatientOutInfoRequest, DefaultResponse> ac = (req, resp) =>
            {

                _hospPatientBasicInfoRepo.UpdateInpatientOutRecallInfoRequest(this.UserIdentity.OrganizeId, req.zyh, req.zybz, req.bq, req.cw, req.cyrq, req.doctor, req.cyzd);

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, pat);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 入院诊断查询
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientRyDiagnosisQuery")]
        public HttpResponseMessage PatientRyDiagnosisQuery(InPatientDetailQueryRequest pat)
        {
            Action<InPatientDetailQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _hospMultiDiagnosisRepo.SelectData(req.zyh, req.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = CommonExecute(ac, pat);
            return CreateResponse(response);
        }

        /// <summary>
        /// 修改入院诊断
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ModifyRyDiagnosis")]
        public HttpResponseMessage ModifyRyDiagnosis(ModifyRyDiagnosisRequestDTO pat)
        {
            Action<ModifyRyDiagnosisRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                _sysPatBasicInfoApp.ModifyRyDiagnosis(req);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = CommonExecute(ac, pat);
            return CreateResponse(response);
        }
        
        /// <summary>
        /// 住院病人信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InPatientInfoQuery")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage InPatientInfoQuery(InPatientInfoQueryRequest req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _patientBasicInfoDmnService.InPatientInfoQuery(req);
                    foreach(var l in list) {
                        var ryzd = _patientBasicInfoDmnService.getRyzdByZyh(l.zyh,req.HospitalID);
                        l.ryzd= ryzd;
                        var cyzd= _patientBasicInfoDmnService.getCyzdByZyh(l.zyh, req.HospitalID);
                        l.cyzd = cyzd;
                    }
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到相关记录";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }

        /// <summary>
        /// 门诊就诊列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutPatientConsultationQuery")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage OutPatientConsultationQuery(OutPatientConsultationQueryRequest req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _patientBasicInfoDmnService.OutPatientConsultationQuery(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到相关记录";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }

        /// <summary>
        /// 住院一日清
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InpatientDayFee")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage InpatientDayFee(InpatientDayFeeRequest req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _patientBasicInfoDmnService.InpatientDayFee(req);
                    InpatientDayFeeResp DayFeeResp = new InpatientDayFeeResp();
                    DayFeeResp.zyh = req.zyh;
                    DayFeeResp.zje = list.Count > 0 ? list[0].zje : 0;
                    DayFeeResp.data = new List<InpatientDayFeeRQ>();
                    foreach (var l in list) {
                        var rqflag = -1;
                        InpatientDayFeeDL dlObj = new InpatientDayFeeDL();
                        dlObj.dlmc = l.dlmc;
                        dlObj.dlje = l.dlje;
                        for (var i = 0; i < DayFeeResp.data.Count; i++) {
                            if (DayFeeResp.data[i].rq.IndexOf(l.rq) != -1)
                            {//存在该日期
                                rqflag = i;
                            }
                        }
                        if (rqflag == -1)
                        {//日期不存在，创建该日期
                            InpatientDayFeeRQ rqObj = new InpatientDayFeeRQ();
                            rqObj.data = new List<InpatientDayFeeDL>();
                            rqObj.rq = l.rq;
                            rqObj.drzje = l.drje;
                            rqObj.data.Add(dlObj);
                            DayFeeResp.data.Add(rqObj);
                        }
                        else {
                            DayFeeResp.data[rqflag].data.Add(dlObj);
                        }
                       
                    }
                    resp.data = DayFeeResp;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到相关记录";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }

        [HttpPost]
        [Route("MZhistorybill")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage MZhistorybill(MZhistorybillRequest req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _patientBasicInfoDmnService.MZhistorybill(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到相关记录";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }

        [HttpPost]
        [Route("MZhistorybillMX")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage MZhistorybillMX(MZhistorybillMXRequest req)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    resp.msg = "请求数据不完整：" + ModelState.Values.First().Errors[0].ErrorMessage;
                    resp.code = ResponseResultCode.FAIL;
                }
                else
                {
                    var list = _patientBasicInfoDmnService.MZhistorybillMX(req);
                    resp.data = list;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (list.Count == 0)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "未找到相关记录";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.msg = ex.Message;
                resp.code = ResponseResultCode.FAIL;
            }
            return base.CreateResponse(resp);
        }

    }
}
