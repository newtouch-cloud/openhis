using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Areas.Settlement.Controllers
{
    /// <summary>
    /// 中医证候
    /// </summary>
    public class SysTCMSyndromeController : ControllerBase
    {
        private readonly ISysTCMSyndromeRepo _sysTCMSyndromeRepo;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;

        public SysTCMSyndromeController(ISysTCMSyndromeRepo sysTCMSyndromeRepo
            , ISysOrganizeDmnService sysOrganizeDmnService)
        {
            this._sysTCMSyndromeRepo = sysTCMSyndromeRepo;
            this._sysOrganizeDmnService = sysOrganizeDmnService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]

        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            var list = _sysTCMSyndromeRepo.GetCommonPagintionList(pagination, keyword);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(int? keyValue)
        {
            var data = _sysTCMSyndromeRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitForm(SysTCMSyndromeEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = "*";
            _sysTCMSyndromeRepo.SubmitForm(entity, keyValue);
            return Success("操作成功！");
        }
    }
}