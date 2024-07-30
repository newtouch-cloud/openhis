using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_1318:InputBase
    {
        public dataInput1318 data { get; set; }
    }
   public class dataInput1318
    {
       

        /// <summary>
        /// 1|查询时间点|日期型||||
        /// </summary> 
        public string query_date { get; set; }

        /// <summary>
        /// 2|医保目录编码|字符型|30|||
        /// </summary> 
		public string hilist_code { get; set; }

        /// <summary>
        /// 3|医保目录限价类型|字符型|6|Y||
        /// </summary> 
		public string hilist_lmtpric_type { get; set; }

        /// <summary>
        /// 4|医保目录超限处理方式|字符型|6|Y||
        /// </summary> 
		public string overlmt_dspo_way { get; set; }

        /// <summary>
        /// 5|参保机构医保区划|字符型|6|Y||
        /// </summary> 
		public string insu_admdvs { get; set; }

        /// <summary>
        /// 6|开始日期|日期型||||
        /// </summary> 
		public string begndate { get; set; }

        /// <summary>
        /// 7|结束日期|日期型||||
        /// </summary> 
		public string enddate { get; set; }

        /// <summary>
        /// 8|有效标志|字符型|3|Y||
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 9|唯一记录号|字符型|40|||
        /// </summary> 
		public string rid { get; set; }

        /// <summary>
        /// 10|表名|字符型|100|||
        /// </summary> 
		public string tabname { get; set; }

        /// <summary>
        /// 11|统筹区|字符型|6|Y||
        /// </summary> 
		public string poolarea_no { get; set; }

        /// <summary>
        /// 12|更新时间|日期型|||Y|
        /// </summary> 
		public string updt_time { get; set; }

        /// <summary>
        /// 13|当前页数|数值型|4||Y|
        /// </summary> 
		public string page_num { get; set; }

        /// <summary>
        /// 14|本页数据量|数值型|4||Y|
        /// </summary> 
		public string page_size { get; set; }

    }
}
