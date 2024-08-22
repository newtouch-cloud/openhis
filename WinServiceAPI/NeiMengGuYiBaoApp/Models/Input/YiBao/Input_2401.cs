using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_2401 : InputBase
    {
        public mdtrtinfo mdtrtinfo;

        /// <summary>
        /// 表 101 输入-入院诊断信息（节点标识：diseinfo）
        /// </summary>
        public List<diseinfo> diseinfo { get; set; }
    }

    public class mdtrtinfo
    {
        /// <summary>
        /// 1|人员编号|字符型|30|  |Y  |  
        /// </summary> 
        public string psn_no { get; set; }

        /// <summary>
        /// 2|险种类型|字符型|6|Y|Y|  
        /// </summary> 
		public string insutype { get; set; }

        /// <summary>
        /// 3|联系人姓名|字符型|50|  |  |  
        /// </summary> 
		public string coner_name { get; set; }

        /// <summary>
        /// 4|联系电话|字符型|50|  |  |  
        /// </summary> 
		public string tel { get; set; }

        /// <summary>
        /// 5|开始时间|日期时间型|  |  |Y  |入院时间 yyyy-MM-dd HH:mm:ss
        /// </summary> 
		public string begntime { get; set; }

        /// <summary>
        /// 6|就诊凭证类型|字符型|3|Y|Y  |  
        /// </summary> 
		public string mdtrt_cert_type { get; set; }

        /// <summary>
        /// 7|就诊凭证编号|字符型|50||Y|就诊凭证类型为“01” 时填写电子凭证令牌，为“02”时填写身份证号，为“03” 时填写社会保障卡卡号
        /// </summary> 
		public string mdtrt_cert_no { get; set; }

        /// <summary>
        /// 8|医疗类别|字符型|6|Y|Y  |  
        /// </summary> 
		public string med_type { get; set; }

        /// <summary>
        /// 9|住院号|字符型|30|  |Y  |院内就诊流水号
        /// </summary> 
		public string ipt_no { get; set; }

        /// <summary>
        /// 10|病历号|字符型|30|  |  |
        /// </summary> 
		public string medrcdno { get; set; }

        /// <summary>
        /// 11|主治医生编码|字符型|30|  |Y  |  
        /// </summary> 
		public string atddr_no { get; set; }

        /// <summary>
        /// 12|主诊医师姓名|字符型|50|  |Y  |  
        /// </summary> 
		public string chfpdr_name { get; set; }

        /// <summary>
        /// 13|入院诊断描述|字符型|200|  |Y  |  
        /// </summary> 
		public string adm_diag_dscr { get; set; }

        /// <summary>
        /// 14|入院科室编码|字符型|30|  |Y  |  
        /// </summary> 
		public string adm_dept_codg { get; set; }

        /// <summary>
        /// 15|入院科室名称|字符型|100|  |Y  |  
        /// </summary> 
		public string adm_dept_name { get; set; }

        /// <summary>
        /// 16|入院床位|字符型|30|  |Y  |  
        /// </summary> 
		public string adm_bed { get; set; }

        /// <summary>
        /// 17|住院主诊断代码|字符型|20|  |Y  |  
        /// </summary> 
		public string dscg_maindiag_code { get; set; }

        /// <summary>
        /// 18|住院主诊断名称|字符型|300|  |Y  |  
        /// </summary> 
        public string dscg_maindiag_name { get; set; }

        /// <summary>
        /// 19|主要病情描述|字符型|1000|  |  |  
        /// </summary> 
        public string main_cond_dscr { get; set; }

        /// <summary>
        /// 20|病种编码|字符型|30|  |  |按照标准编码填写：按病种结算病种目录代码(bydise_setl_list_ code)日间手术病种目录代码(daysrg_dise_list_ code)
        /// </summary> 
        public string dise_codg { get; set; }

        /// <summary>
        /// 21|病种名称|字符型|500|  |  |  
        /// </summary> 
        public string dise_name { get; set; }

        /// <summary>
        /// 22|手术操作代码|字符型|30|  |  |日间手术病种时必填
        /// </summary> 
        public string oprn_oprt_code { get; set; }

        /// <summary>
        /// 23|手术操作名称|字符型|500|  |  |  
        /// </summary> 
        public string oprn_oprt_name { get; set; }

        /// <summary>
        /// 24|计划生育服务证号|字符型|50|  |  |  
        /// </summary> 
        public string fpsc_no { get; set; }

        /// <summary>
        /// 25|生育类别|字符型|6|Y|  |  
        /// </summary> 
        public string matn_type { get; set; }

        /// <summary>
        /// 26|计划生育手术类别|字符型|6|Y  |  |  
        /// </summary> 
        public string birctrl_type { get; set; }

        /// <summary>
        /// 27|晚育标志|字符型|3|Y|  |  
        /// </summary> 
        public string latechb_flag { get; set; }

        /// <summary>
        /// 28|孕周数|数值型|2|  |  |  
        /// </summary> 
        public string geso_val { get; set; }

        /// <summary>
        /// 29|胎次|数值型|3|  |  |  
        /// </summary> 
        public string fetts { get; set; }

        /// <summary>
        /// 30|胎儿数|数值型|3|  |  |  
        /// </summary> 
        public string fetus_cnt { get; set; }

        /// <summary>
        /// 31|早产标志|字符型|3|Y|  |  
        /// </summary> 
        public string pret_flag { get; set; }

        /// <summary>
        /// 32|计划生育手术或生育日期|日期型|  |  |  |yyyy-MM-dd 如果医疗类别为新生儿随母住院，此处传新生儿出生日期
        /// </summary> 
        public string birctrl_matn_date { get; set; }

        /// <summary>
        /// 33|病种类型|字符型|6|||
        /// </summary> 
        public string dise_type_code { get; set; }
        /// <summary>
        /// 4|扩展字段|字符型|4000|
        /// </summary>
        public string expContent { get; set; }
    }
    /*

    public class diseinfo
    {

        /// <summary>
        /// 1|人员编号|字符型|30|  | Y|  
        /// </summary> 
        public string psn_no { get; set; }

        /// <summary>
        /// 2|诊断类别|字符型|3| Y| Y|  
        /// </summary> 
		public string diag_type { get; set; }

        /// <summary>
        /// 3|主诊断标志|字符型|3| Y| Y|  
        /// </summary> 
		public string maindiag_flag { get; set; }

        /// <summary>
        /// 4|诊断排序号|数值型|2|  | Y|  
        /// </summary> 
        public string diag_srt_no { get; set; }

        /// <summary>
        /// 5|诊断代码|字符型|20|  | Y|  
        /// </summary> 
        public string diag_code { get; set; }

        /// <summary>
        /// 6|诊断名称|字符型|100|  | Y|  
        /// </summary> 
        public string diag_name { get; set; }

        /// <summary>
        /// 7|入院病情|字符型|500|  |  |  
        /// </summary> 
        public string adm_cond { get; set; }

        /// <summary>
        /// 8|诊断科室|字符型|50|  | Y|  
        /// </summary> 
        public string diag_dept { get; set; }

        /// <summary>
        /// 9|诊断医生编码|字符型|30|  | Y|  
        /// </summary> 
        public string dise_dor_no { get; set; }

        /// <summary>
        /// 10|诊断医生姓名|字符型|50|  | Y|  
        /// </summary> 
        public string dise_dor_name { get; set; }

        /// <summary>
        /// 11|诊断时间|日期时间型|  |  | Y|yyyy-MM-dd HH:mm:ss
        /// </summary> 
        public string diag_time { get; set; }

    }
    */
}
