using System;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Common;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class SysChargeClassifyController : ControllerBase
    {
        private ISysChargeClassifyApp _SysChargeClassifyApp;

        public SysChargeClassifyController(ISysChargeClassifyApp SysChargeClassifyApp)
        {
            this._SysChargeClassifyApp = SysChargeClassifyApp;
        }
        public ActionResult SysChargeClassify()
        {
            return View();
        }

        public ActionResult SubmitForm(SysChargeClassificationEntity xt_sfflEntity, string keyValue)
        {
            _SysChargeClassifyApp.SubmitForm(xt_sfflEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult DeleteForm(int keyValue)
        {

            _SysChargeClassifyApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 获取grid的值
        /// </summary>
        /// <returns></returns>
        public ActionResult GetdlGridJson(Pagination Pagination, string keyword)
        {
            var data = new
            {
                rows = _SysChargeClassifyApp.GetListBySearch(Pagination, keyword),
                total = Pagination.total,
                page = Pagination.page,
                records = Pagination.records
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
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = new SysChargeClassificationEntity();
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity = _SysChargeClassifyApp.GetForm(int.Parse(keyValue));
            }
            else
            {
                entity.fl = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("xt_sffl.fl");
                entity.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                entity.CreateTime = DateTime.Now;
            }
            return Content(entity.ToJson());
        }
    }
}
