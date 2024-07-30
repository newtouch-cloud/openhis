using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSViewer_MVC_Core.OutputModels
{
    public class SysReportTemplateOM
    {
        public int TemplateId { get; set; }
        public string HospitalCode { get; set; }
        public string TemplateCode { get; set; }
        public string TemplateDesc { get; set; }

        public string TemplateEnName { get; set; }

        public int TemplateType { get; set; }

        public string ReportNameSuffix { get; set; }

        public int? ReportType { get; set; }
    }
}
