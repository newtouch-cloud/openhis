using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_2208 : InputBase
    {
        public data2208 data { get; set; }
    }

    public class data2208
    {
        /// <summary>
        /// 1|结算ID|字符型|30|  |Y  |
        /// </summary> 
        public string setl_id { get; set; }

        /// <summary>
        /// 2|就诊ID|字符型|30|  |Y|
        /// </summary> 
		public string mdtrt_id { get; set; }

        /// <summary>
        /// 3|人员编号|字符型|30|  |Y|
        /// </summary> 
		public string psn_no { get; set; }
        public string expContent { get; set; }
    }
}
