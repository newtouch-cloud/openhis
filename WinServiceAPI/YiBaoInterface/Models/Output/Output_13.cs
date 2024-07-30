using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_13:OutputBase
    {
        /// <summary>
        /// 文件查询号
        /// </summary>
        public string file_qury_no { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 下载截止日yyyy-MM-dd
        /// </summary>
        public string dld_end_time { get; set; }
        /// <summary>
        /// 下载数量
        /// </summary>
        public string data_cn { get; set; }



    }


}
