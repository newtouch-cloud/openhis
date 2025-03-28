using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.OR.ManageSystem.Domain.DTO.InputDto;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.Tools;

namespace Newtouch.OR.ManageSystem.Web.Areas.SystemManage.Controllers
{
    public class SysChargeTemplateController : OrgControllerBase
    {
        // GET: SystemManage/SysChargeTemplate

        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysChargeItemDmnService _sysChargeItemDmnService;

        public ActionResult ChargeTemplateIndex()
        {
            return View();
        }

        public ActionResult ChargeTemplate_EditForm()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult ChargeTemplate_GetGridJson(Pagination pagination, string keyword)
        {
            var opdept = _sysConfigRepo.GetValueByCode("OperationDeptConfig", this.OrganizeId);
            var list = _sysChargeItemDmnService.Search(pagination, keyword,opdept, this.OrganizeId);
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
        /// 保存操作
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult ChargeTemplate_SubmitForm(SysChargeTemplateDto entity, string xmListStr)
        {
            var opdept = _sysConfigRepo.GetValueByCode("OperationDeptConfig", this.OrganizeId);
            entity.zt = entity.zt == "true" ? "1" : "0";
            entity.OrganizeId = this.OrganizeId;
            entity.ks = string.IsNullOrWhiteSpace(opdept) == true ? "" : opdept;
            _sysChargeItemDmnService.ChargeTemplate_SubmitForm(entity, xmListStr, UserIdentity.UserCode);
            return Success("操作成功。");
        }

        /// <summary>
        /// 诊断 检索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public JsonResult GetDicChargeItems(string keyword, string zdlx, string ybnhlx)
        {
            var list = _sysChargeItemDmnService.GetDicChargeItems(100,this.OrganizeId,"2", keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChargeTemplate_EditForm_Data(string keyValue)
        {
            SysChargeTemplateInfoVM vm = null;
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                vm = _sysChargeItemDmnService.GetSysChargeTemplateInfo(keyValue,this.OrganizeId);
            }

            vm = vm ?? new SysChargeTemplateInfoVM();
            return Content(vm.ToJson());
        }
    }
}