using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService.PDSData;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;

namespace NewtouchHIS.WebAPI.Manage.Areas.PDS.Controllers
{
    /// <summary>
    /// 基础业务字典
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// 药品库存管理
    /// </summary>
    public class DrugStorageController : ControllerBase
    {
        private readonly IDepartmentDrugsService _departmentDrugsService;
        public DrugStorageController(IDepartmentDrugsService departmentDrugsService) 
        {
            _departmentDrugsService = departmentDrugsService;
        }
        /// <summary>
        /// 获取科室发药数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDepartmentDrugs")]
        public async Task<BusResult<List<DepartmentDrugsVO>>> GetDepartmentDrugsAsync(Request<QueryParamsBase> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<DepartmentDrugsVO>> { code = ResponseResultCode.FAIL, msg = "机构Id必传|请求数据不可为null" };
            }
            request.Data.orgId = request.OrganizeId;
            var itemData = await _departmentDrugsService.GetDepartmentDrugs(request.Data);
            if (itemData == null)
            {
                return new BusResult<List<DepartmentDrugsVO>> { code = ResponseResultCode.FAIL, msg = "未找到相关数据" };
            }
            return new BusResult<List<DepartmentDrugsVO>> { code = ResponseResultCode.SUCCESS, Data = itemData };
        }
        /// <summary>
        /// 获取科室发药数据(分页)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDepartmentDrugsPage")]
        public async Task<BusResult<PageResponseRow<List<DepartmentDrugsVO>>>> GetDepartmentDrugsPageAsync(Request<OLPagination<QueryParamsBase>> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<PageResponseRow<List<DepartmentDrugsVO>>> { code = ResponseResultCode.FAIL, msg = "机构Id必传|请求数据不可为null" };
            }
            request.Data.queryParams.orgId = request.OrganizeId;
            var itemData = await _departmentDrugsService.GetDepartmentDrugsPage(request.Data);
            if (itemData == null)
            {
                return new BusResult<PageResponseRow<List<DepartmentDrugsVO>>> { code = ResponseResultCode.FAIL, msg = "未找到相关数据" };
            }
            return new BusResult<PageResponseRow<List<DepartmentDrugsVO>>> { code = ResponseResultCode.SUCCESS, Data = itemData };
        }
        /// <summary>
        /// 已入库状态修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateMedicationStatus")]
        public async Task<BusResult<bool>> UpdateMedicationStatusAsync(Request<QueryParamsBase> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId)|| string.IsNullOrWhiteSpace(request.Data.crkmxId))
            {
                return new BusResult<bool> { code = ResponseResultCode.FAIL, msg = "机构Id必传|crkmxId必传|请求数据不可为null" };
            }
            request.Data.orgId = request.OrganizeId;
            var itemData = await _departmentDrugsService.UpdateMedicationStatus(request.Data);
            return new BusResult<bool> { code = ResponseResultCode.SUCCESS, Data = itemData };
        }
        ///// <summary>
        ///// 药品库存
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[Route("DrugStorageQuery")]
        //[HttpPost]
        //public async Task<ResponseBase> DrugStorageQueryAsync(RequestBus<DrugIndexRequest> request)
        //{

        //}

    }
}
