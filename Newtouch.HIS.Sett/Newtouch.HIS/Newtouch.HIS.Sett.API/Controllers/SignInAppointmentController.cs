using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Attributes;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Sett.Request;
using Newtouch.HIS.Sett.Request.Patient;

namespace Newtouch.HIS.Sett.API.Controllers
{
    /// <summary>
    /// 预约签到
    /// </summary>
    [RoutePrefix("api/SignInAppointment")]
    [DefaultAuthorize]
    public class SignInAppointmentController : ApiControllerBase<SignInAppointmentController>
    {
        public SignInAppointmentController(IComponentContext com)
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
        /// 签到
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreTokenDecrypt]
        [Route("PatientAppointment")]
        public HttpResponseMessage PatientAppointment(PatientAppointmentRequest par)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                    bool cnt = _patientBasicInfoDmnService.PatientAppointment(par.mzh, par.orgId);
                    resp.data = null;
                    resp.code = ResponseResultCode.SUCCESS;
                    if (!cnt)
                    {
                        resp.code = ResponseResultCode.FAIL;
                        resp.msg = "签到失败,请重试";
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
        /// 签到状态修改
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreTokenDecrypt]
        [Route("SignInState")]
        public HttpResponseMessage SignInState(SignInStateRequest par)
        {
            DefaultResponse resp = new DefaultResponse();
            try
            {
                bool cnt = _patientBasicInfoDmnService.SignInStateUpdate(par.mzh, par.calledstu, par.yhcode, par.orgId);
                resp.data = null;
                resp.code = ResponseResultCode.SUCCESS;
                if (!cnt)
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.msg = "签到状态修改失败,请重试";
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
