using HIS.BaseAPI.Models.System;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Lib.Base.Model;

namespace HIS.BaseAPI.Controllers
{
    /// <summary>
    /// 系统通用API 
    /// （涉及 Entity CRUD）
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SystemedController : ControllerBase
    {
        private readonly ISysModuleDmnService _sysModuleDmn;
        public SystemedController(ISysModuleDmnService sysModuleDmn)
        {
            _sysModuleDmn = sysModuleDmn;
        }
        #region Menu        
        /// <summary>
        /// 菜单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetMenubyId")]
        [HttpPost]
        public async Task<BusResult<SysModuleEntity>> GetMenubyIdAsync(Request<string> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null)
            {
                return new BusResult<SysModuleEntity> { code = ResponseResultCode.FAIL, msg = "机构Id及菜单Id不可为空" };
            }
            var menu = await _sysModuleDmn.GetEntity(request.Data);
            if (menu == null)
            {
                return new BusResult<SysModuleEntity> { code = ResponseResultCode.FAIL, msg = "未找到相关菜单" };
            }
            return new BusResult<SysModuleEntity> { code = ResponseResultCode.SUCCESS, Data = menu };
        }
        #region cud
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AddMenu")]
        [HttpPost]
        public async Task<BusResult<string>> AddMenuAsync(Request<MenuAddRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "机构Id及菜单信息不可为空" };
            }
            return await _sysModuleDmn.AddEntity(request.Data, request.Data.user, request.OrganizeId);
        }
        #endregion
        #endregion
 
    }
}
