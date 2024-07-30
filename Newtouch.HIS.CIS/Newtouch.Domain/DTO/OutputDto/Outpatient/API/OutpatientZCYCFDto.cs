using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.OutputDto.Outpatient.API
{
   public class OutpatientZCYCFDto
    {
        public PatientInfo PatientInfo { get; set; }
        public List<zycf_xmInfo> zycf_xmInfo { get; set; }
        public cfd_Info cfd_Info { get; set; }
        public orgInfo orgInfo { get; set; }
    }

    public class zycf_xmInfo {
        public string mc1 { get; set; }
        public string mc2 { get; set; }
        public string mc3 { get; set; }
    }

    public class cfd_Info {
        public decimal je { get; set; }
        public DateTime CreateTime { get; set; }
        public int? tieshu { get; set; }
        public string yfmc { get; set; }
        public string cfh { get; set; }
    }
}
