using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers.PharmacyDrugStorage
{
    public class SysMedicineElectronicPrescriptionController : Controller
    {
        // GET: SysMedicineElectronicPrescription
        private readonly ISysMedicineElectronicPrescriptionDmnService _sysMedicineElectronicPrescriptionDmnService;

        public SysMedicineElectronicPrescriptionController(ISysMedicineElectronicPrescriptionDmnService SysMedicineElectronicPrescriptionDmnService)
        {
            this._sysMedicineElectronicPrescriptionDmnService = SysMedicineElectronicPrescriptionDmnService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string genname, string medListCodg)
        {
            var data = new
            {
                rows = _sysMedicineElectronicPrescriptionDmnService.GetPaginationList(pagination, genname,  medListCodg),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
    }
}