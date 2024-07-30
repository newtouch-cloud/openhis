using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_1304:InputBase
    {
        public dataInput1314 data { get; set; }
    }
    public class dataInput1314
    {

        /// <summary>
        /// 1|med_list_codg|字符型|20|||
        /// </summary> 
        public string med_list_codg { get; set; }

        /// <summary>
        /// 2|genname_codg|字符型|100|||
        /// </summary> 
		public string genname_codg { get; set; }

        /// <summary>
        /// 3|drug_genname|字符型|20|||
        /// </summary> 
		public string drug_genname { get; set; }

        /// <summary>
        /// 4|drug_prodname|字符型|500|||
        /// </summary> 
		public string drug_prodname { get; set; }

        /// <summary>
        /// 5|reg_name|字符型|500|||
        /// </summary> 
		public string reg_name { get; set; }

        /// <summary>
        /// 6|tcmherb_name|字符型|30|||
        /// </summary> 
		public string tcmherb_name { get; set; }

        /// <summary>
        /// 7|mlms_name|字符型|255|||
        /// </summary> 
		public string mlms_name { get; set; }

        /// <summary>
        /// 8|vali_flag|字符型|3|Y||
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 9|rid|字符型|30|||
        /// </summary> 
		public string rid { get; set; }

        /// <summary>
        /// 10|ver|字符型|20|||
        /// </summary> 
		public string ver { get; set; }

        /// <summary>
        /// 11|ver_name|字符型|30|||
        /// </summary> 
		public string ver_name { get; set; }

        /// <summary>
        /// 12|opt_begn_time|日期型||||
        /// </summary> 
		public string opt_begn_time { get; set; }

        /// <summary>
        /// 13|opt_end_time|日期型||||
        /// </summary> 
		public string opt_end_time { get; set; }

        /// <summary>
        /// 14|updt_time|日期型|||Y|
        /// </summary> 
		public string updt_time { get; set; }

        /// <summary>
        /// 15|page_num|数值型|4||Y|
        /// </summary> 
		public string page_num { get; set; }

        /// <summary>
        /// 16|page_size|数值型|4||Y|
        /// </summary> 
        public string page_size { get; set; }

    }
    public class CatalogRequest
    {
        public string tradiNumber { get; set; }
        public string operatorId { get; set; }
        public string operatorName { get; set; }
        public string drug_prodname { get; set; }
        public string vali_flag { get; set; }
        public string page_num { get; set; }
        public string page_size { get; set; }
        public string updt_time { get; set; }
    }
    public class dataInput1314DtoV2 : InputBase
    {
        public dataInput1314Dto data { get; set; }
    }
    public class dataInput1314Dto
    {
        /// <summary>
        /// 1|med_list_codg|字符型|20|||
        /// </summary> 
        public string med_list_codg { get; set; }

        /// <summary>
        /// 2|genname_codg|字符型|100|||
        /// </summary> 
		public string genname_codg { get; set; }

        /// <summary>
        /// 3|drug_genname|字符型|20|||
        /// </summary> 
		public string drug_genname { get; set; }

        /// <summary>
        /// 4|drug_prodname|字符型|500|||
        /// </summary> 
		public string drug_prodname { get; set; }

        /// <summary>
        /// 5|reg_name|字符型|500|||
        /// </summary> 
		public string cpnd_flag { get; set; }

        /// <summary>
        /// 6|tcmherb_name|字符型|30|||
        /// </summary> 
		public string tcmherb_name { get; set; }


        /// <summary>
        /// 8|vali_flag|字符型|3|Y||
        /// </summary> 
		public string vali_flag { get; set; }

        /// <summary>
        /// 9|rid|字符型|30|||
        /// </summary> 
		public string rid { get; set; }

        /// <summary>
        /// 10|ver|字符型|20|||
        /// </summary> 
		public string ver { get; set; }

        /// <summary>
        /// 12|opt_begn_time|日期型||||
        /// </summary> 
		public string opt_begn_time { get; set; }

        /// <summary>
        /// 13|opt_end_time|日期型||||
        /// </summary> 
		public string opt_end_time { get; set; }

        /// <summary>
        /// 14|updt_time|日期型|||Y|
        /// </summary> 
		public string updt_time { get; set; }

        /// <summary>
        /// 15|page_num|数值型|4||Y|
        /// </summary> 
		public string page_num { get; set; }

        /// <summary>
        /// 16|page_size|数值型|4||Y|
        /// </summary> 
        public string page_size { get; set; }
    }
}
