using Newtouch.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.DTO
{
    public class QueryArrangeDto
    {
        public string bq { get; set; }
        public DateTime? ksrq { get; set; }
        public DateTime? jsrq { get; set; }
        public string room { get; set; }
        public string ysgh { get; set; }
        public string orgId { get; set; }
        public string sszt { get; set; }
        public string keyword { get; set; }
        public Pagination pagination { get; set; }
    }
}
