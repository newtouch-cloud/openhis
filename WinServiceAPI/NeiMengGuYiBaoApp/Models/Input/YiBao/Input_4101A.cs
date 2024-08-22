using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4101A : InputBase
    {
        /// <summary>
        /// 表 154 结算清单信息（节点标识setlinfo）
        /// </summary>
        public setlinfoA setlinfo { get; set; }
        /// 表 156 门诊慢特病诊断信息（节点标识：opspdiseinfo） 
        /// </summary>
        public List<opspdiseinfoA> opspdiseinfo { get; set; }

        /// <summary>
        /// 表 157 住院诊断信息（节点标识：diseinfo）
        /// </summary>
        public List<diseinfo101A> diseinfo { get; set; }

        /// <summary>
        /// 表 159手术操作信息（节点标识：oprninfo） 
        /// </summary>
        public List<oprninfoA> oprninfo { get; set; }

        /// <summary>
        /// 表 160 重症监护信息（节点标识：icuinfo） 
        /// </summary>
        public List<icuinfoA> icuinfo { get; set; }
        /// <summary>
        /// 表 194输血信息（节点标识：bldinfo） 
        /// </summary>
        public List<bldinfoA> bldinfo { get; set; }

    }


    public class setlinfoA
    {
        public string psn_no { get; set; }
        public string mdtrt_id { get; set; }
        public string setl_id { get; set; }
        public string hi_no { get; set; }
        public string medcasno { get; set; }
        public string dcla_time { get; set; }
        public string ntly { get; set; }
        public string prfs { get; set; }
        public string curr_addr { get; set; }
        public string emp_name { get; set; }
        public string emp_addr { get; set; }
        public string emp_tel { get; set; }
        public string poscode { get; set; }
        public string coner_name { get; set; }
        public string patn_rlts { get; set; }
        public string coner_addr { get; set; }
        public string coner_tel { get; set; }
        public string nwb_adm_type { get; set; }
        public string nwb_bir_wt { get; set; }
        public string nwb_adm_wt { get; set; }
        public string mul_nwb_bir_wt { get; set; }
        public string mul_nwb_adm_wt { get; set; }
        public string opsp_diag_caty { get; set; }
        public string opsp_mdtrt_date { get; set; }
        public string adm_way { get; set; }
        public string trt_type { get; set; }
        public string adm_time { get; set; }
        public string refldept_dept { get; set; }
        public string dscg_time { get; set; }
        public string dscg_caty { get; set; }
        public string otp_wm_dise { get; set; }
        public string wm_dise_code { get; set; }
        public string otp_tcm_dise { get; set; }
        public string tcm_dise_code { get; set; }
        public string vent_used_dura { get; set; }
        public string pwcry_bfadm_coma_dura { get; set; }
        public string pwcry_afadm_coma_dura { get; set; }
        public string spga_nurscare_days { get; set; }
        public string lv1_nurscare_days { get; set; }
        public string scd_nurscare_days { get; set; }
        public string lv3_nurscare_days { get; set; }
        public string dscg_way { get; set; }
        public string acp_medins_name { get; set; }
        public string acp_optins_code { get; set; }
        public string bill_code { get; set; }
        public string bill_no { get; set; }
        public string biz_sn { get; set; }
        public string days_rinp_flag_31 { get; set; }
        public string days_rinp_pup_31 { get; set; }
        public string chfpdr_code { get; set; }
        public string setl_begn_date { get; set; }
        public string setl_end_date { get; set; }
        public string medins_fill_dept { get; set; }
        public string medins_fill_psn { get; set; }
        public string resp_nurs_code { get; set; }
        public string stas_type { get; set; }
        public string hi_paymtd { get; set; }
    }

    public class opspdiseinfoA
    {

        /// <summary>
        /// 1  |诊断名称|字符型|100||Y|
        /// </summary> 
        public string diag_name { get; set; }

        /// <summary>
        /// 2  |诊断代码|字符型|20||Y|
        /// </summary> 
		public string diag_code { get; set; }

        /// <summary>
        /// 3  |手术操作名称|字符型|500||Y|
        /// </summary> 
		public string oprn_oprt_name { get; set; }

        /// <summary>
        /// 4  |手术操作代码|字符型|30||Y|
        /// </summary> 
		public string oprn_oprt_code { get; set; }

    }

    public class diseinfo101A
    {
        /// <summary>
        /// 1|诊断类别|字符型|3| Y| Y|  
        /// </summary> 
        public string diag_type { get; set; }

        /// <summary>
        /// 2|诊断代码|字符型|20|  | Y|  
        /// </summary> 
		public string diag_code { get; set; }

        /// <summary>
        /// 3|诊断名称|字符型|100|  | Y|  
        /// </summary> 
		public string diag_name { get; set; }

        /// <summary>
        /// 4|入院病情类型|字符型|3|Y  |  |  
        /// </summary> 
		public string adm_cond_type { get; set; }

        public string maindiag_flag { get; set; }
    }
    
    public class oprninfoA
    {
        public string oprn_oprt_type { get; set; }
		public string oprn_oprt_name { get; set; }
		public string oprn_oprt_code { get; set; }
        public string anst_way { get; set; }
        public string oper_dr_code { get; set; }
        public string anst_dr_code { get; set; }
		public string oprn_oprt_begntime { get; set; }
		public string oprn_oprt_endtime { get; set; }
		public string anst_begntime { get; set; }
		public string anst_endtime { get; set; }
    }
    public class icuinfoA
    {
        /// <summary>
        /// 1  |重症监护病房类型|字符型|6|Y|Y|
        /// </summary> 
        public string scs_cutd_ward_type { get; set; }

        /// <summary>
        /// 2  |重症监护进入时间|日期时间型|||Y|
        /// </summary> 
		public string scs_cutd_inpool_time { get; set; }

        /// <summary>
        /// 3  |重症监护退出时间|日期时间型|||Y|
        /// </summary> 
		public string scs_cutd_exit_time { get; set; }

        /// <summary>
        /// 4  |重症监护合计时长|字符型|10||Y|格式：天数/小时数/分钟数例：1/13/24
        /// </summary> 
		public string scs_cutd_sum_dura { get; set; }
    }
    public class bldinfoA
    {
        public string bld_cat { get; set; }
		public string bld_amt { get; set; }
        public string bld_unt { get; set; }
    }
}
