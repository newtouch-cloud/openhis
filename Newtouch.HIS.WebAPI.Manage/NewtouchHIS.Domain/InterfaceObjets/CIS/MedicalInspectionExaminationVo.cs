using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.CIS
{
    public class MedicalInspectionExaminationVo
    {
        public string OrganizeId { get; set; }
        public string Xm { get; set; }
        public string Lissqdh { get; set; }
        public string Sqdh { get; set; }
        public string Sqys { get; set; }
        public string Sqxm { get; set; }
        public string SyncStatus { get; set; }
        public string Sqsj { get; set; }
        public DateTime? Bgrq { get; set; }
    }
}
