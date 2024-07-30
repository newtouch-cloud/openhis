using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Filter;
using Newtouch.HIS.Application.Interface;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// 资源操作
    /// </summary>
    [RoutePrefix("api/ResourcesOperate")]
    public class ResourcesOperateController : ApiControllerBase<ResourcesOperateController>
    {
        private readonly IResourcesOperateApp _resourcesOperateApp;

        public ResourcesOperateController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// 资源预定（冻结库存）
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientBook")]
        public HttpResponseMessage OutpatientBook(OutpatientBookRequestDTO par)
        {
            Action<OutpatientBookRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientBook(par);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, par);
            return CreateResponse(result);
        }

        /// <summary>
        /// 资源预定修改
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientBookModify")]
        public HttpResponseMessage OutpatientBookModify(OutpatientBookModifyRequestDTO par)
        {
            Action<OutpatientBookModifyRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientBookModify(par);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, par);
            return CreateResponse(result);
        }

        /// <summary>
        /// 取消部分预定
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientCancelPartBook")]
        public HttpResponseMessage OutpatientCancelPartBook(OutpatientCancelPartBookRequestDTO request)
        {
            Action<OutpatientCancelPartBookRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCancelBook(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }

        /// <summary>
        /// 取消指定处方全部预定
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientCancelAllBook")]
        public HttpResponseMessage OutpatientCancelAllBook(OutpatientCancelAllBookRequestDTO request)
        {
            Action<OutpatientCancelAllBookRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCancelBook(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }

        /// <summary>
        /// 门诊确认资源（Commit）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientCommit")]
        public HttpResponseMessage OutpatientCommit(OutpatientCommitRequestDTO request)
        {
            Action<OutpatientCommitRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCommit(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }
        /// <summary>
        /// 门诊确认资源（Commit）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientCommitApi")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage OutpatientCommitforAPI(OutpatientCommitRequestDTO request)
        {
            Action<OutpatientCommitRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCommit(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            request.Token = null;
            request.TokenType = null;
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }
        /// <summary>
        /// (未发药前)部分退药
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientPartReturnBeforeDispensingMedicine")]
        [IgnoreTokenDecrypt]
        public HttpResponseMessage OutpatientPartReturnBeforeDispensingMedicine(OutpatientPartReturnBeforeDispensingMedicineRequestDTO request)
        {
            Action<OutpatientPartReturnBeforeDispensingMedicineRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientPartReturnBeforeDispensingMedicine(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }

        /// <summary>
        /// 门诊取消排药
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientCancelArrangement")]
        public HttpResponseMessage OutpatientCancelArrangement(OutpatientCancelArrangementRequestDTO request)
        {
            Action<OutpatientCancelArrangementRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.OutpatientCancelArrangement(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }

        /// <summary>
        /// 住院退药
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("HospitalizatiionReturnDispensingMedicine")]
        public HttpResponseMessage HospitalizatiionReturnDispensingMedicine(HospitalizatiionReturnDispensingMedicineRequestDTO request)
        {
            Action<HospitalizatiionReturnDispensingMedicineRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _resourcesOperateApp.HospitalizatiionReturnDispensingMedicine(request);
                resp.code = ResponseResultCode.SUCCESS;
            };
            var result = CommonExecute(ac, request);
            return CreateResponse(result);
        }
    }
}