using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    public class MedRecordTypeVO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? px { get; set; }

    }
}
