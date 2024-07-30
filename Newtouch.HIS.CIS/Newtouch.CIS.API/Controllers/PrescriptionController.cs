using Autofac;
using Newtouch.CIS.APIRequest.Prescription;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.API.Controllers
{
    public class PrescriptionController : ApiControllerBase<PrescriptionController>
    {
        private readonly IPrescriptionRepo _IPrescriptionRepo;
        public PrescriptionController(IComponentContext com)
            : base(com)
        {
        }

        /// <summary>
        /// 更新处方收费标志
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateChangeStatus")]
        public HttpResponseMessage UpdateChangeStatus(PrescriptionChargeRequest par)
        {
            Action<PrescriptionChargeRequest, DefaultResponse> ac = (req, resp) =>
            {
                _IPrescriptionRepo.UpdateChangeStatus(par.OrganizeId, par.cfh, par.sfbz);

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }
        
    }
}