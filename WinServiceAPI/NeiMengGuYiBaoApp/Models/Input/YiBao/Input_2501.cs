using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_2501 : InputBase
    {
        public refmedin refmedin { get; set; }
    }

    public class refmedin
    {
        /// <summary>
        /// 1|人员编号|字符型|30|  |Y|  
        /// </summary> 
        public string psn_no { get; set; }

        /// <summary>
        /// 2|险种类型|字符型|6|Y|  |  
        /// </summary> 
		public string insutype { get; set; }

        /// <summary>
        /// 3|联系电话|字符型|50|  |  |  
        /// </summary> 
		public string tel { get; set; }

        /// <summary>
        /// 4|联系地址|字符型|200|  |  |  
        /// </summary> 
		public string addr { get; set; }

        /// <summary>
        /// 5|参保机构医保区划|字符型|6||  |  
        /// </summary> 
		public string insu_optins { get; set; }

        /// <summary>
        /// 6|诊断代码|字符型|20|  |Y  |  
        /// </summary> 
		public string diag_code { get; set; }

        /// <summary>
        /// 7|诊断名称|字符型|100|  |Y  |  
        /// </summary> 
		public string diag_name { get; set; }

        /// <summary>
        /// 8|疾病病情描述|字符型|2000|  |  |  
        /// </summary> 
		public string dise_cond_dscr { get; set; }

        /// <summary>
        /// 9|转往定点医药机构编号|字符型|12|  |Y  |通过【1201】交易获取医药机构管理码
        /// </summary> 
		public string reflin_medins_no { get; set; }

        /// <summary>
        /// 10|转往医院名称|字符型|200|  |Y|  
        /// </summary> 
		public string reflin_medins_name { get; set; }

        /// <summary>
        /// 11|就医地行政区划|字符型|6||Y|转往医院所属的行政区划
        /// </summary> 
		public string mdtrtarea_admdvs { get; set; }

        /// <summary>
        /// 12|医院同意转院标志|字符型|3|Y||
        /// </summary> 
        public string hosp_agre_refl_flag { get; set; }

        /// <summary>
        /// 13|转院类型|字符型|30|Y|Y|
        /// </summary> 
        public string refl_type { get; set; }

        /// <summary>
        /// 14|转院日期|日期型|  |  |Y|yyyy-MM-dd
        /// </summary> 
        public string refl_date { get; set; }

        /// <summary>
        /// 15|转院原因|字符型|100|  |Y|
        /// </summary> 
        public string refl_rea { get; set; }

        /// <summary>
        /// 16|转院意见|字符型|200||Y|
        /// </summary> 
        public string refl_opnn { get; set; }

        /// <summary>
        /// 17|开始日期|日期型|  |  ||yyyy-MM-dd
        /// </summary> 
        public string begndate { get; set; }

        /// <summary>
        /// 18|结束日期|日期型|  |  |  |yyyy-MM-dd
        /// </summary> 
        public string enddate { get; set; }

        /// <summary>
        /// 19|转诊使用标志|字符型|3|||
        /// </summary> 
        public string refl_used_flag { get; set; }

    }
}
