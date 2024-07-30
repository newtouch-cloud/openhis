using Autofac;
using Newtouch.CIS.APIRequest.Prescription;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Attributes;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.HIS.Domain.IRepository;
using System;
using System.Net.Http;
using System.Web.Http;

namespace NewtouchCIS.API.Controllers
{
    [RoutePrefix("api/Prescription")]
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
        [Route("UpdateChargeStatus")]
        public HttpResponseMessage UpdateChargeStatus(PrescriptionChargeRequest par)
        {
            Action<PrescriptionChargeRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (par.cfList != null)
                {
                    foreach (var item in par.cfList)
                    {
                        _IPrescriptionRepo.UpdateChargeStatus(this.UserIdentity.OrganizeId, item.cfh, par.sfbz, this.UserIdentity.Account);
                    }
                }

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
        /// <summary>
        /// 更新处方收费标志
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateChargeStatusApi")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage UpdateChargeStatusApi(PrescriptionChargeRequest par)
        {
            par.TokenType = null;
            par.Token = null;
            Action<PrescriptionChargeRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (par.cfList != null)
                {
                    foreach (var item in par.cfList)
                    {
                        _IPrescriptionRepo.UpdateChargeStatus(par.OrganizeId, item.cfh, par.sfbz, par.AppId);
                    }
                }

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
        /// <summary>
        /// 更新处方退标志
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateChargeTbz")]
        public HttpResponseMessage UpdateChargeTbz(PrescriptionReturnChargeRequest par)
        {
            Action<PrescriptionReturnChargeRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (par.cfList != null)
                {
                    foreach (var item in par.cfList)
                    {
                        _IPrescriptionRepo.UpdateChargeTbz(this.UserIdentity.OrganizeId, item, par.tbz, this.UserIdentity.Account);
                    }
                }

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
    }
}