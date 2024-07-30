using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Sett.Request;
using Newtouch.HIS.Sett.Request.OutPatientPharmacy;

namespace Newtouch.HIS.Sett.API.Controllers
{
    /// <summary>
    /// 门诊药房
    /// </summary>
    [RoutePrefix("api/OutpatientPharmacy")]
    public class OutpatientPharmacyController : ApiControllerBase<OutpatientPharmacyController>
    {
        private readonly IOutpatientPharmacyAPIDmnService _outpatientPharmacyAPIDmnService;
        public OutpatientPharmacyController(IComponentContext com) : base(com)
        {

        }

        /// <summary>
        /// 门诊排药合计信息（待排药）
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseMedicineTotal")]
        public HttpResponseMessage WaitingDispenseMedicineTotal(WaitingDispenseMedicineQueryRequest par)
        {
            Action<WaitingDispenseMedicineQueryRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetFyhjInfo(par.yfbmCode, par.fysjs, par.fybz);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }


        /// <summary>
        /// 补打发药单 查询发药信息
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PrintFyInfo")]
        public HttpResponseMessage PrintFyInfo(FyListOnPrintRequest par)
        {
            Action<FyListOnPrintRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetFyListOnPrint(par.ksdm, par.fph, par.xm, par.kh, par.kssj, par.jssj, UserIdentity.OrganizeId); ;
                resp.code = ResponseResultCode.SUCCESS;
            };
            return base.CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 门诊排药查询
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseMedicineQuery")]
        public HttpResponseMessage WaitingDispenseMedicineQuery(WaitingDispenseMedicineListRequest par)
        {

            Action<WaitingDispenseMedicineListRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetFyList(par.yfbmCode, par.yxq, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 门诊排药药品详细查询
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseMedicineDetailQuery")]
        public HttpResponseMessage WaitingDispenseMedicineDetailQuery(WaitingDispenseMedicineDetailListRequest par)
        {

            Action<WaitingDispenseMedicineDetailListRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetFyDetailList(par.cfh, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 根据处方内码获取排药参数list
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetpyParListBycfnm")]
        public HttpResponseMessage GetpyParListBycfnm(GetpyParListRequest par)
        {
            Action<GetpyParListRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetpyParList(par.cfnm, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 根据处方内码获取排药参数list
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetcfInfoBycfnm")]
        public HttpResponseMessage GetcfInfoBycfnm(GetpyParListRequest par)
        {
            Action<GetpyParListRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetcfInfo(par.cfnm, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 排药后更新mz_cf表的排药日期，排药人员，领药窗口，发药标志
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Updatefyzt")]
        public HttpResponseMessage Updatefyzt(UpdatefyztRequest par)
        {
            Action<UpdatefyztRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.Updatecfzt(par);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 门诊发药查询主表信息
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetfyMainInfoList")]
        public HttpResponseMessage GetfyMainInfoList(fyMainRequest par)
        {
            Action<fyMainRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetfyMainInfoList(par.keyword, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 门诊发药查询处方详细信息
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetfyDetailInfoList")]
        public HttpResponseMessage GetfyDetailInfoList(fyMainRequest par)
        {
            Action<fyMainRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetfyDetailInfoList(par.keyword, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 门诊发药,发药时获取处方验证信息
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetfyDetailCFInfo")]
        public HttpResponseMessage GetfyDetailCFInfo(fyMainRequest par)
        {
            Action<fyMainRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetfyDetailCFInfo(par.cfh, par.usercode, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 门诊发药,发药时获取处方验证信息
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeliveryRpDetailQuery")]
        public HttpResponseMessage DeliveryRpDetailQuery(DeliveryRpDetailQueryRequestDto par)
        {
            Action<DeliveryRpDetailQueryRequestDto, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetfyDetailCFInfo(par.cfh, par.lyyf, par.organizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 发药完成后更新处方表的发药标志
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatefyztByFY")]
        public HttpResponseMessage UpdatefyztByFY(UpdatefyztRequest par)
        {
            Action<UpdatefyztRequest, DefaultResponse> ac = (req, resp) =>
            {
                var rowCount = _outpatientPharmacyAPIDmnService.UpdatecfztByFY(par.cfnm, par.user_code, "2");
                resp.code = rowCount ? ResponseResultCode.SUCCESS : ResponseResultCode.FAIL;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 门诊退药显示主表信息
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GettyMainInfoList")]
        public HttpResponseMessage GettyMainInfoList(tyCFYpInfoRequest par)
        {
            Action<tyCFYpInfoRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetTyMainInfoList(par, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 门诊退药查询处方详细信息
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GettyDetailInfoList")]
        public HttpResponseMessage GettyDetailInfoList(fyMainRequest par)
        {
            Action<fyMainRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GettyDetailInfoList(par.cfh, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 门诊发药查询 查主表信息
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetfyQueryInfoList")]
        public HttpResponseMessage GetfyQueryInfoList(searchFyInfoReqVO par)
        {
            Action<searchFyInfoReqVO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetfyQueryInfoList(par, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 门诊发药查询显示子表信息
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetfyQueryDetailInfoList")]
        public HttpResponseMessage GetfyQueryDetailInfoList(GetpyParListRequest par)
        {
            Action<GetpyParListRequest, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.GetfyQueryDetailInfoList(par.cfnm, UserIdentity.OrganizeId);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 已结算处方明细查询
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientSettledRpQuery")]
        public HttpResponseMessage OutpatientSettledRpQuery(OutpatientSettledRpQueryRequestDTO par)
        {
            Action<OutpatientSettledRpQueryRequestDTO, DefaultResponse> ac = (req, resp) =>
            {
                resp.data = _outpatientPharmacyAPIDmnService.OutpatientSettledRpQuery(UserIdentity.OrganizeId, req.kssj, req.jssj, req.fph, req.kh, req.yfCode,req.mzh);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

        /// <summary>
        /// 通知门诊退药
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OutpatientDrugWithdrawalNotify")]
        public HttpResponseMessage OutpatientDrugWithdrawalNotify(OutpatientDrugWithdrawalNotifyRequest par)
        {
            Action<OutpatientDrugWithdrawalNotifyRequest, DefaultResponse> ac = (req, resp) =>
            {
                _outpatientPharmacyAPIDmnService.OutpatientDrugWithdrawalNotify(par.OrganizeId, par.cfnm, par.yp, par.sl, par.czh);
                resp.code = ResponseResultCode.SUCCESS;
            };
            return CreateResponse(CommonExecute(ac, par));
        }

    }
}
