using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.OutputDto.Outpatient.API
{
   public class OutpatientJCDDto
    {
        public PatientInfo PatientInfo { get; set; }
        public List<jcd_xmInfo> jcd_xmInfo { get; set; }
        public orgInfo orgInfo { get; set; }
        public DataInfo DataInfo { get; set; }
    }

    public class jcd_xmInfo
    {
        public long num { get; set; }
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
        public string bw { get; set; }
    }

    public class DataInfo {
        public string lcyx { get; set; }
        public string sqbz { get; set; }
        public string zs { get; set; }
    }
}
