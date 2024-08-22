using Newtouch.Core.Common;
using Newtouch.HIS.Application;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemSecurity.Controllers
{
    public class LogController : ControllerBase
    {
        private readonly ILogApp _logApp;

        public LogController(ILogApp logApp)
        {
            this._logApp = logApp;
        }

        [HttpGet]
        public ActionResult RemoveLog()
        {
            return View();
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = _logApp.GetList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitRemoveLog(string keepTime)
        {
            _logApp.RemoveLog(keepTime);
            return Success("清空成功。");
        }
    }
}
