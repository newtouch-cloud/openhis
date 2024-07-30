using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Domain.Entity.LIS;
using NewtouchHIS.Domain.IDomainService.LIS;
using NewtouchHIS.Domain.IDomainService.PatientCenter;
using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.DomainService.LIS;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.WebAPI.Manage.Areas.LIS.Controllers
{
    /// <summary>
    /// 患者基础信息管理(自费优先)
    /// </summary>
    [Route("api/lis")]
    [ApiController]
    public class LISController : ControllerBase
    {
        public readonly ILisHisDeptDmnService _lisHisDeptDmnService;
        public readonly ILisHisDoctorDmnService _lisHisDoctorDmnService;
        public readonly ILisHisDiagnoseDmnService _lisHisDiagnoseDmnService;
        public readonly ILisHisFeeItemDmnService _lisHisFeeItemDmnService;
        public readonly ILisHisOperationDmnService _lisHisOperationDmnService;
        public readonly ILisHisPatientInfoDmnService _lisHisPatientInfoDmnService;
        public readonly ILisHisPatientRequestDmnService _lisHisPatientRequestDmnService;
        public readonly ILisHisUserDeptDmnService _lisHisUserDeptDmnService;
        public readonly ILisDoctorDmnService _lisDoctorDmnService;
        public readonly ILisDeptDmnService _lisDeptDmnService;
        public readonly ILisFeeItemDmnService _lisFeeItemDmnService;

        public LISController(ILisHisDeptDmnService lisHisDeptDmnService
            , ILisHisDoctorDmnService lisHisDoctorDmnService
            , ILisHisDiagnoseDmnService lisHisDiagnoseDmnService
            , ILisHisFeeItemDmnService lisHisFeeItemDmnService
            , ILisHisOperationDmnService lisHisOperationDmnService
            , ILisHisPatientInfoDmnService lisHisPatientInfoDmnService
            , ILisHisPatientRequestDmnService lisHisPatientRequestDmnService
            , ILisHisUserDeptDmnService lisHisUserDeptDmnService
            , ILisDoctorDmnService lisDoctorDmnService
            , ILisDeptDmnService lisDeptDmnService
            , ILisFeeItemDmnService lisFeeItemDmnService
            )
        {
            _lisHisDeptDmnService = lisHisDeptDmnService;
            _lisHisDoctorDmnService = lisHisDoctorDmnService;
            _lisHisDiagnoseDmnService = lisHisDiagnoseDmnService;
            _lisHisFeeItemDmnService = lisHisFeeItemDmnService;
            _lisHisOperationDmnService = lisHisOperationDmnService;
            _lisHisPatientInfoDmnService = lisHisPatientInfoDmnService;
            _lisHisPatientRequestDmnService = lisHisPatientRequestDmnService;
            _lisHisUserDeptDmnService = lisHisUserDeptDmnService;
            _lisDoctorDmnService = lisDoctorDmnService;
            _lisDeptDmnService = lisDeptDmnService;
            _lisFeeItemDmnService = lisFeeItemDmnService;
        }


        /// <summary>
        /// 获取科室信息 
        /// </summary>
        /// <returns>List</returns>
        [HttpPost]
        [Route("HisDeptQuery")]
        public async Task<BusResult<List<HisDeptEntity>>> HisDeptQuery()
        {

            var result = await _lisHisDeptDmnService.HisDeptQuery();
            if (result == null)
            {
                return new BusResult<List<HisDeptEntity>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<HisDeptEntity>> { code = ResponseResultCode.SUCCESS, Data = result };
        }

        /// <summary>
        /// 获取人员信息 
        /// </summary>
        /// <returns>List</returns>
        [HttpPost]
        [Route("HisDoctorQuery")]
        public async Task<BusResult<List<HisDoctorEntity>>> HisDoctorQuery()
        {

            var result = await _lisHisDoctorDmnService.HisDoctorQuery();
            if (result == null)
            {
                return new BusResult<List<HisDoctorEntity>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<HisDoctorEntity>> { code = ResponseResultCode.SUCCESS, Data = result };
        }

        /// <summary>
        /// 获取诊断信息 
        /// </summary>
        /// <returns>List</returns>
        [HttpPost]
        [Route("HisDiagnoseQuery")]
        public async Task<BusResult<List<HisDiagnoseEntity>>> HisDiagnoseQuery()
        {

            var result = await _lisHisDiagnoseDmnService.HisDiagnoseQuery();
            if (result == null)
            {
                return new BusResult<List<HisDiagnoseEntity>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<HisDiagnoseEntity>> { code = ResponseResultCode.SUCCESS, Data = result };
        }

        /// <summary>
        /// 获取收费项目信息 
        /// </summary>
        /// <returns>List</returns>
        [HttpPost]
        [Route("HisFeeItemQuery")]
        public async Task<BusResult<List<HisFeeItemEntity>>> HisFeeItemQuery()
        {

            var result = await _lisHisFeeItemDmnService.HisFeeItemQuery();
            if (result == null)
            {
                return new BusResult<List<HisFeeItemEntity>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<HisFeeItemEntity>> { code = ResponseResultCode.SUCCESS, Data = result };
        }

        /// <summary>
        /// 获取手术信息 
        /// </summary>
        /// <returns>List</returns>
        [HttpPost]
        [Route("HisOperationQuery")]
        public async Task<BusResult<List<HisOperationEntity>>> HisOperationQuery()
        {

            var result = await _lisHisOperationDmnService.HisOperationQuery();
            if (result == null)
            {
                return new BusResult<List<HisOperationEntity>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<HisOperationEntity>> { code = ResponseResultCode.SUCCESS, Data = result };
        }

        /// <summary>
        /// 获取患者信息 
        /// </summary>
        /// <returns>List</returns>
        [HttpPost]
        [Route("HisPatientInfoQuery")]
        public async Task<BusResult<List<HisPatientInfoEntity>>> HisPatientInfoQuery()
        {

            var result = await _lisHisPatientInfoDmnService.HisPatientInfoQuery();
            if (result == null)
            {
                return new BusResult<List<HisPatientInfoEntity>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<HisPatientInfoEntity>> { code = ResponseResultCode.SUCCESS, Data = result };
        }

        /// <summary>
        /// 获取申请单信息 
        /// </summary>
        /// <returns>List</returns>
        //[HttpPost]
        //[Route("HisPatientRequestQuery")]
        //public async Task<BusResult<List<HisPatientRequestEntity>>> HisPatientRequestQuery()
        //{

        //    var result = await _lisHisPatientRequestDmnService.HisPatientRequestQuery();
        //    if (result == null)
        //    {
        //        return new BusResult<List<HisPatientRequestEntity>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
        //    }
        //    return new BusResult<List<HisPatientRequestEntity>> { code = ResponseResultCode.SUCCESS, Data = result };
        //}

        /// <summary>
        /// 获取科室和人员对应关系 
        /// </summary>
        /// <returns>List</returns>
        [HttpPost]
        [Route("HisUserDeptQuery")]
        public async Task<BusResult<List<HisUserDeptEntity>>> HisUserDeptQuery()
        {

            var result = await _lisHisUserDeptDmnService.HisUserDeptQuery();
            if (result == null)
            {
                return new BusResult<List<HisUserDeptEntity>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<HisUserDeptEntity>> { code = ResponseResultCode.SUCCESS, Data = result };
        }

        /// <summary>
        /// 科室字典
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("LisDeptQuery")]
        public async Task<BusResult<List<LisDeptEntity>>> LisDeptQuery()
        {

            var result = await _lisDeptDmnService.LisDeptQuery();
            if (result == null)
            {
                return new BusResult<List<LisDeptEntity>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<LisDeptEntity>> { code = ResponseResultCode.SUCCESS, Data = result };
        }


        /// <summary>
        /// 医生字典
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("LisDoctorQuery")]
        public async Task<BusResult<List<LisDoctorEntity>>> LisDoctorQuery()
        {

            var result = await _lisDoctorDmnService.LisDoctorQuery();
            if (result == null)
            {
                return new BusResult<List<LisDoctorEntity>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<LisDoctorEntity>> { code = ResponseResultCode.SUCCESS, Data = result };
        }

        /// <summary>
        /// 收费项目
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("LisFeeItemQuery")]
        public async Task<BusResult<List<LisFeeItemEntity>>> LisFeeItemQuery()
        {

            var result = await _lisFeeItemDmnService.LisFeeItemQuery();
            if (result == null)
            {
                return new BusResult<List<LisFeeItemEntity>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<LisFeeItemEntity>> { code = ResponseResultCode.SUCCESS, Data = result };
        }


    }
}
