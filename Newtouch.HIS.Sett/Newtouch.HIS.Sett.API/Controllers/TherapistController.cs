using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common.Attributes;
using System.Net.Http;
using Newtouch.HIS.Sett.Request.Therapist;
using Newtouch.HIS.API.Common;
using System;
using Newtouch.HIS.Domain.IDomainServices;
using System.Linq;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Sett.API.Controllers
{
    /// <summary>
    /// 处方相关
    /// </summary>
    [RoutePrefix("api/Therapist")]
    public class TherapistController : ApiControllerBase<TherapistController>
    {
        private readonly ITherapistSuggestionDmnService _therapistSuggestionDmnService;

        public TherapistController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// 治疗建议查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TherapistSuggestionQuery")]
        public HttpResponseMessage TherapistSuggestionQuery(TherapistSuggestionQueryRequest par)
        {
            Action<TherapistSuggestionQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                var list = _therapistSuggestionDmnService.GetTherapistSuggestionList(req.mzzyh, req.OrganizeId, req.brlx, req.startTime.Value);
                resp.data = list;
                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }

        /// <summary>
        /// 更新建议的转换状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateSuggestionCvStatus")]
        public HttpResponseMessage UpdateSuggestionCvStatus(UpdateSuggestionCvStatusRequest par)
        {
            Action<UpdateSuggestionCvStatusRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (req.CvList == null)
                {
                    throw new FailedCodeException("CVLIST_IS_REQUIRED");
                }
                var cvList = req.CvList.Where(p => !string.IsNullOrWhiteSpace(p.jyId) && !string.IsNullOrWhiteSpace(p.zhbz)).Select(p => new SuggestionCvStatuUpdateDTO()
                {
                    jyId = p.jyId,
                    zhbz = p.zhbz,
                }).ToList();
                if (req.CvList.Count == 0)
                {
                    throw new FailedCodeException("CVLIST_IS_REQUIRED");
                }

                _therapistSuggestionDmnService.UpdateSuggestionCvStatus(req.OrganizeId, cvList);
                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, par);

            return base.CreateResponse(response);
        }


    }
}
