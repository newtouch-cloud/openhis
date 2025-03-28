using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Attributes;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Sett.Request.Patient;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.HIS.Sett.API.Controllers
{
	/// <summary>
	/// 门诊挂号结算相关
	/// </summary>
	[RoutePrefix("api/OutReg")]
    [DefaultAuthorize]
    public class OutpatientRegController : ApiControllerBase<OutpatientRegController>
    {
        private readonly IOutpatientRegApp _outpatientRegApp;
        private readonly IOutPatientSettleDmnService _outPatientSettleDmnService;

        public OutpatientRegController(IComponentContext com)
            : base(com)
        {
        }

        /// <summary>
        /// chongqing 新排班唯一门诊号、就诊序号
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="ghpbId"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutPatientRegistr")]
        public HttpResponseMessage OutPatientRegistr(OutPatientRegistrationUnSettRequest par)
        {

            Action<OutPatientRegistrationUnSettRequest, DefaultResponse> ac = (req, resp) =>
            {
                _outpatientRegApp.UnSettSave(par);

                resp.data = null;

                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);

   //         return base.CreateResponse(response);
   //         var ysxx = new CqybGjbmInfoVo();
   //         if (patid <= 0 || string.IsNullOrWhiteSpace(ghpbId) || string.IsNullOrWhiteSpace(ks))
   //         {
   //             return Error("请求数据不完整");
   //         }
   //         var mzhjzxh = _outPatientSettleDmnService.GetCQMzhJzxh(patid, ghpbId.ToString(), ks, ys, OrganizeId, this.UserIdentity.UserCode, mjzbz, null, null);
   //         var data = new
   //         {
   //             jzxh = mzhjzxh.Item1,
   //             mzh = mzhjzxh.Item2,
   //             ysxx = ysxx,
   //         };
   //         return Success(null, data);


   //         try
   //         {
			//	if (string.IsNullOrWhiteSpace(kh) || string.IsNullOrWhiteSpace(mjzbz) || string.IsNullOrWhiteSpace(ks) || string.IsNullOrWhiteSpace(ksmc) || ghpbId < 0
			//		|| string.IsNullOrWhiteSpace(brxz) || (feeRelated != null && feeRelated.zje < 0))
			//	{
			//		return Error("请求数据不完整");
			//	}
			//	object newJszbInfo;
			//	_outpatientRegApp.Save(patid, kh, ghly, mjzbz,
			//	ks, ys, ksmc, ysmc, ghxm, zlxm, fph, sfrq, isCkf, isGbf, ghpbId, feeRelated, brxz, ybjsh, Request.Params["mzyyghId"], ref qzjzxh, ref qzmzh, jzyy, jzid, jzlx, bzbm, bzmc, out newJszbInfo);

			//	return Success(null, newJszbInfo);
			//}
   //         catch (Exception ex)
   //         {
   //             return Error("请求数据不完整");
   //         }
        }

    }
}
