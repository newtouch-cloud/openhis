using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysExpertVO : IEntity<SysExpertVO>
    {

        public string gh { get; set; }
        public string name { get; set; }
        public string dutyCode { get; set; }
        public string deptCode { get; set; }

    }
}
