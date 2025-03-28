using Autofac;
using Newtouch.EMR.APIRequest.Bljgh.Request;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Newtouch.EMR.API.Controllers
{
    [RoutePrefix("api/BljgData")]
    public class BljgDataController : ApiControllerBase<BljgDataController>
    {
        public BljgDataController(IComponentContext com)
           : base(com)
        {

        }
        private readonly Ibl_ElementDomainRepo _bljsRepo;
        private readonly Ibl_ElementDomain_DetailRepo _bljsmxRepo;
        private readonly Ibl_bllxRepo _bllxRepo;
        [HttpPost]
        [Route("BljghDataDealwith")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage BljghDataDealwith(BljghReq par)
        {

            par.Timestamp = DateTime.Now;
            Action<BljghReq, DefaultResponse> ac = (req, resp) =>
            {
                _bljsRepo.BljghDataDealwith(par);
                resp.data = null;
                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }
    }
}