using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class ItemsDataController : ControllerBase
    {
        private readonly IItemsDetailApp _itemsDetailApp;

        public ItemsDataController(IItemsDetailApp itemsDetailApp)
        {
            this._itemsDetailApp = itemsDetailApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            var data = _itemsDetailApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string enCode)
        {
            var data = _itemsDetailApp.GetItemList(enCode);
            List<object> list = new List<object>();
            foreach (ItemsDetailEntity item in data)
            {
                list.Add(new { id = item.F_ItemCode, text = item.F_ItemName });
            }
            return Content(list.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _itemsDetailApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ItemsDetailEntity itemsDetailEntity, string keyValue)
        {
            itemsDetailEntity.zt = itemsDetailEntity.zt == "true" ? ((int)EnumZT.Valid).ToString() : ((int)EnumZT.Invalid).ToString();
            _itemsDetailApp.SubmitForm(itemsDetailEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _itemsDetailApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
