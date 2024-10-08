﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_1318:OutputBase
    {
        public string firstPage { get; set; }
        public string lastPage { get; set; }
        public string size { get; set; }
        public string startRow { get; set; }
        public string pageSize { get; set; }
        public string pageNum { get; set; }
        public string recordCounts { get; set; }
        public string pages { get; set; }
        public List<dataOuput1318> data { get; set; }
    }
   public class dataOuput1318
    {

        /// <summary>
        /// 1|医保目录编码|字符型|30|  |Y|  
        /// </summary> 
        public string hilist_code { get; set; }

        /// <summary>
        /// 2|医保目录限价类型|字符型|6|Y|Y|  
        /// </summary> 
		public string hilist_lmtpric_type { get; set; }

        /// <summary>
        /// 3|医保目录超限处理方式|字符型|6|Y|Y|  
        /// </summary> 
		public string overlmt_dspo_way { get; set; }

        /// <summary>
        /// 4|参保机构医保区划|字符型|6|Y|Y|  
        /// </summary> 
		public string insu_admdvs { get; set; }

        /// <summary>
        /// 5|开始日期|日期型|  |  |Y|  
        /// </summary> 
		public string begndate { get; set; }

        /// <summary>
        /// 6|结束日期|日期型|  |  |N|  
        /// </summary> 
		public string enddate { get; set; }

        /// <summary>
        /// 7|医保目录定价上限金额|BigDecima l|16|  |Y|  
        /// </summary> 
		public string hilist_pric_uplmt_amt { get; set; }

        /// <summary>
        /// 8|有效标志|字符型|3|Y|Y|  
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 9|唯一记录号|字符型|40|  |Y|  
        /// </summary> 
		public string rid { get; set; }

        /// <summary>
        /// 10|更新时间|日期型|  |  |Y|  
        /// </summary> 
		public string updt_time { get; set; }

        /// <summary>
        /// 11|创建人|字符型|20|  |N|  
        /// </summary> 
		public string crter_id { get; set; }

        /// <summary>
        /// 12|创建人姓名|字符型|50|  |N|  
        /// </summary> 
		public string crter_name { get; set; }

        /// <summary>
        /// 13|创建时间|日期型|  |  |Y|  
        /// </summary> 
		public string crte_time { get; set; }

        /// <summary>
        /// 14|创建机构|字符型|20|  |N|  
        /// </summary> 
		public string crte_optins_no { get; set; }

        /// <summary>
        /// 15|经办人|字符型|20|  |N|  
        /// </summary> 
		public string opter_id { get; set; }

        /// <summary>
        /// 16|经办人姓名|字符型|50|  |N|  
        /// </summary> 
		public string opter_name { get; set; }

        /// <summary>
        /// 17|经办时间|日期型|  |  |N|  
        /// </summary> 
		public string opt_time { get; set; }

        /// <summary>
        /// 18|经办机构|字符型|20|  |N|  
        /// </summary> 
		public string optins_no { get; set; }

        /// <summary>
        /// 19|表名|字符型|100|  |N|  
        /// </summary> 
		public string tabname { get; set; }

        /// <summary>
        /// 20|统筹区|字符型|6|  |N|  
        /// </summary> 
		public string poolarea_no { get; set; }

    }
}
