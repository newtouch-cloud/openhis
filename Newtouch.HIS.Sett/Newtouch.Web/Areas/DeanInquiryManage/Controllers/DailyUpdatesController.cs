using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class DailyUpdatesController : ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 全院收入
        /// </summary>
        public ActionResult GetQysr()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetQysr();
            return Content(list.ToJson());
        }
        /// <summary>
        /// 院长查询-今日动态-banner 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetJRDTBanner()
        {
            var dto = _deanInquiryDmnService.GetJRDTBanner(this.OrganizeId);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// 今日门诊动态
        /// </summary>
        public ActionResult GetJrdt()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetJrdt(this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 门诊处方
        /// </summary>
        public ActionResult GetMzcf()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetMzcf(this.OrganizeId);
            return Content(list.ToJson());
        }
         
        /// <summary>
        /// 门诊费用
        /// </summary>
        public ActionResult GetMzfy()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetMzfy(this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 住院占床率
        /// </summary>
        public ActionResult GetZyzcl()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetZyzcl(this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 门诊挂号统计
        /// </summary>
        public ActionResult GetMzghtj()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetMzghtj(this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 住院患者统计
        /// </summary>
        public ActionResult GetZyhztj()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetZyhztj(this.OrganizeId);
            return Content(list.ToJson());
        }
    }
}