using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_9102 : InputBase
    {
        public fsUploadIn9102 fsDownloadIn;
    }

    public class fsUploadIn9102
    {
        /// <summary>
        /// 1|文件查询号|字符型|30||Y|
        /// </summary> 
        public string file_qury_no { get; set; }

        /// <summary>
        /// 2|文件名|字符型|200||Y  |下载【1301-1319】生成的文件，固定传入“plc” 
        /// </summary> 
		public string filename { get; set; }

        /// <summary>
        /// 3|医药机构编号|字符型|30|  |Y  |
        /// </summary> 
		public string fixmedins_code { get; set; }

    }
}
