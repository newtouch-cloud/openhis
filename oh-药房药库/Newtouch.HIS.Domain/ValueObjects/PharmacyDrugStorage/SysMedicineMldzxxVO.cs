using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysMedicineMldzxxVO : IEntity<SysMedicineMldzxxVO>
    {
        public string code { get; set; }
        public string name { get; set; }
        public string gg { get; set; }
        public string gjybdm { get; set; }
        public string isdz { get; set; }
    }
}
