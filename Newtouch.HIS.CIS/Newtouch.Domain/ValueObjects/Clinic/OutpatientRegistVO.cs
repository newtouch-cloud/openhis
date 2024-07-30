using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Clinic
{
    public class OutpatientRegistVO
    {
        public string RegId { get; set; }
        public int QueueNo { get; set; }
        public string Mzh { get; set; }
    }
}
