using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3505 : InputBase
    {
        public purcinfo3505 selinfo { get; set; }
    }
    public class purcinfo3505
    {

        public string med_list_codg { get; set; }
        public string fixmedins_hilist_id { get; set; }
        public string fixmedins_hilist_name { get; set; }
        public string fixmedins_bchno { get; set; }
        public string prsc_dr_cert_type { get; set; }
        public string prsc_dr_certno { get; set; }
        public string prsc_dr_name { get; set; }
        public string phar_cert_type { get; set; }
        public string phar_certno { get; set; }
        public string phar_name { get; set; }
        public string phar_prac_cert_no { get; set; }
        public string hi_feesetl_type { get; set; }
        public string setl_id { get; set; }
        public string mdtrt_sn { get; set; }
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
        public string rxno { get; set; }
        public string rx_circ_flag { get; set; }
        public string rtal_docno { get; set; }
        public string stoout_no { get; set; }
        public string bchno { get; set; }
        public string drug_trac_codg { get; set; }
        public string drug_prod_barc { get; set; }
        public string shelf_posi { get; set; }
        public string sel_retn_cnt { get; set; }
        public string sel_retn_time { get; set; }
        public string sel_retn_opter_name { get; set; }
        public string memo { get; set; }

        public string MDTRT_SETL_TYPE { get; set; }
        public drugtracinfo drugtracinfo { get; set; }
    }
}
