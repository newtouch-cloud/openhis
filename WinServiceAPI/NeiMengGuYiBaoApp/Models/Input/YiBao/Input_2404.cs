using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_2404 : InputBase
    {
        public data2404 data { get; set; }
    }

    public class data2404
    {
        /// <summary>
        /// 1|就诊ID|字符型|30||Y|
        /// </summary> 
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 2|人员编号|字符型|30|  |Y|
        /// </summary> 
		public string psn_no { get; set; }

        /// <summary>
        /// 结算 ID  字符型  30 
        /// </summary>
        //public string setl_id { get; set; }
        /// <summary>
        /// 3|扩展字段|字符型|4000|
        /// </summary>
        public string expContent { get; set; }
    }
}
