using System;
using System.Collections.Generic;

namespace JSViewer_MVC_Core.Models.OpenSource
{
    /// <summary>
    /// 模板主表
    /// </summary>
    public partial class SysReport
    {
        public int ReportID { get; set; }
        public string HospitalCode { get; set; }
        public string SystemCode { get; set; }
        public string SystemName { get; set; }
        public string ReportCode { get; set; }
        public string ReportName { get; set; }
        public string PinYin { get; set; }
        public string ReportDes { get; set; }
        public int? ReportType { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? px { get; set; }
        public int? zt { get; set; }
        public int? StatusTemplateID { get; set; }
        public int? ParentId { get; set; }
        public int? DirectoryFlag { get; set; }
    }
}
