using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.OutputDto.Outpatient.API
{
    public class Outpatientfastreportreq {
        public string cfId { get; set; }
        public string mzh { get; set; }
        public string cfh { get; set; }
        public string title { get; set; }
    }
   public class OutpatientCFDDto
    {
        public PatientInfo PatientInfo { get; set; }
        public List<cfd_xmInfo> cfd_xmInfo { get; set; }
        public orgInfo orgInfo { get; set; }
    }

    public class cfd_xmInfo {
        public string cfh { get; set; }
        public DateTime CreateTime { get; set; }
        public string sl { get; set; }
        public string xmmc { get; set; }
        public decimal dj { get; set; }
        public string ypgg { get; set; }
        public string mcjl { get; set; }
        public string mcjldw { get; set; }
        public string yfmc { get; set; }
        public string yzpcmcsm { get; set; }
        public decimal je { get; set; }
        public string mzcldw { get; set; }
        public string ds { get; set; }
        /// <summary>
        /// 处方标签 JI 精I JII 精II MZ 麻醉
        /// </summary>
        public string cftag { get; set; }
        public string Name { get; set; }
    }

    public class PatientInfo
    {
        public string blh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string nl { get; set; }
        public string ghksmc { get; set; }
        public string brxzmc { get; set; }
        public string jzysmc { get; set; }
        public string xyzdmc { get; set; }
        public string zyzdmc { get; set; }
        public string zs { get; set; }
        public string jws { get; set; }
        public string ContactNum { get; set; }
        public string kh { get; set; }
        public string ADDRESS { get; set; }
        public string hf { get; set; }
        public string mzh { get; set; }
        public DateTime? ghczsj { get; set; }
    }
}
