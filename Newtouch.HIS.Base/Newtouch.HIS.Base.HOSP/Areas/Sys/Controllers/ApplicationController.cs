using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Common.Operator;

namespace Newtouch.HIS.Base.HOSP.Areas.Sys.Controllers
{
    public class ApplicationController : ControllerBase
    {
        private readonly ISysApplicationRepo _sysApplicationRepo;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysApplicationDmnService _sysApplicationDmnService;

        public ApplicationController(ISysApplicationRepo sysApplicationRepo, ISysOrganizeDmnService sysOrganizeDmnService
            , ISysApplicationDmnService sysApplicationDmnService)
        {
            this._sysApplicationRepo = sysApplicationRepo;
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysApplicationDmnService = sysApplicationDmnService;
        }

        //grid json
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson()
        {
            var data = _sysApplicationRepo.GetList();
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = _sysApplicationRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SysApplicationEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";

            _sysApplicationRepo.SubmitForm(entity, keyValue);

            return Success("操作成功。");
        }

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
            if (!string.IsNullOrWhiteSpace(keyValue) && !string.IsNullOrWhiteSpace(orgList))
            {
                _sysApplicationDmnService.UpdateAuthOrganizeList(keyValue, orgList, OperatorProvider.GetCurrent().UserCode);
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
                _sysApplicationDmnService.AuthAllOrganize(keyValue, OperatorProvider.GetCurrent().UserCode);
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
                _sysApplicationDmnService.AuthCancelAllOrganize(keyValue);
            }
            return Success("操作成功。");
        }

    }
}