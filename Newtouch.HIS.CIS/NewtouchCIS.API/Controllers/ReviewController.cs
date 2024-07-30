using Autofac;
using Newtouch.CIS.APIRequest.Prescription;
using Newtouch.Domain.IDomainServices;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Attributes;
using System;
using System.Net.Http;
using System.Web.Http;

namespace NewtouchCIS.API.Controllers
{
    [RoutePrefix("api/Review")]
    [DefaultAuthorize]
    public class ReviewController : ApiControllerBase<ReviewController>
    {
        private readonly IDoctorserviceDmnService _doctorserviceDmnService;
        public ReviewController(IComponentContext com)
            : base(com)
        {
        }
        /// <summary>
        /// 调用病案智能审核
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetBaShData")]
        public HttpResponseMessage GetBaShData(ReviewRequest par)
        {
            Action<ReviewRequest, DefaultResponse> ac = (req, resp) =>
            {
                _doctorserviceDmnService.GetBashData(par.orgId, par.zyh, par.rygh, par.username,par.GetMAC);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
    }
}