using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class DrugTrackingController : ControllerBase
    { 

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDrugTracking(string kssj, string jssj,string type)
        {
            var result = _deanInquiryDmnService.DrugTrackingEntity_Lsqs(kssj,jssj,this.OrganizeId,type);
            return Content(result.ToJson());
        }

        public ActionResult Profitandlossranking(string kssj, string jssj)
        {
            
            var result = _deanInquiryDmnService.Profitandlossranking_Lsqs(kssj,jssj, this.OrganizeId);
            return Content(result.ToJson());
        }

        public ActionResult DoctorBillingRanking(string kssj, string jssj,string Ypcode)
        {
            var result = _deanInquiryDmnService.DoctorBillingRanking_Lsqs(kssj,jssj,Ypcode, this.OrganizeId);
            return Content(result.ToJson());
        }
    }
}