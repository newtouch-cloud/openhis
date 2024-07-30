using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_5301 : InputBase
    {
        public data5301 data { get; set; }
    }

    public class data5301
    {
        // <summary>
        /// 人员编号 字符型 30 Y
        /// </summary>
        public string psn_no { get; set; }
    }
}
