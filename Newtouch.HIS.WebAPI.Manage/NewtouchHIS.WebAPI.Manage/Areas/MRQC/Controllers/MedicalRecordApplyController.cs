using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Domain.IDomainService.MRQC;
using NewtouchHIS.Domain.InterfaceObjets.MRQC;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;

namespace NewtouchHIS.WebAPI.Manage.Areas.MRQC.Controllers
{
    /// <summary>
    /// 病历申请
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordApplyController : ApiBaseController
    {
        private readonly IMedicalRecordApplyDmnService _medicalrecordapplyDmn;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="mapper"></param>
        /// <param name="medicalrecordapplyDmn"></param>
        public MedicalRecordApplyController(IHttpClientHelper httpClient,
           IMedicalRecordApplyDmnService medicalrecordapplyDmn)
           : base(httpClient)
        {
            _medicalrecordapplyDmn = medicalrecordapplyDmn;
        }

        /// <summary>
        /// 质控病历申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMedicalApplyRecord")]
        public async Task<BusResult<MedicalBlApplyResponse>> AddMedicalApplyRecordAsync(Request<MedicalBlApplyVo> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<MedicalBlApplyResponse> { code = ResponseResultCode.FAIL, msg = "机构Id必传" };
            }
            request.Data.OrganizeId = request.OrganizeId;
            var data = await _medicalrecordapplyDmn.AddMedicalApplyRecord(request.Data);
            if (data==null)
            {
                return new BusResult<MedicalBlApplyResponse> { code = ResponseResultCode.FAIL, msg = "质控病历申请失败，请重新申请" };
            }
            return new BusResult<MedicalBlApplyResponse> { code = ResponseResultCode.SUCCESS, msg = "添加成功", Data = data };

        }
    }
}
