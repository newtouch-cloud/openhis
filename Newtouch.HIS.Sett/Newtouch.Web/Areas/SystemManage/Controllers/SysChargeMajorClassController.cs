using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Common;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class SysChargeMajorClassController : ControllerBase
    {

        private ISysChargeMajorClassApp _SysChargeMajorClassApp;
        private ISysChargeCategoryRepo _sysChargeCategoryRepo;


        public SysChargeMajorClassController(ISysChargeMajorClassApp SysChargeMajorClassApp
            , ISysChargeCategoryRepo sysChargeCategoryRepo)
        {
            this._SysChargeMajorClassApp = SysChargeMajorClassApp;
            this._sysChargeCategoryRepo = sysChargeCategoryRepo;
        }
        public ActionResult SysChargeMajorClass()
        {
            return View();
        }

        public ActionResult SubmitForm(SysChargeCategoryEntity xt_sfdlEntity, string keyValue)
        {
            _SysChargeMajorClassApp.SubmitForm(xt_sfdlEntity, keyValue);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(int keyValue)
        {

            _SysChargeMajorClassApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 获取grid的值
        /// </summary>
        /// <returns></returns>
        public ActionResult GetdlGridJson(Pagination pagination, string keyword)
        {
            return null;
            //var data = new
            //{
            //    rows = _sysChargeCategoryRepo.GetListBySearch(pagination, keyword),
            //    total = pagination.total,
            //    page = pagination.page,
            //    records = pagination.records
            //};
            //return Content(data.ToJson());
        }

        /// <summary>
        /// 收费大类下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult GetsfdlSelect()
        {
            var data = _SysChargeMajorClassApp.GetsfdlTreeSelectJson();
            return Content(data);
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
            var entity = new SysChargeCategoryEntity();
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                
                entity = _SysChargeMajorClassApp.GetForm(int.Parse(keyValue));
            }
            else
            {
                entity.dlCode = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("xt_sfdl.dl");
                entity.CreateTime = DateTime.Now;
                entity.CreatorCode = OperatorProvider.GetCurrent().UserCode;
            }
            return Content(entity.ToJson());
        }

    }
}
