using System;
using System.Collections.Generic;


namespace JSViewer_MVC_Core.Models.OpenSource
{
    /// <summary>
    /// 模板明细
    /// </summary>
    public partial class SysReportTemplate
    {
        public int TemplateID { get; set; }
        public string HospitalCode { get; set; }
        public string SystemCode { get; set; }
        public string ReportCode { get; set; }
        public string ReportNameDes { get; set; }
        public string Content { get; set; }
        public int? ReportStatus { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? zt { get; set; }
    }
}
