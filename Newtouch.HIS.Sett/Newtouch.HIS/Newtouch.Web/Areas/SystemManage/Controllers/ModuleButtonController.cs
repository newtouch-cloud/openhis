using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class ModuleButtonController : ControllerBase
    {
        private readonly IModuleApp _moduleApp;
        private readonly IModuleButtonApp _moduleButtonApp;
        private readonly ISysModuleDmnService _sysModuleDmnService;

        //grid json
        public ActionResult GetGridJson(string moduleId)
        {
            var data = _sysModuleDmnService.GetMenuButtonListByTopOrg(Constants.TopOrganizeId, moduleId);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _moduleButtonApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SysModuleButtonEntity moduleButtonEntity, string keyValue)
        {
            moduleButtonEntity.zt = moduleButtonEntity.zt == "true" ? "1" : "0";
            _sysModuleDmnService.ModuleButtonSubmitForm(moduleButtonEntity, keyValue, Constants.TopOrganizeId);
            return Success("操作成功。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            _moduleButtonApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #region 授权机构

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Organizes()
        {
            return View();
        }

        /// <summary>
        /// 授权给指定组织机构
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="orgList"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult UpdateAuthOrganizeList(string keyValue, string orgList)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysModuleDmnService.UpdateAuthOrganizeList(keyValue, orgList, 2);
            }
            return Success("操作成功。");
        }

        /// <summary>
        /// 授权给所有组织机构
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult AuthAllOrganize(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysModuleDmnService.AuthAllOrganize(keyValue, 2);
            }
            return Success("操作成功。");
        }

        /// <summary>
        /// 撤销全部授权（组织机构）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult AuthCancelAllOrganize(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysModuleDmnService.AuthCancelAllOrganize(keyValue, 2);
            }
            return Success("操作成功。");
        }

        #endregion
    }
}
