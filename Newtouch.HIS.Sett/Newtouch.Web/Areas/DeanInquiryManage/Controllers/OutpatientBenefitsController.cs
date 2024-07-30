using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class OutpatientBenefitsController : ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        { 
            return View();
        }

        /// <summary>
        /// 门诊效益明细
        /// </summary>
        public ActionResult GetMzxymx(string orgId, string ksrq, string jsrq)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Mzxymx(this.OrganizeId, ksrq, jsrq);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 医生效益
        /// </summary>
        public ActionResult GetYsxy(string orgId, string ksrq, string jsrq, string rtype, string type)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Ysxy(this.OrganizeId, ksrq, jsrq, type);
            return Content(list.ToJson());
        }
        /// <summary>
        /// 历史趋势
        /// </summary>
        public ActionResult GetLsqs(string ksrq, string jsrq, string rtype, string type)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Lsqs(this.OrganizeId, ksrq, jsrq, rtype, type);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 科室效益排名
        /// </summary>
        public ActionResult GetKSXYPM(string ksrq, string jsrq, string rtype, string type)
        {
            if (type == "1")//诊疗人次
            {
                var list = _deanInquiryDmnService.GetKSXYPM(this.OrganizeId, ksrq, jsrq);
                return Content(list.ToJson());
            }
            else//实际诊疗收入
            {
                var list = _deanInquiryDmnService.GetKSXYPMBySJSY(this.OrganizeId, ksrq, jsrq);
                return Content(list.ToJson());
            }
        }

    }
}