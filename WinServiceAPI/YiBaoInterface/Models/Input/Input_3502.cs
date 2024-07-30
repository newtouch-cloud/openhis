using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
   public class Input_3502:InputBase
    {
        public invinfo3502 invinfo { get; set; }
    }

    public class invinfo3502
    {
        public string med_list_codg { get; set; }
        public string inv_chg_type { get; set; }
        public string fixmedins_hilist_id { get; set; }
        public string fixmedins_hilist_name { get; set; }
        public string fixmedins_bchno { get; set; }
        public string pric { get; set; }
        public string cnt { get; set; }
        public string rx_flag { get; set; }
        public string inv_chg_time { get; set; }
        public string inv_chg_opter_name { get; set; }
        public string memo { get; set; }
        public string trdn_flag { get; set; }
    }
}
