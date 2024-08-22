using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4401 : InputBase
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        public baseinfo_4401 baseinfo { get; set; }
        /// <summary>
        /// 诊断信息
        /// </summary>
        public List<diseinfo_4401> diseinfo { get; set; }
        /// <summary>
        /// 手术信息
        /// </summary>
        public List<oprninfo_4401> oprninfo { get; set; }

        /// <summary>
        /// 重症监护信息（节点标识：icuinfo）
        /// </summary>
        public List<icuinfo_4401> icuinfo { get; set; }
    }

    public class baseinfo_4401
    {

        /// <summary>
        /// 1|就医流水号|字符型|30||Y|格式：定点医药机构编号+院内唯一流水号
        /// </summary> 
        public string mdtrt_sn { get; set; }

        /// <summary>
        /// 2|就诊ID|字符型|30|||医保病人必填
        /// </summary> 
		public string mdtrt_id { get; set; }

        /// <summary>
        /// 3|人员编号|字符型|30|||医保病人必填
        /// </summary> 
		public string psn_no { get; set; }

        /// <summary>
        /// 4|患者住院次数|字符型|5,0|||
        /// </summary> 
		public string patn_ipt_cnt { get; set; }

        /// <summary>
        /// 5|住院号|字符型|30|||
        /// </summary> 
		public string ipt_no { get; set; }

        /// <summary>
        /// 6|病案号|字符型|40||Y|
        /// </summary> 
		public string medcasno { get; set; }

        /// <summary>
        /// 7|人员姓名|字符型|50||Y|
        /// </summary> 
		public string psn_name { get; set; }

        /// <summary>
        /// 8|性别|字符型|6|Y||
        /// </summary> 
		public string gend { get; set; }

        /// <summary>
        /// 9|出生日期|日期型||||
        /// </summary> 
		public string brdy { get; set; }

        /// <summary>
        /// 10|国籍|字符型|6|Y||
        /// </summary> 
		public string ntly { get; set; }

        /// <summary>
        /// 11|国籍名称|字符型|70|||
        /// </summary> 
		public string ntly_name { get; set; }

        /// <summary>
        /// 12|新生儿出生体重|数值型|6,2|||
        /// </summary> 
		public string nwb_bir_wt { get; set; }

        /// <summary>
        /// 13|新生儿入院体重|数值型|6,2|||
        /// </summary> 
		public string nwb_adm_wt { get; set; }

        /// <summary>
        /// 14|出生地|字符型|200|||
        /// </summary> 
		public string birplc { get; set; }

        /// <summary>
        /// 15|籍贯|字符型|100|||
        /// </summary> 
		public string napl { get; set; }

        /// <summary>
        /// 16|民族名称|字符型|100|||
        /// </summary> 
		public string naty_name { get; set; }

        /// <summary>
        /// 17|民族|字符型|3|Y||
        /// </summary> 
		public string naty { get; set; }

        /// <summary>
        /// 18|证件号码|字符型|50|||
        /// </summary> 
		public string certno { get; set; }

        /// <summary>
        /// 19|职业|字符型|6|Y||
        /// </summary> 
		public string prfs { get; set; }

        /// <summary>
        /// 20|婚姻状态|字符型|3|Y||
        /// </summary> 
		public string mrg_stas { get; set; }

        /// <summary>
        /// 21|现住址-邮政编码|字符型|6|||
        /// </summary> 
		public string curr_addr_poscode { get; set; }

        /// <summary>
        /// 22|现住址|字符型|200|||
        /// </summary> 
		public string curr_addr { get; set; }

        /// <summary>
        /// 23|个人联系电话|字符型|70|||
        /// </summary> 
		public string psn_tel { get; set; }

        /// <summary>
        /// 24|户口地址-省（自治区、直辖市）|字符型|30|||
        /// </summary> 
		public string resd_addr_prov { get; set; }

        /// <summary>
        /// 25|户口地址-市（地区）|字符型|500|||
        /// </summary> 
		public string resd_addr_city { get; set; }

        /// <summary>
        /// 26|户口地址-县（区）|字符型|500|||
        /// </summary> 
		public string resd_addr_coty { get; set; }

        /// <summary>
        /// 27|户口地址-乡（镇、街道办事处）|字符型|500|||
        /// </summary> 
		public string resd_addr_subd { get; set; }

        /// <summary>
        /// 28|户口地址-村（街、路、弄等）|字符型|500|||
        /// </summary> 
		public string resd_addr_vil { get; set; }

        /// <summary>
        /// 29|户口地址-门牌号码|字符型|200|||
        /// </summary> 
		public string resd_addr_housnum { get; set; }

        /// <summary>
        /// 30|户口地址- 邮政编码|字符型|30|||
        /// </summary> 
		public string resd_addr_poscode { get; set; }

        /// <summary>
        /// 31|户口地址|字符型|200|||
        /// </summary> 
		public string resd_addr { get; set; }

        /// <summary>
        /// 32|工作单位联系电话|字符型|20|||
        /// </summary> 
		public string empr_tel { get; set; }

        /// <summary>
        /// 33|工作单位- 邮政编码|字符型|6|||
        /// </summary> 
		public string empr_poscode { get; set; }

        /// <summary>
        /// 34|工作单位及地址|字符型|200|||
        /// </summary> 
		public string empr_addr { get; set; }

        /// <summary>
        /// 35|联系人电话|字符型|50|||
        /// </summary> 
		public string coner_tel { get; set; }

        /// <summary>
        /// 36|联系人姓名|字符型|50|||
        /// </summary> 
		public string coner_name { get; set; }

        /// <summary>
        /// 37|联系人地址|字符型|200|||
        /// </summary> 
		public string coner_addr { get; set; }

        /// <summary>
        /// 38|与联系人关系代码|字符型|20|Y||参照“与患者关系patn_rlts”
        /// </summary> 
		public string coner_rlts_code { get; set; }

        /// <summary>
        /// 39|入院途径名称|字符型|300|||
        /// </summary> 
		public string adm_way_name { get; set; }

        /// <summary>
        /// 40|入院途径代码|字符型|3|Y||
        /// </summary> 
		public string adm_way_code { get; set; }

        /// <summary>
        /// 41|治疗类别名称|字符型|300|||
        /// </summary> 
		public string trt_type_name { get; set; }

        /// <summary>
        /// 42|治疗类别|字符型|3|Y||
        /// </summary> 
		public string trt_type { get; set; }

        /// <summary>
        /// 43|入院科别|字符型|6|Y||dept
        /// </summary> 
		public string adm_caty { get; set; }

        /// <summary>
        /// 44|入院病房|字符型|50|||
        /// </summary> 
		public string adm_ward { get; set; }

        /// <summary>
        /// 45|入院日期|日期型||||
        /// </summary> 
		public string adm_date { get; set; }

        /// <summary>
        /// 46|出院日期|日期型||||
        /// </summary> 
		public string dscg_date { get; set; }

        /// <summary>
        /// 47|出院科别|字符型|6|Y||dept
        /// </summary> 
		public string dscg_caty { get; set; }

        /// <summary>
        /// 48|转科科别名称|字符型|50|||
        /// </summary> 
		public string Refldept_caty_name { get; set; }

        /// <summary>
        /// 49|出院病房|字符型|50|||
        /// </summary> 
		public string dscg_ward { get; set; }

        /// <summary>
        /// 50|住院天数|数值型|4,0|||
        /// </summary> 
		public string ipt_days { get; set; }

        /// <summary>
        /// 51|药物过敏标志|字符型|30|Y||
        /// </summary> 
		public string drug_dicm_flag { get; set; }

        /// <summary>
        /// 52|过敏药物名称|字符型|200|||
        /// </summary> 
		public string dicm_drug_name { get; set; }

        /// <summary>
        /// 53|死亡患者尸检标志|字符型|30|Y||
        /// </summary> 
		public string die_autp_flag { get; set; }

        /// <summary>
        /// 54|ABO血型代码|字符型|30|Y||
        /// </summary> 
		public string abo_code { get; set; }

        /// <summary>
        /// 55|ABO血型名称|字符型|30|||
        /// </summary> 
		public string abo_name { get; set; }

        /// <summary>
        /// 56|Rh血型代码|字符型|30|Y||
        /// </summary> 
		public string rh_code { get; set; }

        /// <summary>
        /// 57|RH血型|字符型|30|||
        /// </summary> 
		public string rh_name { get; set; }

        /// <summary>
        /// 58|死亡标志|字符型|3|Y||
        /// </summary> 
		public string die_flag { get; set; }

        /// <summary>
        /// 59|科主任姓名|字符型|50|||
        /// </summary> 
		public string deptdrt_name { get; set; }

        /// <summary>
        /// 60|主任( 副主任)医师姓名|字符型|50|||
        /// </summary> 
		public string chfdr_name { get; set; }

        /// <summary>
        /// 61|主治医生姓名|字符型|50|||
        /// </summary> 
		public string atddr_name { get; set; }

        /// <summary>
        /// 62|主诊医师姓名|字符型|50|||
        /// </summary> 
		public string chfpdr_name { get; set; }

        /// <summary>
        /// 63|住院医师姓名|字符型|50|||
        /// </summary> 
		public string ipt_dr_name { get; set; }

        /// <summary>
        /// 64|责任护士姓名|字符型|50|||
        /// </summary> 
		public string resp_nurs_name { get; set; }

        /// <summary>
        /// 65|进修医师姓名|字符型|50|||
        /// </summary> 
		public string train_dr_name { get; set; }

        /// <summary>
        /// 66|实习医师姓名|字符型|50|||
        /// </summary> 
		public string intn_dr_name { get; set; }

        /// <summary>
        /// 67|编码员姓名|字符型|50|||
        /// </summary> 
		public string codr_name { get; set; }

        /// <summary>
        /// 68|质控医师姓名|字符型|50|||
        /// </summary> 
		public string qltctrl_dr_name { get; set; }

        /// <summary>
        /// 69|质控护士姓名|字符型|50|||
        /// </summary> 
		public string qltctrl_nurs_name { get; set; }

        /// <summary>
        /// 70|病案质量名称|字符型|100|||
        /// </summary> 
		public string medcas_qlt_name { get; set; }

        /// <summary>
        /// 71|病案质量代码|字符型|30|||
        /// </summary> 
		public string medcas_qlt_code { get; set; }

        /// <summary>
        /// 72|质控日期|日期型||||
        /// </summary> 
		public string qltctrl_date { get; set; }

        /// <summary>
        /// 73|离院方式名称|字符型|200|||
        /// </summary> 
		public string dscg_way_name { get; set; }

        /// <summary>
        /// 74|离院方式|字符型|3|Y||
        /// </summary> 
		public string dscg_way { get; set; }

        /// <summary>
        /// 75|拟接收医疗机构代码|字符型|50|||
        /// </summary> 
		public string acp_medins_code { get; set; }

        /// <summary>
        /// 76|拟接收医疗机构名称|字符型|500|||
        /// </summary> 
		public string acp_medins_name { get; set; }

        /// <summary>
        /// 77|出院 31天内再住院计划标志|字符型|3|Y||
        /// </summary> 
		public string dscg_31days_rinp_flag { get; set; }

        /// <summary>
        /// 78|出院31天内再住院目的|字符型|200|||
        /// </summary> 
		public string dscg_31days_rinp_pup { get; set; }

        /// <summary>
        /// 79|损伤、中毒的外部原因|字符型|1000|||
        /// </summary> 
		public string damg_intx_ext_rea { get; set; }

        /// <summary>
        /// 80|损伤、中毒的外部原因疾病编码|字符型|30|||
        /// </summary> 
		public string damg_intx_ext_rea_disecode { get; set; }

        /// <summary>
        /// 81|颅脑损伤患者入院前昏迷时长|字符型|10|||格式：天数/小时数/分钟数例：1/13/24
        /// </summary> 
		public string brn_damg_bfadm_coma_dura { get; set; }

        /// <summary>
        /// 82|颅脑损伤患者入院后昏迷时长|字符型|10|||格式：天数/小时数/分钟数例：1/13/24
        /// </summary> 
        public string brn_damg_afadm_coma_dura { get; set; }

        /// <summary>
        /// 83|呼吸机使用时长|字符型|10|||格式：天
        /// </summary> 
        public string vent_used_dura { get; set; }

        /// <summary>
        /// 84|确诊日期|日期型||||
        /// </summary> 
        public string cnfm_date { get; set; }

        /// <summary>
        /// 85|患者疾病诊断对照|字符型|20|||
        /// </summary> 
        public string patn_dise_diag_crsp { get; set; }

        /// <summary>
        /// 86|住院患者疾病诊断对照代码|字符型|3|Y||
        /// </summary> 
        public string patn_dise_diag_crsp_code { get; set; }

        /// <summary>
        /// 87|住院患者诊断符合情况|字符型|20|||
        /// </summary> 
        public string ipt_patn_diag_inscp { get; set; }

        /// <summary>
        /// 88|住院患者诊断符合情况代码|字符型|1|Y||
        /// </summary> 
        public string ipt_patn_diag_inscp_code { get; set; }

        /// <summary>
        /// 89|出院治疗结果|字符型|4|||
        /// </summary> 
        public string dscg_trt_rslt { get; set; }

        /// <summary>
        /// 90|出院治疗结果代码|字符型|30|Y||
        /// </summary> 
        public string dscg_trt_rslt_code { get; set; }

        /// <summary>
        /// 91|医疗机构组织机构代码|字符型|50|||
        /// </summary> 
        public string medins_orgcode { get; set; }

        /// <summary>
        /// 92|年龄|数值型|4,1|||
        /// </summary> 
        public string age { get; set; }

        /// <summary>
        /// 93|过敏源|字符型|30|Y||
        /// </summary> 
        public string aise { get; set; }

        /// <summary>
        /// 94|研究生实习医师姓名|字符型|50|Y||
        /// </summary> 
        public string pote_intn_dr_name { get; set; }

        /// <summary>
        /// 95|乙肝表面抗原（HBsAg）|字符型|30|Y||
        /// </summary> 
        public string hbsag { get; set; }

        /// <summary>
        /// 96|丙型肝炎抗体（HCV-Ab）|字符型|30|Y||
        /// </summary> 
        public string hcv_ab { get; set; }

        /// <summary>
        /// 97|艾滋病毒抗体（hiv-ab）|字符型|30|Y||
        /// </summary> 
		public string hiv_ab { get; set; }

        /// <summary>
        /// 98|抢救次数|数值型|3,0|||
        /// </summary> 
		public string resc_cnt { get; set; }

        /// <summary>
        /// 99|抢救成功次数|数值型|3,0|||
        /// </summary> 
        public string resc_succ_cnt { get; set; }

        /// <summary>
        /// 100|手术、治疗、检查、诊断为本院第一例|字符型|30|Y||
        /// </summary> 
        public string hosp_dise_fsttime { get; set; }

        /// <summary>
        /// 101|医保基金付费方式名称|字符型|20|||
        /// </summary> 
        public string hif_pay_way_name { get; set; }

        /// <summary>
        /// 102|医保基金付费方式代码|字符型|2|Y||
        /// </summary> 
        public string hif_pay_way_code { get; set; }

        /// <summary>
        /// 103|医疗费用支付方式名称|字符型|200|||
        /// </summary> 
        public string med_fee_paymtd_name { get; set; }

        /// <summary>
        /// 104|医疗费用支付方式代码|字符型|3|Y||
        /// </summary> 
        public string medfee_paymtd_code { get; set; }

        /// <summary>
        /// 105|自付金额|数值型|16,2|||
        /// </summary> 
        public string selfpay_amt { get; set; }

        /// <summary>
        /// 106|医疗费总额|数值型|16,2||Y|
        /// </summary> 
        public string medfee_sumamt { get; set; }

        /// <summary>
        /// 107|一般医疗服务费|数值型|16,2|||
        /// </summary> 
        public string ordn_med_servfee { get; set; }

        /// <summary>
        /// 108|一般治疗操作费|数值型|16,2|||
        /// </summary> 
        public string ordn_trt_oprt_fee { get; set; }

        /// <summary>
        /// 109|护理费|数值型|16,2|||
        /// </summary> 
        public string nurs_fee { get; set; }

        /// <summary>
        /// 110|综合医疗服务类其他费用|数值型|16,2|||
        /// </summary> 
        public string com_med_serv_oth_fee { get; set; }

        /// <summary>
        /// 111|病理诊断费|数值型|16,2|||
        /// </summary> 
        public string palg_diag_fee { get; set; }

        /// <summary>
        /// 112|实验室诊断费|数值型|16,2|||
        /// </summary> 
        public string lab_diag_fee { get; set; }

        /// <summary>
        /// 113|影像学诊断费|数值型|16,2|||
        /// </summary> 
        public string rdhy_diag_fee { get; set; }

        /// <summary>
        /// 114|临床诊断项目费|数值型|16,2|||
        /// </summary> 
        public string clnc_dise_fee { get; set; }

        /// <summary>
        /// 115|非手术治疗项目费|数值型|16,2|||
        /// </summary> 
        public string nsrgtrt_item_fee { get; set; }

        /// <summary>
        /// 116|临床物理治疗费|数值型|16,2|||
        /// </summary> 
        public string clnc_phys_trt_fee { get; set; }

        /// <summary>
        /// 117|手术治疗费|数值型|16,2|||
        /// </summary> 
        public string rgtrt_trt_fee { get; set; }

        /// <summary>
        /// 118|麻醉费|数值型|16,2|||
        /// </summary> 
        public string anst_fee { get; set; }

        /// <summary>
        /// 119|手术费|数值型|16,2|||
        /// </summary> 
        public string rgtrt_fee { get; set; }

        /// <summary>
        /// 120|康复费|数值型|16,2|||
        /// </summary> 
        public string rhab_fee { get; set; }

        /// <summary>
        /// 121|中医治疗费|数值型|16,2|||
        /// </summary> 
        public string tcm_trt_fee { get; set; }

        /// <summary>
        /// 122|西药费|数值型|16,2|||
        /// </summary> 
        public string wm_fee { get; set; }

        /// <summary>
        /// 123|抗菌药物费用|数值型|16,2|||
        /// </summary> 
        public string abtl_medn_fee { get; set; }

        /// <summary>
        /// 124|中成药费|数值型|16,2|||
        /// </summary> 
        public string tcmpat_fee { get; set; }

        /// <summary>
        /// 125|中药饮片费|数值型|16,2|||
        /// </summary> 
        public string tcmherb_fee { get; set; }

        /// <summary>
        /// 126|血费|数值型|16,2|||
        /// </summary> 
        public string blo_fee { get; set; }

        /// <summary>
        /// 127|白蛋白类制品费|数值型|16,2|||
        /// </summary> 
        public string albu_fee { get; set; }

        /// <summary>
        /// 128|球蛋白类制品费|数值型|16,2|||
        /// </summary> 
        public string glon_fee { get; set; }

        /// <summary>
        /// 129|凝血因子类制品费|数值型|16,2|||
        /// </summary> 
        public string clotfac_fee { get; set; }

        /// <summary>
        /// 130|细胞因子类制品费|数值型|16,2|||
        /// </summary> 
        public string cyki_fee { get; set; }

        /// <summary>
        /// 131|检查用一次性医用材料费|数值型|16,2|||
        /// </summary> 
        public string exam_dspo_matl_fee { get; set; }

        /// <summary>
        /// 132|治疗用一次性医用材料费|数值型|16,2|||
        /// </summary> 
        public string trt_dspo_matl_fee { get; set; }

        /// <summary>
        /// 133|手术用一次性医用材料费|数值型|16,2|||
        /// </summary> 
        public string oprn_dspo_matl_fee { get; set; }

        /// <summary>
        /// 134|其他费|数值型|16,2|||
        /// </summary> 
        public string oth_fee { get; set; }

        /// <summary>
        /// 135|有效标志|字符型|3|Y  |Y|
        /// </summary> 
        public string vali_flag { get; set; }

        /// <summary>
        /// 136|定点医药机构编号|字符型|3||Y|
        /// </summary> 
        public string fixmedins_code { get; set; }

    }

    public class diseinfo_4401
    {

        /// <summary>
        /// 1|病理号|字符型|30|||
        /// </summary> 
        public string palg_no { get; set; }

        /// <summary>
        /// 2|住院患者疾病诊断类型代码|字符型|30|Y||
        /// </summary> 
		public string ipt_patn_disediag_type_code { get; set; }

        /// <summary>
        /// 3|疾病诊断类型|字符型|100|||
        /// </summary> 
		public string disediag_type { get; set; }

        /// <summary>
        /// 4|主诊断标志|字符型|3|Y||
        /// </summary> 
		public string maindiag_flag { get; set; }

        /// <summary>
        /// 5|诊断代码|字符型|20|Y||
        /// </summary> 
		public string diag_code { get; set; }

        /// <summary>
        /// 6|诊断名称|字符型|100|||
        /// </summary> 
		public string diag_name { get; set; }

        /// <summary>
        /// 7|院内诊断代码|字符型|20|||
        /// </summary> 
		public string inhosp_diag_code { get; set; }

        /// <summary>
        /// 8|院内诊断名称|字符型|100|||
        /// </summary> 
		public string inhosp_diag_name { get; set; }

        /// <summary>
        /// 9|入院疾病病情名称|字符型|300|||
        /// </summary> 
		public string adm_dise_cond_name { get; set; }

        /// <summary>
        /// 10|入院疾病病情代码|字符型|20|Y||
        /// </summary> 
		public string adm_dise_cond_code { get; set; }

        /// <summary>
        /// 11|入院病情|字符型|500|||
        /// </summary> 
		public string adm_cond { get; set; }

        /// <summary>
        /// 12|入院时病情代码|字符型|3|Y||
        /// </summary> 
		public string adm_cond_code { get; set; }

        /// <summary>
        /// 13|最高诊断依据|字符型|30|Y||
        /// </summary> 
		public string high_diag_evid { get; set; }

        /// <summary>
        /// 14|分化程度|字符型|16|||
        /// </summary> 
		public string bkup_deg { get; set; }

        /// <summary>
        /// 15|分化程度代码|字符型|30|Y||
        /// </summary> 
		public string bkup_deg_code { get; set; }

        /// <summary>
        /// 16|有效标志|字符型|3|Y  |Y|
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 17|住院病案首页流水号|字符型|30||Y|主键
        /// </summary> 
		public string ipt_medcas_hmpg_sn { get; set; }

        /// <summary>
        /// 18|就医流水号|字符型|30||Y|格式：定点医药机构编号+院内唯一流水号
        /// </summary> 
		public string mdtrt_sn { get; set; }
    }

    public class oprninfo_4401
    {

        /// <summary>
        /// 1|手术操作日期|日期型||||
        /// </summary> 
        public string oprn_oprt_date { get; set; }

        /// <summary>
        /// 2|手术操作名称|字符型|500|||
        /// </summary> 
		public string oprn_oprt_name { get; set; }

        /// <summary>
        /// 3|手术操作代码|字符型|30|||
        /// </summary> 
		public string oprn_oprt_code { get; set; }

        /// <summary>
        /// 4|手术操作序列号|字符型|16|||
        /// </summary> 
		public string oprn_oprt_sn { get; set; }

        /// <summary>
        /// 5|手术级别代码|字符型|50|||
        /// </summary> 
		public string oprn_lv_code { get; set; }

        /// <summary>
        /// 6|手术级别名称|字符型|300|||
        /// </summary> 
		public string oprn_lv_name { get; set; }

        /// <summary>
        /// 7|手术者姓名|字符型|50|||
        /// </summary> 
		public string oper_name { get; set; }

        /// <summary>
        /// 8|助手Ⅰ姓名|字符型|50|||
        /// </summary> 
		public string asit_1_name { get; set; }

        /// <summary>
        /// 9|助手Ⅱ姓名|字符型|50|||
        /// </summary> 
		public string asit_name2 { get; set; }

        /// <summary>
        /// 10|手术切口愈合等级|字符型|30|Y||
        /// </summary> 
		public string sinc_heal_lv { get; set; }

        /// <summary>
        /// 11|手术切口愈合等级代码|字符型|5|Y||
        /// </summary> 
		public string sinc_heal_lv_code { get; set; }

        /// <summary>
        /// 12|麻醉-方法名称|字符型|50|||
        /// </summary> 
		public string anst_mtd_name { get; set; }

        /// <summary>
        /// 13|麻醉-方法代码|字符型|5|Y||
        /// </summary> 
		public string anst_mtd_code { get; set; }

        /// <summary>
        /// 14|麻醉医师姓名|字符型|50|||
        /// </summary> 
		public string anst_dr_name { get; set; }

        /// <summary>
        /// 15|手术操作部位|字符型|10|||
        /// </summary> 
		public string oprn_oper_part { get; set; }

        /// <summary>
        /// 16|手术操作部位代码|字符型|30|Y||
        /// </summary> 
		public string oprn_oper_part_code { get; set; }

        /// <summary>
        /// 17|手术持续时间|字符型|3|||分钟
        /// </summary> 
		public string oprn_con_time { get; set; }

        /// <summary>
        /// 18|麻醉分级名称|字符型|50|||
        /// </summary> 
		public string anst_lv_name { get; set; }

        /// <summary>
        /// 19|麻醉分级代码|字符型|30|Y||
        /// </summary> 
		public string anst_lv_code { get; set; }

        /// <summary>
        /// 20|手术患者类型|字符型|100|||
        /// </summary> 
		public string oprn_patn_type { get; set; }

        /// <summary>
        /// 21|手术患者类型代码|字符型|30|Y||
        /// </summary> 
		public string oprn_patn_type_code { get; set; }

        /// <summary>
        /// 22|主要手术标志|字符型|30|Y||
        /// </summary> 
		public string main_oprn_flag { get; set; }

        /// <summary>
        /// 23|麻醉ASA分级代码|字符型|50|Y||参照国家卫生健康委下发的麻醉ASA分级代码
        /// </summary> 
		public string anst_asa_lv_code { get; set; }

        /// <summary>
        /// 24|麻醉ASA分级名称|字符型|100|||
        /// </summary> 
		public string anst_asa_lv_name { get; set; }

        /// <summary>
        /// 25|麻醉药物代码|字符型|50|Y||参照国家卫生健康委下发的麻醉药物代码
        /// </summary> 
		public string anst_medn_code { get; set; }

        /// <summary>
        /// 26|麻醉药物名称|字符型|100|||
        /// </summary> 
		public string anst_medn_name { get; set; }

        /// <summary>
        /// 27|麻醉药物剂量|字符型|20|||
        /// </summary> 
		public string anst_medn_dos { get; set; }

        /// <summary>
        /// 28|计量单位|字符型|10|||
        /// </summary> 
		public string unt { get; set; }

        /// <summary>
        /// 29|麻醉开始时间|日期型||||
        /// </summary> 
		public string anst_begntime { get; set; }

        /// <summary>
        /// 30|麻醉结束时间|日期型||||
        /// </summary> 
		public string anst_endtime { get; set; }

        /// <summary>
        /// 31|麻醉合并症代码|字符型|30|Y||参照国家卫生健康委下发的麻醉合并症代码
        /// </summary> 
		public string anst_copn_code { get; set; }

        /// <summary>
        /// 32|麻醉合并症名称|字符型|100|||
        /// </summary> 
		public string anst_copn_name { get; set; }

        /// <summary>
        /// 33|麻醉合并症描述|字符型|1000|||
        /// </summary> 
		public string anst_copn_dscr { get; set; }

        /// <summary>
        /// 34|复苏室开始时间|日期型||||
        /// </summary> 
		public string pacu_begntime { get; set; }

        /// <summary>
        /// 35|复苏室结束时间|日期型||||
        /// </summary> 
		public string pacu_endtime { get; set; }

        /// <summary>
        /// 36|取消手术标志|字符型|3|Y||
        /// </summary> 
		public string canc_oprn_flag { get; set; }

        /// <summary>
        /// 37|有效标志|字符型|3|Y  |Y|
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 38|住院病案首页流水号|字符型|30||Y|主键
        /// </summary> 
		public string ipt_medcas_hmpg_sn { get; set; }

        /// <summary>
        /// 39|就医流水号|字符型|30||Y|格式：定点医药机构编号+院内唯一流水号
        /// </summary> 
		public string mdtrt_sn { get; set; }
    }

    public class icuinfo_4401
    {

        /// <summary>
        /// 1|重症监护室代码|字符型|10|||院内重症监护室代码
        /// </summary> 
        public string icu_codeid { get; set; }

        /// <summary>
        /// 2|进入监护室时间|日期时间型||||
        /// </summary> 
		public string inpool_icu_time { get; set; }

        /// <summary>
        /// 3|退出监护室时间|日期时间型||||
        /// </summary> 
		public string out_icu_time { get; set; }

        /// <summary>
        /// 4|医疗机构组织机构代码|字符型|50|||参照医院组织机构代码
        /// </summary> 
		public string medins_orgcode { get; set; }

        /// <summary>
        /// 5|护理等级代码|字符型|30|||
        /// </summary> 
		public string nurscare_lv_code { get; set; }

        /// <summary>
        /// 6|护理等级名称|字符型|10|||
        /// </summary> 
		public string nurscare_lv_name { get; set; }

        /// <summary>
        /// 7|护理天数|数值型|3,0|||
        /// </summary> 
		public string nurscare_days { get; set; }

        /// <summary>
        /// 8|重返重症监护室标志|字符型|30|||
        /// </summary> 
		public string back_icu { get; set; }

        /// <summary>
        /// 9|有效标志|字符型|3|Y  |Y|
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 10|住院病案首页流水号|字符型|30||Y|主键
        /// </summary> 
		public string ipt_medcas_hmpg_sn { get; set; }

        /// <summary>
        /// 11|就医流水号|字符型|30||Y|格式：定点医药机构编号+院内唯一流水号
        /// </summary> 
		public string mdtrt_sn { get; set; }
    }
}
