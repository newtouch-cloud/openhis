using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class OutpatientWorkloadController : ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        {
            return View(); 
        }

        /// <summary>
        /// 门诊科室工作量
        /// </summary>
        public ActionResult GetMzksgzl(string ksrq, string jsrq)
        {
            var list = _deanInquiryDmnService.OutpatientWorkloadEntiy_Mzksgzl(ksrq,jsrq,this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 医生工作量
        /// </summary>
        public ActionResult GetYsgzl(string ksrq, string jsrq, string ks)
        {
            var list = _deanInquiryDmnService.OutpatientWorkloadEntiy_Ysgzl(ksrq, jsrq, this.OrganizeId,ks);
            return Content(list.ToJson());
        }

    }
}