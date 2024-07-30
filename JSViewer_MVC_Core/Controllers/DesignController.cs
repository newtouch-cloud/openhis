using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JSViewer_MVC_Core.Controllers
{
	[Route("/design/")]
	public class DesignController : Controller
	{
		/// <summary>
		/// design 路径 默认跳转到新建报表
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult Index()
		{
			return RedirectToAction("create");
		}

		/// <summary>
		/// 新建报表
		/// </summary>
		/// <returns></returns>
		[HttpGet("create")]
		public ActionResult Create()
		{
			return View("Index");
		}

		/// <summary>
		/// 编辑报表
		/// </summary>
		/// <param name="templateCode">模板代码</param>
		/// <param name="reportName">报表名称 {医院代码}_{模板代码}_{模板类别}_{模板英文名称或拼音码}.{报表后缀}</param>
		/// <returns></returns>
		[HttpGet("edit/{templateCode}/{reportName}")]
		public ActionResult Edit([FromRoute] string templateCode, [FromRoute] string reportName)
		{
			if (string.IsNullOrWhiteSpace(reportName)) return BadRequest();
			ViewBag.TemplateCode = templateCode;
			ViewBag.ReportName = reportName;
			return View("Index");
		}

        /// <summary>
        /// 编辑报表
        /// </summary>
        /// <param name="TemplateCode">模板Code</param>
        /// <param name="reportName">报表名称 {模板明细ID}_{医院代码}_{模板名称}}</param>
        /// <returns></returns>
        [HttpGet("kyedit/{TemplateCode}/{reportName}")]
        public ActionResult kyedit([FromRoute] string TemplateCode,[FromRoute] string reportName)
        {
            if (string.IsNullOrWhiteSpace(TemplateCode) || string.IsNullOrWhiteSpace(reportName)) return BadRequest();
            ViewBag.TemplateCode = TemplateCode;
            ViewBag.ReportName = reportName;
            return View("Index");
        }

       
    }
}