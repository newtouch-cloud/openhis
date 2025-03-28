using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Domain.IDomainService.CIS;
using NewtouchHIS.Domain.IDomainService.EMR;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Domain.InterfaceObjets.EMR;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;
using NewtouchHIS.WebAPI.Manage.Models.CIS;
using NewtouchHIS.WebAPI.Manage.Models.EMR;

namespace NewtouchHIS.WebAPI.Manage.Areas.CIS.Controllers
{
    /// <summary>
    /// 质控调用:医嘱处方接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalDoctorsAdviceController : ApiBaseController
    {
        private readonly IMedicalDoctorsAdviceDmnService _medicaldoctorsadviceDmn;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="medicaldoctorsadviceDmn"></param>
        public MedicalDoctorsAdviceController(IHttpClientHelper httpClient, IMedicalDoctorsAdviceDmnService medicaldoctorsadviceDmn) : base(httpClient)
        {
            _medicaldoctorsadviceDmn = medicaldoctorsadviceDmn;
        }
        /// <summary>
        /// 住院医嘱
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetDoctorAdviceRecore")]
        [HttpPost]
        public async Task<BusResult<List<MedicalDoctorsAdviceVo>>> GetDoctorAdviceRecoreAsync(RequestBus<DoctorAdviceReq> request)
        {
            if (request == null || request.Data == null)
            {
                return new BusResult<List<MedicalDoctorsAdviceVo>> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<MedicalDoctorsAdviceVo>> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.yzlx))
            {
                return new BusResult<List<MedicalDoctorsAdviceVo>> { code = ResponseResultCode.FAIL, msg = "医嘱类型不可为空" };
            }
            var data = await _medicaldoctorsadviceDmn.MedicalDoctorsAdviceRecord(request.Data.zyh,request.OrganizeId, request.Data.yzlx, DBEnum.CisDb);
            return new BusResult<List<MedicalDoctorsAdviceVo>> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        /// <summary>
        /// 门诊住院检验检查
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetInspectionExaminationRecore")]
        [HttpPost]
        public async Task<BusResult<List<MedicalInspectionExaminationVo>>> GetInspectionExaminationRecoreAsync(RequestBus<InspectionExaminationReq> request)
        {
            if (request == null || request.Data == null)
            {
                return new BusResult<List<MedicalInspectionExaminationVo>> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<MedicalInspectionExaminationVo>> { code = ResponseResultCode.FAIL, msg = "机构编码不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.reportType))
            {
                return new BusResult<List<MedicalInspectionExaminationVo>> { code = ResponseResultCode.FAIL, msg = "报告类型不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.mzzybz))
            {
                return new BusResult<List<MedicalInspectionExaminationVo>> { code = ResponseResultCode.FAIL, msg = "门诊住院标志不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.ksrq.ToString()))
            {
                return new BusResult<List<MedicalInspectionExaminationVo>> { code = ResponseResultCode.FAIL, msg = "日期开始时间不可为空" };
            }
            if (string.IsNullOrWhiteSpace(request.Data.jsrq.ToString()))
            {
                return new BusResult<List<MedicalInspectionExaminationVo>> { code = ResponseResultCode.FAIL, msg = "日期结束时间不可为空" };
            }
            var data = await _medicaldoctorsadviceDmn.MedicalJyjcRecord(request.Data.jzh, request.OrganizeId, request.Data.reportType, request.Data.mzzybz, request.Data.ksrq, request.Data.jsrq, DBEnum.CisDb);
            return new BusResult<List<MedicalInspectionExaminationVo>> { code = ResponseResultCode.SUCCESS, Data = data };
        }
    }
}
