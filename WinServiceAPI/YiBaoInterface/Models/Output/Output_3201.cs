using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_3201 : OutputBase
    {
        public stmtinfo stmtinfo { get; set; }
    }

    public class stmtinfo
    {
        /// <summary>
        /// 1|结算经办机构|字符型|6|  |  |  
        /// </summary> 
        public string setl_optins { get; set; }

        /// <summary>
        /// 2|对账结果 |字符型|6|Y|Y  |  
        /// </summary> 
		public string stmt_rslt { get; set; }

        /// <summary>
        /// 3|对账结果说明|字符型|200|||
        /// </summary> 
		public string stmt_rslt_dscr { get; set; }
    }
}
