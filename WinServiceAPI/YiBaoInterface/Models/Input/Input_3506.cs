using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_3506 : InputBase
    {
        public purcinfo3506 selinfo { get; set; }
    }

    public class purcinfo3506
    {
        public string med_list_codg { get; set; }
        public string fixmedins_hilist_id { get; set; }
        public string fixmedins_hilist_name { get; set; }
        public string fixmedins_bchno { get; set; }
        public string setl_id { get; set; }
        public string psn_no { get; set; }
        public string psn_cert_type { get; set; }
        public string certno { get; set; }
        public string psn_name { get; set; }
        public string manu_lotnum { get; set; }
        public string manu_date { get; set; }
        public string expy_end { get; set; }
        public string rx_flag { get; set; }
        public string trdn_flag { get; set; }
        public string finl_trns_pric { get; set; }
        public string sel_retn_cnt { get; set; }
        public string sel_retn_time { get; set; }
        public string sel_retn_opter_name { get; set; }
        public string memo { get; set; }
    }
}
