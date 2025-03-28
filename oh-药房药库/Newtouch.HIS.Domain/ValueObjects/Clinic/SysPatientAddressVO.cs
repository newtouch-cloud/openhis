using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.Clinic
{
    public class SysPatientAddressVO
    {
        public string Id { get; set; }
        public int patid { get; set; }
        //public string OrganizeId { get; set; }
        public string xm { get; set; }
        public string dh { get; set; }
        public string xian_sheng { get; set; }
        public string xian_shi { get; set; }
        public string xian_xian { get; set; }
        public string xian_dz { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
    }
}
