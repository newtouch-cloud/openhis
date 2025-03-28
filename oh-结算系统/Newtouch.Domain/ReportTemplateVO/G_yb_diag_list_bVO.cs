using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// 诊断目录表
    /// </summary>
    public class G_yb_diag_list_bVO
    {
        //	诊断目录ID				
        public string DIAG_LIST_ID { get; set; }
        
        //	章				
        public string CPR { get; set; }
        //	章代码范围				
        public string CPR_CODE_SCP { get; set; }
        //	章名称				
        public string CPR_NAME { get; set; }
        //	节代码范围				
        public string SEC_CODE_SCP { get; set; }
        //	节名称				
        public string SEC_NAME { get; set; }
        //	类目代码				
        public string CGY_CODE { get; set; }
        //	类目名称				
        public string CGY_NAME { get; set; }
        //	亚目代码				
        public string SOR_CODE { get; set; }
        //	亚目名称				
        public string SOR_NAME { get; set; }
        //	诊断代码				
        public string DIAG_CODE { get; set; }
        //	诊断名称				
        public string DIAG_NAME { get; set; }
        //	使用标记				
        public string USED_STD { get; set; }
        //	国标版诊断代码				
        public string NATSTD_DIAG_CODE { get; set; }
        //	国标版诊断名称				
        public string NATSTD_DIAG_NAME { get; set; }
        //	临床版诊断代码				
        public string CLNC_DIAG_CODE { get; set; }
        //	临床版诊断名称				
        public string CLNC_DIAG_NAME { get; set; }
        //	备注				
        public string MEMO { get; set; }
        //	有效标志				
        public string VALI_FLAG { get; set; }
        //	数据唯一记录号				
        public string RID { get; set; }
        //	数据创建时间				
        public DateTime CRTE_TIME { get; set; }
        //	数据更新时间				
        public DateTime UPDT_TIME { get; set; }
        
        //	版本号				
        public string VER { get; set; }
        //	版本名称				
        public string VER_NAME { get; set; }
        

    }
}
