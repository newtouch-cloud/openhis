using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_1317:OutputBase
    {
        public string firstPage { get; set; }
        public string lastPage { get; set; }
        public string size { get; set; }
        public string startRow { get; set; }
        public string pageSize { get; set; }
        public string pageNum { get; set; }
        public string recordCounts { get; set; }
        public string pages { get; set; }
        public List<dataOut1317> data { get; set; }
    }
    public class dataOut1317
    {
        public string begndate { get; set; }
        public string list_type { get; set; }
        public string insu_admdvs { get; set; }
        public string pages { get; set; }
        public string med_list_codg { get; set; }

        public string opt_time { get; set; }
        public string vali_flag { get; set; }
        public string hilist_code { get; set; }
        public string memo { get; set; }
        public string rid { get; set; }
        public string updt_time { get; set; }
        /*
                /// <summary>
                /// 1|定点医药机构编号|字符型|30|  |Y|  
                /// </summary> 
                public string fixmedins_code { get; set; }

                /// <summary>
                /// 2|定点医药机构目录编号|字符型|30|  |Y|  
                /// </summary> 
                public string medins_list_codg { get; set; }

                /// <summary>
                /// 3|定点医药机构目录名称|字符型|200|  |N|  
                /// </summary> 
                public string medins_list_name { get; set; }

                /// <summary>
                /// 4|参保机构医保区划|字符型|6|Y  |Y|  
                /// </summary> 
                public string insu_admdvs { get; set; }

                /// <summary>
                /// 5|目录类别|字符型|3|Y  |Y|  
                /// </summary> 
                public string list_type { get; set; }

                /// <summary>
                /// 6|医疗目录编码|字符型|30|  |Y|  
                /// </summary> 
                public string med_list_codg { get; set; }

                /// <summary>
                /// 7|开始日期|日期型|  |  |Y|  
                /// </summary> 
                public string begndate { get; set; }

                /// <summary>
                /// 8|结束日期|日期型|  |  |N|  
                /// </summary> 
                public string enddate { get; set; }

                /// <summary>
                /// 9|批准文号|字符型|30|  |N|  
                /// </summary> 
                public string aprvno { get; set; }

                /// <summary>
                /// 10|剂型|字符型|200|  |N|  
                /// </summary> 
                public string dosform { get; set; }

                /// <summary>
                /// 11|除外内容|字符型|2000|  |N|  
                /// </summary> 
                public string exct_cont { get; set; }

                /// <summary>
                /// 12|项目内涵|字符型|2000|  |N|  
                /// </summary> 
                public string item_cont { get; set; }

                /// <summary>
                /// 13|计价单位|字符型|100|  |N|  
                /// </summary> 
                public string prcunt { get; set; }

                /// <summary>
                /// 14|规格|字符型|200|  |N|  
                /// </summary> 
                public string spec { get; set; }

                /// <summary>
                /// 15|包装规格|字符型|100|  |N|  
                /// </summary> 
                public string pacspec { get; set; }

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
                public string poolarea_no { get; set; }*/

    }
    
}
