using Newtouch.Common;
using Newtouch.HIS.Domain.IDomainServices.ReportManage;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.HighchartsManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonController : ControllerBase
    {
        private readonly IHighchartsDmnService _highchartsDmnService;

        #region 页面初始化
        public ActionResult NumberofSalaryCompared()
        {
            return View();
        }
        #endregion

        public ActionResult GetSalaryNumCompared()
        {
            var data = _highchartsDmnService.GetSalaryNumCompared(OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }
    }
}