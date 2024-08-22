using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_5102 : InputBase
    {
        public data5102 data { get; set; }
    }
    public class data5102 {
        public string prac_psn_type { get; set; }

        public string psn_cert_type { get; set; }

        public string certno { get; set; }

        public string prac_psn_name { get; set; }

        public string prac_psn_code { get; set; }
    }
    public class data_5102
    {
        public string operatorId { get; set; }
        public string operatorName { get; set; }
        public string prac_psn_type { get; set; }
    }
}
