using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_1201:InputBase
    {
    
        public medinsinfo1201 medinsinfo { get; set; }

    }
    public class medinsinfo1201
    {
        public string fixmedins_type { get; set; }
        public string fixmedins_code { get; set; }
        public string fixmedins_name { get; set; }
    }
}
