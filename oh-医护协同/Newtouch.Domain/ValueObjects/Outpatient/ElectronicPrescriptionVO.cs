using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    public class ElectronicPrescriptionVO
    {
        public string cfId { get; set; }
        public string cfh { get; set; }
        public string mzh { get; set; }
        public string xm { get; set; }
        public string zjlx { get; set; }
        public string zjh { get; set; }
        public string shzt { get; set; }
        public string qyzt { get; set; }
        public string cfzt { get; set; }
        public string ghks { get; set; }
        public string kdks { get; set; }
        public DateTime? ghsj { get; set; }
        public DateTime? cfklrq { get; set; }
        public string ysshyj { get; set; }

    }

    public class examSearchDTO
    {
        public string hiRxno { get; set; }
        public string fixmedinsCode { get; set; }
        public string mdtrtId { get; set; }
        public string psnName { get; set; }
        public string psnCertType { get; set; }
        public string certno { get; set; }
    }
}
