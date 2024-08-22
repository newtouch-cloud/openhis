using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common;
using System;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class SysCIRemindContentController : ControllerBase
    {

        private ISysCIRemindContentApp _SysCIRemindContentApp;
        private ISysChargeItemApp _SysChargeItemApp;

        public SysCIRemindContentController(ISysCIRemindContentApp SysCIRemindContentApp, ISysChargeItemApp SysChargeItemApp)
        {
            this._SysCIRemindContentApp = SysCIRemindContentApp;
            this._SysChargeItemApp = SysChargeItemApp;
        }

        public ActionResult SysCIRemindContent()
        {
            return View();
        }

        public ActionResult SubmitForm(SysChargeItemWarningContentEntity xt_sfxmjsnrEntity, string keyValue)
        {
            _SysCIRemindContentApp.SubmitForm(xt_sfxmjsnrEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(int keyValue)
        {

            _SysCIRemindContentApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 获取grid的值
        /// </summary>
        /// <returns></returns>
        public ActionResult GetnrGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _SysCIRemindContentApp.GetListBySearch(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 收费项目下拉框
        /// </summary>
        /// <returns></returns>
        [Obsolete("please use SysChargeItemController.GetChargeItemSelectData(string keyword)")]
        public ActionResult GetxmSelect()
        {
            return null;
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
            var entity = _SysCIRemindContentApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }

    }
}
