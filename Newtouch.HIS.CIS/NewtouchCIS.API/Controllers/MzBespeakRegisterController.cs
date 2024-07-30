using Autofac;
using Newtouch.CIS.APIRequest.Dto;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.HIS.API.Common;
using System;
using System.Net.Http;
using System.Web.Http;

namespace NewtouchCIS.API.Controllers
{
    /// <summary>
    /// 授权认证
    /// </summary>
    [RoutePrefix("api/MzBespeakRegister")]
    public class MzBespeakRegisterController : ApiControllerBase<MzBespeakRegisterController>
    {
        private readonly IMzyyghRepo _mzyyghRepo;
        private readonly IMzyyghDmnService _mzyyghDmnService;
        private readonly ISysBespeakRegisterDmnService _sysBespeakRegisterDmnService;

        public MzBespeakRegisterController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// 当前患者门诊预约查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MzBespeakRegisterQuery")]
        public HttpResponseMessage MzBespeakRegisterQuery(MzBespeakRegisterQueryRequestDTO request)
        {
            Action<MzBespeakRegisterQueryRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _mzyyghDmnService.MzBespeakRegisterQuery(req.blh, req.kh, req.zjh, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, request);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// 科室或专家预约挂号排班信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MzBespeakRegisterSchedulingQuery")]
        public HttpResponseMessage MzBespeakRegisterSchedulingQuery(SysBespeakRegisterQueryRequestDTO request)
        {
            Action<SysBespeakRegisterQueryRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _sysBespeakRegisterDmnService.SelectSysBespeakregister(req.ksCode, req.ysgh, UserIdentity.OrganizeId, req.regDate, req.regTime);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, request);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// 科室或专家已挂号总数查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MzAlreadyBespeakRegisterCountQuery")]
        public HttpResponseMessage MzAlreadyBespeakRegisterCountQuery(MzAlreadyBespeakRegisterCountQueryRequestDTO request)
        {
            Action<MzAlreadyBespeakRegisterCountQueryRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _mzyyghDmnService.SelectAlreadyBespeakRegisterCount(req.ksCode, req.ysgh, req.regDate, req.regTime, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, request);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// 赴约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("KeepAnAppointment")]
        public HttpResponseMessage KeepAnAppointment(KeepAnAppointmentRequestDTO request)
        {
            Action<KeepAnAppointmentRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _mzyyghRepo.KeepAnAppointment(req.mzyyghId, req.arrivalDate, req.arrivalOpereater);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, request);
            return base.CreateResponse(response);
        }
    }
}