using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_2402 : InputBase
    {
        public dscginfo dscginfo;
        /// <summary>
        /// 表 104 输入-出院诊断信息（节点标识：diseinfo）
        /// </summary>
        public List<diseinfo> diseinfo { get; set; }
    }

    public class dscginfo
    {
        /// <summary>
        /// 1|就诊ID|字符型|30|  |Y|  
        /// </summary> 
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 2|人员编号|字符型|30|  |Y|   
        /// </summary> 
		public string psn_no { get; set; }

        /// <summary>
        /// 3|险种类型|字符型|6|Y|Y|  
        /// </summary> 
		public string insutype { get; set; }

        /// <summary>
        /// 4|结束时间|日期时间型|  |  |Y|出院时间 yyyy-MM-dd HH:mm:ss
        /// </summary> 
		public string endtime { get; set; }

        /// <summary>
        /// 5|病种编码|字符型|30|  |  |按照标准编码填写：按病种结算病种目录代码(bydise_setl_list_code)日间手术病种目录代码(daysrg_dise_list_code)
        /// </summary> 
		public string dise_codg { get; set; }

        /// <summary>
        /// 6|病种名称|字符型|500|  |  |  
        /// </summary> 
		public string dise_name { get; set; }

        /// <summary>
        /// 7|手术操作代码|字符型|30|  |  |日间手术病种时必填
        /// </summary> 
		public string oprn_oprt_code { get; set; }

        /// <summary>
        /// 8|手术操作名称|字符型|500|  |  |  
        /// </summary> 
        public string oprn_oprt_name { get; set; }

        /// <summary>
        /// 9|计划生育服务证号|字符型|50|  |  |  
        /// </summary> 
        public string fpsc_no { get; set; }

        /// <summary>
        /// 10|生育类别|字符型|6|Y|  |  
        /// </summary> 
        public string matn_type { get; set; }

        /// <summary>
        /// 11|计划生育手术类别|字符型|6|Y  |  |  
        /// </summary> 
        public string birctrl_type { get; set; }

        /// <summary>
        /// 12|晚育标志|字符型|3|Y|  |  
        /// </summary> 
        public string latechb_flag { get; set; }

        /// <summary>
        /// 13|孕周数|数值型|2|  |  |  
        /// </summary> 
        public string geso_val { get; set; }

        /// <summary>
        /// 14|胎次|数值型|3|  |  |  
        /// </summary> 
        public string fetts { get; set; }

        /// <summary>
        /// 15|胎儿数|数值型|3|  |  |  
        /// </summary> 
        public string fetus_cnt { get; set; }

        /// <summary>
        /// 16|早产标志|字符型|3|Y|  |  
        /// </summary> 
        public string pret_flag { get; set; }

        /// <summary>
        /// 17|计划生育手术或生育日期|日期型|  |  |  |yyyy-MM-dd
        /// </summary> 
        public string birctrl_matn_date { get; set; }

        /// <summary>
        /// 18|伴有并发症标志|字符型|3|Y||
        /// </summary> 
		public string cop_flag { get; set; }

        /// <summary>
        /// 19|出院科室编码|字符型|30|  |Y|   
        /// </summary> 
        public string dscg_dept_codg { get; set; }

        /// <summary>
        /// 20|出院科室名称|字符型|100|  |Y|   
        /// </summary> 
		public string dscg_dept_name { get; set; }

        /// <summary>
        /// 21|出院床位|字符型|30|  |  |  
        /// </summary> 
		public string dscg_bed { get; set; }

        /// <summary>
        /// 22|离院方式|字符型|3|Y|Y|   
        /// </summary> 
        public string dscg_way { get; set; }

        /// <summary>
        /// 23|死亡日期|日期型|  |  |  |yyyy-MM-dd
        /// </summary> 
        public string die_date { get; set; }

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
