using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.WebPages;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    public class InpatientListController : OrgControllerBase
    {

        private readonly ITreatmentRepo _treatmentRepo;
        private readonly IPatientVitalSignsRepo _patientVitalSignsRepo;
        private readonly IInpatientPatientDmnService _inpatientPatientDmnService;
        private readonly FrameworkBase.MultiOrg.Domain.IDomainServices.IBaseDataDmnService _iBaseDataDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IIDBDmnService _idbDmnService;
        private readonly IVisitDeptSetRepo visitDeptSetRepo;


        public InpatientListController()
        {

        }
        // GET: NurseManage/InpatientList
        public ActionResult Inpatient()
        {
            var bqAuth = _iBaseDataDmnService.GetWardListByStaffGh(UserIdentity.rygh, OrganizeId).FirstOrDefault();
            if (bqAuth != null)
            {
                ViewBag.bqval = bqAuth.bqCode;
                ViewBag.ysgh = UserIdentity.rygh;
                return View();
            }
            else
            {
                return Content("javascript:void()");
            }
        }
    }
}