using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_2305 : InputBase
    {
        public data2305 data { get; set; }

    }

    public class data2305
    {
        /// <summary>
        /// 1|就诊ID|字符型|30|  |Y|
        /// </summary> 
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 2|结算ID|字符型|30||Y|
        /// </summary> 
		public string setl_id { get; set; }

        /// <summary>
        /// 3|人员编号|字符型|30|  |Y|
        /// </summary> 
		public string psn_no { get; set; }
        /// <summary>
        /// 4|扩展字段|字符型|4000|
        /// </summary>
        public List<exp_content> exp_content { get; set; }
    }
}
