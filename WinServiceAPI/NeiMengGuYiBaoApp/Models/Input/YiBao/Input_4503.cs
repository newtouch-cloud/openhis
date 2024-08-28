using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4503 : InputBase
    {
        public data_4503 data { get; set; }
    }

    public class data_4503
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
        /// 4|申请单号|字符型|18||Y|
        /// </summary> 
		public string appy_no { get; set; }

        /// <summary>
        /// 5|报告单号|字符型|50||Y|
        /// </summary> 
		public string rpotc_no { get; set; }

        /// <summary>
        /// 6|细菌代号|日期型|||Y|
        /// </summary> 
		public string germ_code { get; set; }

        /// <summary>
        /// 7|细菌名称|日期型|||Y|
        /// </summary> 
		public string germ_name { get; set; }

        /// <summary>
        /// 8|菌落计数|字符型|200||Y|
        /// </summary> 
		public string coly_cntg { get; set; }

        /// <summary>
        /// 9|培养基|字符型|1000||Y|
        /// </summary> 
		public string clte_medm { get; set; }

        /// <summary>
        /// 10|培养时间|字符型|1000||Y|
        /// </summary> 
		public string clte_time { get; set; }

        /// <summary>
        /// 11|培养条件|字符型|1000||Y|
        /// </summary> 
		public string clte_cond { get; set; }

        /// <summary>
        /// 12|检验结果|字符型|4000||Y|
        /// </summary> 
		public string exam_rslt { get; set; }

        /// <summary>
        /// 13|发现方式|字符型|50||Y|
        /// </summary> 
		public string fnd_way { get; set; }
        /// <summary>
        /// 14|检验机构名称|字符型|50||Y|
        /// </summary> 
        public string exam_org_name { get; set; }
        /// <summary>
        /// 15|有效标志|字符型|3|Y  |Y|
        /// </summary> 
		public string vali_flag { get; set; }
    }
}
