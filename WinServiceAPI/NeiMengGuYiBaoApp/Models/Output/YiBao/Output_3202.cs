using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_3202 : OutputBase
    {
        public fileinfo fileinfo { get; set; }
    }

    public class fileinfo
    {
        /// <summary>
        /// 1|文件查询号|字符型|30||Y  |用于下载明细对账结果文件
        /// </summary> 
        public string file_qury_no { get; set; }

        /// <summary>
        /// 2|文件名称|字符型|200||Y|  
        /// </summary> 
        public string filename { get; set; }

        /// <summary>
        /// 3|下载截止时间|日期时间型|||Y|yyyy-MM-dd HH:mm:ss
        /// </summary> 
        public string dld_endtime { get; set; }
    }

	public class result
	{
		public string psn_no { get; set; }
		public string mdtrt_id { get; set; }
		public string setl_id { get; set; }
		public string msgid { get; set; }
		public string stmt_rslt { get; set; }
		public string refd_setl_flag { get; set; }
		public string memo { get; set; }
		public decimal medfee_sumamt { get; set; }
		public decimal fund_pay_sumamt { get; set; }
		public decimal acct_pay { get; set; }
	}
}
