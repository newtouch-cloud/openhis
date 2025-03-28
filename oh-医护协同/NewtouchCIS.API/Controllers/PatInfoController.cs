using Autofac;
using Newtouch.CIS.APIRequest;
using Newtouch.CIS.APIRequest.Inpatient;
using Newtouch.Domain.IRepository;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Attributes;
using System;
using System.Net.Http;
using System.Web.Http;

namespace NewtouchCIS.API.Controllers
{
    [RoutePrefix("api/PatInfo")]
    [DefaultAuthorize]
    public class PatInfoController : ApiControllerBase<PatInfoController>
    {
        private readonly IInpatientPatientInfoRepo _iInpatientPatientInfoRepo;
        private readonly Newtouch.Domain.IDomainServices.IBaseDataDmnService _IBaseDataDmnService;

        public PatInfoController(IComponentContext com)
            : base(com)
        {
        }

        [HttpPost]
        [Route("UpdateInpatientBasicInfo")]
        public HttpResponseMessage UpdateInpatientBasicInfo(PatInfoRequest par)
        {
            Action<PatInfoRequest, DefaultResponse> ac = (req, resp) =>
            {
                _iInpatientPatientInfoRepo.UpdateInpatientStatus(this.UserIdentity.OrganizeId, req.zyh, req.zybz, req.ryzd, req.ryzdmc, req.brxz, req.brxzmc, req.lxr, req.lxrgx, req.lxrdh);

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        [HttpPost]
        [Route("UpdatebrxzInfo")]
        public HttpResponseMessage UpdatebrxzInfo(UpdatebrxzRequest par)
        {
            Action<UpdatebrxzRequest, DefaultResponse> ac = (req, resp) =>
            {
                _IBaseDataDmnService.UpdatebrxzInfo(this.UserIdentity.OrganizeId, req.mzh, req.zyh, req.brxzCode, req.brxzmc);
                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 获取住院病人基本信息
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InpatientBaseInfo")]
        public HttpResponseMessage InpatientBaseInfo(InpatientBaseInfoRequest par)
        {
            Action<InpatientBaseInfoRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _iInpatientPatientInfoRepo.SelectData(par.zyh, par.xm, par.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }
    }
}
