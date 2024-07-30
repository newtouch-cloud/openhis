using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_2505 : InputBase
    {
        public data2505 data { get; set; }
    }
    public class data2505
    {
        /// <summary>
        /// 1|人员编号|字符型|30|  |Y  |  
        /// </summary> 
        public string psn_no { get; set; }

        /// <summary>
        /// 2|联系电话|字符型|50|  |  |  
        /// </summary> 
		public string tel { get; set; }

        /// <summary>
        /// 3|联系地址|字符型|200|  |  |  
        /// </summary> 
		public string addr { get; set; }

        /// <summary>
        /// 4|业务申请类型|字符型|3|Y  |Y  |  
        /// </summary> 
		public string biz_appy_type { get; set; }

        /// <summary>
        /// 5|开始日期|日期型|  |  |Y  |  
        /// </summary> 
		public string begndate { get; set; }

        /// <summary>
        /// 6|结束日期|日期型|  |  |  |  
        /// </summary> 
		public string enddate { get; set; }

        /// <summary>
        /// 7|代办人姓名|字符型|50|  |  |  
        /// </summary> 
		public string agnter_name { get; set; }

        /// <summary>
        /// 8|代办人证件类型|字符型|6|Y  |  |  
        /// </summary> 
		public string agnter_cert_type { get; set; }

        /// <summary>
        /// 9|代办人证件号码|字符型|50|  |  |  
        /// </summary> 
		public string agnter_certno { get; set; }

        /// <summary>
        /// 10|代办人联系方式|字符型|30|  |  |  
        /// </summary> 
		public string agnter_tel { get; set; }

        /// <summary>
        /// 11|代办人联系地址|字符型|200|  |  |  
        /// </summary> 
		public string agnter_addr { get; set; }

        /// <summary>
        /// 12|代办人关系|字符型|3|Y  |  |  
        /// </summary> 
		public string agnter_rlts { get; set; }

        /// <summary>
        /// 13|定点排序号|字符型|3|  |Y  |  
        /// </summary> 
		public string fix_srt_no { get; set; }

        /// <summary>
        /// 14|定点医药机构编号|字符型|12|  |Y  |  
        /// </summary> 
		public string fixmedins_code { get; set; }

        /// <summary>
        /// 15|定点医药机构名称|字符型|200|  |Y  |  
        /// </summary> 
		public string fixmedins_name { get; set; }

        /// <summary>
        /// 16|备注|字符型|500|  |  |  
        /// </summary> 
		public string memo { get; set; }
    }
}
