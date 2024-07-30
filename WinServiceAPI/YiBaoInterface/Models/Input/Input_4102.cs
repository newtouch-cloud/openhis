using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_4102 : InputBase
    {
        public data data { get; set; }
    }

    public class data
    {

        public List<stastinfo> stastinfo { get; set; }
    }

    public class stastinfo
    {
        public string psn_no { get; set; }
        public string setl_id { get; set; }
        public string stas_type { get; set; }
    }
}
