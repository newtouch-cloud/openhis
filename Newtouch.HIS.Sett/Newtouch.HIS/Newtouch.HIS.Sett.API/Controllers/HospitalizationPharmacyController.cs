using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Domain.IDomainServices.HospitalizationManage;
using Newtouch.HIS.Sett.Request.HospitalizationPharmacy;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.HIS.Sett.API.Controllers
{
    /// <summary>
    /// 门诊药房
    /// </summary>
    [RoutePrefix("api/HospitalizationPharmacy")]
    public class HospitalizationPharmacyController : ApiControllerBase<HospitalizationPharmacyController>
    {
        private readonly IDispenseIndexDmnService _idispenseIndexDmnService;

        public HospitalizationPharmacyController(IComponentContext com)
            : base(com)
        {

        }
        #region 住院发药页面绑定信息

        /// <summary>
        /// 住院发药病区
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseBQDispenseBRTotal")]
        public HttpResponseMessage WaitingDispenseBQDispenseBRTotal(WaitingDispenseBQMedicineDispenseRequest par)
        {
            Action<WaitingDispenseBQMedicineDispenseRequest, DefaultResponse> ac = (req, resp) =>
           {
               var data = _idispenseIndexDmnService.GetZYFYBQPagList(par.zyyf, UserIdentity.OrganizeId);
               resp.data = data;
               resp.code = ResponseResultCode.SUCCESS;
           };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// 住院发药病人
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseDispenseBRTotal")]
        public HttpResponseMessage WaitingDispenseDispenseBRTotal(WaitingDispenseMedicineDispenseRequest par)
        {
            Action<WaitingDispenseMedicineDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _idispenseIndexDmnService.GetZYFYBRPagList(par.zyyf, par.bq);
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// 住院发药人员详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseDispenseRYXXTotal")]
        public HttpResponseMessage WaitingDispenseDispenseRYXXTotal(WaitingDispenseBRXXMedicineDispenseRequest par)
        {
            Action<WaitingDispenseBRXXMedicineDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                //var data = _idispenseIndexDmnService.GetZYFYXXPagList(par.Bq, par.Zyh, par.Kssj,par.Jssj, par.Cl, par.Fyzt);
                var data = new
                {
                    rows = _idispenseIndexDmnService.GetZYFYXXPagList(par),
                    total = par.pagination.total,
                    page = par.pagination.page,
                    records = par.pagination.records
                };
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
        #endregion

        #region 配药操作
        /// <summary>
        /// 查询用户执行配药 根据开始时间结束时间获取住院病人医嘱,药品，数量等信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseDispenseBQPYTotal")]
        public HttpResponseMessage WaitingDispenseDispenseBQPYTotal(WaitingDispenseBQBRPYMedicineDispenseRequest par)
        {
            Action<WaitingDispenseBQBRPYMedicineDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _idispenseIndexDmnService.SetZYBRBQPY(par);
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }

        //查询用户执行配药  配药操作后修改发药备注 状态
        [HttpPost]
        [Route("WaitingDispenseDispenseUPBQPYyzzxTotal")]
        public HttpResponseMessage WaitingDispenseDispenseUPBQPYyzzxTotal(WaitingDispenseBQPYyzzxZTDispenseRequest par)
        {
            Action<WaitingDispenseBQPYyzzxZTDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _idispenseIndexDmnService.UpBQPYyzzx(par);
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
        #endregion

        #region 发药操作


        #endregion

        #region 发药查询操作
        /// <summary>
        /// 住院发药人员详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseDispenseRYCXXXTotal")]
        public HttpResponseMessage WaitingDispenseDispenseRYCXXXTotal(WaitingDispenseBRCXXXMedicineDispenseRequest par)
        {
            Action<WaitingDispenseBRCXXXMedicineDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                //var data = _idispenseIndexDmnService.GetZYFYXXPagList(par.Bq, par.Zyh, par.Kssj,par.Jssj, par.Cl, par.Fyzt);
                var data = new
                {
                    rows = _idispenseIndexDmnService.GetZYFYCXXXPagList(par),
                    total = par.pagination.total,
                    page = par.pagination.page,
                    records = par.pagination.records
                };
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
        #endregion

        #region 退药操作

        /// <summary>
        /// 获取退药明细医生站医嘱ID 医嘱表ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseDispenseTYYszyzlist")]
        public HttpResponseMessage WaitingDispenseDispenseTYYszyzlist(WaitingDispenseTYYszyzIdDispenseRequest par)
        {
            Action<WaitingDispenseTYYszyzIdDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _idispenseIndexDmnService.GetZYYszysList(par);
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// 获取住院病区退药人员详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WaitingDispenseDispenseTYRYXXTotal")]
        public HttpResponseMessage WaitingDispenseDispenseTYRYXXTotal(WaitingDispenseBRTYXXMedicineDispenseRequest par)
        {
            Action<WaitingDispenseBRTYXXMedicineDispenseRequest, DefaultResponse> ac = (req, resp) =>
            {
                var data = _idispenseIndexDmnService.GetZYTYXXPagList(par);
                resp.data = data;
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, par);
            return base.CreateResponse(response);
        }
        #endregion
    }
}
