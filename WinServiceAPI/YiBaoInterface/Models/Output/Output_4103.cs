using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_4103 : OutputBase
    {
        public setlinfo4103 setlinfo { get; set; }
        public payinfo4103 payinfo { get; set; }
        public opspdiseinfo4103 opspdiseinfo { get; set; }
        public diseinfo4103 diseinfo { get; set; }
        public iteminfo4103 iteminfo { get; set; }
        public oprninfo4103 oprninfo { get; set; }
        public icuinfo4103 icuinfo { get; set; }
        public bldinfo4103 bldinfo { get; set; }
    }

    public class setlinfo4103
    {
        public string setl_id { get; set; }
        public string setl_list_sn { get; set; }
        public string psn_no { get; set; }
        public string mdtrt_id { get; set; }
        public string fixmedins_name { get; set; }
        public string fixmedins_code { get; set; }
        public string hi_setl_lv { get; set; }
        public string medcasno { get; set; }
        public string dcla_time { get; set; }
        public string psn_name { get; set; }

        public string gend { get; set; }
        public string brdy { get; set; }
        public string age { get; set; }
        public string age_days { get; set; }
        public string ntly { get; set; }
        public string naty { get; set; }
        public string psn_cert_type { get; set; }
        public string certno { get; set; }
        public string prfs { get; set; }
        public string curr_addr { get; set; }

        public string emp_name { get; set; }
        public string emp_addr { get; set; }
        public string emp_tel { get; set; }
        public string emp_poscode { get; set; }
        public string coner_name { get; set; }
        public string patn_rlts { get; set; }
        public string coner_addr { get; set; }
        public string coner_tel { get; set; }
        public string insutype { get; set; }
        public string sp_psn_type { get; set; }

        public string insu_admdvs { get; set; }
        public string nwb_adm_type { get; set; }
        public string nwb_bir_wt { get; set; }
        public string nwb_adm_wt { get; set; }
        public string mul_nwb_bir_wt { get; set; }
        public string mul_nwb_adm_wt { get; set; }
        public string opsp_diag_caty { get; set; }
        public string opsp_mdtrt_time { get; set; }
        public string ipt_med_type { get; set; }
        public string adm_way_code { get; set; }

        public string trt_type { get; set; }
        public string adm_time { get; set; }
        public string dscg_time { get; set; }
        public string adm_caty { get; set; }
        public string refl_caty { get; set; }
        public string dscg_caty { get; set; }
        public string act_ipt_days { get; set; }
        public string otp_wm_diag { get; set; }
        public string otp_wm_diag_dise_code { get; set; }
        public string otp_tcm_diag { get; set; }

        public string otp_tcm_diag_dise_code { get; set; }
        public string diag_code_cnt { get; set; }
        public string oprn_oprt_code_cnt { get; set; }
        public string vent_used_days { get; set; }
        public string vent_used_h_cnt { get; set; }
        public string vent_used_m_cnt { get; set; }
        public string bfadm_coma_days { get; set; }
        public string bfadm_coma_h_cnt { get; set; }
        public string bfadm_coma_m_cnt { get; set; }
        public string afadm_coma_days { get; set; }

        public string afadm_coma_h_cnt { get; set; }
        public string afadm_coma_m_cnt { get; set; }
        public string spga_nurscare_days { get; set; }
        public string lv1_nurscare_days { get; set; }
        public string scd_nurscare_days { get; set; }
        public string lv3_nurscare_days { get; set; }
        public string dscg_way { get; set; }

        public string acp_optins_name { get; set; }
        public string acp_optins_code { get; set; }
        public string dscg31days_rinp_flag { get; set; }
        public string rinp_pup { get; set; }
        public string chfpdr_name { get; set; }
        public string chfpdr_code { get; set; }
        public string biz_sn { get; set; }
        public string bill_code { get; set; }
        public string bill_no { get; set; }
        public string setl_begndate { get; set; }

        public string setl_enddate { get; set; }
        public string psn_selfpay_amt { get; set; }
        public string psn_ownpay_fee { get; set; }
        public string acct_payamt { get; set; }
        public string psn_cashpay { get; set; }
        public string hi_paymtd { get; set; }
        public string medins_fill_dept { get; set; }
        public string medins_fill_psn { get; set; }
        public string hi_no { get; set; }
        public string hi_type { get; set; }

        public string opsp_dise_name { get; set; }
        public string opsp_dise_code { get; set; }
        public string dscg_diag { get; set; }
        public string resp_nurs_name { get; set; }
        public string resp_nurs_code { get; set; }
        public string hsorg_opter_code { get; set; }
        public string stas_type { get; set; }
        public string hsorg_opter_name { get; set; }
        public string hsorg_name { get; set; }
        public string hsorg_code { get; set; }
        public string chk_cont { get; set; }

    }
    public class payinfo4103
    {
        public string setl_id { get; set; }
        public string psn_no { get; set; }
        public string mdtrt_id { get; set; }
        public string fund_pay_type { get; set; }
        public string poolarea_fund_pay_type { get; set; }
        public string poolarea_fund_pay_name { get; set; }
        public string fund_payamt { get; set; }
    }
    public class opspdiseinfo4103
    {
        public string setl_list_opsp_trt_id { get; set; }
        public string mdtrt_id { get; set; }
        public string setl_id { get; set; }
        public string psn_no { get; set; }
        public string diag_code { get; set; }
        public string diag_name { get; set; }
        public string oprn_oprt_name { get; set; }
        public string oprn_oprt_code { get; set; }
    }
    public class diseinfo4103
    {
        public string setl_list_diag_id { get; set; }
        public string setl_id { get; set; }
        public string mdtrt_id { get; set; }
        public string psn_no { get; set; }
        public string diag_type { get; set; }
        public string maindiag_flag { get; set; }
        public string diag_code { get; set; }
        public string diag_name { get; set; }
        public string adm_cond_type { get; set; }
    }
    public class iteminfo4103
    {
        public string setl_list_chrgitm_id { get; set; }
        public string setl_id { get; set; }
        public string mdtrt_id { get; set; }
        public string psn_no { get; set; }
        public string med_chrgitm_type { get; set; }
        public string item_sumamt { get; set; }
        public string item_claa_amt { get; set; }
        public string item_clab_amt { get; set; }
        public string item_ownpay_amt { get; set; }
        public string item_oth_amt { get; set; }
        public string sindise_code_name { get; set; }
        public string daysrg_code_name { get; set; }
    }
    public class oprninfo4103
    {
        public string setl_list_oprn_id { get; set; }
        public string setl_id { get; set; }
        public string psn_no { get; set; }
        public string mdtrt_id { get; set; }
        public string main_oprn_flag { get; set; }
        public string oprn_oprt_name { get; set; }
        public string oprn_oprt_code { get; set; }
        public string oprn_oprt_date { get; set; }
        public string anst_way { get; set; }
        public string oper_dr_name { get; set; }
        public string oper_dr_code { get; set; }
        public string anst_dr_name { get; set; }
        public string anst_dr_code { get; set; }
        public string oprn_oprt_begntime { get; set; }
        public string oprn_oprt_endtime { get; set; }
        public string anst_begntime { get; set; }
        public string anst_endtime { get; set; }
    }
    public class icuinfo4103
    {
        public string setl_list_scs_cutd_id { get; set; }
        public string psn_no { get; set; }
        public string mdtrt_id { get; set; }
        public string setl_id { get; set; }
        public string scs_cutd_ward_type { get; set; }
        public string scs_cutd_inpool_time { get; set; }
        public string scs_cutd_exit_time { get; set; }
        public string scs_cutd_sum_dura { get; set; }
    }
    public class bldinfo4103
    {
        public string setl_List_bld_Id { get; set; }
        public string psn_no { get; set; }
        public string mdtrt_id { get; set; }
        public string setl_id { get; set; }
        public string bld_cat { get; set; }
        public string bld_amt { get; set; }
        public string bld_unt { get; set; }
    }
}
