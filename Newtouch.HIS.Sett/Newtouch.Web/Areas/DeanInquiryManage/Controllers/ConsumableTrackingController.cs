using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class ConsumableTrackingController : ControllerBase
    { 

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Consumablestatistics(string kssj, string jssj)
        {
      
            var result = _deanInquiryDmnService.Consumablestatistics_Lsqs(kssj, jssj, this.OrganizeId);
            return Content(result.ToJson());
        }

        public ActionResult DoctorBillingJHCRanking(string kssj, string jssj, string sfxmdm)
        {
           
            var result = _deanInquiryDmnService.DoctorBillingRankingHC_Lsqs(kssj, jssj, sfxmdm, this.OrganizeId);
            return Content(result.ToJson());
        }

        public ActionResult ProfitandlossrankingHC()
        {
            var result = _deanInquiryDmnService.ProfitandlossrankingHC_Lsqs("2023-03-01", "2023-09-08", this.OrganizeId);
            return Content(result.ToJson());
        }
    }
}