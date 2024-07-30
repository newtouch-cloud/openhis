using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MedicalRecord
{
    public class OrderInformationVO
    {
        public string Id { get; set; }
        public string yzlb { get; set; }
        public int yzlx { get; set; }
        public string zyh { get; set; }
        public string DeptCode { get; set; }
        public string kssj { get; set; }
        public string ysgh { get; set; }
        public string yznr { get; set; }
        public string zxsj { get; set; }

        public string ryrq { get; set; }
        public int ? yzzt { get; set; }

    }
}
