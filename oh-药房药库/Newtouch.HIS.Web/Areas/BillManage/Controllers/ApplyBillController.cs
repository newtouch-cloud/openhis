using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Application.Implementation.Process;

namespace Newtouch.HIS.Web.Areas.BillManage.Controllers
{
    /// <summary>
    /// 内部申领单
    /// </summary>
    public class ApplyBillController : ControllerBase
    {
        private readonly IApplyDmnService applyDmnService;

        #region 内部申领单查询
        /// <summary>
        /// 内部申领单查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Query()
        {
            return View();
        }

        /// <summary>
        /// 获取申领单主信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ffzt"></param>
        /// <param name="djh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet]
        //[Core.Attributes.HandlerAuthorizeIgnore]
        public ActionResult ApplyMainInfoSearch(Pagination pagination, int ffzt, string djh, DateTime startTime, DateTime endTime, string slbm = "")
        {
            var receiptMaininfoList = new
            {
                rows = applyDmnService.GetApplyMainInfo(pagination, ffzt, djh, startTime, endTime, Constants.CurrentYfbm.yfbmCode, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptMaininfoList.ToJson());
        }

        /// <summary>
        /// 获取申领单主信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ffzt"></param>
        /// <param name="djh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ApplyDetailSearch(Pagination pagination, string sldId)
        {
            var receiptdetailList = new
            {
                rows = applyDmnService.GetApplyDetails(pagination, sldId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptdetailList.ToJson());
        }
        #endregion

        #region 申领出库

        /// <summary>
        /// 申领出库
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplyOutStock()
        {
            return View();
        }

        /// <summary>
        /// 获取申领出库明细
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldIds"></param>
        /// <returns></returns>
        public ActionResult ApplyOutStockDetailSearch(Pagination pagination, string sldIds)
        {
            IList<ApplyOutStockVEntity> data = new List<ApplyOutStockVEntity>();
            if (!string.IsNullOrWhiteSpace(sldIds))
            {
                data = applyDmnService.GetApplyOutStockDetail(pagination, sldIds, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            }
            var receiptdetailList = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptdetailList.ToJson());
        }

        /// <summary>
        /// 获取申领出库明细
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldIds"></param>
        /// <returns></returns>
        public ActionResult Abandonzt(string sldId)
        {
            var result = applyDmnService.UpgradeStatus(sldId, (int)EnumSLDDeliveryStatus.Abandon, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// 提交发药
        /// </summary>
        /// <param name="fymx"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitApplyOutStock(List<ApplyOutStockVEntity> fymx, string slbm)
        {
            var processResult = new SubmitApplyOutStockProcess(fymx).Process();
            return processResult.IsSucceed ? Success() : Error(processResult.ResultMsg);
        }

        #endregion
    }
}