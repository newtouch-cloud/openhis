using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Attributes;
using Newtouch.HIS.Base.HOSP.Request;
using Newtouch.HIS.Domain.IRepository;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>
    /// 病区相关
    /// </summary>
    [RoutePrefix("api/Ward")]
    [DefaultAuthorize]
    public class WardController : ApiControllerBase<WardController>
    {
        private readonly ISysWardBedRepo _sysWardBedRepo;

        public WardController(IComponentContext com)
       : base(com)
        {

        }

        /// <summary>
        /// 更新床位是否占用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("UpdateBedOccupy")]
        [HttpPost]
        public HttpResponseMessage UpdateBedOccupy(UpdateBedOccupyRequest request)
        {
            Action<UpdateBedOccupyRequest, DefaultResponse> ac = (req, resp) =>
            {
                _sysWardBedRepo.UpdateOccupyByCode(req.cwCode, this.UserIdentity.OrganizeId, req.isOccupy);

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, request);

            return base.CreateResponse(response);
        }

    }
}