using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_3102 : InputBase
    {
        //输入-规则分析信息（节点标识：data）
        public InputData3102 data { get; set; }
        ////输入-参保人信息（节点标识：patient_dtos）
        //public InputPD3102 patient_dtos { get; set; }
        ////输入-就诊信息（节点标识：fsi_encounter_dtos）
        //public InputFED3102 fsi_encounter_dtos { get; set; }
        ////输入-诊断信息（节点标识：fsi_diagnose_dtos）
        //public InputFDD3102 fsi_diagnose_dtos { get; set; }
        ////输入-处方（医嘱）信息（节点标识：fsi_order_dtos）
        //public InputFOD3102 fsi_order_dtos { get; set; }
        ////输入-手术操作信息（节点标识：fsi_operation_dtos）
        //public InputFODSS3102 fsi_operation_dtos { get; set; }
    }
    //输入-规则分析信息（节点标识：data）
    public class InputData3102
    {
        //参保人信息	参保人信息集合
        //public string patient_dtos { get; set; }
        public  List<InputPD3102> patient_dtos { get; set; }
        //规则标识集合	规则标识集合
        public string rule_ids { get; set; }
        //触发场景	字符型  此值与ruleIds指定其一即可,请优先指定此值
        public string trig_scen { get; set; }
        public string syscode { get; set; }
    }
    //输入-参保人信息（节点标识：patient_dtos）
    public class InputPD3102
    {
        //	参保人标识	字符型	50	参保人唯一ID
        public string patn_id { get; set; }
        //	姓名	字符型	50	
        public string patn_name { get; set; }
        //	性别	字符型	3	参考字典表
        public string gend { get; set; }
        //	出生日期	日期型		格式：yyyy-MM-dd
        public string brdy { get; set; }
        //	统筹区编码	字符型	10	参保人所属统筹区
        public string poolarea { get; set; }
        //	当前就诊标识	字符型	50	本次就诊记录唯一ID
        public string curr_mdtrt_id { get; set; }
        //	就诊信息集合	就诊信息集合		
        //public string fsi_encounter_dtos { get; set; }
        public List<InputFED3102> fsi_encounter_dtos { get; set; }
        //	医院信息集合	医院信息集合		
        public string fsi_his_data_dto { get; set; }

    }
    //输入-就诊信息（节点标识：fsi_encounter_dtos）
    public class InputFED3102
    {
        //	就诊标识	字符型	50	就诊记录唯一ID
        public string mdtrt_id { get; set; }
        //	医疗服务机构标识	字符型	50	定点医疗机构ID
        public string medins_id { get; set; }
        //	医疗机构名称	字符型	200	
        public string medins_name { get; set; }
        //	医疗机构行政区划编码	字符型	6	
        public string medins_admdvs { get; set; }
        //	医疗服务机构类型	字符型	3	参考字典表
        public string medins_type { get; set; }
        //	医疗机构等级	字符型	3	参考字典表
        public string medins_lv { get; set; }
        //	病区标识	字符型	20	
        public string wardarea_codg { get; set; }
        //	病房号	字符型	20	
        public string wardno { get; set; }
        //	病床号	字符型	20	
        public string bedno { get; set; }
        //	入院日期	日期型		格式：yyyy-MM-dd HH:mm:ss
        public string adm_date { get; set; }
        //	出院日期	日期型		格式：yyyy-MM-dd HH:mm:ss
        public string dscg_date { get; set; }
        //	主诊断编码	字符型	20	例如：I63.9
        public string dscg_main_dise_codg { get; set; }
        //	主诊断名称	字符型	50	例如：脑梗塞
        public string dscg_main_dise_name { get; set; }
        //	诊断信息DTO	诊断信息集合		
        //public string fsi_diagnose_dtos { get; set; }
        public List<InputFDD3102> fsi_diagnose_dtos { get; set; }
        //	医师标识	字符型	20	医生唯一ID
        public string dr_codg { get; set; }
        //	入院科室标识	字符型	20	科室唯一ID
        public string adm_dept_codg { get; set; }
        //	入院科室名称	字符型	50	
        public string adm_dept_name { get; set; }
        //	出院科室标识	字符型	20	科室唯一ID
        public string dscg_dept_codg { get; set; }
        //	出院科室名称	字符型	50	
        public string dscg_dept_name { get; set; }
        //	就诊类型	字符型	3	参考字典表
        public string med_mdtrt_type { get; set; }
        //	医疗类别	字符型	3	参考字典表
        public string med_type { get; set; }
        //	处方(医嘱)信息	处方信息集合		
        //public string fsi_order_dtos { get; set; }
        public List<InputFOD3102> fsi_order_dtos { get; set; }
        //	生育状态	字符型	3	参考字典表
        public string matn_stas { get; set; }
        //	总费用	数值型	16,2	
        public string medfee_sumamt { get; set; }
        //	自费金额	数值型	16,2	
        public string ownpay_amt { get; set; }
        //	自付金额	数值型	16,2	
        public string selfpay_amt { get; set; }
        //	个人账户支付金额	数值型	16,2	
        public string acct_payamt { get; set; }
        //	救助金支付金额	数值型	16,2	
        public string ma_amt { get; set; }
        //	统筹金支付金额	数值型	16,2	
        public string hifp_payamt { get; set; }
        //	结算总次数	数值型	4	
        public string setl_totlnum { get; set; }
        //	险种	字符型	3	参考字典表
        public string insutype { get; set; }
        //	报销标志	字符型	3	参考字典表
        public string reim_flag { get; set; }
        //	异地结算标志	字符型	3	参考字典表
        public string out_setl_flag { get; set; }
        //	手术操作集合	手术操作集合		
        public string fsi_operation_dtos { get; set; }

    }
    //输入-诊断信息（节点标识：fsi_diagnose_dtos）
    public class InputFDD3102
    {
        //诊断标识	字符型	50  诊断记录唯一标识
        public string dise_id { get; set; }
        //出入诊断类别	字符型	3
        public string inout_dise_type { get; set; }
        //主诊断标志	字符型	3
        public string maindise_flag { get; set; }
        //诊断排序号	字符型	2  例如：1,2,3…
        public string dias_srt_no { get; set; }
        //诊断(疾病)编码	字符型	30
        public string dise_codg { get; set; }
        //诊断(疾病)名称	字符型	200
        public string dise_name { get; set; }
        //诊断日期	日期型  格式：yyyy-MM-dd HH:mm:ss
        public string dise_date { get; set; }
    }
    //输入-处方（医嘱）信息（节点标识：fsi_order_dtos）
    public class InputFOD3102
    {
        //	处方(医嘱)标识	字符型	50	处方(医嘱)记录唯一ID
        public string rx_id { get; set; }
        //	处方号	字符型	20	
        public string rxno { get; set; }
        //	组编号	字符型	20	
        public string grpno { get; set; }
        //	是否为长期医嘱	字符型	3	[1=是,0=否]
        public string long_drord_flag { get; set; }
        //	目录类别	字符型	3	参考字典表
        public string hilist_type { get; set; }
        //	收费类别	字符型	3	参考字典表
        public string chrg_type { get; set; }
        //	医嘱行为	字符型	3	参考字典表
        public string drord_bhvr { get; set; }
        //	医保目录代码	字符型	20	国家统一标准编码
        public string hilist_code { get; set; }
        //	医保目录名称	字符型	50	国家统一标准名称
        public string hilist_name { get; set; }
        //	医保目录(药品)剂型	字符型	50	国家统一标准药品剂型
        public string hilist_dosform { get; set; }
        //	医保目录等级	字符型	3	
        public string hilist_lv { get; set; }
        //	医保目录价格	数值型	16,2	
        public string hilist_pric { get; set; }
        //	一级医院目录价格	数值型	16,2	
        public string lv1_hosp_item_pric { get; set; }
        //	二级医院目录价格	数值型	16,2	
        public string lv2_hosp_item_pric { get; set; }
        //	三级医院目录价格	数值型	16,2	
        public string lv3_hosp_item_pric { get; set; }
        //	医保目录备注	字符型	200	
        public string hilist_memo { get; set; }
        //	医院目录代码	字符型	20	
        public string hosplist_code { get; set; }
        //	医院目录名称	字符型	50	
        public string hosplist_name { get; set; }
        //	医院目录(药品)剂型	字符型	20	
        public string hosplist_dosform { get; set; }
        //	数量	数值型	6,2	
        public string cnt { get; set; }
        //	单价	数值型	16,2	
        public string pric { get; set; }
        //	总费用	数值型	16,2	
        public string sumamt { get; set; }
        //	自费金额	数值型	16,2	
        public string ownpay_amt { get; set; }
        //	自付金额	数值型	16,2	
        public string selfpay_amt { get; set; }
        //	规格	字符型	100	例如:0.25g×12片/盒
        public string spec { get; set; }
        //	数量单位	字符型	20	例如：盒
        public string spec_unt { get; set; }
        //	医嘱开始日期	日期型		格式：yyyy-MM-dd HH:mm:ss
        public string drord_begn_date { get; set; }
        //	医嘱停止日期	日期型		格式：yyyy-MM-dd HH:mm:ss
        public string drord_stop_date { get; set; }
        //	下达医嘱的科室标识	字符型	30	
        public string drord_dept_codg { get; set; }
        //	下达医嘱科室名称	字符型	50	
        public string drord_dept_name { get; set; }
        //	开处方(医嘱)医生标识	字符型	30	
        public string drord_dr_codg { get; set; }
        //	开处方(医嘱)医生姓名	字符型	30	
        public string drord_dr_name { get; set; }
        //	开处方(医嘱)医职称	字符型	3	参考字典表
        public string drord_dr_profttl { get; set; }
        //	是否当前处方(医嘱)	字符型	3	本次处方(医嘱)标记[1=是,0=否]
        public string curr_drord_flag { get; set; }

    }
    //输入-手术操作信息（节点标识：fsi_operation_dtos）
    public class InputFODSS3102
    {
        //	手术操作ID	字符型	30
        public string setl_list_oprn_id { get; set; }
        //	手术操作代码	字符型	30
        public string oprn_code { get; set; }
        //	手术操作名称	字符型	500
        public string oprn_name { get; set; }
        //	主手术操作标志	字符型	3
        public string main_oprn_flag { get; set; }
        //	手术操作日期	日期型	
        public string oprn_date { get; set; }
        //	麻醉方式	字符型	30
        public string anst_way { get; set; }
        //	术者医师姓名	字符型	50
        public string oper_dr_name { get; set; }
        //	术者医师代码	字符型	30
        public string oper_dr_code { get; set; }
        //	麻醉医师姓名	字符型	50
        public string anst_dr_name { get; set; }
        //	麻醉医师代码	字符型	30
        public string anst_dr_code { get; set; }

    }


    public class Receive3102
    {
        public string zyh { get; set; }
        public string operatorId { get; set; }
        public string operatorName { get; set; }
        public string txlx { get; set; }
    }
}
