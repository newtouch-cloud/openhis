using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class InpatientBedUseInfoVO
    {
        public string blh { get; set; }
        public string zyh { get; set; }
        public string BedCode { get; set; }
        public string BedNo { get; set; }
        public string WardCode { get; set; }
        public string WardName { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }

        public string TransWardCode { get; set; }

        public string TransDeptCode { get; set; }

        public string TransBedCode { get; set; }

        public int Status { get; set; }

        public string CreateTime { get; set; }

        public string LastModifyTime { get; set; }

    }
}
