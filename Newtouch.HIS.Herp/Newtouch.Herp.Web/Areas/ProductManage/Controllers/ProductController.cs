using System;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.ProductManage.Controllers
{
    /// <summary>
    /// 物资管理
    /// </summary>
    public class ProductController : ControllerBase
    {
        private readonly IWzProductDmnService _wzProductDmnService;
        private readonly IProductApp _productApp;
        private readonly IWzProductRepo _wzProductRepo;
        private readonly IRelProductUnitRepo _relProductUnitRepo;

        public override ActionResult Form()
        {
            var maxSize = ConfigurationHelper.GetAppConfigValue("imageMaxSize");
            maxSize = string.IsNullOrWhiteSpace(maxSize) ? 5.ToString() : maxSize;
            ViewData["maxSize"] = Convert.ToInt32(maxSize);
            return View();
        }

        /// <summary>
        /// 物资列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetProductGridJson(Pagination pagination, string lb, string zt, string keyWord)
        {
            var data = new
            {
                rows = _wzProductDmnService.GetList(pagination, OrganizeId, lb, zt, keyWord),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 删除物资
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            try
            {
                _wzProductDmnService.DeleteProduct(keyValue);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
            return Success("删除成功");
        }

        /// <summary>
        /// get product information
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyWord)
        {
            var entity = _wzProductDmnService.GetProductInfo(keyWord, OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// get product unit
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetProductUnitGridJson(string keyWord)
        {
            var units = _relProductUnitRepo.IQueryable(p => p.productId == keyWord && p.OrganizeId == OrganizeId).ToList();
            return Content(units.ToJson());
        }

        /// <summary>
        /// delete contacts
        /// </summary>
        /// <param name="relId"></param>
        /// <returns></returns>
        public ActionResult DeleteProductUnit(string relId)
        {
            return _relProductUnitRepo.DeleteById(relId) > 0 ? Success("删除成功") : Error("删除失败");
        }

        /// <summary>
        /// add or update product unit
        /// </summary>
        /// <param name="productUnitRelVo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitUnit(ProductUnitRelVo productUnitRelVo)
        {
            return _productApp.SubmitRelProductUnit(productUnitRelVo, OrganizeId) > 0 ? Success("操作成功") : Error("操作失败");
        }

        /// <summary>
        /// 物资维护 表单提交
        /// </summary>
        /// <param name="wzProductEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(VProductInfoEntity wzProductEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(_wzProductDmnService.CheckProductCode(wzProductEntity.productCode, keyValue)))
            {
                return Error("物资代码已存在");//防止出现并发情况
            }
            var file = Request.Files["imgUpload"];
            wzProductEntity.zt = wzProductEntity.zt == "on" ? "1" : "0";
            wzProductEntity.sflkc = wzProductEntity.sflkc == "on" ? "1" : "0";
            wzProductEntity.sffy = wzProductEntity.sffy == "on" ? "1" : "0";
            wzProductEntity.sfgt = wzProductEntity.sfgt == "on" ? "1" : "0";
            wzProductEntity.OrganizeId = OrganizeId;
            wzProductEntity.imageUrl = _productApp.UploadImag(file);
			wzProductEntity.iswzsame = wzProductEntity.iswzsame == "on" ? "1" : "0";
			return _productApp.SubmitForm(wzProductEntity, keyValue) > 0 ? Success("操作成功") : Error("操作失败");
        }

        /// <summary>
        /// 获取物资代码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductCode()
        {
            return Content(_wzProductDmnService.GetProductCode());
        }

        /// <summary>
        /// 检查物资代码是否已存在
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckProductCode(string productCode, string keyWord)
        {
            return Content(_wzProductDmnService.CheckProductCode(productCode, keyWord));
        }

        /// <summary>
        /// 根据物资类别获取物资
        /// </summary>
        /// <param name="typId"></param>
        /// <returns></returns>
        public ActionResult GetProductbyType(string typId)
        {
            var data = _wzProductRepo.IQueryable(p => p.typeId == typId && p.zt == "1" && p.OrganizeId == OrganizeId);
            if (data == null || !data.Any()) return Content(new { Id = "", name = "==请选择物资==" }.ToJson());
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取物资明细
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult ProductDetailQuery(string keyword)
        {
            var result = _wzProductDmnService.SelectProductDetail(keyword, OrganizeId);
            return Content(result.ToJson());
        }
        
        /// <summary>
        /// 获取物资所有单位
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductUnitsQuery(string keyword)
        {
            var result = _wzProductDmnService.SelectProductUnits(keyword, OrganizeId);
            return Content(result.ToJson());
        }
    }
}