using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_4101 : InputBase
    {
        /// <summary>
        /// 表 154 结算清单信息（节点标识setlinfo）
        /// </summary>
        public setlinfo setlinfo { get; set; }

        /// <summary>
        /// 表 155 基金支付信息（节点标识：payinfo）
        /// </summary>
        public List<payinfo> payinfo { get; set; }

        /// <summary>
        /// 表 156 门诊慢特病诊断信息（节点标识：opspdiseinfo） 
        /// </summary>
        public List<opspdiseinfo> opspdiseinfo { get; set; }

        /// <summary>
        /// 表 157 住院诊断信息（节点标识：diseinfo）
        /// </summary>
        public List<diseinfo101> diseinfo { get; set; }

        /// <summary>
        /// 表 158 收费项目信息（节点标识：iteminfo）
        /// </summary>
        public List<iteminfo> iteminfo { get; set; }

        /// <summary>
        /// 表 159手术操作信息（节点标识：oprninfo） 
        /// </summary>
        public List<oprninfo> oprninfo { get; set; }

        /// <summary>
        /// 表 160 重症监护信息（节点标识：icuinfo） 
        /// </summary>
        public List<icuinfo> icuinfo { get; set; }
    }


    public class setlinfo
    {

        /// <summary>
        /// 1|就诊ID|字符型|30||Y|
        /// </summary> 
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 2|结算ID|字符型|30||Y|
        /// </summary> 
		public string setl_id { get; set; }

        /// <summary>
        /// 3|定点医药机构名称|字符型|200|  |Y|
        /// </summary> 
		public string fixmedins_name { get; set; }

        /// <summary>
        /// 4|定点医药机构编号|字符型|12|  |Y|
        /// </summary> 
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 5|医保结算等级|字符型|3|Y  ||
        /// </summary> 
        public string hi_setl_lv { get; set; }

        /// <summary>
        /// 6|医保编号|字符型|30|  |  |参保人在医保系统中的唯一身份代码
        /// </summary> 
        public string hi_no { get; set; }

        /// <summary>
        /// 7|病案号|字符型|40|  |Y|  
        /// </summary> 
        public string medcasno { get; set; }

        /// <summary>
        /// 8|申报时间|日期时间型|  |  |  |结算清单上报时间
        /// </summary> 
        public string dcla_time { get; set; }

        /// <summary>
        /// 9|人员姓名|字符型|50|  |Y|  
        /// </summary> 
        public string psn_name { get; set; }

        /// <summary>
        /// 10|性别|字符型|6|Y|Y|
        /// </summary> 
        public string gend { get; set; }

        /// <summary>
        /// 11|出生日期|日期型|  |  |Y  |
        /// </summary> 
        public string brdy { get; set; }

        /// <summary>
        /// 12|年龄|数值型|4,1|  ||大于1岁（含1岁）时必填，单位岁
        /// </summary> 
        public string age { get; set; }

        /// <summary>
        /// 13|国籍|字符型|6|Y|Y  |  
        /// </summary> 
        public string ntly { get; set; }

        /// <summary>
        /// 14|（年龄不足1周岁）年龄|数值型|3|  |  |小于1岁时必填，单位天
        /// </summary> 
        public string nwb_age { get; set; }

        /// <summary>
        /// 15|民族|字符型|3|Y|Y  |  
        /// </summary> 
        public string naty { get; set; }

        /// <summary>
        /// 16|患者证件类别|字符型|3|Y|Y|
        /// </summary> 
        public string patn_cert_type { get; set; }

        /// <summary>
        /// 17|证件号码|字符型|50|  |Y|患者证件号码
        /// </summary> 
		public string certno { get; set; }

        /// <summary>
        /// 18|职业|字符型|6|Y  |Y  |  
        /// </summary> 
        public string prfs { get; set; }

        /// <summary>
        /// 19|现住址|字符型|200|  |  |  
        /// </summary> 
        public string curr_addr { get; set; }

        /// <summary>
        /// 20|单位名称|字符型|200|  |  |  
        /// </summary> 
        public string emp_name { get; set; }

        /// <summary>
        /// 21|单位地址|字符型|200|  |  |  
        /// </summary> 
        public string emp_addr { get; set; }

        /// <summary>
        /// 22|单位电话|字符型|50|  |  |  
        /// </summary> 
        public string emp_tel { get; set; }

        /// <summary>
        /// 23|邮编|字符型|6|  |  |  
        /// </summary> 
        public string poscode { get; set; }

        /// <summary>
        /// 24|联系人姓名|字符型|50|  |Y  |  
        /// </summary> 
        public string coner_name { get; set; }

        /// <summary>
        /// 25|与患者关系|字符型|6|Y  |Y  |  
        /// </summary> 
        public string patn_rlts { get; set; }

        /// <summary>
        /// 26|联系人地址|字符型|200|  |Y  |  
        /// </summary> 
        public string coner_addr { get; set; }

        /// <summary>
        /// 27|联系人电话|字符型|50|  |Y  |
        /// </summary> 
        public string coner_tel { get; set; }

        /// <summary>
        /// 28|医保类型|字符型|3|Y  |Y  |
        /// </summary> 
        public string hi_type { get; set; }

        /// <summary>
        /// 29|参保地|字符型|6|  |Y  |
        /// </summary> 
        public string insuplc { get; set; }

        /// <summary>
        /// 30|特殊人员类型|字符型|6|Y||
        /// </summary> 
        public string sp_psn_type { get; set; }

        /// <summary>
        /// 31|新生儿入院类型|字符型|3|Y|  |  
        /// </summary> 
        public string nwb_adm_type { get; set; }

        /// <summary>
        /// 32|新生儿出生体重|数值型|6,2|  |  |精确到10克(g)
        /// </summary> 
        public string nwb_bir_wt { get; set; }

        /// <summary>
        /// 33|新生儿入院体重|数值型|6,2|  |  |精确到10克(g)
        /// </summary> 
        public string nwb_adm_wt { get; set; }

        /// <summary>
        /// 34|门诊慢特病诊断科别|字符型|50|  |  |
        /// </summary> 
        public string opsp_diag_caty { get; set; }

        /// <summary>
        /// 35|门诊慢特病就诊日期|日期型|  |  |  |
        /// </summary> 
		public string opsp_mdtrt_date { get; set; }

        /// <summary>
        /// 36|住院医疗类型|字符型|3|Y  |Y  |
        /// </summary> 
		public string ipt_med_type { get; set; }

        /// <summary>
        /// 37|入院途径|字符型|3|Y  |  |  
        /// </summary> 
        public string adm_way { get; set; }

        /// <summary>
        /// 38|治疗类别|字符型|3|Y  |  |  
        /// </summary> 
        public string trt_type { get; set; }

        /// <summary>
        /// 39|入院时间|日期时间型|  |  |  |
        /// </summary> 
        public DateTime adm_time { get; set; }

        /// <summary>
        /// 40|入院科别|字符型|6|Y  |Y  |参照科室代码（dept）
        /// </summary> 
        public string adm_caty { get; set; }

        /// <summary>
        /// 41|转科科别|字符型|6|Y  |  |参照科室代码（dept），如果超过一次以上的转科，用“→”转接表示
        /// </summary> 
        public string refldept_dept { get; set; }

        /// <summary>
        /// 42|出院时间|日期时间型|  |  |  |
        /// </summary> 
		public string dscg_time { get; set; }

        /// <summary>
        /// 43|出院科别|字符型|6|Y  |Y  |参照科室代码（dept）
        /// </summary> 
        public string dscg_caty { get; set; }

        /// <summary>
        /// 44|实际住院天数|数值型|3|  |  |  
        /// </summary> 
        public string act_ipt_days { get; set; }

        /// <summary>
        /// 45|门（急）诊西医诊断|字符型|200|  |  |  
        /// </summary> 
        public string otp_wm_dise { get; set; }

        /// <summary>
        /// 46|西医诊断疾病代码|字符型|20|  |  |
        /// </summary> 
        public string wm_dise_code { get; set; }

        /// <summary>
        /// 47|门（急）诊中医诊断|字符型|200|  |  |  
        /// </summary> 
        public string otp_tcm_dise { get; set; }

        /// <summary>
        /// 48|中医诊断代码|字符型|20|  |  |
        /// </summary> 
        public string tcm_dise_code { get; set; }

        /// <summary>
        /// 49|诊断代码计数|数值型|3|  |  |  
        /// </summary> 
		public string diag_code_cnt { get; set; }

        /// <summary>
        /// 50|主诊断标志|字符型|3|Y|Y|
        /// </summary> 
		public string maindiag_flag { get; set; }

        /// <summary>
        /// 51|手术操作代码计数|数值型|3|  |  |  
        /// </summary> 
		public string oprn_oprt_code_cnt { get; set; }

        /// <summary>
        /// 52|呼吸机使用时长|字符型|10|  |  |格式：天数/小时数/分钟数 例：1/13/24
        /// </summary> 
		public string vent_used_dura { get; set; }

        /// <summary>
        /// 53|颅脑损伤患者入院前昏迷时长|字符型|10|  |  |格式：天数/小时数/分钟数 例：1/13/24
        /// </summary> 
		public string pwcry_bfadm_coma_dura { get; set; }

        /// <summary>
        /// 54|颅脑损伤患者入院后昏迷时长|字符型|10|  |  |格式：天数/小时数/分钟数 例：1/13/24
        /// </summary> 
        public string pwcry_afadm_coma_dura { get; set; }

        /// <summary>
        /// 55|输血品种|字符型|3|Y  |  |  
        /// </summary> 
        public string bld_cat { get; set; }

        /// <summary>
        /// 56|输血量|数值型|6|  |  |  
        /// </summary> 
        public string bld_amt { get; set; }

        /// <summary>
        /// 57|输血计量单位|字符型|3|  |  |  
        /// </summary> 
        public string bld_unt { get; set; }

        /// <summary>
        /// 58|特级护理天数|数值型|3|  |  |  
        /// </summary> 
        public string spga_nurscare_days { get; set; }

        /// <summary>
        /// 59|一级护理天数|数值型|3|  |  |  
        /// </summary> 
		public string lv1_nurscare_days { get; set; }

        /// <summary>
        /// 60|二级护理天数|数值型|3|  |  |  
        /// </summary> 
        public string scd_nurscare_days { get; set; }

        /// <summary>
        /// 61|三级护理天数|数值型|3|  |  |  
        /// </summary> 
        public string lv3_nurscare_days { get; set; }

        /// <summary>
        /// 62|离院方式|字符型|3|Y|  |  
        /// </summary> 
        public string dscg_way { get; set; }

        /// <summary>
        /// 63|拟接收机构名称|字符型|100|  |  |当离院方式为“2”时，如果接收患者的医疗机构明确，需要填写转入医疗机构的名称；当离院方式为“3”时，如果接收患者的社区卫生服务机构明确，需要填写社区卫生服务机构/乡镇卫生院名称
        /// </summary> 
        public string acp_medins_name { get; set; }

        /// <summary>
        /// 64|拟接收机构代码|字符型|30|  |  |当离院方式为“2”或“3”时，如果接收患者的医疗机构或社区卫生服务机构明确，需要填写机构对应的医保定点医疗机构代码
        /// </summary> 
		public string acp_optins_code { get; set; }

        /// <summary>
        /// 65|票据代码|字符型|50||Y|
        /// </summary> 
		public string bill_code { get; set; }

        /// <summary>
        /// 66|票据号码|字符型|30||Y|
        /// </summary> 
        public string bill_no { get; set; }

        /// <summary>
        /// 67|业务流水号|字符型|50||Y|业务流水号
        /// </summary> 
        public string biz_sn { get; set; }

        /// <summary>
        /// 68|出院31天内再住院计划标志|字符型|3|Y  |  |
        /// </summary> 
        public string days_rinp_flag_31 { get; set; }

        /// <summary>
        /// 69|出院31天内再住院目的|字符型|200|  |  |  
        /// </summary> 
		public string days_rinp_pup_31 { get; set; }

        /// <summary>
        /// 70|主诊医师姓名|字符型|50|  |  |  
        /// </summary> 
		public string chfpdr_name { get; set; }

        /// <summary>
        /// 71|主诊医师代码|字符型|30|  |  |主诊医师在《医保医师代码》中的代码，在就医地未完成标准化前，可传医师在就医地系统中的唯一编号
        /// </summary> 
        public string chfpdr_code { get; set; }

        /// <summary>
        /// 72|结算开始日期|日期型|  |  |Y  |
        /// </summary> 
        public DateTime setl_begn_date { get; set; }

        /// <summary>
        /// 73|结算结束日期|日期型|  |  |Y  |
        /// </summary> 
		public DateTime setl_end_date { get; set; }

        /// <summary>
        /// 74|个人自付|数值型|16,2|  |Y  |  
        /// </summary> 
		public string psn_selfpay { get; set; }

        /// <summary>
        /// 75|个人自费|数值型|16,2|  |Y  |  
        /// </summary> 
        public string psn_ownpay { get; set; }

        /// <summary>
        /// 76|个人账户支出|数值型|16,2|  |Y  |  
        /// </summary> 
        public string acct_pay { get; set; }

        /// <summary>
        /// 77|个人现金支付|数值型|16,2|  |Y  |  
        /// </summary> 
        public string psn_cashpay { get; set; }

        /// <summary>
        /// 78|医保支付方式|字符型|3|Y  |Y  |  
        /// </summary> 
        public string hi_paymtd { get; set; }

        /// <summary>
        /// 79|医保机构|字符型|100||Y|
        /// </summary> 
        public string hsorg { get; set; }

        /// <summary>
        /// 80|医保机构经办人|字符型|50||Y|
        /// </summary> 
        public string hsorg_opter { get; set; }

        /// <summary>
        /// 81|医疗机构填报部门|字符型|100|  |Y  |  
        /// </summary> 
        public string medins_fill_dept { get; set; }

        /// <summary>
        /// 82|医疗机构填报人|字符型|50|  |Y  |  
        /// </summary> 
		public string medins_fill_psn { get; set; }

    }

    public class payinfo
    {

        /// <summary>
        /// 1|基金支付类型|字符型|6| Y| Y|  
        /// </summary> 
        public string fund_pay_type { get; set; }

        /// <summary>
        /// 2|基金支付金额|数值型|16,2|  | Y|  
        /// </summary> 
		public string fund_payamt { get; set; }
    }

    public class opspdiseinfo
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

    public class diseinfo101
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

    public class iteminfo
    {

        /// <summary>
        /// 1  |医疗收费项目|字符型|6|Y|Y|参照医疗收费项目类别
        /// </summary> 
        public string med_chrgitm { get; set; }

        /// <summary>
        /// 2  |金额|数值型|16,2||Y|
        /// </summary> 
		public string amt { get; set; }

        /// <summary>
        /// 3  |甲类费用合计|数值型|16,2||Y|
        /// </summary> 
		public string claa_sumfee { get; set; }

        /// <summary>
        /// 4  |乙类金额|数值型|16,2||Y|
        /// </summary> 
		public string clab_amt { get; set; }

        /// <summary>
        /// 5  | 	全自费金额|数值型|16,2||Y|
        /// </summary> 
		public string fulamt_ownpay_amt { get; set; }

        /// <summary>
        /// 6  |其他金额|数值型|16,2||Y|
        /// </summary> 
		public string oth_amt { get; set; }

    }

    public class oprninfo
    {

        /// <summary>
        /// 1|手术操作类别|字符型|3|Y|Y|
        /// </summary> 
        public string oprn_oprt_type { get; set; }

        /// <summary>
        /// 2|手术操作名称|字符型|500|  |Y|  
        /// </summary> 
		public string oprn_oprt_name { get; set; }

        /// <summary>
        /// 3|手术操作代码|字符型|30|  |Y|  
        /// </summary> 
		public string oprn_oprt_code { get; set; }

        /// <summary>
        /// 4|手术操作日期|日期型||  |Y|  
        /// </summary> 
		public string oprn_oprt_date { get; set; }

        /// <summary>
        /// 5|麻醉方式|字符型|6|Y|  |参照麻醉方法代码
        /// </summary> 
		public string anst_way { get; set; }

        /// <summary>
        /// 6|术者医师姓名|字符型|50|  |Y|  
        /// </summary> 
		public string oper_dr_name { get; set; }

        /// <summary>
        /// 7|术者医师代码|字符型|20|  |Y|
        /// </summary> 
		public string oper_dr_code { get; set; }

        /// <summary>
        /// 8|麻醉医师姓名|字符型|50|  |  |
        /// </summary> 
		public string anst_dr_name { get; set; }

        /// <summary>
        /// 9|麻醉医师代码|字符型|20|  |  |
        /// </summary> 
		public string anst_dr_code { get; set; }

    }

    public class icuinfo
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
}
