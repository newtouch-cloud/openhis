using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class DutyController : ControllerBase
    {
        private readonly IDutyApp _dutyApp;

        public DutyController(IDutyApp dutyApp)
        {
            this._dutyApp = dutyApp;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetListJson()
        {
            var data = _dutyApp.GetList();
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination,string keyword)
        {
            var data = new {
                rows = _dutyApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _dutyApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(DutyEntity roleEntity, string keyValue)
        {
            roleEntity.zt = roleEntity.zt == "true" ? "1" : "0";
            _dutyApp.SubmitForm(roleEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _dutyApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
