using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_2302 : InputBase
    {
        public List<data2302> data { get; set; }
    }
    public class data2302
    {
        /// <summary>
        /// 1|费用明细流水号|字符型|30||Y|传入“0000”时删除全部
        /// </summary> 
        public string feedetl_sn { get; set; }

        /// <summary>
        /// 2|就诊ID|字符型|30|  |Y  |
        /// </summary> 
		public string mdtrt_id { get; set; }

        /// <summary>
        /// 3|人员编号|字符型|30|  |Y|
        /// </summary> 
		public string psn_no { get; set; }
        /// <summary>
        /// 4|扩展字段|字符型|4000|
        /// </summary>
        public string expContent { get; set; }
    }
}
