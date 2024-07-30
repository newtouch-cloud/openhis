using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_4505 : InputBase
    {
        public data_4505 data { get; set; }
    }
    public class data_4505
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
        /// 4|病理号|字符型|18||Y|
        /// </summary> 
		public string palg_no { get; set; }

        /// <summary>
        /// 5|冰冻号|字符型|50||Y|
        /// </summary> 
		public string frozen_no { get; set; }

        /// <summary>
        /// 6|送检日期|日期型|||Y|
        /// </summary> 
		public string cma_date { get; set; }

        /// <summary>
        /// 7|报告日期|日期型|||Y|
        /// </summary> 
		public string rpot_date { get; set; }

        /// <summary>
        /// 8|送检材料|字符型|200||Y|
        /// </summary> 
		public string cma_matl { get; set; }

        /// <summary>
        /// 9|临床诊断|字符型|1000||Y|
        /// </summary> 
		public string clnc_dise { get; set; }

        /// <summary>
        /// 10|检查所见|字符型|1000||Y|
        /// </summary> 
		public string exam_fnd { get; set; }

        /// <summary>
        /// 11|免疫组化|字符型|1000||Y|
        /// </summary> 
		public string sabc { get; set; }

        /// <summary>
        /// 12|病理诊断|字符型|4000||Y|
        /// </summary> 
		public string palg_diag { get; set; }

        /// <summary>
        /// 13|报告医师|字符型|50||Y|
        /// </summary> 
		public string rpot_doc { get; set; }

        /// <summary>
        /// 14|有效标志|字符型|3|Y  |Y|
        /// </summary> 
		public string vali_flag { get; set; }
    }
}
