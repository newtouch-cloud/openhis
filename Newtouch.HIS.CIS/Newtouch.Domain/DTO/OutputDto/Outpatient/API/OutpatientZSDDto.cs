using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.OutputDto.Outpatient.API
{
   public class OutpatientZSDDto
    {
        public PatientInfo PatientInfo { get; set; }
        public List<zsd_xmInfo> zsd_xmInfo { get; set; }
        public orgInfo orgInfo { get; set; }
    }

    public class zsd_xmInfo
    {
        public string num { get; set; }
        public string yzpcmcsm { get; set; }
        public string zh { get; set; }
        public long zhNum { get; set; }
        public DateTime createtime { get; set; }
        public string yfmc { get; set; }
        public string dw { get; set; }
        public string ypmc { get; set; }
        public decimal sl { get; set; }
        public string ds { get; set; }
    }
}
