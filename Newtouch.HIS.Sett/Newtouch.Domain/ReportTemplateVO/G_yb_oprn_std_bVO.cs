using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// 手术操作分类和代码表
    /// </summary>
    public class G_yb_oprn_std_bVO
    {
        //	手术标准目录ID				
        public string OPRN_STD_LIST_ID { get; set; }
        //	版本名称				
        public string VER_NAME { get; set; }
        //	章				
        public string CPR { get; set; }
        //	章代码范围				
        public string CPR_CODE_SCP { get; set; }
        //	章名称				
        public string CPR_NAME { get; set; }
        //	类目代码				
        public string CGY_CODE { get; set; }
        //	类目名称				
        public string CGY_NAME { get; set; }
        //	亚目代码				
        public string SOR_CODE { get; set; }
        //	亚目名称				
        public string SOR_NAME { get; set; }
        //	细目代码				
        public string DTLS_CODE { get; set; }
        //	细目名称				
        public string DTLS_NAME { get; set; }
        //	手术操作代码				
        public string OPRN_OPRT_CODE { get; set; }
        //	手术操作名称				
        public string OPRN_OPRT_NAME { get; set; }
        //	使用标记				
        public string USED_STD { get; set; }
        //	团标版手术操作代码				
        public string RTL_OPRN_OPRT_CODE { get; set; }
        //	团标版手术操作名称				
        public string RTL_OPRN_OPRT_NAME { get; set; }
        //	临床版手术操作代码				
        public string CLNC_OPRN_OPRT_CODE { get; set; }
        //	临床版手术操作名称				
        public string CLNC_OPRN_OPRT_NAME { get; set; }
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
        ////	创建人ID				
        //public string CRTER_ID { get; set; }
        ////	创建人姓名				
        //public string CRTER_NAME { get; set; }
        ////	创建机构编号				
        //public string CRTE_OPTINS_NO { get; set; }
        ////	经办人ID				
        //public string OPTER_ID { get; set; }
        ////	经办人姓名				
        //public string OPTER_NAME { get; set; }
        ////	经办时间				
        //public DateTime OPT_TIME { get; set; }
        ////	经办机构编号				
        //public string OPTINS_NO { get; set; }
        //	版本号				
        public string VER { get; set; }
        //	下发标志				
        //public string ISU_FLAG { get; set; }
        ////	传输数据ID				
        //public string TRAM_DATA_ID { get; set; }
        ////	生效时间				
        //public DateTime? EFFT_TIME { get; set; }
        ////	失效时间				
        //public DateTime? INVD_TIME { get; set; }

    }
}
