using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_1316 : OutputBase
    {
        //       "firstPage": true,
        //	"lastPage": false,
        //	"size": 10,
        //	"startRow": 1,
        //	"endRow": 10,
        //	"pageSize": 10,
        //	"pageNum": 1

        //       	"recordCounts": 101182,
        //"pages": 10119,
        //"data": [{

        public string firstPage { get; set; }
        public string lastPage { get; set; }
        public string size { get; set; }
        public string startRow { get; set; }
        public string pageSize { get; set; }
        public string pageNum { get; set; }
        public string recordCounts { get; set; }
        public string pages { get; set; }
        public List<dataOuput1316> data { get; set; }
    }


    public class dataOuput1316
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
        //实际返回
        //   "begndate": "2000-01-01 00:00:00",
        //"list_type": "201",
        //"insu_admdvs": "500000",
        //"med_list_codg": "001102000000100-110200002.10",
        //"opt_time": 1620288730000,
        //"crte_time": "2021-05-06 16:12:10",
        //"vali_flag": "1",
        //"hilist_code": "001102000000100-110200002.10",
        //"memo": "1、专家要求：具有20年以上工作经验，同时具备以下条件之一者：(1)被国务院授予“有突出贡献的中青年专家”；(2)享受“国务院政府特殊津贴”的专家；(3)博士生导师；(4)重庆市重点学科带头人(包括曾经担任过)；(5)经国家审定的老中医专家学术经验继承指导老师；2、专家门诊诊疗室必须设在相对独立的诊区；3、对个别优秀专家或具有优异就诊环境的可申请单独定价",
        //"rid": "ZG20210529224641066940100000044172357378",
        //"updt_time": "2021-05-06 16:12:10"


        /*
        /// <summary>
        /// 1|医疗目录编码|字符型|30|  |Y|  
        /// </summary> 
        public string med_list_codg { get; set; }

        /// <summary>
        /// 2|医保目录编码|字符型|30|  |Y|  
        /// </summary> 
		public string hilist_code { get; set; }

        /// <summary>
        /// 3|目录类别|字符型|3|Y  |Y|  
        /// </summary> 
		public string list_type { get; set; }

        /// <summary>
        /// 4|参保机构医保区划|字符型|6|Y  |Y|  
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
        /// 7|备注|字符型|500|  |N|  
        /// </summary> 
		public string memo { get; set; }

        /// <summary>
        /// 8|有效标志|字符型|3|Y  |Y|  
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
        /// 19|统筹区|字符型|6|Y  |N|  
        /// </summary> 
		public string poolarea_no { get; set; }
        */
    }
}
