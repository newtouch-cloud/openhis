using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class ReortMXListVO
    {
        public int ReportID { get; set; }
        public string ReportCode { get; set; }
        public string ReportName { get; set; }
        public string HospitalCode { get; set; }
        public string SystemCode { get; set; }
        public string SystemName { get; set; }
        public string px { get; set; }
        public string mc { get; set; }
        public string ly { get; set; }
        public string ReportType { get; set; }
    }
}
