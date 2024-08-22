using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.SQL
{
    public class Drjk_rymtbba_input:SqlBase
    {
       
        /// <summary>
        /// 状态 1 正常 0作废
        /// </summary>
        public int zt { get; set; }
        /// <summary>
        /// 状态操作员
        /// </summary>
        public string zt_czy { get; set; }
        /// <summary>
        /// 状态日期
        /// </summary>
        public DateTime zt_rq { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string czydm { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime czrq { get; set; }

        public string trt_dcla_detl_sn { get; set; }

        /// <summary>
        /// 1|人员编号|字符型|30|  |Y  |  
        /// </summary> 
        public string psn_no { get; set; }

        /// <summary>
        /// 2|险种类型|字符型|6|Y|Y  |  
        /// </summary> 
		public string insutype { get; set; }

        /// <summary>
        /// 3|门慢门特病种目录代码|字符型|30|  |Y  |
        /// </summary> 
		public string opsp_dise_code { get; set; }

        /// <summary>
        /// 4|门慢门特病种名称|字符型|300|  |Y  |  
        /// </summary> 
		public string opsp_dise_name { get; set; }

        /// <summary>
        /// 5|联系电话|字符型|50|  |  |  
        /// </summary> 
		public string tel { get; set; }

        /// <summary>
        /// 6|联系地址|字符型|200|  |  |  
        /// </summary> 
		public string addr { get; set; }

        /// <summary>
        /// 7|参保机构医保区划|字符型|6|  |Y  |  
        /// </summary> 
		public string insu_optins { get; set; }

        /// <summary>
        /// 8|鉴定定点医药机构编号|字符型|30|  |Y  |  
        /// </summary> 
		public string ide_fixmedins_no { get; set; }

        /// <summary>
        /// 9|鉴定定点医药机构名称|字符型|200|  |Y  |  
        /// </summary> 
		public string ide_fixmedins_name { get; set; }

        /// <summary>
        /// 10|医院鉴定日期|日期型|  |  |Y  |  
        /// </summary> 
		public string hosp_ide_date { get; set; }

        /// <summary>
        /// 11|诊断医师编码|字符型|30|  |Y  |  
        /// </summary> 
		public string diag_dr_codg { get; set; }

        /// <summary>
        /// 12|诊断医师姓名|字符型|50|  |Y  |  
        /// </summary> 
		public string diag_dr_name { get; set; }

        /// <summary>
        /// 13|开始日期|日期型|  |  |Y  |  
        /// </summary> 
		public string begndate { get; set; }

        /// <summary>
        /// 15|结束日期|日期型|  |  |  |  
        /// </summary> 
		public string enddate { get; set; }


    }
}
