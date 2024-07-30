using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
   public class DSrepeatRequestVO
    {
        public string Id { get; set; }
        public string pccode { get; set; }
        public DateTime kssj { get; set; }
        public string xmdm { get; set; }
    }
}
