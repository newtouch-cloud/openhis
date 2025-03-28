using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Attributes;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.OR.ManageSystem.APIRequest;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;

namespace Newtouch.OR.ManageSystem.API.Controllers
{
    [RoutePrefix("api/ORPatient")]
    [DefaultAuthorize]
    public class ORPatientController : ApiControllerBase<ORPatientController>
    {
        private readonly IOPRegisterDmnService _opRegisterDmnService;
        public ORPatientController(IComponentContext com)
            : base(com)
        {
        }

        [HttpPost]
        //[IgnoreTokenDecrypt]
        [Route("ORPatList")]
        public HttpResponseMessage ORPatList(QueryRequest par)
        {
            Action<QueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                var list = _opRegisterDmnService.GetPatOpRegistList(par.ksrq, par.jsrq, par.zyh, par.djzt, par.OrganizeId);
                resp.data = list;
                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
    }
}
