using System.Collections.Generic;
using System.IO;
using System.Linq;

using JSViewer_MVC_Core.Bll;
using JSViewer_MVC_Core.Models;
using JSViewer_MVC_Core.OutputModels;

using Microsoft.AspNetCore.Mvc;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Utilities;
using System;
using System.Text;
using JSViewer_MVC_Core.Bll.OpenSource;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Bibliography;

namespace JSViewer_MVCCore.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        public SysReportTemplateBll TemplateBll { get; set; }
        public SysReportTemplatesBll KyTemplateBll { get; set; }

        public HomeController(SysReportTemplateBll templateBll, SysReportTemplatesBll kyTemplateBll)
        {
            this.TemplateBll = templateBll;
            this.KyTemplateBll = kyTemplateBll;
        }

        /// <summary>
        /// 运行默认打开wwwroot文件夹下的index.html
        /// </summary>
        /// <returns></returns>
        public object Index() => Resource("index.html");

        /// <summary>
        /// 根据文件名称从wwwroot资源文件夹下读取文件返回给前端
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpGet("{file}")]
        public object Resource(string file)
        {
            var stream = GetType().Assembly.GetManifestResourceStream("JSViewer_MVC_Core.wwwroot." + file);
            if (stream == null)
                return new NotFoundResult();

            if (Path.GetExtension(file) == ".html")
                return new ContentResult() { Content = new StreamReader(stream).ReadToEnd(), ContentType = "text/html" };

            if (Path.GetExtension(file) == ".ico")
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return new FileContentResult(memoryStream.ToArray(), "image/x-icon") { FileDownloadName = file };
                }

            using var streamReader = new StreamReader(stream);
            return new FileContentResult(System.Text.Encoding.UTF8.GetBytes(streamReader.ReadToEnd()),
                GetMimeType(file))
            { FileDownloadName = file };
        }

        /// <summary>
        /// 查询报表列表用于左侧展示
        /// </summary>
        /// <param name="hospitalCode">医院代码</param>
        /// <param name="templateType">模板类型</param>
        /// <returns></returns>
        [HttpGet("reports")]
        public ActionResult Reports(string hospitalCode, int templateType)
        {
            var reportsList = GetSysReportTemplate(hospitalCode, templateType);
            return new ObjectResult(reportsList);
        }

        /// <summary>
        /// 判断报表是否存在,不存在则使用空白模板进行初始化
        /// </summary>
        /// <param name="hospitalCode">医院代码</param>
        /// <param name="templateType">模板类型</param>
        /// <param name="templateCode">模板代码</param>
        /// <returns></returns>
        [HttpGet("initializeReport/{hospitalCode}/{templateType}/{templateCode}")]
        public ActionResult InitializeReport(string hospitalCode, int templateType, string templateCode)
        {
            bool successFlag = TemplateBll.InitializeReport(hospitalCode, templateType, templateCode, out string promptMsg);

            return new ObjectResult(WqsjResponse.ToResponse(successFlag, promptMsg));
        }

        /// <summary>
        /// 查询报表列表
        /// </summary>
        /// <param name="hospitalCode">医院代码</param>
        /// <param name="templateType">报表类型</param>
        /// <returns></returns>
        private List<SysReportTemplateOM> GetSysReportTemplate(string hospitalCode, int templateType)
        {
            List<SysReportTemplateOM> oms = new List<SysReportTemplateOM>();

            var templates = TemplateBll.GetSysReportTemplates(hospitalCode, templateType);

            foreach (var template in templates)
            {
                oms.Add(new SysReportTemplateOM
                {
                    TemplateId = template.TemplateId,
                    HospitalCode = hospitalCode,
                    TemplateCode = template.TemplateCode,
                    TemplateDesc = template.TemplateDesc,
                    TemplateEnName = template.TemplateEnName,
                    TemplateType = template.TemplateType,
                    ReportNameSuffix = template.ReportNameSuffix,
                    ReportType = template.ReportType
                });
            }

            return oms;
        }



        /// <summary>
        /// Gets the MIME type from the file extension
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>MIME type</returns>
        private static string GetMimeType(string fileName)
        {
            if (fileName.EndsWith(".css"))
                return "text/css";

            if (fileName.EndsWith(".js"))
                return "text/javascript";

            return "text/html";
        }

        [HttpGet("ExportPDFTest/{reportName}")]
        public ActionResult ExportPDFTest(string reportName)
        {
            var report = TemplateBll.GetReport(reportName);

            var reportXml = ReportConverter.ToXml(report);
            System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
            string content = utf8.GetString(reportXml).Replace("﻿<?xml version=\"1.0\" encoding=\"utf - 8\"?>","");

            string directoryPath = "RPT";
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            string filePath = Guid.NewGuid().ToString() + ".rdlx";

            string path = string.Format("{0}\\{1}", directoryPath, filePath);
            System.IO.File.WriteAllText(path, content, new System.Text.UTF8Encoding(false));

            //获取模板
            PageReport rpt = new PageReport(new FileInfo( path));
            DirectoryInfo outputDirectory = new DirectoryInfo("PDF");
            GrapeCity.ActiveReports.Export.Pdf.Page.Settings pdfSetting =
            new GrapeCity.ActiveReports.Export.Pdf.Page.Settings();
            string[] arrIDs = new string[] { "202108300014", "1028" };

            for (int j = 0; j < arrIDs.Length; j++)
            {
                try
                {
                    rpt.Report.ReportParameters[j].DefaultValue.Values.Clear();
                    rpt.Report.ReportParameters[j].DefaultValue.Values.Add(arrIDs[j]);

                    
                }
                catch
                {
                }
            }

            //此行代码不要注释，否则出现批量加载报表的时候出现问题
            GrapeCity.ActiveReports.Document.PageDocument reportDocument =
        new GrapeCity.ActiveReports.Document.PageDocument(rpt);
            // Set the rendering extension and render the report .
            GrapeCity.ActiveReports.Export.Pdf.Page.PdfRenderingExtension pdfRenderingExtension =
            new GrapeCity.ActiveReports.Export.Pdf.Page.PdfRenderingExtension();
            GrapeCity.ActiveReports.Rendering.IO.FileStreamProvider outputProvider =
            new GrapeCity.ActiveReports.Rendering.IO.FileStreamProvider(
            outputDirectory, Path.GetFileNameWithoutExtension(reportName));
            // Overwrite output file if it already exists
            outputProvider.OverwriteOutputFile = false;
            rpt.Document.Render(pdfRenderingExtension, outputProvider, pdfSetting);

            return new ObjectResult(WqsjResponse.ToResponse(true, "测试"));
        }


        #region 开源报表
        /// <summary>
        /// 判断报表是否存在,不存在则使用空白模板进行初始化
        /// </summary>
        /// <param name="TemplateId">模板明细ID</param>
        /// <param name="hospitalCode">医院代码</param>
        /// <returns></returns>
        [HttpGet("InitReport/{TemplateId}/{hospitalCode}")]
        public ActionResult InitReport(int TemplateId, string hospitalCode)
        {
            bool successFlag = KyTemplateBll.InitializeReport( TemplateId, hospitalCode, out string promptMsg);

            return new ObjectResult(WqsjResponse.ToResponse(successFlag, promptMsg));
        }


        [HttpGet("kygetInfo/{templateId}/{hospitalCode}/{reportdec}")]
        public ActionResult kygetInfo(string templateId, string hospitalCode,string reportdec)
        {
            if (string.IsNullOrWhiteSpace(templateId) || string.IsNullOrWhiteSpace(hospitalCode)
                || string.IsNullOrWhiteSpace(reportdec)) return BadRequest();
            var reqstr = templateId+","+ hospitalCode+","+ reportdec;
            return new ObjectResult(WqsjResponse.ToResponse(true, "", reqstr));
        }
        [HttpGet("GetTempLateData/{reportCode}/{hospitalCode}/{systemCode}")]
        public ActionResult GetTempLateData(string reportCode, string hospitalCode,string systemCode)
        {
            var reqstr = KyTemplateBll.GetTempLateData(reportCode, hospitalCode, systemCode);
            return new ObjectResult(WqsjResponse.ToResponse(true, "", reqstr));
        }
        #endregion
    }
}