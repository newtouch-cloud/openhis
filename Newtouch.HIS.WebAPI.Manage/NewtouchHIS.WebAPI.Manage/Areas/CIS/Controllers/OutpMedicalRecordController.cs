using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Domain.IDomainService.CIS;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Domain.InterfaceObjets.PatientCenter;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;

namespace NewtouchHIS.WebAPI.Manage.Areas.CIS.Controllers
{
    /// <summary>
    /// 门诊病历
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OutpMedicalRecordController : ApiBaseController
    {
        private readonly IOutpPrescriptionService _outpPrescription;
        private readonly IOutpMedicalRecordService _outpMedical;
        public OutpMedicalRecordController(IHttpClientHelper httpClient,
            IOutpPrescriptionService outpPrescription, IOutpMedicalRecordService outpMedical) : base(httpClient)
        {
            _outpPrescription = outpPrescription;
            _outpMedical = outpMedical;
        }
        [HttpPost]
        [Route("GetPrescriptionData")]
        public async Task<BusResult<List<OutpPrescriptionDataVO>>> GetPrescriptionDataAsync(Request<OutpBaseVO> request)
        {
            if(string.IsNullOrWhiteSpace(request.OrganizeId)||request.Data==null||string.IsNullOrWhiteSpace(request.Data.mzh))
            {
                return new BusResult<List<OutpPrescriptionDataVO>> { code = ResponseResultCode.FAIL, msg = "机构信息、患者基本信息不可为空" };
            }
            var data = await _outpMedical.OutpPrescriptionDataByMzh(request.Data.mzh,request.OrganizeId);
            return new BusResult<List<OutpPrescriptionDataVO>> { Data = new List<OutpPrescriptionDataVO>() };
        }
    }
}
