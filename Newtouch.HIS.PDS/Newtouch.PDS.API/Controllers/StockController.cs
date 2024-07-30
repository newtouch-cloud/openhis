using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Web.Core.Attributes;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.Stock;

namespace Newtouch.PDS.API.Controllers
{
    [RoutePrefix("api/Stock")]
    public class StockController : ApiControllerBase<StockController>
    {
        private readonly IDrugStorageApp _drugStorageApp;

        public StockController(IComponentContext com) : base(com)
        {
        }

        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("query")]
        [HandlerAuthorizeIgnore]
        public HttpResponseMessage query(StockQueryRequestDTO par)
        {
            Action<StockQueryRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _drugStorageApp.StockQuery(req.yplist, req.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }
        [HttpPost]
        [Route("PrepareMedicine")]
        [HandlerAuthorizeIgnore]
        public HttpResponseMessage PrepareMedicine(PrepareMedicineDTO par)
        {
            Action<PrepareMedicineDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.msg = _drugStorageApp.PrepareMedicine(req.yplist, req.OrganizeId,req.yhgh);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }
        [HttpPost]
        [Route("WithdrawalPreparation")]
        [HandlerAuthorizeIgnore]
        public HttpResponseMessage WithdrawalPreparation(WithdrawalPrepareDTO par)
        {
            Action<WithdrawalPrepareDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.msg = _drugStorageApp.WithdrawalPreparation(req.Djh, req.OrganizeId, req.yhgh,par.shzt);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }
        
        [HttpPost]
        [Route("PrepareMedicineReturn")]
        [HandlerAuthorizeIgnore]
        public HttpResponseMessage PrepareMedicineReturn(PrepareMedicineReturnDTO par)
        {
            Action<PrepareMedicineReturnDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.msg = _drugStorageApp.PrepareMedicineReturn(req.yplist, req.OrganizeId, req.yhgh);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }


        [HttpPost]
        [Route("WithdrawalPreparationReturn")]
        [HandlerAuthorizeIgnore]
        public HttpResponseMessage WithdrawalPreparationReturn(WithdrawalPrepareDTO par)
        {
            Action<WithdrawalPrepareDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.msg = _drugStorageApp.WithdrawalPreparationReturn(req.Djh, req.OrganizeId, req.yhgh, par.shzt);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

    }
}
