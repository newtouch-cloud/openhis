using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Domain.IRepository.InsuranceManage;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.HIS.Domain.IDomainServices;

namespace Newtouch.HIS.Web.Areas.InsuranceManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class CommercialInsuranceController : ControllerBase
    {
        private readonly ICommercialInsuranceRepo _commercialInsuranceRepo;
        private readonly ISysCommercialInsuranceDmnService _sysCommercialInsuranceDmnService;

        public ActionResult GetFormJson(string keyValue)
        {
            var data = _commercialInsuranceRepo.GetForm(keyValue,OrganizeId);
            return Content(data.ToJson());
        }

        public ActionResult SubmitForm(SysCommercialInsuranceVO entity, string keyValue)
        {
            entity.OrganizeId = OrganizeId;
            entity.zt = "1";
            _sysCommercialInsuranceDmnService.SubmitForm(entity, keyValue, OrganizeId);
            return Success("操作成功。");
        }

        public ActionResult GetListJson(string engName, string code)
        {
            var data = _commercialInsuranceRepo.GetListJson(OrganizeId, code, engName);
            return Content(data.ToJson());
        }

        public ActionResult DeleteForm(string keyValue)
        {
            _commercialInsuranceRepo.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}