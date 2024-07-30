using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_3212 : OutputBase
    {
        public fileinfo3212 fileinfo { get; set; }
    }

    public class fileinfo3212
    {
        /// <summary>
        /// 1|对账状态|字符型|Y  |
        /// </summary> 
        public string check_state { get; set; }
        /// <summary>
        /// 2|任务状态|字符型|Y  |
        /// </summary> 
        public string task_status { get; set; }
        /// <summary>
        /// 3|文件查询号1|字符型|Y  |
        /// </summary> 
        public string file_qury_no1 { get; set; }
        /// <summary>
        /// 4|文件查询号2|字符型|Y  |
        /// </summary> 
        public string file_qury_no2 { get; set; }
        /// <summary>
        /// 5|文件查询号3|字符型|Y  |
        /// </summary> 
        public string file_qury_no3 { get; set; }
    }
}
