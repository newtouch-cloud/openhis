using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_2601 : InputBase
    {

        public data2601 data { get; set; }

        
    }
    public class data2601
    {
        /// <summary>
        /// 1|人员编号|字符型|30|  |Y|
        /// </summary> 
        public string psn_no { get; set; }

        /// <summary>
        /// 2|原发送方报文ID|字符型|30||Y|
        /// </summary> 
        public string omsgid { get; set; }

        /// <summary>
        /// 3|原交易编号|字符型|4||Y|
        /// </summary> 
        public string oinfno { get; set; }
    }
}
