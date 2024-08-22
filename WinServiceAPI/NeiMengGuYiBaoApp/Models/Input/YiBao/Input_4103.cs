using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4103 : InputBase
    {
        public data4103 data { get; set; }
    }
    public class data4103
    {
        public string psn_no { get; set; }
        public string setl_id { get; set; }
    }
}
