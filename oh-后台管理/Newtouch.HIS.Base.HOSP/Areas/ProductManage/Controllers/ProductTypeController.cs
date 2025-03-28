using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.Common;
using Newtouch.HIS.Application.Interface;

namespace Newtouch.HIS.Base.HOSP.Areas.ProductManage.Controllers
{
    /// <summary>
    /// 物资类别维护
    /// </summary>
    public class ProductTypeController : ControllerBase
    {
        private readonly IWzTypeRepo _wzTypeRepo;
        private readonly IProductTypeApp _productTypeApp;

        public ProductTypeController(IWzTypeRepo wzTypeRepo, IProductTypeApp productTypeApp)
        {
            _wzTypeRepo = wzTypeRepo;
            _productTypeApp = productTypeApp;
        }

        /// <summary>
        /// 获取物资类别列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetTypeGridJson(string keyWord = "")
        {
            var data = _wzTypeRepo.IQueryable().ToList();
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                data = data.TreeWhere(t => t.name.Contains(keyWord), parentId: "ParentId");
            }
            var treeList = (from item in data
                            let hasChildren = data.Count(t => t.parentId == item.Id) != 0
                            select new TreeGridModel
                            {
                                id = item.Id,
                                isLeaf = hasChildren,
                                parentId = item.parentId,
                                expanded = hasChildren,
                                entityJson = item.ToJson()
                            }).ToList();
            return Content(treeList.TreeGridJson(null));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            return _wzTypeRepo.DeleteUnitById(keyValue) > 0 ? Success("删除成功") : Error("删除失败");
        }

        /// <summary>
        /// get product type information
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyWord)
        {
            var supplier = _wzTypeRepo.FindEntity(p => p.Id == keyWord);
            return Content(supplier.ToJson());
        }

        /// <summary>
        /// 物资类别维护 表单提交
        /// </summary>
        /// <param name="wzTypeEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(WzTypeEntity wzTypeEntity, string keyWord)
        {
            wzTypeEntity.zt = wzTypeEntity.zt == "true" ? "1" : "0";
            return _productTypeApp.SubmitForm(wzTypeEntity, keyWord) > 0 ? Success("操作成功") : Error("操作失败");
        }

        /// <summary>
        /// 获取上级
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPatientTreeSelectJson()
        {
            var warehouse = _wzTypeRepo.IQueryable(p => p.zt == "1");
            var treeList = new List<TreeSelectModel>();
            foreach (var item in warehouse)
            {
                var treeModel = new TreeSelectModel
                {
                    id = item.Id,
                    text = item.name,
                    parentId = item.parentId
                };
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }
    }
}