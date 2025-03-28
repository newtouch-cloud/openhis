using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class ReortListVO
    {
        public string Id { get; set; }
        public string parentId { get; set; }
        public string ReportName { get; set; }
        public string ismx { get; set; }
        public string isty { get; set; }
        public string ReportCode { get; set; }
    }
}
