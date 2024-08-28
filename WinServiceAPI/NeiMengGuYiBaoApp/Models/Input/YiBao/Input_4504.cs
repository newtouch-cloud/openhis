using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_4504: InputBase
    {
        public data_4504 data { get; set; }
    }
    public class data_4504
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
        /// 8|药敏代码|字符型|200||Y|
        /// </summary> 
		public string sstb_code { get; set; }

        /// <summary>
        /// 9|药敏名称|字符型|1000||Y|
        /// </summary> 
		public string sstb_name { get; set; }

        /// <summary>
        /// 10|reta_rslt_code|字符型|1000||Y|
        /// </summary> 
		public string reta_rslt_code { get; set; }

        /// <summary>
        /// 11|抗药结果|字符型|1000||Y|
        /// </summary> 
		public string reta_rslt_name { get; set; }

        /// <summary>
        /// 12|参考值|字符型|4000||Y|
        /// </summary> 
		public string ref_val { get; set; }

        /// <summary>
        /// 13|检验方法|字符型|50||Y|
        /// </summary> 
		public string exam_mtd { get; set; }
        /// <summary>
        /// 14|检验结果|字符型|50||Y|
        /// </summary> 
        public string exam_rslt { get; set; }
        /// <summary>
        /// 15|检验机构名称|字符型|50||Y|
        /// </summary> 
        public string exam_org_name { get; set; }
        /// <summary>
        /// 16|有效标志|字符型|3|Y  |Y|
        /// </summary> 
        public string vali_flag { get; set; }
    }
}
