using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_1312:OutputBase
    {
        public string firstPage { get; set; }
        public string lastPage { get; set; }
        public string size { get; set; }
        public string startRow { get; set; }
        public string pageSize { get; set; }
        public string pageNum { get; set; }
        public string recordCounts { get; set; }
        public string pages { get; set; }
        public List<dataOutput1312> data { get; set; }
    }
    public class dataOutput1312
    {

        /// <summary>
        /// 1|医保目录编码|字符型|30|  |Y|  
        /// </summary> 
        public string hilist_code { get; set; }

        /// <summary>
        /// 2|医保目录名称|字符型|200|  |Y|  
        /// </summary> 
		public string hilist_name { get; set; }

        /// <summary>
        /// 3|参保机构医保区划|字符型|6|Y  |Y|  
        /// </summary> 
		public string insu_admdvs { get; set; }

        /// <summary>
        /// 4|开始日期|日期型|  |  |Y|  
        /// </summary> 
		public string begndate { get; set; }

        /// <summary>
        /// 5|结束日期|日期型|  |  |N|  
        /// </summary> 
		public string enddate { get; set; }

        /// <summary>
        /// 6|医疗收费项目类别|字符型|6|Y  |Y|  
        /// </summary> 
		public string med_chrgitm_type { get; set; }

        /// <summary>
        /// 7|收费项目等级|字符型|3|Y  |Y|  
        /// </summary> 
		public string chrgitm_lv { get; set; }

        /// <summary>
        /// 8|限制使用标志|字符型|3|Y  |Y|  
        /// </summary> 
		public string lmt_used_flag { get; set; }

        /// <summary>
        /// 9|目录类别|字符型|3|Y  |Y|  
        /// </summary> 
		public string list_type { get; set; }

        /// <summary>
        /// 10|医疗使用标志|字符型|3|Y  |Y|  
        /// </summary> 
		public string med_use_flag { get; set; }

        /// <summary>
        /// 11|生育使用标志|字符型|3|Y  |Y|  
        /// </summary> 
		public string matn_used_flag { get; set; }

        /// <summary>
        /// 12|医保目录使用类别|字符型|3|Y  |Y|  
        /// </summary> 
		public string hilist_use_type { get; set; }

        /// <summary>
        /// 13|限复方使用类型|字符型|3|Y  |Y|  
        /// </summary> 
		public string lmt_cpnd_type { get; set; }

        /// <summary>
        /// 14|五笔助记码|字符型|30|  |N|  
        /// </summary> 
		public string wubi { get; set; }

        /// <summary>
        /// 15|拼音助记码|字符型|30|  |N|  
        /// </summary> 
		public string pinyin { get; set; }

        /// <summary>
        /// 16|备注|字符型|500|  |N|  
        /// </summary> 
		public string memo { get; set; }

        /// <summary>
        /// 17|有效标志|字符型|3|Y  |Y|  
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 18|唯一记录号|字符型|40|  |Y|  
        /// </summary> 
		public string rid { get; set; }

        /// <summary>
        /// 19|更新时间|日期型|  |  |Y|  
        /// </summary> 
		public string updt_time { get; set; }

        /// <summary>
        /// 20|创建人|字符型|20|  |N|  
        /// </summary> 
		public string crter_id { get; set; }

        /// <summary>
        /// 21|创建人姓名|字符型|50|  |N|  
        /// </summary> 
		public string crter_name { get; set; }

        /// <summary>
        /// 22|创建时间|日期型|  |  |Y|  
        /// </summary> 
		public string crte_time { get; set; }

        /// <summary>
        /// 23|创建机构|字符型|20|  |N|  
        /// </summary> 
		public string crte_optins_no { get; set; }

        /// <summary>
        /// 24|经办人|字符型|20|  |N|  
        /// </summary> 
		public string opter_id { get; set; }

        /// <summary>
        /// 25|经办人姓名|字符型|50|  |N|  
        /// </summary> 
		public string opter_name { get; set; }

        /// <summary>
        /// 26|经办时间|日期型|  |  |N|  
        /// </summary> 
		public string opt_time { get; set; }

        /// <summary>
        /// 27|经办机构|字符型|20|  |N|  
        /// </summary> 
		public string optins_no { get; set; }

        /// <summary>
        /// 28|统筹区|字符型|6|Y  |N|  
        /// </summary> 
		public string poolarea_no { get; set; }

    }
}
