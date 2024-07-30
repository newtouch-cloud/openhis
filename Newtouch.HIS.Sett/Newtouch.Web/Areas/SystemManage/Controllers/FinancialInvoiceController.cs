using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class FinancialInvoiceController : ControllerBase
    {
        private readonly IFinancialInvoiceRepo _financialInvoiceRepo;
        private readonly ISysFinancialDmnService _sysFinancialDmnService;

        public ActionResult FinanicalInvoiceIndex()
        {
            return View();
        }

        //[HandlerAuthorize]
        public override ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetFinancialInvoiceList(string keyword)
        {
            var data = _financialInvoiceRepo.GetFinancialInvoiceList(keyword, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// GetCwfpList
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetCwfpList(string keyword)
        {
            var lyry = "";
            if (!UserIdentity.IsAdministrator && !UserIdentity.IsHospAdministrator)
            {
                lyry = UserIdentity.UserCode;
            }
            var data = _sysFinancialDmnService.GetCwfpList(keyword, lyry, OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string fpdm)
        {
            var entity = _financialInvoiceRepo.GetFinancialInvoiceEntity(fpdm, this.OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(FinancialInvoiceEntity financialInvoiceEntity, string fpdm)
        {
            financialInvoiceEntity.zt = financialInvoiceEntity.zt == "true" ? "1" : "0";
            if (financialInvoiceEntity.is_del == 1)
            {
                financialInvoiceEntity.zt = "0";
            }
            financialInvoiceEntity.OrganizeId = this.OrganizeId;
            _financialInvoiceRepo.VlidateRepeat(financialInvoiceEntity, OrganizeId, fpdm);
            _financialInvoiceRepo.SubmitForm(financialInvoiceEntity, fpdm);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string fpdm)
        {
            _financialInvoiceRepo.DeleteForm(fpdm, this.OrganizeId);
            return Success("操作成功。");
        }


        public ActionResult InvoiceQuery()
        {
            return View();
        }

        /// <summary>
        /// 查询发票列表
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult InvoiceQueryList(DateTime? kssj, DateTime? jssj)
        {
            kssj = kssj == null ? DateTime.Now.AddDays(-7) : kssj;
            jssj = jssj == null ? DateTime.Now : jssj;

            var data = _sysFinancialDmnService.InvoiceQueryList((DateTime)kssj, (DateTime)jssj, OrganizeId, UserIdentity.UserCode);
            return Content(data.ToJson());
        }
    }
}