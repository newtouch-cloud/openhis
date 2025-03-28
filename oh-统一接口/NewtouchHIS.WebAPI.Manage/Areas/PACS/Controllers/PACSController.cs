using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Domain.Entity.PACS;
using NewtouchHIS.Domain.IDomainService;
using NewtouchHIS.Domain.IDomainService.PACS;
using NewtouchHIS.Domain.InterfaceObjets;
using NewtouchHIS.Domain.InterfaceObjets.Pacs;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model.PACS;

namespace NewtouchHIS.WebAPI.Manage.Areas.PACS.Controllers
{
    /// <summary>
    /// Pacs 接口服务
    /// </summary>
    [Route("api/pacs")]
    [ApiController]
    public class PACSController : ControllerBase
    {
        public readonly IOrgBaseDmnService _orgBaseDmnService;
        public readonly IPacsDmnService _pacsDmnService;

        public PACSController(IOrgBaseDmnService orgBaseDmnService
            , IPacsDmnService pacsDmnService
            )
        {
            _orgBaseDmnService = orgBaseDmnService;
            _pacsDmnService = pacsDmnService;
        }


        /// <summary>
        /// 获取科室字典
        /// </summary>
        /// <returns>List</returns>
        [HttpPost]
        [Route("PacsDeptQuery")]
        public async Task<BusResult<List<HisDeptVO>>> PacsDeptQuery(Request<QueryParamsBase> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<HisDeptVO>> { code = ResponseResultCode.FAIL, msg = "机构信息不可为空" };
            }
            var result = await _orgBaseDmnService.HisDeptQuery(request.OrganizeId, request.Data?.keyword);
            if (result == null)
            {
                return new BusResult<List<HisDeptVO>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<HisDeptVO>> { code = ResponseResultCode.SUCCESS, msg = "接收成功", Data = result };
        }

        /// <summary>
        /// 获取医生字典
        /// </summary>
        /// <returns>List</returns>
        [HttpPost]
        [Route("PacsDocQuery")]
        public async Task<BusResult<List<HisStaffVO>>> PacsDocQuery(Request<QueryParamsBase> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<HisStaffVO>> { code = ResponseResultCode.FAIL, msg = "机构信息不可为空" };
            }
            var result = await _orgBaseDmnService.HisDocQuery(request.OrganizeId, request.Data?.keyword);
            if (result == null)
            {
                return new BusResult<List<HisStaffVO>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<HisStaffVO>> { code = ResponseResultCode.SUCCESS, msg = "接收成功", Data = result };
        }

        /// <summary>
        /// 获取收费项目字典
        /// </summary>
        /// <returns>List</returns>
        [HttpPost]
        [Route("PacsFeeitemQuery")]
        public async Task<BusResult<List<PacsFeeitemVEntity>>> PacsFeeitemQuery(Request<QueryParamsBase> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<PacsFeeitemVEntity>> { code = ResponseResultCode.FAIL, msg = "机构信息不可为空" };
            }
            var result = await _pacsDmnService.PacsFeeitemQuery(request.OrganizeId, request.Data?.keyword);
            if (result == null)
            {
                return new BusResult<List<PacsFeeitemVEntity>> { code = ResponseResultCode.FAIL, msg = "未找到相关信息" };
            }
            return new BusResult<List<PacsFeeitemVEntity>> { code = ResponseResultCode.SUCCESS, msg = "接收成功", Data = result };
        }



        /// <summary>
        /// 获取pacs报告结果（金风易通版）
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PACSPatientReportV2")]
        public BusResult<List<PACSgetPatientReportVO>> PACSgetPatientReportV2Async(Request<PacsIndexVO> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<PACSgetPatientReportVO>> { code = ResponseResultCode.FAIL, msg = "机构信息不可为空" };
            }
            if (request.Data == null || request.Data?.mzzybz == null || (string.IsNullOrWhiteSpace(request.Data.sqdh) && string.IsNullOrWhiteSpace(request.Data.ywlsh)))
            {
                return new BusResult<List<PACSgetPatientReportVO>> { code = ResponseResultCode.FAIL, msg = "mzzybz 不可为空、sqdh与ywlsh不可同时为空" };
            }

            List<PACSgetPatientReportDTO> list = new List<PACSgetPatientReportDTO>();
            //foreach (var obj in post.reqBody)
            //{
            //    PACSgetPatientReportDTO entity = new PACSgetPatientReportDTO();
            //    entity.hosCode = obj.hosCode;
            //    entity.pid = obj.pid;
            //    entity.applyNo = obj.applyNo;
            //    entity.checkTime = obj.checkTime;
            //    entity.ssid = obj.ssid;
            //    list.Add(entity);
            //}

            //调用接口 (配置)

            //返回参数
            ApiResult<List<PACSgetPatientReportVO>> result = new ApiResult<List<PACSgetPatientReportVO>>();
            List<PACSgetPatientReportVO> data = new List<PACSgetPatientReportVO>();

            if (result == null)
            {
                return new BusResult<List<PACSgetPatientReportVO>> { code = ResponseResultCode.FAIL, msg = "error" };
            }
            return new BusResult<List<PACSgetPatientReportVO>> { code = ResponseResultCode.SUCCESS, msg = "接收成功", Data = data };
        }
    }
}
