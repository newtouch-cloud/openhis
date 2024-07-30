using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Domain.IDomainService.EMR;
using NewtouchHIS.Domain.IDomainService.MRQC;
using NewtouchHIS.Domain.InterfaceObjets.MRQC;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;
using NewtouchHIS.WebAPI.Manage.Models.EMR;

namespace NewtouchHIS.WebAPI.Manage.Areas.MRQC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MRQCScoreController : ApiBaseController
    {
        private readonly IMRQCScoreDmnService _mrqcScoreDmn;
        public MRQCScoreController(IHttpClientHelper httpClient, IMRQCScoreDmnService mrqcScoreDmn) : base(httpClient)
        {
            _mrqcScoreDmn = mrqcScoreDmn;
        }


        /// <summary>
        /// 获取病历质控达标情况
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetMrqcScore")]
        [HttpPost]
        public async Task<BusResult<List<MRQCScoreVO>>> GetMrqcScore(RequestBus<MRQCScoreRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<MRQCScoreVO>> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            if (request == null || request.Data == null || (string.IsNullOrWhiteSpace(request.Data.zyh) && string.IsNullOrWhiteSpace(request.Data.bllxId) && string.IsNullOrWhiteSpace(request.Data.orgId)))
            {
                return new BusResult<List<MRQCScoreVO>> { code = ResponseResultCode.FAIL, msg = "请录入查询条件（住院号/病历类型/机构编码）" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.orgId))
            {
                request.Data.orgId = request.OrganizeId;
            }
            var data = await _mrqcScoreDmn.GetMrqcScore(request.Data.orgId, request.Data.zyh, request.Data.bllxId, DBEnum.MrQcDb);
            return new BusResult<List<MRQCScoreVO>> { code = ResponseResultCode.SUCCESS, Data = data };
        }
    }
}
