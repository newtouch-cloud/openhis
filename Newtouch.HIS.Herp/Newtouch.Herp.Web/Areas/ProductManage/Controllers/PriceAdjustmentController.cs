using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Common;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.ProductManage.Controllers
{
    public class PriceAdjustmentController : ControllerBase
    {
        private readonly IWzPriceAdjustmentRepo _wzPriceAdjustmentRepo;
        private readonly IWzPriceAdjustmentDmnService _wzPriceAdjustmentDmnService;
        private readonly IAdjustPriceApp adjustPriceApp;

        /// <summary>
        /// GET: WarehouseManage/PriceAdjustment  调价申请
        /// </summary>
        /// <returns></returns>
        public ActionResult PriceAdjustmentRequest()
        {
            return View();
        }

        /// <summary>
        /// 调价审核
        /// </summary>
        /// <returns></returns>
        public ActionResult PriceAdjustmentApproval()
        {
            return View();
        }

        /// <summary>
        /// 调价历史
        /// </summary>
        /// <returns></returns>
        public ActionResult PriceAdjustmentHistory()
        {
            return View();
        }

        /// <summary>
        /// 调价盈亏查询
        /// </summary>
        /// <returns></returns>
        public ActionResult PriceAdjustmentProfitAndLossQuery()
        {
            return View();
        }

        /// <summary>
        /// 提交调价申请
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult SubmitPriceAdjustmentRequest(SubmitPriceAdjustmentRequestDTO param)
        {
            if (_wzPriceAdjustmentRepo.GetUnexecutedList(param.productId, OrganizeId).Any())
            {
                return Error("该物资已申请调价（未执行），请先执行已提交申请！");
            }

            var entity = new WzPriceAdjustmentEntity
            {
                lsj = param.lsj,
                OrganizeId = OrganizeId,
                productId = param.productId,
                zhyz = param.zhyz,
                dwmc = param.dwmc,
                shzt = ((int)EnumTjShzt.Waiting).ToString(),
                tzczy = OperatorProvider.GetCurrent().UserCode,
                shczy = null,
                tzsj = DateTime.Now,
                tzwj = param.tzwj,
                warehouseId = Constants.CurrentKf.currentKfId,
                xglx = "tj",
                zt = ((int)Enumzt.Enable).ToString(),
                zxbz = ((int)EnumTjZxbz.Not).ToString(),
                ylsj = param.ylsj,
                zxsj = param.zxsj
            };
            entity.Create(true);
            return _wzPriceAdjustmentRepo.Insert(entity) > 0 ? Success() : Error("保存调价信息失败");
        }

        /// <summary>
        /// 调价审核查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        public ActionResult SelectAdjustPriceApprovalList(Pagination pagination, string inputCode, string shzt)
        {
            var list = new
            {
                rows = _wzPriceAdjustmentDmnService.GetPriceAdjustmentList(pagination, inputCode, shzt, OrganizeId, ((int)EnumTjZxbz.Not).ToString()),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 调价审核 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public ActionResult AdjustPriceApproval(string ids, string operationType)
        {
            var idList = ids.TrimEnd(',').Split(',').ToList();
            var result = _wzPriceAdjustmentDmnService.Approval(idList, operationType, OperatorProvider.GetCurrent().UserCode, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// 调价执行
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult AdjustPriceExecute(string ids)
        {
            var result = adjustPriceApp.AdjustPriceExecute(ids);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// 调价历史查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inputCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ActionResult SelectAdjustPriceHistoryList(Pagination pagination, string inputCode, DateTime startTime, DateTime endTime)
        {
            var list = new
            {
                rows = _wzPriceAdjustmentDmnService.GetPriceAdjustmentHistoryList(pagination, inputCode, OrganizeId, startTime, endTime, Constants.CurrentKf.currentKfId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 调价历史查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetPriceAdjustmentProfitLoss(Pagination pagination, PriceAdjustmentProfitLossDTO param)
        {
            param.kfList = new System.Collections.Generic.List<string>();
            param.organizeId = OrganizeId;
            if (Constants.CurrentKf.kfList != null && Constants.CurrentKf.kfList.Count > 0)
            {
                Constants.CurrentKf.kfList.ForEach(p =>
                {
                    param.kfList.Add("'" + p.kfId.FilterSql() + "'");
                });
            }
            var list = new
            {
                rows = _wzPriceAdjustmentDmnService.GetPriceAdjustmentProfitLoss(pagination, param),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取当前登陆者所有库房
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCurrentUserKfList()
        {
            var list = Constants.CurrentKf.kfList;
            return Content(list.ToJson());
        }
    }
}