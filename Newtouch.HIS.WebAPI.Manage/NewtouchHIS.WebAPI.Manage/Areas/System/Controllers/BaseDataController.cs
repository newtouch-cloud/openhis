
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.WebAPI.Manage.Areas.System.Controllers
{
    /// <summary>
    /// 基础业务字典
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseDataController : ControllerBase
    {
        private readonly IChargeItemService _chargeItemService;
        private readonly ISysMedicineService _sysMedicine;
        public BaseDataController(IChargeItemService chargeItemService, ISysMedicineService sysMedicine)
        {
            _chargeItemService = chargeItemService;
            _sysMedicine = sysMedicine;
        }
        /// <summary>
        /// 系统收费大类
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChargeCategory")]
        public async Task<BusResult<List<SysChargeCategoryVO>>> GetChargeCategoryAsync(Request<QueryParamsBase> request)
        {
            if (request == null || request.Data == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysChargeCategoryVO>> { code = ResponseResultCode.FAIL, msg = "机构Id必传|请求数据不可为null" };
            }
            request.Data.orgId = request.OrganizeId;
            var itemData = await _chargeItemService.GetChargeCategory(request.Data);
            if (itemData == null)
            {
                return new BusResult<List<SysChargeCategoryVO>> { code = ResponseResultCode.FAIL, msg = "未找到相关数据" };
            }
            return new BusResult<List<SysChargeCategoryVO>> { code = ResponseResultCode.SUCCESS, Data = itemData };
        }
        /// <summary>
        /// 系统药品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMedicineList")]
        public async Task<BusResult<List<SysMedicineVVO>>> GetMedicineListAsync(Request<QueryParamsBase> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysMedicineVVO>> { code = ResponseResultCode.FAIL, msg = "机构Id必传" };
            }
            request.Data.orgId = request.OrganizeId;
            var itemData = await _sysMedicine.GetMedicineList(request.Data);
            if (itemData == null)
            {
                return new BusResult<List<SysMedicineVVO>> { code = ResponseResultCode.FAIL, msg = "未找到相关数据" };
            }
            return new BusResult<List<SysMedicineVVO>> { code = ResponseResultCode.SUCCESS, Data = itemData };
        }
        /// <summary>
        /// 系统药品列表（分页）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMedicinePage")]
        public async Task<BusResult<PageResponseRow<List<SysMedicineVVO>>>> GetMedicinePageAsync(Request<OLPagination<QueryParamsBase>> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || request.Data.limit == 0)
            {
                return new BusResult<PageResponseRow<List<SysMedicineVVO>>> { code = ResponseResultCode.FAIL, msg = "机构Id必传及分页参数必传" };
            }
            request.Data.queryParams.orgId = request.OrganizeId;
            var itemData = await _sysMedicine.GetMedicinePage(request.Data);
            if (itemData == null)
            {
                return new BusResult<PageResponseRow<List<SysMedicineVVO>>> { code = ResponseResultCode.FAIL, msg = "未找到相关数据" };
            }
            return new BusResult<PageResponseRow<List<SysMedicineVVO>>> { code = ResponseResultCode.SUCCESS, Data = itemData };
        }

        /// <summary>
        /// 收费项目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChargeItemList")]
        public async Task<BusResult<List<SysChargeItemVO>>> GetChargeItemListAsync(Request<QueryParamsBase> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysChargeItemVO>> { code = ResponseResultCode.FAIL, msg = "机构Id必传" };
            }
            request.Data.orgId= request.OrganizeId;
            var itemData = await _chargeItemService.GetChargeItemList(request.Data);
            if (itemData == null)
            {
                return new BusResult<List<SysChargeItemVO>> { code = ResponseResultCode.FAIL, msg = "未找到相关数据" };
            }
            return new BusResult<List<SysChargeItemVO>> { code = ResponseResultCode.SUCCESS, Data = itemData };
        }
        /// <summary>
        /// 收费项目列表（分页）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetChargeItemPage")]
        public async Task<BusResult<PageResponseRow<List<SysChargeItemVO>>>> GetChargeItemPageAsync(Request<OLPagination<QueryParamsBase>> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || request.Data.limit == 0)
            {
                return new BusResult<PageResponseRow<List<SysChargeItemVO>>> { code = ResponseResultCode.FAIL, msg = "机构Id必传及分页参数必传" };
            }
            request.Data.queryParams.orgId = request.OrganizeId;
            var itemData = await _chargeItemService.GetChargeItemPage(request.Data);
            if (itemData == null)
            {
                return new BusResult<PageResponseRow<List<SysChargeItemVO>>> { code = ResponseResultCode.FAIL, msg = "未找到相关数据" };
            }
            return new BusResult<PageResponseRow<List<SysChargeItemVO>>> { code = ResponseResultCode.SUCCESS, Data = itemData };
        }
        /// <summary>
        /// 材料项目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMaterialItemList")]
        public async Task<BusResult<List<SysChargeItemVO>>> GetMaterialItemListAsync(Request<QueryParamsBase> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysChargeItemVO>> { code = ResponseResultCode.FAIL, msg = "机构Id必传" };
            }
            request.Data.orgId = request.OrganizeId;
            var itemData = await _chargeItemService.GetMaterialItemList(request.Data);
            if (itemData == null)
            {
                return new BusResult<List<SysChargeItemVO>> { code = ResponseResultCode.FAIL, msg = "未找到相关数据" };
            }
            return new BusResult<List<SysChargeItemVO>> { code = ResponseResultCode.SUCCESS, Data = itemData };
        }
        /// <summary>
        /// 材料项目列表（分页）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMaterialItemPage")]
        public async Task<BusResult<PageResponseRow<List<SysChargeItemVO>>>> GetMaterialItemPageAsync(Request<OLPagination<QueryParamsBase>> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || request.Data.limit == 0)
            {
                return new BusResult<PageResponseRow<List<SysChargeItemVO>>> { code = ResponseResultCode.FAIL, msg = "机构Id必传及分页参数必传" };
            }
            request.Data.queryParams.orgId = request.OrganizeId;
            var itemData = await _chargeItemService.GetMaterialItemPage(request.Data);
            if (itemData == null)
            {
                return new BusResult<PageResponseRow<List<SysChargeItemVO>>> { code = ResponseResultCode.FAIL, msg = "未找到相关数据" };
            }
            return new BusResult<PageResponseRow<List<SysChargeItemVO>>> { code = ResponseResultCode.SUCCESS, Data = itemData };
        }
    }
}
