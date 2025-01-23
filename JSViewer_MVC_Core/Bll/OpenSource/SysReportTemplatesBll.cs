using DocumentFormat.OpenXml.VariantTypes;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Utilities;
using GrapeCity.ActiveReports.PageReportModel;
using GrapeCity.DataVisualization.Chart;
using JSViewer_MVC_Core.Code;
using JSViewer_MVC_Core.Implementation.CustomStore.Reports;
using JSViewer_MVC_Core.Implementation.Storage;
using JSViewer_MVC_Core.Models;
using JSViewer_MVC_Core.Models.Enums;
using JSViewer_MVC_Core.Properties;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static GrapeCity.Enterprise.Data.DataEngine.DataProcessing.DataProcessor;

namespace JSViewer_MVC_Core.Bll.OpenSource
{
    public class SysReportTemplatesBll
    {
        /// <summary>
        /// 获取报表
        /// </summary>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public Report GetReport(string reportName)
        
        {
            string[] strs = reportName.Split('_');

            if (strs.Length < 3) return null;

            using (NewTouchKyContext context = new NewTouchKyContext())
            {
                var template = context.SysReportTemplate.FirstOrDefault(x => x.TemplateID == Convert.ToInt32(strs[0]) && x.HospitalCode == strs[1] && x.zt == 1);

                if (template == null || string.IsNullOrEmpty(template.Content))
                    return null;


                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(template.Content);
                string AppConn = AppSettingHelper.ReadAppSettings("ConnectionStrings", "KyTemplateConnection");


                string contentstr = template.Content;
                int starindex = 0; int endindex = 0;
                string starstr = "<ConnectString>"; string endstr = "</ConnectString>";
                starindex = contentstr.IndexOf(starstr);
                if (starindex != -1)
                {
                    string curconn = contentstr.Substring(starindex + starstr.Length);
                    endindex = curconn.IndexOf(endstr);
                    curconn = curconn.Remove(endindex);
                    if (curconn != AppConn)
                    {
                        contentstr = contentstr.Replace(curconn, AppConn);
                        template.Content = contentstr;
                        template.LastModifierCode = "admin";
                        template.LastModifyTime = DateTime.Now;
                        context.SysReportTemplate.Update(template);
                        context.SaveChanges();
                    }
                }
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
            using (NewTouchKyContext context = new NewTouchKyContext())
            {
                var template = context.SysReportTemplate.FirstOrDefault(x => x.TemplateID == Convert.ToInt32(strs[0]) && x.HospitalCode == strs[1] && x.zt==1);

                if (template == null || string.IsNullOrEmpty(template.Content))
                    return;

                template.Content = templateContent;
                template.LastModifierCode = "admin";
                template.LastModifyTime = DateTime.Now;
                context.SysReportTemplate.Update(template);
                context.SaveChanges();
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
        public bool InitializeReport(int TemplateId, string hospitalCode, out string promptMsg)
        {
            promptMsg = string.Empty;
            using (NewTouchKyContext context = new NewTouchKyContext())
            {
                var d = Models.Enums.ReportType.AREA;
                //int status = Models.Enums.Status.qy;

                var template = context.SysReportTemplate.OrderByDescending(x=>x.HospitalCode).FirstOrDefault(x => (x.HospitalCode == hospitalCode || x.HospitalCode == "*") && x.TemplateID == TemplateId && x.zt==1);
                if (template == null)
                {
                    promptMsg = "模板目录不存在";
                    return false;
                }

                if (!string.IsNullOrEmpty(template.Content))
                {
                    return true;
                }
                var temp = context.SysReport.FirstOrDefault(x=>x.ReportCode== template.ReportCode && x.HospitalCode== template.HospitalCode && x.zt==1);
                string content = string.Empty;
                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
                switch ((Models.Enums.ReportType)temp.ReportType)
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

                template.Content = xmlStr;
                context.SysReportTemplate.Update(template);
                context.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 获取有效报表
        /// </summary>
        /// <param name="templateCode"></param>
        /// <param name="hospitalCode"></param>
        /// <returns></returns>
        public string GetTempLateData(string templateCode, string hospitalCode,string systemCode)
        {
            using (NewTouchKyContext context = new NewTouchKyContext()) {
                //获取当前机构报表 如无获取*号报表
                var template = context.SysReportTemplate.OrderByDescending(x=>x.HospitalCode).FirstOrDefault(x => x.ReportCode == templateCode && (x.HospitalCode == hospitalCode || x.HospitalCode=="*") && x.SystemCode==systemCode && x.ReportStatus==1 && x.zt == 1);

                if (template == null)
                    return "";
                var tempName = template.TemplateID + "_" + template.HospitalCode + "_" + template.ReportNameDes;
                return tempName;
            }
        }
    }
}
