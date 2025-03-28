using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class BedLevelChargeController : OrgControllerBase
    {
        private readonly IOrderAuditDmnService _OrderAuditDmnService;
        private readonly IBaseDataDmnService _iBaseDataDmnService;
        private readonly ILevelChargeDmnService _ilevelChargeDmnService;
        private readonly IInpatientBedLevelChargeItemRepo _inpatientBedBaseChargeItemRepo;
        /// <summary>
        /// 等级费用列表
        /// </summary>
        /// <param name="aa"></param>
        /// <returns></returns>
        public ActionResult GetIndexGridJson(Pagination pagination,string LevelCode)
        {
            var data = new
            {
                rows = _ilevelChargeDmnService.GetLevelCharge(pagination, OrganizeId, LevelCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public ActionResult submitForm(SysLevelChargeVO xm,string keyvalue) {
            InpatientBedLevelChargeItemEntity entity = new InpatientBedLevelChargeItemEntity();
            entity= xm.MapperTo(entity);
            entity.OrganizeId = OrganizeId;
            _inpatientBedBaseChargeItemRepo.SubmitForm(entity,keyvalue);
            return Success();
        }

        public ActionResult GetFormJson(string keyvalue) {
           var data= _ilevelChargeDmnService.GetFormJson(OrganizeId, keyvalue);
            return Content(data.ToJson());
        }

        public ActionResult DeleteData(string keyValue) {
            _inpatientBedBaseChargeItemRepo.DeleteForm(keyValue, OrganizeId);
            return Success();
        }
    }
}