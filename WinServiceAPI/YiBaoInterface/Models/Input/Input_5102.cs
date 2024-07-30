using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_5102 : InputBase
    {
        public data5102 data { get; set; }
    }
    public class data5102 {
        public string prac_psn_type { get; set; }
    }
    public class data_5102
    {
        public string operatorId { get; set; }
        public string operatorName { get; set; }
        public string prac_psn_type { get; set; }
    }
}
