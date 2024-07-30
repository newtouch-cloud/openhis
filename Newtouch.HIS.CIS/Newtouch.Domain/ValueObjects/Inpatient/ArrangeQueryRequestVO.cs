using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    /// <summary>
    /// 手术医嘱查询Grid request
    /// </summary>
    public class ArrangeQueryRequestVO
    {
        public DateTime kssj { get; set; }
        public DateTime jssj { get; set; }
        public string orgId { get; set; }
        public string zyh { get; set; }
        public string WardCode { get; set; }
    }
}
