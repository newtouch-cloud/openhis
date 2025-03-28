using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysCISpecialMarkController : ControllerBase
    {

        private readonly ISysCISpecialMarkApp _SysCISpecialMarkApp;
        private readonly ISysPatiNatureApp _SysPatiNatureApp;

        public ActionResult SysCISpecialMark()
        {
            return View();
        }

        public ActionResult SubmitForm(SysChargeItemSpecialMarkEntity xt_sfxmtsbzEntity, string keyValue)
        {
            _SysCISpecialMarkApp.SubmitForm(xt_sfxmtsbzEntity, keyValue);
            return Success("操作成功。");
        }

        public ActionResult DeleteForm(int keyValue)
        {

            _SysCISpecialMarkApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 获取grid的值
        /// </summary>
        /// <returns></returns>
        public ActionResult GetbzGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _SysCISpecialMarkApp.GetListBySearch(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _SysCISpecialMarkApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }

    }
}
