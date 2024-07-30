using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MedicalRecord
{
    public class AdviceListGridVO
    {
        public string yzlb { get; set; }
        public int yzlx { get; set; }
        public int yzzt { get; set; }
        public DateTime kssj { get; set; }
        public string ysmc { get; set; }
        public string yznr { get; set; }
        public int? zh { get; set; }
        public DateTime? tzsj { get; set; }
        public int yl { get; set; }
        public string yldw { get; set; }
        public string yf { get; set; }
        public string pc { get; set; }
        public string yzt { get; set; }
    }
}
