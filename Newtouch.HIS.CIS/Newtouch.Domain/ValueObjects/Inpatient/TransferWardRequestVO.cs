using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
   public class TransferWardRequestVO
    {
        public string zyh { get; set; }
        public string bq { get; set; }
        public DateTime kssj { get; set; }
        public string orgId { get; set; }
        public string czr { get; set; }
    }
}
