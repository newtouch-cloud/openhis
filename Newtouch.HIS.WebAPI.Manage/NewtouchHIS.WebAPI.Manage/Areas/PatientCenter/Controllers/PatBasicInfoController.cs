using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Domain.Entity.PatientCenter;
using NewtouchHIS.Domain.IDomainService.PatientCenter;
using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using NewtouchHIS.Domain.InterfaceObjets.PatientCenter;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.WebAPI.Manage.Areas.PatientCenter.Controllers
{
    /// <summary>
    /// 患者基础信息管理(自费优先)
    /// </summary>
    [Route("api/patient")]
    [ApiController]
    public class PatBasicInfoController : ControllerBase
    {
        public readonly IPatientInfoDmnService _patientInfoDmn;
        public readonly IPatientAddressDmnService _patientAddressDmn;
        public PatBasicInfoController(IPatientInfoDmnService patientInfoDmn, IPatientAddressDmnService patientAddressDmn)
        {
            _patientInfoDmn = patientInfoDmn;
            _patientAddressDmn = patientAddressDmn;
        }
        /// <summary>
        /// 患者基本信息查询 
        /// 自费优先
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List</returns>
        [HttpPost]
        [Route("PatientQuerySelfPay")]
        public async Task<BusResult<SysPatInfoVO>> PatientQueryAsync(Request<SysPatIndexVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<SysPatInfoVO> { code = ResponseResultCode.FAIL, msg = "机构及患者信息不可为空" };
            }
            request.Data.OrganizeId = request.OrganizeId;
            var pat = await _patientInfoDmn.PatientQuery(request.Data, (int)Domain.Enum.HisEnum.EnumBrxz.zf);
            if (pat == null)
            {
                return new BusResult<SysPatInfoVO> { code = ResponseResultCode.FAIL, msg = "未找到相关患者信息" };
            }
            return new BusResult<SysPatInfoVO> { code = ResponseResultCode.SUCCESS, Data = pat };
        }

        /// <summary>
        /// 患者地址信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientAddressQuery")]
        public async Task<BusResult<SysPatientAddressEntity>> PatientAddressQuery(Request<SysPatientAddressIndexVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.FAIL, msg = "机构及患者信息不可为空" };
            }
            request.Data.OrganizeId = request.OrganizeId;
            var pat = await _patientAddressDmn.PatientAddressQuery(request.Data.patid, request.OrganizeId);
            if (pat == null)
            {
                return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.FAIL, msg = "未找到相关患者信息" };
            }
            return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.SUCCESS, Data = pat };
        }

        /// <summary>
        /// 患者地址信息更新
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientAddressUpdate")]
        public async Task<BusResult<bool>> PatientAddressUpdate(Request<SysPatientAddressInfoVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "机构信息及关键申请信息不可为空" };
            }
            if (request.Data.patid==0)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "patId不可为空" };
            }
            SysPatientAddressEntity entity = request.Data.Adapt<SysPatientAddressEntity>();
            var response = await _patientAddressDmn.PatientAddressUpdate(entity, request.AppId);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "患者地址信息更新成功", Data = response };
        }

        /// <summary>
        /// 患者地址信息添加
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientAddressAdd")]
        public async Task<BusResult<bool>> PatientAddressAdd(Request<SysPatientAddressInfoVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "机构信息及关键申请信息不可为空" };
            }
            if (request.Data.patid == 0)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "patId不可为空" };
            }
            SysPatientAddressEntity entity = request.Data.Adapt<SysPatientAddressEntity>();
            var response = await _patientAddressDmn.PatientAddressAdd(entity, request.AppId);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "患者地址信息添加成功", Data = true };
        }

        /// <summary>
        /// 患者地址信息删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientAddressDelete")]
        public async Task<BusResult<bool>> PatientAddressDelete(Request<SysPatientAddressInfoVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "机构信息及关键申请信息不可为空" };
            }
            if (request.Data.patid == 0)
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "patId不可为空" };
            }
            SysPatientAddressEntity entity = request.Data.Adapt<SysPatientAddressEntity>();
            var response = await _patientAddressDmn.PatientAddressDelete(entity, request.AppId);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, msg = "患者地址信息删除成功", Data = response };
        }

        /// <summary>
        /// 获取订单收件人信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PatientOrderAddressQuery")]
        public async Task<BusResult<SysPatientAddressEntity>> PatientOrderAddressQuery(Request<SysPatientOrderAddressIndexVO> request)
        {
            if (request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.FAIL, msg = "机构及患者信息不可为空" };
            }
            if (request.Data == null || string.IsNullOrWhiteSpace(request.Data.visitNo))
            {
                return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.FAIL, msg = "门诊号不可为空" };
            }
            if ( request.Data.ks!= "00000080")
            {
                return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.FAIL, msg = "该订单不是远程诊疗" };
            }
            request.Data.OrganizeId = request.OrganizeId;
            var pat = await _patientAddressDmn.PatientOrderAddressQuery(request.Data.visitNo,request.Data.ks,request.OrganizeId);
            if (pat == null)
            {
                return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.FAIL, msg = "未找到相关患者信息" };
            }
            return new BusResult<SysPatientAddressEntity> { code = ResponseResultCode.SUCCESS, Data = pat };
        }


    }
}
