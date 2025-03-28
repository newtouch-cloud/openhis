using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
   public class DAFormVO
    {
        public string Name {get; set; }
        public string DietType { get; set; }
        public string ParentId { get; set; }
        public string DietGroup { get; set; }
        public string bdsfxm { get; set; }
        public string py { get; set; }
        public string DietBigType { get; set; }
    }
}
