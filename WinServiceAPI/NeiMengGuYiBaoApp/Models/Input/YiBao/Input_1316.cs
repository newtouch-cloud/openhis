using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_1316 : InputBase
    {

        public data1316 data { get; set; }

    }
    public class data1316
    {
        /// <summary>
        /// 1|查询时间点|日期型||||
        /// </summary> 
        public string query_date { get; set; }

        /// <summary>
        /// 2|定点医药机构目录编号|字符型|30|||
        /// </summary> 
		public string medins_list_codg { get; set; }

        /// <summary>
        /// 3|医保目录编码|字符型|30|||
        /// </summary> 
		public string hilist_code { get; set; }

        /// <summary>
        /// 4|目录类别|字符型|30|||
        /// </summary> 
		public string list_type { get; set; }

        /// <summary>
        /// 5|参保机构医保区划|字符型|6|Y||
        /// </summary> 
		public string insu_admdvs { get; set; }

        /// <summary>
        /// 6|开始日期|日期型||||
        /// </summary> 
		public string begndate { get; set; }

        /// <summary>
        /// 7|有效标志|字符型|3|Y||
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 9|更新时间|日期型|||Y|
        /// </summary> 
		public string updt_time { get; set; }

        /// <summary>
        /// 10|当前页数|数值型|4||Y|
        /// </summary> 
		public string page_num { get; set; }

        /// <summary>
        /// 11|本页数据量|数值型|4||Y|
        /// </summary> 
		public string page_size { get; set; }
    }
}
