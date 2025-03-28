using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.CIS.APIRequest.Dto;
using Newtouch.Domain.IDomainServices;
using Newtouch.HIS.API.Common;

namespace NewtouchCIS.API.Controllers
{
    /// <summary>
    /// 授权认证
    /// </summary>
    [RoutePrefix("api/PatientSeekingInfo")]
    public class PatientTreatmentInfoController : ApiControllerBase<PatientTreatmentInfoController>
    {
        private readonly ITreatmentDmnService _treatmentDmnService;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="com"></param>
        public PatientTreatmentInfoController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// 患者就诊信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientTreatmentInfoQuery")]
        public HttpResponseMessage PatientTreatmentInfoQuery(PatientTreatmentInfoQueryRequestDTO request)
        {
            Action<PatientTreatmentInfoQueryRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _treatmentDmnService.SelectTreatmentEntities(req.mzh, req.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = CommonExecute(ac, request);
            return CreateResponse(response);
        }
    }
}