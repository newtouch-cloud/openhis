using Newtouch.Core.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// 报表
    /// </summary>
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 打印单页面
        /// </summary>
        /// <param name="type">1:住院发药（明细）,2:住院发药（汇总）
        /// 3：住院退药（明细），4：住院退药（汇总）</param>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport(string type, string param)
        {
            string Url = "";
            var Params = new StringBuilder();
            if (!string.IsNullOrEmpty(param))
            {
                string[] paramArray = param.Split('|');
                foreach (string item in paramArray)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        Params.Append("&" + item);
                    }
                }
            }
            if (!string.IsNullOrEmpty(type))
            {
                Url = "/Pages/ReportViewer.aspx?/Newtouch.Herp.ReportService/" + type;
            }
            ViewBag.ReportServerHost = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST") + Url + (string.IsNullOrEmpty(Params.ToString()) ? "" : Params.ToString());
            return View();
        }
    }
}