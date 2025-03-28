using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;

namespace HIS.BaseAPI.Controllers
{
    /// <summary>
    /// 组织机构基础服务接口
    /// </summary>
    [Route("api/organize")]
    [ApiController]
    public class OrgServiceController : ControllerBase
    {
        private readonly ISysOrgDmnService _sysOrgVDmn;
        private readonly ISysUserStaffDutyDmnService _sysUserStaffDutyDmnService;
        private readonly ISysDepartmentDmnService _sysDepartmentDmnService;
        private readonly ISysWardDmnService _sysWardDmnService;
        private readonly ISysChargeCategoryDmnService _sysChargeCategoryDmnService;
        private readonly ISysPatBasicInfoAppDmnService _sysPatBasicInfoAppDmnService;
        private readonly ISysFailedCodeMessageMappDmnService _sysFailedCodeMessageMappDmnService;
        public OrgServiceController(ISysOrgDmnService sysOrgVDmn, ISysUserStaffDutyDmnService sysUserStaffDutyDmnService, ISysDepartmentDmnService sysDepartmentDmnService, ISysWardDmnService sysWardDmnService, ISysChargeCategoryDmnService sysChargeCategoryDmnService, ISysPatBasicInfoAppDmnService sysPatBasicInfoAppDmnService, ISysFailedCodeMessageMappDmnService sysFailedCodeMessageMappDmnService)
        {
            _sysOrgVDmn = sysOrgVDmn;
            _sysUserStaffDutyDmnService = sysUserStaffDutyDmnService;
            _sysDepartmentDmnService = sysDepartmentDmnService;
            _sysWardDmnService = sysWardDmnService;
            _sysChargeCategoryDmnService = sysChargeCategoryDmnService;
            _sysPatBasicInfoAppDmnService = sysPatBasicInfoAppDmnService;
            _sysFailedCodeMessageMappDmnService = sysFailedCodeMessageMappDmnService;
        }
        /// <summary>
        /// 机构列表（Toporganize归属）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OrgList")]
        public async Task<BusResult<List<SysOrgVo>>> GetOrgListAsync(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysOrgVo>> { code = ResponseResultCode.FAIL, msg = "机构信息（OrganizeId）不可为空" };
            }
            var orgList = await _sysOrgVDmn.GetOrganizeList(null);
            return new BusResult<List<SysOrgVo>> { code = ResponseResultCode.SUCCESS, Data = orgList };
        }
        /// <summary>
        /// 人员岗位关联关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysStaffDutyList")]
        public async Task<BusResult<List<SysStaffDutyComplexVEntity>>> GetsysStaffDutyList(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysStaffDutyComplexVEntity>> { code = ResponseResultCode.FAIL, msg = "机构信息（OrganizeId）不可为空" };
            }
            var sysStaffDutyList = await _sysUserStaffDutyDmnService.GetStaffDutyListByOrganizeId(request.OrganizeId);
            return new BusResult<List<SysStaffDutyComplexVEntity>> { code = ResponseResultCode.SUCCESS, Data = sysStaffDutyList };
        }
        /// <summary>
        /// 获取科室
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysDepartList")]
        public async Task<BusResult<List<SysDepartmentEntity>>> GetsysDepartList(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysDepartmentEntity>> { code = ResponseResultCode.FAIL, msg = "机构信息（OrganizeId）不可为空" };
            }
            var sysDepartList = await _sysDepartmentDmnService.GetList(request.OrganizeId);
            return new BusResult<List<SysDepartmentEntity>> { code = ResponseResultCode.SUCCESS, Data = sysDepartList };
        }
        /// <summary>
        /// 获取病区信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysPatiAreaList")]
        public async Task<BusResult<List<SysWardVEntity>>> GetsysPatiAreaList(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysWardVEntity>> { code = ResponseResultCode.FAIL, msg = "机构信息（OrganizeId）不可为空" };
            }
            var sysPatiAreaList = await _sysWardDmnService.GetbqList(request.OrganizeId);
            return new BusResult<List<SysWardVEntity>> { code = ResponseResultCode.SUCCESS, Data = sysPatiAreaList };
        }
        /// <summary>
        /// 获取收费大类信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysMajorClassList")]
        public async Task<BusResult<List<SysChargeCategoryVEntity>>> GetsysMajorClassList(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysChargeCategoryVEntity>> { code = ResponseResultCode.FAIL, msg = "机构信息（OrganizeId）不可为空" };
            }
            var sysMajorClassList = await _sysChargeCategoryDmnService.GetList(request.OrganizeId);
            return new BusResult<List<SysChargeCategoryVEntity>> { code = ResponseResultCode.SUCCESS, Data = sysMajorClassList };
        }
        /// <summary>
        /// 住院记账获取门诊医生
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStaffByDutyCode")]
        public async Task<BusResult<List<SysDutyStaffVO>>> GetStaffByDutyCode(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysDutyStaffVO>> { code = ResponseResultCode.FAIL, msg = "机构信息（OrganizeId）不可为空" };
            }
            var doctorInHosBookkeep = await _sysUserStaffDutyDmnService.GetStaffByDutyCode(request.OrganizeId);
            return new BusResult<List<SysDutyStaffVO>> { code = ResponseResultCode.SUCCESS, Data = doctorInHosBookkeep };
        }
        /// <summary>
        /// 病人性质,报销政策
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysPatientNatureList")]
        public async Task<BusResult<List<SysPatientNatureEntity>>> GetsysPatientNatureList(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysPatientNatureEntity>> { code = ResponseResultCode.FAIL, msg = "机构信息（OrganizeId）不可为空" };
            }
            var sysPatientNatureList = await _sysPatBasicInfoAppDmnService.GetBRXZListAsync(request.OrganizeId);
            return new BusResult<List<SysPatientNatureEntity>> { code = ResponseResultCode.SUCCESS, Data = sysPatientNatureList };
        }
        /// <summary>
        /// 根据科室获取病区
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysWardDeptRelation")]
        public async Task<BusResult<List<SysDepartmentWardRelationVO>>> GetsysWardDeptRelation(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysDepartmentWardRelationVO>> { code = ResponseResultCode.FAIL, msg = "机构信息（OrganizeId）不可为空" };
            }
            var sysPatientNatureList = await _sysWardDmnService.GetWardbyDept(request.OrganizeId, null, null);
            return new BusResult<List<SysDepartmentWardRelationVO>> { code = ResponseResultCode.SUCCESS, Data = sysPatientNatureList };
        }
        /// <summary>
        /// 获取报错信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSysFailedCodeMessageMapList")]
        public async Task<BusResult<List<SysFailedCodeMessageMappEntity>>> GetSysFailedCodeMessageMapList(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysFailedCodeMessageMappEntity>> { code = ResponseResultCode.FAIL, msg = "机构信息（OrganizeId）不可为空" };
            }
            var sysPatientNatureList = await _sysFailedCodeMessageMappDmnService.GetList(request.OrganizeId);
            return new BusResult<List<SysFailedCodeMessageMappEntity>> { code = ResponseResultCode.SUCCESS, Data = sysPatientNatureList };
        }

    }
}
