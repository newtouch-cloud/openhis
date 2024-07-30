using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_1317:InputBase
    {
        public dataInput1317 data { get; set; }
    }
   public class dataInput1317
    {

        /// <summary>
        /// 1|查询时间点|日期型||||
        /// </summary> 
        public string query_date { get; set; }

        /// <summary>
        /// 2|定点医药机构编号|字符型|30|||
        /// </summary> 
		public string fixmedins_code { get; set; }

        /// <summary>
        /// 3|定点医药机构目录编号|字符型|30|||
        /// </summary> 
		public string medins_list_codg { get; set; }

        /// <summary>
        /// 4|定点医药机构目录名称|字符型|200|||
        /// </summary> 
		public string medins_list_name { get; set; }

        /// <summary>
        /// 5|参保机构医保区划|字符型|6|Y||
        /// </summary> 
		public string insu_admdvs { get; set; }

        /// <summary>
        /// 6|目录类别|字符型|30|||
        /// </summary> 
		public string list_type { get; set; }

        /// <summary>
        /// 7|医疗目录编码|字符型|30|||
        /// </summary> 
		public string med_list_codg { get; set; }

        /// <summary>
        /// 8|开始日期|日期型||||
        /// </summary> 
		public string begndate { get; set; }

        /// <summary>
        /// 9|有效标志|字符型|3|Y||
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 10|更新时间|日期型|||Y|
        /// </summary> 
		public string updt_time { get; set; }

        /// <summary>
        /// 11|当前页数|数值型|4||Y|
        /// </summary> 
		public string page_num { get; set; }

        /// <summary>
        /// 12|本页数据量|数值型|4||Y|
        /// </summary> 
		public string page_size { get; set; }

    }
}
