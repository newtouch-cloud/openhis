using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.OutputDto.Outpatient.API
{
   public class OutpatientJYDDto
    {
        public PatientInfo PatientInfo { get; set; }
        public List<jyd_xmInfo> jyd_xmInfo { get; set; }
        public orgInfo orgInfo { get; set; }
    }

    public class jyd_xmInfo
    {
        public long num { get; set; }
        public string cflxmc { get; set; }
        public int cflx { get; set; }
        public string cfh { get; set; }
        public decimal zje { get; set; }
        public DateTime createtime { get; set; }
        public string xmmc { get; set; }
        public decimal dj { get; set; }
        public int sl { get; set; }
        public string zxks { get; set; }
        public string sqys { get; set; }
        public string sqks { get; set; }
    }

    public class orgInfo {
        public string Name { get; set; }
    }

    public class barInfo
    {
        public string barcode { get; set; }
    }
}
