using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_3211 : OutputBase
    {
        public fileinfo3211 fileinfo { get; set; }
    }

    public class fileinfo3211
    {
        /// <summary>
        /// 1|对账任务查询号|字符型|30||Y  |
        /// </summary> 
        public string task_key { get; set; }
    }
}
