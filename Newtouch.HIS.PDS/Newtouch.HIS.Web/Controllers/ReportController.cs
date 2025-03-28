using System.Linq;
using Newtouch.Core.Common.Utils;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Report Controller
    /// </summary>
    public class ReportController : Controller
    {
        /// <summary>
        /// 打印单页面
        /// </summary>
        /// <param name="type">1:住院发药（明细）,2:住院发药（汇总）
        /// 3：住院退药（明细），4：住院退药（汇总）</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult PrintReport(string type, string param)
        {
            var url = "";
            var Params = string.Empty;
            if (!string.IsNullOrEmpty(param))
            {
                var paramArray = param.Split('|');
                Params = paramArray.Where(item => !string.IsNullOrEmpty(item)).Aggregate(Params, (current, item) => current + ("&" + item));
            }
            if (!string.IsNullOrEmpty(type))
            {
                url = "/Pages/ReportViewer.aspx?/Newtouch.HIS.Bill/" + type;
            }
            ViewBag.ReportServerHost = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST") + url + (string.IsNullOrEmpty(Params) ? "" : Params);
            return View();
        }
    }
}