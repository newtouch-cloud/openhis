using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
   public class Input_3212:InputBase
    {
        public data3212 data { get; set; }
    }

    public class data3212
    {
        /// <summary>s
        /// 1|对账任务查询号|字符型|30||Y  |
        /// </summary> 
        public string task_key { get; set; }

    }
}
