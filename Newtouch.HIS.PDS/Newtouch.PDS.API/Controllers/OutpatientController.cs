using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.PDS.Requset;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// 门诊管理
    /// </summary>
    [RoutePrefix("api/Outpatient")]
    public class OutpatientController : ApiControllerBase<OutpatientController>
    {
        private readonly IPatientBaseInfoApp _patientBaseInfoApp;

        public OutpatientController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// 更新门诊处方患者信息
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SupplementPatientBaseInfo")]
        public HttpResponseMessage SupplementPatientBaseInfo(SupplementPatientBaseInfoRequest par)
        {
            Action<SupplementPatientBaseInfoRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _patientBaseInfoApp.SupplementMzPatientBaseInfo(par);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, par);
            return CreateResponse(result);
        }
    }
}