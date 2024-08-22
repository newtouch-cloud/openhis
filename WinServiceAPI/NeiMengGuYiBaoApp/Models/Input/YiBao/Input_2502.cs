using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_2502 : InputBase
    {
        public data2502 data { get; set; }
    }

    public class data2502
    {

        /// <summary>
        /// 1|待遇申报明细流水号|字符型|30||Y|
        /// </summary> 
        public string trt_dcla_detl_sn { get; set; }

        /// <summary>
        /// 2|人员编号|字符型|30|  |Y|
        /// </summary> 
		public string psn_no { get; set; }

        /// <summary>
        /// 3|备注|字符型|500|||填写撤销原因
        /// </summary> 
		public string memo { get; set; }
    }
}
