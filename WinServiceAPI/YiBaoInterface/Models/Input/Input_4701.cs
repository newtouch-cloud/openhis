using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_4701 : InputBase
    {
        /// <summary>
        ///  187 输入-入院信息（节点标识：adminfo）
        /// </summary>
        public adminfo_4701 adminfo { get; set; }
        /// <summary>
        /// 表 188 输入-诊断信息（节点标识：diseinfo
        /// </summary>
        public List<diseinfo_4701> diseinfo { get; set; }
        /// <summary>
        /// 表 189 输入-病程记录（节点标识：coursrinfo）
        /// </summary>
        public List<coursrinfo_4701> coursrinfo { get; set; }
        /// <summary>
        /// 表 190 输入-手术记录（节点标识：oprninfo）
        /// </summary>
        public List<oprninfo_4701> oprninfo { get; set; }
        /// <summary>
        /// 表 191 输入-病情抢救记录（节点标识：rescinfo）
        /// </summary>
        public List<rescinfo_4701> rescinfo { get; set; }
        /// <summary>
        /// 表 192 输入-死亡记录（节点标识：dieinfo）
        /// </summary>
        public List<dieinfo_4701> dieinfo { get; set; }
        /// <summary>
        /// 193 输入-出院小结（节点标识：dscginfo）
        /// </summary>
        public List<dscginfo_4701> dscginfo { get; set; }
    }
    public class adminfo_4701
    {
        /// <summary>
        /// 1|就医流水号|字符型|30||Y|院内唯一号
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
        /// 4|住院号|字符型|30||Y|
        /// </summary> 
		public string mdtrtsn { get; set; }

        /// <summary>
        /// 5|姓名|字符型|50||Y|
        /// </summary> 
		public string name { get; set; }

        /// <summary>
        /// 6|性别|字符型|6|Y|Y|
        /// </summary> 
		public string gend { get; set; }

        /// <summary>
        /// 7|年龄|数值型|4,1|||
        /// </summary> 
		public string age { get; set; }

        /// <summary>
        /// 8|入院记录流水号|字符型|18||Y|
        /// </summary> 
		public string adm_rec_no { get; set; }

        /// <summary>
        /// 9|病区名称|字符型|50||Y|
        /// </summary> 
		public string wardarea_name { get; set; }

        /// <summary>
        /// 10|科室代码|字符型|30|Y|Y|参照科室代码（de pt）
        /// </summary> 
		public string dept_code { get; set; }

        /// <summary>
        /// 11|科室名称|字符型|50||Y|
        /// </summary> 
		public string dept_name { get; set; }

        /// <summary>
        /// 12|病床号|字符型|10||Y|
        /// </summary> 
		public string bedno { get; set; }

        /// <summary>
        /// 13|入院时间|日期时间型|||Y|
        /// </summary> 
		public string adm_time { get; set; }

        /// <summary>
        /// 14|病史陈述者姓名|字符型|50||Y|
        /// </summary> 
		public string illhis_stte_name { get; set; }

        /// <summary>
        /// 15|陈述者与患者关系代码|字符型|2||Y|
        /// </summary> 
		public string illhis_stte_rltl { get; set; }

        /// <summary>
        /// 16|陈述内容是否可靠标识|字符型|1||Y|
        /// </summary> 
		public string stte_rele { get; set; }

        /// <summary>
        /// 17|主诉|字符型|100||Y|
        /// </summary> 
		public string chfcomp { get; set; }

        /// <summary>
        /// 18|现病史|字符型|100||Y|
        /// </summary> 
		public string dise_now { get; set; }

        /// <summary>
        /// 19|健康状况|字符型|3||Y|
        /// </summary> 
		public string hlcon { get; set; }

        /// <summary>
        /// 20|疾病史（含外伤）|字符型|100||Y|
        /// </summary> 
		public string dise_his { get; set; }

        /// <summary>
        /// 21|患者传染性标志|字符型|1||Y|
        /// </summary> 
		public string ifet { get; set; }

        /// <summary>
        /// 22|传染病史|字符型|1000||Y|
        /// </summary> 
		public string ifet_his { get; set; }

        /// <summary>
        /// 23|预防接种史|字符型|1000||Y|
        /// </summary> 
		public string prev_vcnt { get; set; }

        /// <summary>
        /// 24|手术史|字符型|1000||Y|
        /// </summary> 
		public string oprn_his { get; set; }

        /// <summary>
        /// 25|输血史|字符型|1000||Y|
        /// </summary> 
		public string bld_his { get; set; }

        /// <summary>
        /// 26|过敏史|字符型|1000||Y|
        /// </summary> 
		public string algs_his { get; set; }

        /// <summary>
        /// 27|个人史|字符型|1000||Y|
        /// </summary> 
		public string psn_his { get; set; }

        /// <summary>
        /// 28|婚育史|字符型|1000||Y|
        /// </summary> 
		public string mrg_his { get; set; }

        /// <summary>
        /// 29|月经史|字符型|1000||Y|
        /// </summary> 
		public string mena_his { get; set; }

        /// <summary>
        /// 30|家族史|字符型|1000||Y|
        /// </summary> 
		public string fmhis { get; set; }

        /// <summary>
        /// 31|体格检查--体温（℃）|数值型|5,0||Y|
        /// </summary> 
		public string physexm_tprt { get; set; }

        /// <summary>
        /// 32|体格检查 -- 脉率（次 /mi数字）|数值型|3||Y|
        /// </summary> 
		public string physexm_pule { get; set; }

        /// <summary>
        /// 33|体格检查--呼吸频率|字符型|3||Y|
        /// </summary> 
		public string physexm_vent_frqu { get; set; }

        /// <summary>
        /// 34|体格检查 -- 收缩压（mmHg）|字符型|3||Y|
        /// </summary> 
		public string physexm_systolic_pre { get; set; }

        /// <summary>
        /// 35|体格检查 -- 舒张压（mmHg）|字符型|3||Y|
        /// </summary> 
		public string physexm_dstl_pre { get; set; }

        /// <summary>
        /// 36|体格检查--身高（cm）|数值型|6，1||Y|
        /// </summary> 
		public string physexm_height { get; set; }

        /// <summary>
        /// 37|体格检查--体重（kg）|数值型|7，2||Y|
        /// </summary> 
		public string physexm_wt { get; set; }

        /// <summary>
        /// 38|体格检查 -- 一般状况 检查结果|字符型|100||Y|
        /// </summary> 
		public string physexm_ordn_stas { get; set; }

        /// <summary>
        /// 39|体格检查 -- 皮肤和黏膜检查结果|字符型|100||Y|
        /// </summary> 
		public string physexm_skin_musl { get; set; }

        /// <summary>
        /// 40|体格检查 -- 全身浅表淋巴结检查结果|字符型|100||Y|
        /// </summary> 
		public string physexm_spef_lymph { get; set; }

        /// <summary>
        /// 41|体格检查 -- 头部及其器官检查结果|字符型|100||Y|
        /// </summary> 
		public string physexm_head { get; set; }

        /// <summary>
        /// 42|体格检查 -- 颈部检查结果|字符型|100||Y|
        /// </summary> 
		public string physexm_neck { get; set; }

        /// <summary>
        /// 43|体格检查 -- 胸部检查结果|字符型|100||Y|
        /// </summary> 
		public string physexm_chst { get; set; }

        /// <summary>
        /// 44|体格检查 -- 腹部检查结果|字符型|100||Y|
        /// </summary> 
		public string physexm_abd { get; set; }

        /// <summary>
        /// 45|体格检查 -- 肛门指诊检查结果描述|字符型|100||Y|
        /// </summary> 
		public string physexm_finger_exam { get; set; }

        /// <summary>
        /// 46|体格检查 -- 外生殖器检查结果|字符型|100||Y|
        /// </summary> 
		public string physexm_genital_area { get; set; }

        /// <summary>
        /// 47|体格检查 -- 脊柱检查结果|字符型|100||Y|
        /// </summary> 
		public string physexm_spin { get; set; }

        /// <summary>
        /// 48|体格检查 -- 四肢检查结果|字符型|100||Y|
        /// </summary> 
		public string physexm_all_fors { get; set; }

        /// <summary>
        /// 49|体格检查 -- 神经系统检查结果|字符型|100||Y|
        /// </summary> 
		public string nersys { get; set; }

        /// <summary>
        /// 50|专科情况|字符型|100||Y|
        /// </summary> 
		public string spcy_info { get; set; }

        /// <summary>
        /// 51|辅助检查结果|字符型|100||Y|
        /// </summary> 
		public string asst_exam_rslt { get; set; }

        /// <summary>
        /// 52|中医“四诊”观察结果描述|字符型|100||N|
        /// </summary> 
		public string tcm4d_rslt { get; set; }

        /// <summary>
        /// 53|辨证分型代码|字符型|7||N|
        /// </summary> 
		public string syddclft { get; set; }

        /// <summary>
        /// 54|辩证分型名称|字符型|50||N|
        /// </summary> 
		public string syddclft_name { get; set; }

        /// <summary>
        /// 55|治则治法|字符型|100||N|
        /// </summary> 
		public string prnp_trt { get; set; }

        /// <summary>
        /// 56|接诊医生编号|字符型|30||Y|
        /// </summary> 
		public string rec_doc_code { get; set; }

        /// <summary>
        /// 57|接诊医生姓名|字符型|50||Y|
        /// </summary> 
		public string rec_doc_name { get; set; }

        /// <summary>
        /// 58|住院医师编号|字符型|30||Y|
        /// </summary> 
		public string ipdr_code { get; set; }

        /// <summary>
        /// 59|住院医师姓名|字符型|50||Y|
        /// </summary> 
		public string ipdr_name { get; set; }

        /// <summary>
        /// 60|主任医师编号|字符型|30||Y|
        /// </summary> 
		public string chfdr_code { get; set; }

        /// <summary>
        /// 61|主任医师姓名|字符型|50||Y|
        /// </summary> 
		public string chfdr_name { get; set; }

        /// <summary>
        /// 62|主诊医师代码|字符型|30||Y|
        /// </summary> 
		public string chfpdr_code { get; set; }

        /// <summary>
        /// 63|主诊医师姓名|字符型|50||Y|
        /// </summary> 
		public string chfpdr_name { get; set; }

        /// <summary>
        /// 64|主要症状|字符型|50||Y|
        /// </summary> 
		public string main_symp { get; set; }

        /// <summary>
        /// 65|入院原因|字符型|1000||Y|
        /// </summary> 
		public string adm_rea { get; set; }

        /// <summary>
        /// 66|入院途径|字符型|1||Y|
        /// </summary> 
		public string adm_way { get; set; }

        /// <summary>
        /// 67|评分值(Apgar)|字符型|2||Y|
        /// </summary> 
		public string apgr { get; set; }

        /// <summary>
        /// 68|饮食情况|字符型|1||Y|
        /// </summary> 
		public string diet_info { get; set; }

        /// <summary>
        /// 69|发育程度|字符型|1||Y|
        /// </summary> 
		public string growth_deg { get; set; }

        /// <summary>
        /// 70|精神状态正常标志|字符型|1||Y|
        /// </summary> 
		public string mtl_stas_norm { get; set; }

        /// <summary>
        /// 71|睡眠状况|字符型|1000||Y|
        /// </summary> 
		public string slep_info { get; set; }

        /// <summary>
        /// 72|特殊情况|字符型|1000||Y|
        /// </summary> 
		public string sp_info { get; set; }

        /// <summary>
        /// 73|心理状态|字符型|1||Y|
        /// </summary> 
		public string mind_info { get; set; }

        /// <summary>
        /// 74|营养状态|字符型|1||Y|
        /// </summary> 
		public string nurt { get; set; }

        /// <summary>
        /// 75|自理能力|字符型|1||Y|
        /// </summary> 
		public string self_ablt { get; set; }

        /// <summary>
        /// 76|护理观察项目名称|字符型|30||Y|
        /// </summary> 
		public string nurscare_obsv_item_name { get; set; }

        /// <summary>
        /// 77|护理观察结果|字符型|1000||Y|
        /// </summary> 
		public string nurscare_obsv_rslt { get; set; }

        /// <summary>
        /// 78|吸烟标志|字符型|1||Y|
        /// </summary> 
		public string smoke { get; set; }

        /// <summary>
        /// 79|停止吸烟天数|数值型|5||Y|
        /// </summary> 
		public string stop_smok_days { get; set; }

        /// <summary>
        /// 80|吸烟状况|字符型|1||Y|
        /// </summary> 
		public string smok_info { get; set; }

        /// <summary>
        /// 81|日吸烟量（支）|字符型|3||Y|
        /// </summary> 
		public string smok_day { get; set; }

        /// <summary>
        /// 82|饮酒标志|字符型|1||Y|
        /// </summary> 
		public string drnk { get; set; }

        /// <summary>
        /// 83|饮酒频率|字符型|1||Y|
        /// </summary> 
		public string drnk_frqu { get; set; }

        /// <summary>
        /// 84|日饮酒量（mL）|数值型|3||Y|
        /// </summary> 
		public string drnk_day { get; set; }

        /// <summary>
        /// 85|评估日期时间|数值型|||Y|
        /// </summary> 
		public string eval_time { get; set; }

        /// <summary>
        /// 86|责任护士姓名|字符型|50||Y|
        /// </summary> 
		public string resp_nurs_name { get; set; }

        /// <summary>
        /// 87|有效标志|字符型|3|Y  |Y|
        /// </summary> 
		public string vali_flag { get; set; }
    }
    public class diseinfo_4701
    {
        /// <summary>
        /// 1|出入院诊断类别|字符型|3|Y|Y|
        /// </summary> 
        public string inout_diag_type { get; set; }

        /// <summary>
        /// 2|主诊断标志|字符型|3||Y|
        /// </summary> 
		public string maindiag_flag { get; set; }

        /// <summary>
        /// 3|诊断序列号|数值型|2||Y|
        /// </summary> 
		public string diag_seq { get; set; }

        /// <summary>
        /// 4|诊断时间|日期时间型|||Y|
        /// </summary> 
		public string diag_time { get; set; }

        /// <summary>
        /// 5|西医诊断编码|字符型|20||Y|
        /// </summary> 
		public string wm_diag_code { get; set; }

        /// <summary>
        /// 6|西医诊断名称|字符型|100||Y|
        /// </summary> 
		public string wm_diag_name { get; set; }

        /// <summary>
        /// 7|中医病名代码|字符型|9||N|
        /// </summary> 
		public string tcm_dise_code { get; set; }

        /// <summary>
        /// 8|中医病名|字符型|100||N|
        /// </summary> 
		public string tcm_dise_name { get; set; }

        /// <summary>
        /// 9|中医证候代码|字符型|9||N|
        /// </summary> 
		public string tcmsymp_code { get; set; }

        /// <summary>
        /// 10|中医证候|字符型|100||N|
        /// </summary> 
		public string tcmsymp { get; set; }

        /// <summary>
        /// 11|有效标志|字符型|3|Y  |Y|
        /// </summary> 
		public string vali_flag { get; set; }
    }
    public class coursrinfo_4701
    {
        /// <summary>
        /// 1|科室代码|字符型|30||Y|
        /// </summary> 
        public string dept_code { get; set; }

        /// <summary>
        /// 2|科室名称|字符型|50||Y|
        /// </summary> 
		public string dept_name { get; set; }

        /// <summary>
        /// 3|病区名称|字符型|50||Y|
        /// </summary> 
		public string wardarea_name { get; set; }

        /// <summary>
        /// 4|病床号|字符型|10||Y|
        /// </summary> 
		public string bedno { get; set; }

        /// <summary>
        /// 5|记录日期时间|日期型||||
        /// </summary> 
		public string rcd_time { get; set; }

        /// <summary>
        /// 6|主诉|字符型|100||Y|
        /// </summary> 
		public string chfcomp { get; set; }

        /// <summary>
        /// 7|病例特点|字符型|200||Y|
        /// </summary> 
		public string cas_ftur { get; set; }

        /// <summary>
        /// 8|中医“四诊”观察结果|字符型|1000||N|
        /// </summary> 
		public string tcm4d_rslt { get; set; }

        /// <summary>
        /// 9|诊断依据|字符型|100||Y|
        /// </summary> 
		public string dise_evid { get; set; }

        /// <summary>
        /// 10|初步诊断-西医诊断编码|字符型|20||Y|
        /// </summary> 
		public string prel_wm_diag_code { get; set; }

        /// <summary>
        /// 11|初步诊断-西医诊断名称|字符型|100||Y|
        /// </summary> 
		public string prel_wm_dise_name { get; set; }

        /// <summary>
        /// 12|初步诊断-中医病名代码|字符型|20||N|
        /// </summary> 
		public string prel_tcm_diag_code { get; set; }

        /// <summary>
        /// 13|初步诊断-中医病名|字符型|200||N|
        /// </summary> 
        public string prel_tcm_dise_name { get; set; }

        /// <summary>
        /// 14|初步诊断-中医证候代码|字符型|9||N|
        /// </summary> 
        public string prel_tcmsymp_code { get; set; }

        /// <summary>
        /// 15|初步诊断-中医证候|字符型|200||N|
        /// </summary> 
        public string prel_tcmsymp { get; set; }

        /// <summary>
        /// 16|鉴别诊断-西医诊断编码|字符型|20||Y|
        /// </summary> 
        public string finl_wm_diag_code { get; set; }

        /// <summary>
        /// 17|鉴别诊断-西医诊断名称|字符型|100||Y|
        /// </summary> 
        public string finl_wm_diag_name { get; set; }

        /// <summary>
        /// 18|鉴别诊断-中医病名代码|字符型|9||N|
        /// </summary> 
        public string finl_tcm_dise_code { get; set; }

        /// <summary>
        /// 19|鉴别诊断-中医病名|字符型|200||N|
        /// </summary> 
		public string finl_tcm_dise_name { get; set; }

        /// <summary>
        /// 20|鉴别诊断-中医证候代码|字符型|9||N|
        /// </summary> 
		public string finl_tcmsymp_code { get; set; }

        /// <summary>
        /// 21|鉴别诊断-中医证候|字符型|200||N|
        /// </summary> 
        public string finl_tcmsymp { get; set; }

        /// <summary>
        /// 22|诊疗计划|字符型|2000||Y|
        /// </summary> 
        public string dise_plan { get; set; }

        /// <summary>
        /// 23|治则治法|字符型|100||N|
        /// </summary> 
        public string prnp_trt { get; set; }

        /// <summary>
        /// 24|住院医师编号|字符型|30||Y|
        /// </summary> 
        public string ipdr_code { get; set; }

        /// <summary>
        /// 25|住院医师姓名|字符型|50||Y|
        /// </summary> 
        public string ipdr_name { get; set; }

        /// <summary>
        /// 26|上级医师姓名|字符型|50||Y|
        /// </summary> 
        public string prnt_doc_name { get; set; }

        /// <summary>
        /// 27|有效标志|字符型|3|Y  |Y|
        /// </summary> 
        public string vali_flag { get; set; }
    }
    public class oprninfo_4701
    {
        /// <summary>
        /// 1|手术申请单号|字符型|30||Y|
        /// </summary> 
		public string oprn_appy_id { get; set; }

        /// <summary>
        /// 2|手术序列号|字符型|16|||
        /// </summary> 
		public string oprn_seq { get; set; }

        /// <summary>
        /// 3|ABO血型代码|字符型|2|Y||
        /// </summary> 
		public string blotype_abo { get; set; }

        /// <summary>
        /// 4|手术日期|日期型|||Y|
        /// </summary> 
		public string oprn_time { get; set; }

        /// <summary>
        /// 5|手术分类代码|字符型|1||Y|参照国家卫生健康委发布最新编码标准  
        /// </summary> 
		public string oprn_type_code { get; set; }

        /// <summary>
        /// 6|手术分类名称|字符型|20|||
        /// </summary> 
		public string oprn_type_name { get; set; }

        /// <summary>
        /// 7|术前诊断代码|字符型|100|||
        /// </summary> 
		public string bfpn_diag_code { get; set; }

        /// <summary>
        /// 8|术前诊断名称|字符型|500|||
        /// </summary> 
		public string bfpn_oprn_diag_name { get; set; }

        /// <summary>
        /// 9|术前是否发生院内感染|字符型|10|Y||
        /// </summary> 
		public string bfpn_inhosp_ifet { get; set; }

        /// <summary>
        /// 10|术后诊断代码|字符型|100|||
        /// </summary> 
		public string afpn_diag_code { get; set; }

        /// <summary>
        /// 11|术后诊断名称|字符型|500|||
        /// </summary> 
		public string afpn_diag_name { get; set; }

        /// <summary>
        /// 12|手术切口愈合等级代码|字符型|5|Y||
        /// </summary> 
		public string sinc_heal_lv { get; set; }

        /// <summary>
        /// 13|手术切口愈合等级|字符型|30|||
        /// </summary> 
		public string sinc_heal_lv_code { get; set; }

        /// <summary>
        /// 14|是否重返手术（明确定义）|字符型|2|||
        /// </summary> 
		public string back_oprn { get; set; }

        /// <summary>
        /// 15|是否择期|字符型|2|||
        /// </summary> 
		public string selv { get; set; }

        /// <summary>
        /// 16|是否预防使用抗菌药物|字符型|2|||
        /// </summary> 
		public string prev_abtl_medn { get; set; }

        /// <summary>
        /// 17|预防使用抗菌药物天数|字符型|10|||
        /// </summary> 
		public string abtl_medn_days { get; set; }

        /// <summary>
        /// 18|手术操作代码|字符型|30||Y|
        /// </summary> 
		public string oprn_oprt_code { get; set; }

        /// <summary>
        /// 19|手术操作名称|字符型|500||Y|
        /// </summary> 
		public string oprn_oprt_name { get; set; }

        /// <summary>
        /// 20|手术级别代码|字符型|10|Y||
        /// </summary> 
		public string oprn_lv_code { get; set; }

        /// <summary>
        /// 21|手术级别名称|字符型|300|||
        /// </summary> 
		public string oprn_lv_name { get; set; }

        /// <summary>
        /// 22|麻醉-方法代码|字符型|5|||
        /// </summary> 
		public string anst_mtd_code { get; set; }

        /// <summary>
        /// 23|麻醉-方法名称|字符型|50|||
        /// </summary> 
		public string anst_mtd_name { get; set; }

        /// <summary>
        /// 24|麻醉分级代码|字符型|10|Y||
        /// </summary> 
		public string anst_lv_code { get; set; }

        /// <summary>
        /// 25|麻醉分级名称|字符型|50|||
        /// </summary> 
		public string anst_lv_name { get; set; }

        /// <summary>
        /// 26|麻醉执行科室代码|字符型|30|Y||参照科室代码（dept）
        /// </summary> 
		public string exe_anst_dept_code { get; set; }

        /// <summary>
        /// 27|麻醉执行科室名称|字符型|50|||
        /// </summary> 
		public string exe_anst_dept_name { get; set; }

        /// <summary>
        /// 28|麻醉效果|字符型|100|||
        /// </summary> 
		public string anst_efft { get; set; }

        /// <summary>
        /// 29|手术开始时间|字符型|14||Y|yyyyMMd dHHmmss
        /// </summary> 
		public string oprn_begntime { get; set; }

        /// <summary>
        /// 30|手术结束时间|字符型|14||Y|yyyyMMd dHHmmss
        /// </summary> 
		public string oprn_endtime { get; set; }

        /// <summary>
        /// 31|是否无菌手术|字符型|2|||
        /// </summary> 
		public string oprn_asps { get; set; }

        /// <summary>
        /// 32|无菌手术是否感染|字符型|2|||
        /// </summary> 
		public string oprn_asps_ifet { get; set; }

        /// <summary>
        /// 33|手术后情况|字符型|500|||
        /// </summary> 
		public string afpn_info { get; set; }

        /// <summary>
        /// 34|是否手术合并症|字符型|10|||
        /// </summary> 
		public string oprn_merg { get; set; }

        /// <summary>
        /// 35|是否手术并发症|字符型|10|||
        /// </summary> 
		public string oprn_conc { get; set; }

        /// <summary>
        /// 36|手术执行科室代码|字符型|30|Y||参照科室代码（dept）
        /// </summary> 
		public string oprn_anst_dept_code { get; set; }

        /// <summary>
        /// 37|手术执行科室名称|字符型|50|||
        /// </summary> 
		public string oprn_anst_dept_name { get; set; }

        /// <summary>
        /// 38|病理检查|字符型|500|||
        /// </summary> 
		public string palg_dise { get; set; }

        /// <summary>
        /// 39|其他医学处置|字符型|4000|||
        /// </summary> 
		public string oth_med_dspo { get; set; }

        /// <summary>
        /// 40|是否超出标准手术时间|字符型|2|||
        /// </summary> 
		public string out_std_oprn_time { get; set; }

        /// <summary>
        /// 41|手术者姓名|字符型|50|||
        /// </summary> 
		public string oprn_oper_name { get; set; }

        /// <summary>
        /// 42|助手I姓名|字符型|50|||
        /// </summary> 
		public string oprn_asit_name1 { get; set; }

        /// <summary>
        /// 43|助手Ⅱ姓名|字符型|50|||
        /// </summary> 
		public string oprn_asit_name2 { get; set; }

        /// <summary>
        /// 44|麻醉医师姓名|字符型|50|||
        /// </summary> 
		public string anst_dr_name { get; set; }

        /// <summary>
        /// 45|麻醉ASA分级代码|字符型|50|||参照国家卫生健康委下发的麻醉ASA分级代码
        /// </summary> 
		public string anst_asa_lv_code { get; set; }

        /// <summary>
        /// 46|麻醉ASA分级名称|字符型|100|||
        /// </summary> 
		public string anst_asa_lv_name { get; set; }

        /// <summary>
        /// 47|麻醉药物代码|字符型|50|Y||参照国家卫生健康委下发的麻醉药物代码
        /// </summary> 
		public string anst_medn_code { get; set; }

        /// <summary>
        /// 48|麻醉药物名称|字符型|100|||
        /// </summary> 
		public string anst_medn_name { get; set; }

        /// <summary>
        /// 49|麻醉药物剂量|字符型|20|||
        /// </summary> 
		public string anst_medn_dos { get; set; }

        /// <summary>
        /// 50|计量单位|字符型|10|||
        /// </summary> 
		public string anst_dosunt { get; set; }

        /// <summary>
        /// 51|麻醉开始时间|字符型|14|||yyyyMMd dHHmmss
        /// </summary> 
		public string anst_begntime { get; set; }

        /// <summary>
        /// 52|麻醉结束时间|字符型|14|||yyyyMMd dHHmmss
        /// </summary> 
		public string anst_endtime { get; set; }

        /// <summary>
        /// 53|麻醉合并症代码|字符型|10|||参照国家卫生健康委下发的麻醉合并症代码
        /// </summary> 
		public string anst_merg_symp_code { get; set; }

        /// <summary>
        /// 54|麻醉合并症名称|字符型|100|||
        /// </summary> 
		public string anst_merg_symp { get; set; }

        /// <summary>
        /// 55|麻醉合并症描述|字符型|1K|||
        /// </summary> 
		public string anst_merg_symp_dscr { get; set; }

        /// <summary>
        /// 56|入复苏室时间|字符型|14|||
        /// </summary> 
		public string pacu_begntime { get; set; }

        /// <summary>
        /// 57|出复苏室时间|字符型|14|||
        /// </summary> 
		public string pacu_endtime { get; set; }

        /// <summary>
        /// 58|是否择期手术|字符型|1|Y||
        /// </summary> 
		public string oprn_selv { get; set; }

        /// <summary>
        /// 59|是否择取消手术|字符型|1|Y||
        /// </summary> 
		public string canc_oprn { get; set; }

        /// <summary>
        /// 60|有效标志|字符型|3|Y  |Y|
        /// </summary> 
		public string vali_flag { get; set; }
    }
    public class rescinfo_4701
    {
        /// <summary>
        /// 1|科室代码|字符型|30|Y|Y|参照科室代码（dept）
        /// </summary> 
		public string dept { get; set; }

        /// <summary>
        /// 2|科室名称|字符型|50||Y|
        /// </summary> 
		public string dept_name { get; set; }

        /// <summary>
        /// 3|病区名称|字符型|50||Y|
        /// </summary> 
		public string wardarea_name { get; set; }

        /// <summary>
        /// 4|病床号|字符型|10||Y|
        /// </summary> 
		public string bedno { get; set; }

        /// <summary>
        /// 5|诊断名称|字符型|100||Y|
        /// </summary> 
		public string diag_name { get; set; }

        /// <summary>
        /// 6|诊断代码|字符型|20||Y|
        /// </summary> 
		public string diag_code { get; set; }

        /// <summary>
        /// 7|病情变化情况|字符型|1000||Y|
        /// </summary> 
		public string cond_chg { get; set; }

        /// <summary>
        /// 8|抢救措施|字符型|1000||Y|
        /// </summary> 
		public string resc_mes { get; set; }

        /// <summary>
        /// 9|手术操作代码|字符型|30||Y|
        /// </summary> 
		public string oprn_oprt_code { get; set; }

        /// <summary>
        /// 10|手术操作名称|字符型|500||Y|
        /// </summary> 
		public string oprn_oprt_name { get; set; }

        /// <summary>
        /// 11|手术及操作目标部位名称|字符型|50||Y|
        /// </summary> 
		public string oprn_oper_part { get; set; }

        /// <summary>
        /// 12|介入物名称|字符型|100||Y|
        /// </summary> 
		public string itvt_name { get; set; }

        /// <summary>
        /// 13|操作方法|字符型|200||Y|
        /// </summary> 
		public string oprt_mtd { get; set; }

        /// <summary>
        /// 14|操作次数|数值型|3,0||Y|
        /// </summary> 
		public string oprt_cnt { get; set; }

        /// <summary>
        /// 15|抢救开始日期时间|日期型|||Y|
        /// </summary> 
		public string resc_begntime { get; set; }

        /// <summary>
        /// 16|抢救结束日期时间|日期型|||Y|
        /// </summary> 
		public string resc_endtime { get; set; }

        /// <summary>
        /// 17|检查/检验项目名称|字符型|80||Y|
        /// </summary> 
		public string dise_item_name { get; set; }

        /// <summary>
        /// 18|检查/检验结果|字符型|1000||Y|
        /// </summary> 
		public string dise_ccls { get; set; }

        /// <summary>
        /// 19|检查/检验定量结果|数值型|18，4||Y|
        /// </summary> 
		public string dise_ccls_qunt { get; set; }

        /// <summary>
        /// 20|检查/检验结果代码|字符型|1||Y|
        /// </summary> 
		public string dise_ccls_code { get; set; }

        /// <summary>
        /// 21|注意事项|字符型|1000||Y|
        /// </summary> 
		public string mnan { get; set; }

        /// <summary>
        /// 22|参加抢救人员名单|字符型|200||Y|
        /// </summary> 
		public string resc_psn_list { get; set; }

        /// <summary>
        /// 23|专业技术职务类别代码|字符型|30||Y|
        /// </summary> 
		public string proftechttl_code { get; set; }

        /// <summary>
        /// 24|医师编号|字符型|30||Y|
        /// </summary> 
		public string doc_code { get; set; }

        /// <summary>
        /// 25|医师姓名|字符型|50||Y|
        /// </summary> 
		public string dr_name { get; set; }

        /// <summary>
        /// 26|有效标志|字符型|3|Y  |Y|
        /// </summary> 
		public string vali_flag { get; set; }
    }
    public class dieinfo_4701
    {
        /// <summary>
        /// 1|科室代码|字符型|30||Y|
        /// </summary> 
        public string dept { get; set; }

        /// <summary>
        /// 2|科室名称|字符型|50||Y|
        /// </summary> 
		public string dept_name { get; set; }

        /// <summary>
        /// 3|病区名称|字符型|50||Y|
        /// </summary> 
		public string wardarea_name { get; set; }

        /// <summary>
        /// 4|病床号|字符型|10||Y|
        /// </summary> 
		public string bedno { get; set; }

        /// <summary>
        /// 5|入院时间|日期时间型|||Y|
        /// </summary> 
		public string adm_time { get; set; }

        /// <summary>
        /// 6|入院诊断编码|字符型|20||Y|
        /// </summary> 
		public string adm_dise { get; set; }

        /// <summary>
        /// 7|入院情况|字符型|200||Y|
        /// </summary> 
		public string adm_info { get; set; }

        /// <summary>
        /// 8|诊疗过程描述|字符型|2000||Y|
        /// </summary> 
		public string trt_proc_dscr { get; set; }

        /// <summary>
        /// 9|死亡时间|日期时间型|||Y|
        /// </summary> 
		public string die_time { get; set; }

        /// <summary>
        /// 10|直接死亡原因名称|字符型|50||Y|
        /// </summary> 
		public string die_drt_rea { get; set; }

        /// <summary>
        /// 11|直接死亡原因编码|字符型|10||Y|
        /// </summary> 
		public string die_drt_rea_code { get; set; }

        /// <summary>
        /// 12|死亡诊断名称|字符型|50||Y|
        /// </summary> 
		public string die_dise_name { get; set; }

        /// <summary>
        /// 13|死亡诊断编码|字符型|20|Y|Y|
        /// </summary> 
		public string die_diag_code { get; set; }

        /// <summary>
        /// 14|家属是否同意尸体解剖标志|字符型|1||Y|
        /// </summary> 
		public string agre_corp_dset { get; set; }

        /// <summary>
        /// 15|住院医师姓名|字符型|50||Y|
        /// </summary> 
		public string ipdr_name { get; set; }

        /// <summary>
        /// 16|主诊医师代码|字符型|30||Y|
        /// </summary> 
		public string chfpdr_code { get; set; }

        /// <summary>
        /// 17|主诊医师姓名|字符型|50||Y|
        /// </summary> 
		public string chfpdr_name { get; set; }

        /// <summary>
        /// 18|主任医师姓名|字符型|50||Y|
        /// </summary> 
		public string chfdr_name { get; set; }

        /// <summary>
        /// 19|签字日期时间|日期型|||Y|
        /// </summary> 
		public string sign_time { get; set; }

        /// <summary>
        /// 20|有效标志|字符型|3|Y  |Y|
        /// </summary> 
		public string vali_flag { get; set; }
    }
    public class dscginfo_4701
    {
        /// <summary>
        /// 1|出院日期|日期型||||
        /// </summary> 
        public string dscg_date { get; set; }

        /// <summary>
        /// 2|入院诊断描述|字符型|200|||
        /// </summary> 
		public string adm_diag_dscr { get; set; }

        /// <summary>
        /// 3|出院诊断|字符型|1000|||
        /// </summary> 
		public string dscg_dise_dscr { get; set; }

        /// <summary>
        /// 4|入院情况|字符型|2000|||
        /// </summary> 
		public string adm_info { get; set; }

        /// <summary>
        /// 5|诊治经过及结果（含手术日期名称及结果）|字符型|2000|||
        /// </summary> 
		public string trt_proc_rslt_dscr { get; set; }

        /// <summary>
        /// 6|出院情况（含治疗效果）|字符型|2000|||
        /// </summary> 
        public string dscg_info { get; set; }

        /// <summary>
        /// 7|出院医嘱|字符型|1000|||
        /// </summary> 
        public string dscg_drord { get; set; }

        /// <summary>
        /// 8|科别|字符型|6|Y||
        /// </summary> 
        public string caty { get; set; }

        /// <summary>
        /// 9|记录医师|字符型|80|||
        /// </summary> 
        public string rec_doc { get; set; }

        /// <summary>
        /// 10|主要药品名称|字符型|1500|||
        /// </summary> 
        public string main_drug_name { get; set; }

        /// <summary>
        /// 11|其他重要信息|字符型|1500|||
        /// </summary> 
        public string oth_imp_info { get; set; }

        /// <summary>
        /// 12|有效标志|字符型|3|Y  |Y|
        /// </summary> 
        public string vali_flag { get; set; }
    }
}
