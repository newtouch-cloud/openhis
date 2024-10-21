using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.Domain.IDomainServices.Outpatient;
using Newtouch.Domain.ValueObjects.Outpatient;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    public class ElectronicPrescriptionController : OrgControllerBase
    {
        private readonly IElectronicPrescriptionDmnService _electronicPrescriptionDmnService;
        // GET: DoctorManage/ElectronicPrescription
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult examForm()
        {
            return View();
        }

        public ActionResult medicineForm()
        {
            return View();
        }


        public ActionResult cfForm()
        {
            return View();
        }

        public ActionResult backForm()
        {
            return View();
        }

        public ActionResult GetGridJson(Pagination pagination, DateTime kssj, DateTime jssj, string xm)
        {
            var tt = _electronicPrescriptionDmnService.GetGridJson(pagination, OrganizeId, kssj, jssj, xm);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        

    }
}