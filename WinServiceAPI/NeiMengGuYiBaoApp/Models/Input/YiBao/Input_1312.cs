using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_1312:InputBase
    {
      public dataInput1312 data { get; set; }
    }
  public  class dataInput1312
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
        /// 3|参保机构医保区划|字符型|6|Y||
        /// </summary> 
		public string insu_admdvs { get; set; }

        /// <summary>
        /// 4|开始日期|日期型||||
        /// </summary> 
		public string begndate { get; set; }

        /// <summary>
        /// 5|医保目录名称|字符型|200|||
        /// </summary> 
		public string hilist_name { get; set; }

        /// <summary>
        /// 6|五笔助记码|字符型|30|||
        /// </summary> 
		public string wubi { get; set; }

        /// <summary>
        /// 7|拼音助记码|字符型|30|||
        /// </summary> 
		public string pinyin { get; set; }

        /// <summary>
        /// 8|医疗收费项目类别|字符型|6|Y||
        /// </summary> 
		public string med_chrgitm_type { get; set; }

        /// <summary>
        /// 9|收费项目等级|字符型|3|Y||
        /// </summary> 
		public string chrgitm_lv { get; set; }

        /// <summary>
        /// 10|限制使用标志|字符型|3|Y||
        /// </summary> 
		public string lmt_used_flag { get; set; }

        /// <summary>
        /// 11|目录类别|字符型|30|||
        /// </summary> 
		public string list_type { get; set; }

        /// <summary>
        /// 12|医疗使用标志|字符型|3|Y||
        /// </summary> 
		public string med_use_flag { get; set; }

        /// <summary>
        /// 13|生育使用标志|字符型|3|Y||
        /// </summary> 
		public string matn_used_flag { get; set; }

        /// <summary>
        /// 14|医保目录使用类别|字符型|3|Y||
        /// </summary> 
		public string hilist_use_type { get; set; }

        /// <summary>
        /// 15|限复方使用类型|字符型|3|Y||
        /// </summary> 
		public string lmt_cpnd_type { get; set; }

        /// <summary>
        /// 17|有效标志|字符型|3|Y||
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 18|updt_time|日期型|||Y|
        /// </summary> 
		public string updt_time { get; set; }

        /// <summary>
        /// 19|page_num|数值型|4||Y|
        /// </summary> 
		public string page_num { get; set; }

        /// <summary>
        /// 20|page_size|数值型|4||Y|
        /// </summary> 
		public string page_size { get; set; }
    }
}
