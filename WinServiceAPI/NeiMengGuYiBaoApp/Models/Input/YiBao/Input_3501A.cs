using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3501A: InputBase
    {
        public List<invinfo3501A> invinfoDetail { get; set; }
    }

    public class invinfo3501A
    {
        public string med_list_codg { get; set; }
        public string fixmedins_hilist_id { get; set; }
        public string fixmedins_hilist_name { get; set; }
        public string rx_flag { get; set; }
        public string invdate { get; set; }
        public string inv_cnt { get; set; }
        public string manu_lotnum { get; set; }
        public string fixmedins_bchno { get; set; }
        public string manu_date { get; set; }
        public string expy_end { get; set; }
        public string memo { get; set; }

        public drugtracinfoA drugtracinfo { get; set; }
    }
    public class drugtracinfoA
    {
        public string drug_trac_code { get; set; }
    }
}
