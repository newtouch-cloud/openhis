using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.Application.Interface;
using Newtouch.CIS.APIRequest.Dto;
using Newtouch.HIS.API.Common;

namespace NewtouchCIS.API.Controllers
{
    /// <summary>
    /// 内部通用接口
    /// </summary>
    [RoutePrefix("api/InsideGeneral")]
    public class InsideGeneralController : ApiControllerBase<InsideGeneralController>
    {
        private readonly IInsideGeneralApp _insideGeneralApp;
        public InsideGeneralController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// mq调用入口
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MqGeneralTask")]
        public HttpResponseMessage MqGeneralTask(MqGeneralTaskRequestDto requestDto)
        {
            Action<MqGeneralTaskRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                _insideGeneralApp.PublicPortal(requestDto);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, requestDto));
        }
    }
}