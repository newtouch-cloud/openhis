using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_2301 : InputBase
    {
        public List<feedetail2301> feedetail { get; set; }

        /// <summary>
        /// 输入（节点标识：feedetail） 
        /// </summary>
       
    }
    /*

       public class exp_content
       {
           /// <summary>
           ///1 |急诊标志| 字符型 |6 |0、 非急诊；1、急诊；2、术前门诊检查；3、择期手术项目  
           /// </summary>
           public string er_flag { get; set; }
       }
       */

    public class feedetail2301
    {
        /// <summary>
        /// 1|费用明细流水号|字符型|30|  |Y  |单次就诊内唯一
        /// </summary> 
        public string feedetl_sn { get; set; }

        /// <summary>
        /// 2|原费用流水号|字符型|30|||退单时传入被退单的费用明细流水号
        /// </summary> 
        public string init_feedetl_sn { get; set; }

        /// <summary>
        /// 3|就诊ID|字符型|30|  |Y  |
        /// </summary> 
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 4|医嘱号|字符型|30|||
        /// </summary> 
        public string drord_no { get; set; }

        /// <summary>
        /// 5|人员编号|字符型|30|  |Y  |
        /// </summary> 
        public string psn_no { get; set; }

        /// <summary>
        /// 6|医疗类别|字符型|6|Y|Y  |  
        /// </summary> 
        public string med_type { get; set; }

        /// <summary>
        /// 7|费用发生时间|日期时间型|  |  |Y|yyyy-MM-dd HH:mm:ss
        /// </summary> 
        public string fee_ocur_time { get; set; }

        /// <summary>
        /// 8|医疗目录编码|字符型|50|  |Y  |
        /// </summary> 
        public string med_list_codg { get; set; }

        /// <summary>
        /// 9|医药机构目录编码|字符型|150|  |Y  |
        /// </summary> 
        public string medins_list_codg { get; set; }

        /// <summary>
        /// 10|明细项目费用总额|数值型|16,2|  |Y  |
        /// </summary> 
		public string det_item_fee_sumamt { get; set; }

        /// <summary>
        /// 11|数量|数值型|16,4|  |Y|退单时数量填写负数
        /// </summary> 
		public string cnt { get; set; }

        /// <summary>
        /// 12|单价|数值型|16,6|  |Y  |  
        /// </summary> 
        public string pric { get; set; }

        /// <summary>
        /// 13|开单科室编码|字符型|30|  |Y  |  
        /// </summary> 
        public string bilg_dept_codg { get; set; }

        /// <summary>
        /// 14|开单科室名称|字符型|100|  |Y  |  
        /// </summary> 
		public string bilg_dept_name { get; set; }

        /// <summary>
        /// 15|开单医生编码|字符型|30|  |Y  |
        /// </summary> 
		public string bilg_dr_codg { get; set; }

        /// <summary>
        /// 16|开单医师姓名|字符型|50|  |Y  |  
        /// </summary> 
        public string bilg_dr_name { get; set; }

        /// <summary>
        /// 17|受单科室编码|字符型|30|  |  |  
        /// </summary> 
        public string acord_dept_codg { get; set; }

        /// <summary>
        /// 18|受单科室名称|字符型|100|  |  |  
        /// </summary> 
		public string acord_dept_name { get; set; }

        /// <summary>
        /// 19|受单医生编码|字符型|30|  |  |
        /// </summary> 
		public string orders_dr_code { get; set; }

        /// <summary>
        /// 20|受单医生姓名|字符型|50|  |  |  
        /// </summary> 
		public string orders_dr_name { get; set; }

        /// <summary>
        /// 21|医院审批标志|字符型|3|Y|  |
        /// </summary> 
		public string hosp_appr_flag { get; set; }

        /// <summary>
        /// 22|中药使用方式|字符型|6|Y|  |  
        /// </summary> 
		public string tcmdrug_used_way { get; set; }

        /// <summary>
        /// 23|外检标志|字符型|3|Y|  |  
        /// </summary> 
		public string etip_flag { get; set; }

        /// <summary>
        /// 24|外检医院编码|字符型|30|  |  |
        /// </summary> 
        public string etip_hosp_code { get; set; }

        /// <summary>
        /// 25|出院带药标志|字符型|3|Y|  |  
        /// </summary> 
		public string dscg_tkdrug_flag { get; set; }

        /// <summary>
        /// 26|生育费用标志|字符型|6|Y|  |
        /// </summary> 
		public string matn_fee_flag { get; set; }

        /// <summary>
        /// 27|备注|字符型|500|  |  |  
        /// </summary> 
        public string memo { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现 解决待验证
        public List<exp_content> exp_content { get; set; }

    }
}
