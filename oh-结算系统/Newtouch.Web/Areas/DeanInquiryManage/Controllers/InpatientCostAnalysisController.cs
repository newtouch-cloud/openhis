using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class InpatientCostAnalysisController : ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        {
            return View(); 
        }

        /// <summary>
        /// 住院费用分析_抬头数据
        /// </summary>
        public ActionResult GetTitleData(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.ZYFYFX_TitleData(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// 院长查询-住院费用分析_科室费用分析部分
        /// </summary>
        public ActionResult ZYFYFX_KSFYFXDTO(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.ZYFYFX_KSFYFXDTO(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// 院长查询-住院费用分析_出院患者费用分析
        /// </summary>
        public ActionResult GetCYHZFYFData(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.GetCYHZFYFData(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// 院长查询-住院费用分析_统计图
        /// </summary>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="rtype"></param>
        /// <returns></returns>
        public ActionResult GetCYHZFYFTJData(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.GetCYHZFYFTJData(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }

    }
}