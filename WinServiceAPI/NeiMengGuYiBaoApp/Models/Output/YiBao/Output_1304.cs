using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_1304:OutputBase
    {
        public string firstPage { get; set; }
        public string lastPage { get; set; }
        public string size { get; set; }
        public string startRow { get; set; }
        public string pageSize { get; set; }
        public string pageNum { get; set; }
        public string recordCounts { get; set; }
        public string pages { get; set; }
        public List<dataOuput1304> data { get; set; }
    }
   public class dataOuput1304
    {

        /// <summary>
        /// 1|med_list_codg|字符型|50||Y|
        /// </summary> 
        public string med_list_codg { get; set; }

        /// <summary>
        /// 2|drug_prodname|字符型|500|||  
        /// </summary> 
		public string drug_prodname { get; set; }

        /// <summary>
        /// 3|genname_codg|字符型|20|||
        /// </summary> 
		public string genname_codg { get; set; }

        /// <summary>
        /// 4|drug_genname|字符型|500|||
        /// </summary> 
		public string drug_genname { get; set; }

        /// <summary>
        /// 5|ethdrug_type|字符型|3|Y||
        /// </summary> 
		public string ethdrug_type { get; set; }

        /// <summary>
        /// 6|chemname|字符型|100|||
        /// </summary> 
		public string chemname { get; set; }

        /// <summary>
        /// 7|alis|字符型|100|||
        /// </summary> 
		public string alis { get; set; }

        /// <summary>
        /// 8|eng_name|字符型|500|||
        /// </summary> 
		public string eng_name { get; set; }

        /// <summary>
        /// 9|dosform|字符型|500|||
        /// </summary> 
		public string dosform { get; set; }

        /// <summary>
        /// 10|each_dos|字符型||||
        /// </summary> 
		public string each_dos { get; set; }

        /// <summary>
        /// 11|used_frqu|字符型|30|||
        /// </summary> 
		public string used_frqu { get; set; }

        /// <summary>
        /// 12|nat_drug_no|字符型|20|||
        /// </summary> 
		public string nat_drug_no { get; set; }

        /// <summary>
        /// 13|used_mtd|字符型||||
        /// </summary> 
		public string used_mtd { get; set; }

        /// <summary>
        /// 14|ing|字符型|500|||
        /// </summary> 
		public string ing { get; set; }

        /// <summary>
        /// 15|chrt|字符型|500|||
        /// </summary> 
		public string chrt { get; set; }

        /// <summary>
        /// 16|defs|字符型|1000|||
        /// </summary> 
		public string defs { get; set; }

        /// <summary>
        /// 17|tabo|字符型|500|||
        /// </summary> 
		public string tabo { get; set; }

        /// <summary>
        /// 18|mnan|字符型|500|||
        /// </summary> 
		public string mnan { get; set; }

        /// <summary>
        /// 19|stog|字符型|500|||
        /// </summary> 
		public string stog { get; set; }

        /// <summary>
        /// 20|drug_spec|字符型|500|||
        /// </summary> 
		public string drug_spec { get; set; }

        /// <summary>
        /// 21|prcunt_type|字符型|3|Y||
        /// </summary> 
		public string prcunt_type { get; set; }

        /// <summary>
        /// 22|otc_flag|字符型|3|Y||
        /// </summary> 
		public string otc_flag { get; set; }

        /// <summary>
        /// 23|pacmatl|字符型|500|||
        /// </summary> 
		public string pacmatl { get; set; }

        /// <summary>
        /// 24|pacspec|字符型|3|||
        /// </summary> 
		public string pacspec { get; set; }

        /// <summary>
        /// 25|min_useunt|字符型|30|||
        /// </summary> 
		public string min_useunt { get; set; }

        /// <summary>
        /// 26|min_salunt|字符型|30|||
        /// </summary> 
		public string min_salunt { get; set; }

        /// <summary>
        /// 27|manl|字符型|2000|||
        /// </summary> 
		public string manl { get; set; }

        /// <summary>
        /// 28|rute|字符型|30|||
        /// </summary> 
		public string rute { get; set; }

        /// <summary>
        /// 29|begndate|日期型||||
        /// </summary> 
		public string begndate { get; set; }

        /// <summary>
        /// 30|enddate|日期型||||
        /// </summary> 
		public string enddate { get; set; }

        /// <summary>
        /// 31|pham_type|字符型|40|||
        /// </summary> 
		public string pham_type { get; set; }

        /// <summary>
        /// 32|memo|字符型|500|||
        /// </summary> 
		public string memo { get; set; }

        /// <summary>
        /// 33|pac_cnt|字符型|20|||
        /// </summary> 
		public string pac_cnt { get; set; }

        /// <summary>
        /// 34|min_unt|字符型|30|||
        /// </summary> 
		public string min_unt { get; set; }

        /// <summary>
        /// 35|min_pac_cnt|数值型|20|||
        /// </summary> 
		public string min_pac_cnt { get; set; }

        /// <summary>
        /// 36|min_pacunt|字符型|30|||
        /// </summary> 
		public string min_pacunt { get; set; }

        /// <summary>
        /// 37|min_prepunt|字符型|30|||
        /// </summary> 
		public string min_prepunt { get; set; }

        /// <summary>
        /// 38|drug_expy|字符型|100|||
        /// </summary> 
		public string drug_expy { get; set; }

        /// <summary>
        /// 39|efcc_atd|字符型||||
        /// </summary> 
		public string efcc_atd { get; set; }

        /// <summary>
        /// 40|min_prcunt|字符型|50|||
        /// </summary> 
		public string min_prcunt { get; set; }

        /// <summary>
        /// 41|wubi|字符型|30|||
        /// </summary> 
		public string wubi { get; set; }

        /// <summary>
        /// 42|pinyin|字符型|30|||
        /// </summary> 
		public string pinyin { get; set; }

        /// <summary>
        /// 43|vali_flag|字符型|3|Y|Y|
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 44|rid|字符型|40||Y|
        /// </summary> 
		public string rid { get; set; }

        /// <summary>
        /// 45|crte_time|日期型|||Y|
        /// </summary> 
		public string crte_time { get; set; }

        /// <summary>
        /// 46|updt_time|日期型|||Y|
        /// </summary> 
		public string updt_time { get; set; }

        /// <summary>
        /// 47|crter_id|字符型|20||Y|
        /// </summary> 
		public string crter_id { get; set; }

        /// <summary>
        /// 48|crter_name|字符型|50||Y|
        /// </summary> 
		public string crter_name { get; set; }

        /// <summary>
        /// 49|crte_optins_no|字符型|20||Y|
        /// </summary> 
		public string crte_optins_no { get; set; }

        /// <summary>
        /// 50|opter_id|字符型|20||Y|
        /// </summary> 
		public string opter_id { get; set; }

        /// <summary>
        /// 51|opter_name|字符型|50||Y|
        /// </summary> 
		public string opter_name { get; set; }

        /// <summary>
        /// 52|opt_time|日期型|||Y|
        /// </summary> 
		public string opt_time { get; set; }

        /// <summary>
        /// 53|optins_no|字符型|20||Y|
        /// </summary> 
		public string optins_no { get; set; }

        /// <summary>
        /// 54|ver|字符型|20||Y|
        /// </summary> 
		public string ver { get; set; }

    }
}
