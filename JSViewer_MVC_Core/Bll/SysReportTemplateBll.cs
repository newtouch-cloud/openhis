using GrapeCity.ActiveReports.Aspnetcore.Designer.Utilities;
using GrapeCity.ActiveReports.PageReportModel;

using JSViewer_MVC_Core.Implementation.CustomStore.Reports;
using JSViewer_MVC_Core.Implementation.Storage;
using JSViewer_MVC_Core.Models;
using JSViewer_MVC_Core.Models.Enums;
using JSViewer_MVC_Core.Properties;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JSViewer_MVC_Core.Bll
{
    public class SysReportTemplateBll
    {

        /// <summary>
        /// 查询报表模板列表
        /// </summary>
        /// <param name="hospitalCode">医院代码</param>
        /// <param name="templateType">报表类型</param>
        /// <returns></returns>
        public List<SysReportTemplate> GetSysReportTemplates(string hospitalCode, int templateType)
        {
            using (NewTouchBisContext context = new NewTouchBisContext())
            {
                return context.SysReportTemplate.Where(x => x.HospitalCode == hospitalCode && x.TemplateType == templateType).ToList();
            }
        }

        /// <summary>
        /// 判断报表是否存在,不存在则使用空白模板进行初始化
        /// </summary>
        /// <param name="hospitalCode">医院代码</param>
        /// <param name="templateType">模板类型</param>
        /// <param name="templateCode">模板代码</param>
        /// <param name="reportName">报表名称</param>
        /// <param name="promptMsg">提示信息</param>
        /// <returns></returns>
        public bool InitializeReport(string hospitalCode, int templateType, string templateCode, out string promptMsg)
        {
            promptMsg = string.Empty;
            using (NewTouchBisContext context = new NewTouchBisContext())
            {
                var template = context.SysReportTemplate.FirstOrDefault(x => x.HospitalCode == hospitalCode && x.TemplateType == templateType && x.TemplateCode == templateCode);
                if (template == null)
                {
                    promptMsg = "模板目录不存在";
                    return false;
                }

                if (!string.IsNullOrEmpty(template.TemplateContent))
                {
                    return true;
                }

                string content = string.Empty;
                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
                switch ((Models.Enums.ReportType)template.ReportType)
                {
                    case Models.Enums.ReportType.AREA:
                        content = utf8.GetString(Resources.Area_Untitled);
                        break;
                    case Models.Enums.ReportType.PAGE:
                        content = utf8.GetString(Resources.Page_Untitled);
                        break;
                    case Models.Enums.ReportType.RDL:
                    default:
                        content = utf8.GetString(Resources.Rdl_Untitled);
                        break;
                }
                byte[] reportbyte = System.Text.Encoding.UTF8.GetBytes(content);
                Report report = ReportConverter.FromXML(reportbyte);

                var reportXml = ReportConverter.ToXml(report);
                string xmlStr = utf8.GetString(reportXml);

                template.TemplateContent = xmlStr;
                context.SysReportTemplate.Update(template);
                context.SaveChanges();
                return true;
            }
        }


        /// <summary>
        /// 根据报表名称获取报表
        /// </summary>
        /// <param name="reportName">报表名称 {医院代码}_{模板代码}_{模板类别}_{模板英文名称或拼音码}.{报表后缀}</param>
        /// <returns></returns>
        public Report GetReport(string reportName)
        {
            string[] strs = reportName.Split('_');

            if (strs.Length < 3) return null;

            using (NewTouchBisContext context = new NewTouchBisContext())
            {
                var template = context.SysReportTemplate.FirstOrDefault(x => x.HospitalCode == strs[0] && x.TemplateCode == strs[1] && x.TemplateType == Convert.ToInt32(strs[2]));

                if (template == null || string.IsNullOrEmpty(template.TemplateContent))
                    return null;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(template.TemplateContent);

                var report = ReportConverter.FromXML(byteArray);
                return report;
            }
        }

        /// <summary>
        /// 保存报表
        /// </summary>
        /// <param name="reportName">报表名称</param>
        /// <param name="templateContent">模板内容</param>
        public void SaveReport(string reportName, string templateContent)
        {
            string[] strs = reportName.Split('_');

            if (strs.Length < 3) return;
            using (NewTouchBisContext context = new NewTouchBisContext())
            {
                var template = context.SysReportTemplate.FirstOrDefault(x => x.HospitalCode == strs[0] && x.TemplateCode == strs[1] && x.TemplateType == Convert.ToInt32(strs[2]));

                if (template == null || string.IsNullOrEmpty(template.TemplateContent))
                    return;

                template.TemplateContent = templateContent;
                context.SysReportTemplate.Update(template);
                context.SaveChanges();
            }
        }
    }
}
