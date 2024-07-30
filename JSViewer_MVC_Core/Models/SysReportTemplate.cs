using System;
using System.Collections.Generic;

namespace JSViewer_MVC_Core.Models
{
    public partial class SysReportTemplate
    {
        public int TemplateId { get; set; }
        public string HospitalCode { get; set; }
        public int TemplateType { get; set; }
        public string TemplateCode { get; set; }
        public string TemplateDesc { get; set; }
        public string TemplateContent { get; set; }
        public int? DataSourceType { get; set; }
        public string DefaultDataSource { get; set; }
        public string LastTemplateContent { get; set; }
        public string OperCode { get; set; }
        public string OperName { get; set; }
        public string UpdateOperCode { get; set; }
        public string UpdateOperName { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string TemplateEnName { get; set; }
        public string ReportNameSuffix { get; set; }
        public int? ReportType { get; set; }
    }
}
