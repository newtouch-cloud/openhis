using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class OutpatientCostAnalysisController : ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        { 
            return View();
        }

        /// <summary>
        /// 门诊费用分类分析
        /// </summary>
        public ActionResult GetMzfyflfx(string ksrq, string jsrq, string rtype)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Mzfyflfx(this.OrganizeId,ksrq, jsrq);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 门诊费用分类分析_图表
        /// </summary>
        public ActionResult GetMzfyflfx_tb(string ksrq, string jsrq, string rtype)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Mzfyflfx_tb(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 患者人均费用分析
        /// </summary>
        public ActionResult GetHzrjfyfx(string ksrq, string jsrq, string rtype)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Hzrjfyfx(this.OrganizeId, ksrq, jsrq);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 患者人均费用分析_图标
        /// </summary>
        public ActionResult GetHzrjfyfx_tb(string ksrq, string jsrq, string rtype)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Hzrjfyfx_tb(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(list.ToJson());
        }

    }
}