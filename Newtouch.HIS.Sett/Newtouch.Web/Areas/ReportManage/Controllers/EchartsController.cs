/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 

*********************************************************************************/
using Newtouch.HIS.Domain.IDomainServices.ReportManage;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class EchartsController : ControllerBase
    {
        private readonly IEchartsDmnService _echartsDmnService;

        //绩效指标
        public ActionResult PerformanceIndicator()
        {
            return View();
        }

        /// <summary>
        /// 总人次 平均人次
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPerformanceIndicator(string orgId, string year)
        {
            var list = _echartsDmnService.SelectPerformanceIndicator(orgId, year);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 总人次 平均人次
        /// </summary>
        /// <returns></returns>
        public ActionResult PerformanceMonthIndicator(string orgId, string year,int month)
        {
            var list = _echartsDmnService.PerformanceMonthIndicator(orgId, year,month < 10 ? "0" + month : month + "");
            return Content(list.ToJson());
        }

    }
}
