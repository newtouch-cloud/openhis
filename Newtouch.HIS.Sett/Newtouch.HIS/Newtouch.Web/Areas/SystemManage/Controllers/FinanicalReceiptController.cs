using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class FinancialReceiptController : ControllerBase
    {
        private readonly ISysFinancialDmnService _sysFinancialDmnService;
        private readonly IFinanceReceiptRepo _financeReceiptRepo;
        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetFinancialInvoiceList(string keyword)
        {
            var data = _sysFinancialDmnService.GetFinancialInvoiceList(keyword, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string Id)
        {
            var entity = _financeReceiptRepo.GetFinancialInvoiceEntity(Id, this.OrganizeId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(FinanceReceiptEntity Entity, string Id)
        {
            Entity.zt = Entity.zt == "true" ? "1" : "0";
            Entity.OrganizeId = this.OrganizeId;
            _financeReceiptRepo.VlidateRepeat(Entity, OrganizeId,Id);
            _financeReceiptRepo.SubmitForm(Entity, Id);
            return Success("操作成功。");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string Id)
        {
            _financeReceiptRepo.DeleteForm(Id, this.OrganizeId);
            return Success("操作成功。");
        }
    }
}