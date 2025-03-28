using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class OutpatientComprehensiveAnalysisController : ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 门诊综合分析_抬头数据
        /// </summary>
        public ActionResult GetTitleData(string ksrq,string jsrq,string rtype)
        { 
            var dto = _deanInquiryDmnService.OutpatientComprehensiveAnalysisEntiy_TitleData(this.OrganizeId, ksrq, jsrq,rtype);
            return Content(dto.ToJson());
        }

        /// <summary>
        /// 门诊综合分析_门诊患者分析
        /// </summary>
        public ActionResult GetMzhzfx(string ksrq, string jsrq,string rtype)
        {
            var list = _deanInquiryDmnService.OutpatientComprehensiveAnalysisEntiy_Mzhzfx(this.OrganizeId, ksrq, jsrq);
            return Content(list.ToJson());
        }
    }
}