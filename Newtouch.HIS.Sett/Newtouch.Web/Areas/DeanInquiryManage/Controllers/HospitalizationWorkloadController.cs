using Newtouch.HIS.Domain.Entity.DeanInquiryManage;
using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class HospitalizationWorkloadController : ControllerBase
    { 

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Consumablestatistics()
        {
            var zzqs = _deanInquiryDmnService.zzqs_Lsqs("2023-03-01", this.OrganizeId);
            var ryqs = _deanInquiryDmnService.ryqs_Lsqs("2023-03-01", this.OrganizeId);
            var cyqs = _deanInquiryDmnService.cyqs_Lsqs("2023-03-01", this.OrganizeId);
            NumberTrendList result = new NumberTrendList();
            result.ryqs = ryqs;
            result.cyqs = cyqs;
            result.zzqs = zzqs;
            return Content(result.ToJson());
        }

        public ActionResult ConsumaPersonNumber(string kssj, string jssj)
        {
            
            var result = _deanInquiryDmnService.Rstj_Lsqs(kssj,jssj, this.OrganizeId);
            return Content(result.ToJson());
        }

        public ActionResult DepartmentWorkload(string kssj, string jssj)
        {

            var result = _deanInquiryDmnService.Ksgzl_Lsqs(kssj, jssj, this.OrganizeId);
            return Content(result.ToJson());
        }


        public ActionResult StaffWorkload(string kssj, string jssj,string bq)
        {
           
            var result = _deanInquiryDmnService.YSgzl_Lsqs(kssj, jssj, bq, this.OrganizeId);
            return Content(result.ToJson());
        }

    }
}