using System;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.StorageManage.Controllers
{
    /// <summary>
    /// 库存盘点
    /// </summary>
    public class StockInventoryController : ControllerBase
    {
        private readonly IStockInventoryDmnService _stockInventoryDmnService;
        private readonly IStockInventoryApp _stockInventoryApp;
        private readonly IKcKcjzDmnService _kcKcjzDmnService;

        /// <summary>
        /// 开始盘点
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryAction()
        {
            return View();
        }

        /// <summary>
        /// 开始盘点
        /// </summary>
        /// <returns></returns>
        public ActionResult InventoryQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// 获取盘点时间
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPdsj()
        {
            var result = _stockInventoryDmnService.GetPdSj(Constants.CurrentKf.currentKfId, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 开始盘点
        /// </summary>
        /// <returns></returns>
        public ActionResult StartInventory()
        {
            return Success(null, _stockInventoryApp.StartInventory(Constants.CurrentKf.currentKfId, OrganizeId));
        }

        /// <summary>
        /// 查询盘点信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inventorySearchDTO"></param>
        /// <returns></returns>
        public ActionResult QueryInventoryInfoList(Pagination pagination, InventorySearchDTO inventorySearchDTO)
        {
            var list = new
            {
                rows = inventorySearchDTO == null || inventorySearchDTO.pdId <= 0 ? null : _stockInventoryDmnService.QueryInventoryInfoList(pagination, inventorySearchDTO, Constants.CurrentKf.currentKfId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 查询盘点信息 不分批次
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="inventorySearchDTO"></param>
        /// <returns></returns>
        public ActionResult QueryInventoryInfoListNoPc(Pagination pagination, InventorySearchDTO inventorySearchDTO)
        {
            var list = new
            {
                rows = inventorySearchDTO == null || inventorySearchDTO.pdId <= 0 ? null : _stockInventoryDmnService.QueryInventoryInfoListNoPc(pagination, inventorySearchDTO, Constants.CurrentKf.currentKfId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// 保存盘点
        /// </summary>
        /// <param name="resultObjArr"></param>
        /// <param name="pdId"></param>
        /// <param name="noPc">0：按批次盘点  1：不按批次盘点</param>
        /// <returns></returns>
        public ActionResult SaveInventory(string resultObjArr, long pdId, string noPc)
        {
            _stockInventoryApp.SaveInventoryInfo(resultObjArr.ToList<SaveInventoryDTO>(), pdId, noPc);
            return Success();
        }

        /// <summary>
        /// 取消盘点
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelInventory(long pdId)
        {
            _stockInventoryApp.CancelInventory(pdId);
            return Success();
        }

        /// <summary>
        /// 结束盘点
        /// </summary>
        /// <param name="pdId">盘点单ID</param>
        /// <param name="autoCarryDown">null/0：不自动结转  1：自动结转</param>
        /// <returns></returns>
        public ActionResult EndInventory(long pdId, string autoCarryDown)
        {
            _stockInventoryApp.EndInventory(pdId);
            if (!"1".Equals(autoCarryDown)) return Success();
            var carryOverResult = _kcKcjzDmnService.CarryOverProduct(Constants.CurrentKf.currentKfId, OrganizeId, OperatorProvider.GetCurrent().UserCode);
            if (!string.IsNullOrWhiteSpace(carryOverResult)) throw new Exception(carryOverResult);
            return Success();
        }
    }
}