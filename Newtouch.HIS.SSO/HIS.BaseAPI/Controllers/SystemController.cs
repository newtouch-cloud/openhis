using HIS.BaseAPI.Models.System;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Base.Domain.ValueObjects.AppManage;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Lib.Base.Model;

namespace HIS.BaseAPI.Controllers
{
    /// <summary>
    /// 系统通用API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISysConfigDmnService _sysConfigDmn;
        private readonly ISysNationalityDmnService _sysNationalityDmnService;
        private readonly ISysNationDmnService _sysNationDmnService;
        private readonly ISysForCashPayDmnService _sysForCashPayDmnService;
        private readonly IAppManageDmnService _regAppDmnService;
        private readonly ISysMedicineService _medicineService;
        public SystemController(ISysConfigDmnService sysConfigDmn, IAppManageDmnService regAppDmnService, ISysNationalityDmnService sysNationalityDmnService, ISysNationDmnService sysNationDmnService, ISysForCashPayDmnService sysForCashPayDmnService, ISysMedicineService medicineService)
        {
            _sysConfigDmn = sysConfigDmn;
            _regAppDmnService = regAppDmnService;
            _sysNationalityDmnService = sysNationalityDmnService;
            _sysNationDmnService = sysNationDmnService;
            _sysForCashPayDmnService = sysForCashPayDmnService;
            _medicineService = medicineService;
        }
        #region Menu
        /// <summary>
        /// 获取用户系统菜单列表(主库)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetMenuList")]
        [HttpPost]
        public async Task<BusResult<List<SysModuleVO>>> GetMenuListAsync(Request<MenuAuthRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || (request.Data != null && request.Data.RoleIdList.Count == 0))
            {
                return new BusResult<List<SysModuleVO>> { code = ResponseResultCode.FAIL, msg = "机构Id及角色信息不可为空" };
            }
            var list = await _sysConfigDmn.GetMenuList(request.OrganizeId, request.Data.RoleIdList, request.Data.IsRoot, request.Data.IsAdministrator, request.Data.UserId, request.Data.ValidLimit);
            return new BusResult<List<SysModuleVO>> { code = ResponseResultCode.SUCCESS, Data = list != null ? list.Adapt<List<SysModuleVO>>() : null };
        }
        /// <summary>
        /// 获取业务系统用户菜单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetAppMenuList")]
        [HttpPost]
        public async Task<BusResult<List<SysModuleVO>>> GetAppMenuListAsync(Request<MenuAuthRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null)
            {
                return new BusResult<List<SysModuleVO>> { code = ResponseResultCode.FAILOfEmpty, msg = "机构Id、角色信息不可为空" };
            }
            if(string.IsNullOrWhiteSpace(request.Data.MenuAppId))
            {
                request.Data.MenuAppId = request.AppId;
            }
            var list = await _sysConfigDmn.GetMenuList(request.Data.MenuAppId, request.OrganizeId, request.Data.RoleIdList, request.Data.IsRoot, request.Data.IsAdministrator, request.Data.UserId, request.Data.ValidLimit);
            return new BusResult<List<SysModuleVO>> { code = ResponseResultCode.SUCCESS, Data = list != null ? list.Adapt<List<SysModuleVO>>() : null };
        }
        /// <summary>
        /// 获取应用系统菜单列表（限管理员同步用）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetMenuListbyAppId")]
        [HttpPost]
        public async Task<BusResult<List<SysModuleVO>>> GetMenuListbyDBAsync(Request<MenuAuthRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || string.IsNullOrWhiteSpace(request.Data.UserId))
            {
                return new BusResult<List<SysModuleVO>> { code = ResponseResultCode.FAILOfEmpty, msg = "机构Id、菜单应用Id、用户Id不可为空" };
            }
            var list = await _sysConfigDmn.GetMenuListbyAppId(request.OrganizeId, request.Data.MenuAppId ?? request.AppId, request.Data.UserId, request.Data.ValidLimit);
            return new BusResult<List<SysModuleVO>> { code = ResponseResultCode.SUCCESS, Data = list != null ? list.Adapt<List<SysModuleVO>>() : null };
        }

        #endregion
        /// <summary>
        /// 系统字典表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetItemDetailsList")]
        [HttpPost]
        public async Task<BusResult<List<SysItemExtendVO>>> GetItemDetailsListAsync(Request<string> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysItemExtendVO>> { code = ResponseResultCode.FAIL, msg = "机构Id及角色信息不可为空" };
            }
            var list = await _sysConfigDmn.GetItemDetailsList(request.OrganizeId);
            return new BusResult<List<SysItemExtendVO>> { code = ResponseResultCode.SUCCESS, Data = list };

        }
        /// <summary>
        /// 获取国籍
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysNationalityList")]
        public async Task<BusResult<List<SysNationalityVEntity>>> GetsysNationalityList(Request<string> request)
        {
            var sysNationalityList = await _sysNationalityDmnService.GetgjList();
            return new BusResult<List<SysNationalityVEntity>> { code = ResponseResultCode.SUCCESS, Data = sysNationalityList };
        }
        /// <summary>
        /// 获取民族
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysNationList")]
        public async Task<BusResult<List<SysNationVEntity>>> GetsysNationList()
        {
            var sysNationList = await _sysNationDmnService.GetmzList();
            return new BusResult<List<SysNationVEntity>> { code = ResponseResultCode.SUCCESS, Data = sysNationList };
        }
        /// <summary>
        /// 获取现金支付方式
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSysForCashPayList")]
        public async Task<BusResult<List<SysCashPaymentModelEntity>>> GetSysForCashPayList()
        {
            List<SysCashPaymentModelEntity> sysCashPaymentModelEntity = await _sysForCashPayDmnService.GetList();
            return new BusResult<List<SysCashPaymentModelEntity>> { code = ResponseResultCode.SUCCESS, Data = sysCashPaymentModelEntity };
        }
        /// <summary>
        /// 获取已注册应用列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetRegAppList")]
        public async Task<BusResult<List<AuthAppVO>>> GetRegAppListAsync(Request<string> request)
        {
            var data = await _regAppDmnService.GetRegAppList();
            return new BusResult<List<AuthAppVO>> { code = ResponseResultCode.SUCCESS, Data = data };
        }

        #region 药品
        /// <summary>
        /// 药品用法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost,AllowAnonymous]
        [Route("GetMediUsageList")]
        public async Task<BusResult<List<DrugUsageEntity>>> GetMediUsageListAsync(Request<DrugUsageRequest> request)
        {
            var data = await _medicineService.DrugUsageDic(request.Data?.code,request.Data?.yplx);
            return new BusResult<List<DrugUsageEntity>> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        /// <summary>
        /// 获取药品剂型用法对照关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        [Route("GetDrugFormulationUsageList")]
        public async Task<BusResult<List<DrugFormulationUsageVO>>> GetDrugFormulationUsageListAsync(Request<string> request)
        {
            var data = await _medicineService.GetDrugFormulationUsageList();
            return new BusResult<List<DrugFormulationUsageVO>> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        #endregion

    }
}
