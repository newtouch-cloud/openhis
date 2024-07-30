using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
   public class Input_3211:InputBase
    {
        public data3211 data { get; set; }
    }

    public class data3211
    {
        /// <summary>
        /// 1|清算开始日期 |日期型 |  |  |Y  |yyyy-MM-dd
        /// </summary> 
		public DateTime clr_begin_ymd { get; set; }

        /// <summary>
        /// 2|清算结束日期|日期型|  |  |Y  |yyyy-MM-dd
        /// </summary> 
		public DateTime clr_end_ymd { get; set; }
        /// <summary>
        /// 3|文件查询号 |字符型 |30 ||Y  |上传明细文件后返回的号码
        /// </summary> 
        public string file_qury_no { get; set; }
        /// <summary>
        /// 4|文件名称|字符型  
        /// </summary> 
		public string upload_file_name { get; set; }

    }
}
