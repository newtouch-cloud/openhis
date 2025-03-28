using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// 医疗服务项目表
    /// </summary>
    public class MedinsDaySetlResult
    {
        //	人员编号				
        public string psn_no { get; set; }
        //	医药机构结算ID				
        public string medinsSetlId { get; set; }
        //	医疗费总额				
        public string medfee_sumamt { get; set; }
        //	基金支付总额	
        public string fund_pay_sumamt { get; set; }
        //	个人账户支出	
        public string acct_pay { get; set; }
    }
}
