using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Services.HttpService;
using NewtouchHIS.WebAPI.Manage.Controllers;
using NewtouchHIS.WebAPI.Manage.Models;
using NewtouchHIS.WebAPI.Manage.Models.Organize;
using NewtouchHIS.WebAPI.Manage.Models.OutPatient;

namespace NewtouchHIS.WebAPI.Manage.Areas.Sett.Controllers
{
    /// <summary>
    /// 门诊预约
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ApiBaseController
    {
        private readonly string BookingMethodRoute = "api/OutpBook/BookingESB";
        public BookingController(IHttpClientHelper httpClient) :base(httpClient)
        {
            
        }

        /// <summary>
        /// 科室列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetOutpDepartment")]
        [HttpPost]
        public async Task<ResponseBase> GetOutpDepartmentAsync(RequestBus<DepartmentRequest> request)
        {
            var apiReq = InitController.BuildBookRequest(request, "Y002", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// 医生列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetOutpDoctor")]
        [HttpPost]
        public async Task<ResponseBase> GetOutpDoctorAsync(RequestBus<StaffRequest> request)
        {
            var apiReq = InitController.BuildBookRequest(request, "Y003", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// 排班信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetOutSchedule")]
        [HttpPost]
        public async Task<ResponseBase> GetOutScheduleAsync(RequestBus<ScheduleRequest> request)
        {
            if(request.Data == null || request.Data.ksrq==null || request.Data.jsrq == null)
            {
                request.Data!.ksrq = DateTime.Now.AddDays(1).Date;
                request.Data!.jsrq = DateTime.Now.AddDays(14).Date;
            }
            var apiReq = InitController.BuildBookRequest(request, "Y009", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// 排班信息分页查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetOutSchedulePage")]
        [HttpPost]
        public async Task<ResponseBase> GetOutSchedulePageAsync(RequestBus<Pagination<ScheduleRequest>> request)
        {
            if (request.Data == null || request.Data!.filter!.ksrq == null || request.Data!.filter!.jsrq == null)
            {
                request.Data!.filter!.ksrq = DateTime.Now.AddDays(1).Date;
                request.Data!.filter!.jsrq = DateTime.Now.AddDays(14).Date;
            }
            var groupReq = request.Data==null?new SchedulePageRequest(): request.Data.filter.Adapt<SchedulePageRequest>();
            groupReq.pagination = new Pagination
            {
                page = request.Data!.page,
                rows = request.Data!.rows,
                sidx = request.Data!.sidx,
                records = request.Data!.records,
                sord = request.Data!.sord
            }.ToJson();
            var apiReq = InitController.BuildBookRequest(request, "Y009", "1").Adapt<BookingRequest>(); //optype 1 分页查询
            apiReq.paradata = groupReq;            
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp,request.Data);
        }
        /// <summary>
        /// 预约列表查询by患者信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("PatBookListQuery")]
        [HttpPost]
        public async Task<ResponseBase> PatBookListQueryAsync(RequestBus<BookRecordRequest> request)
        {
            if (request.Data == null || (request.Data.kh == null && request.Data.xm == null && request.Data.zjh == null))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(kh、xm、zjh)不可全部为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y021", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// 预约列表查询by患者信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("PatBookDetailQuery")]
        [HttpPost]
        public async Task<ResponseBase> PatBookDetailQueryAsync(RequestBus<BookRecordRequest> request)
        {
            if (request.Data == null || request.Data.BookId == null)
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(BookId)不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y021", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        
        #region 操作类
        /// <summary>
        /// 预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("ScheduleBookApply")]
        [HttpPost]
        public async Task<ResponseBase> ScheduleBookApplyAsync(RequestBus<BookApplyRequest> request)
        {
            if (request.Data == null || request.Data.kh == null || request.Data.ghxz == null || request.Data.ScheduId == null)
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(kh、ghxz、scheduId)不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y022", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("ScheduleBookCancel")]
        [HttpPost]
        public async Task<ResponseBase> ScheduleBookCancelAsync(RequestBus<BookCancelRequest> request)
        {
            if (request.Data == null || request.Data.kh == null || request.Data.BookId == null)
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "关键信息(kh、BookId)不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y023", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// 预约挂号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("BookOutpatientRegist")]
        [HttpPost]
        public async Task<ResponseBase> BookOutpatientRegistAsync(RequestBus<OutpatientRegistRequest> request)
        {
            if (request.Data == null || (request.Data.kh == null && request.Data.BookId == null))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "预约挂号BookId&kh不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y024", "0").Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        /// <summary>
        /// 普通门诊挂号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("OutpatientRegist")]
        [HttpPost]
        public async Task<ResponseBase> OutpatientRegistAsync(RequestBus<OutpatientRegistRequest> request)
        {
            if (request.Data == null || (request.Data.kh == null && request.Data.ScheduId == null))
            {
                return new ResponseBase { code = ResponseResultCode.FAIL, msg = "普通挂号ScheduId&kh不可为空" };
            }
            var apiReq = InitController.BuildBookRequest(request, "Y026", null).Adapt<BookingRequest>();
            apiReq.paradata = request.Data;
            var resp = await _httpClient.PostAsync<ResponseBaseOld>($"{SettHttpUrl}{BookingMethodRoute}", apiReq.ToJson());
            return InitController.BuildResponseBase(resp);
        }
        #endregion

        #region test
        //public ResponseBase InpatientBaseInfo(RequestBus<InHosPaitentBO> request)
        //{
        //    var req = new InHosPaitentBO()
        //    {
        //        AppId = "sett",
        //        zyh = "03223",
        //        xm = "测试0000",
        //        OrganizeId = "6d5752a7-234a-403e-aa1c-df8b45d3469f",
        //        Token = "Ohqr8wDq7y98zCzqur0eFCadOyT8qLziPSGnpnQZaUAnkXJFyEf+PST7VX41fDcs5/waRDYfMYghRc+fo9BcGfa2wuRQ2szV9OExMqwUXzvctL2zeOOVPgmUeM/bUHzmE2/962pydzCiCPFPxgWwjyJbr2LwsWNXFWWYHNJqBQseNyYM0QGfT0NWmIC7Ldqppw4RjTfOj9xQt473pnvwy9/TBB0V0Ri61tiwYX9DOyTpXh/XXr3eHgCTjg4EcZQvYTn8nRK2ueWEjxMSSS9/6aYO1aDjLl0ihEjRSEpI/7cJAbMVw5aSIe2N89ic4Tx3mpaQ4GlV5Og30bziCxp0q8eQMitqUjhyASoStnHdtfNnjWuqdcAVQ8p7I7rE8u/rRy26dMzvRtloy1AX97gASuN5TMQ7kfbcPotMy6ZJ2rT1CeMoTph+8cBOL/Imk56YUuBTFrgyD2/oaQouSyzqXkMOSKiAf0dFo4Wk/vhPR/omNGAn4ROWXRZyLJqNmAJ4",
        //        TokenType = "kmC8PQOhtFhIpYgt3nK7B9tLGuxvkRUjT1Lkv0M5WevITFPuJheFdPt4StgV6rsS6Vjc2wOcoUDt8cn+aM0rfiw8wBlCDFkq940kAeUmpubcGgIXFVBoCfcHMHiuDeah2elMoqgXy8RGVP2Smh32Eyja005GJPZjtB+cwiXB/9Q=",
        //    };
        //    //return _httpWebRequest.Request<ResponseBase>($"{HttpUrl}/api/PatInfo/InpatientBaseInfo", req.ToJson());
        //    //return _httpClient.HttpPostStringAndRead<ResponseBase>($"{HttpUrl}/api/PatInfo/InpatientBaseInfo", JsonConvert.SerializeObject(req));
        //    return _httpClient.Post<ResponseBase>($"{HttpUrl}/api/PatInfo/InpatientBaseInfo", JsonConvert.SerializeObject(req));
        //}
        #endregion
    }
}
