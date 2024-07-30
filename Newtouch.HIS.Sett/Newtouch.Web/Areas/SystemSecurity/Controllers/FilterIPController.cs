using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Web.Areas.SystemSecurity.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterIPController : ControllerBase
    {
        private readonly IFilterIPApp _filterIPApp;

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string keyword)
        {
            var data = _filterIPApp.GetList(keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _filterIPApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(FilterIPEntity filterIPEntity, string keyValue)
        {
            _filterIPApp.SubmitForm(filterIPEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _filterIPApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

    }
}
