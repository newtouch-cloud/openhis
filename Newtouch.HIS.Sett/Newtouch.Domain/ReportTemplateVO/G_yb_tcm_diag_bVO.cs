using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// 中医诊断表
    /// </summary>
    public class G_yb_tcm_diag_bVO
    {
        //	中医诊断ID				
        public string TCM_DIAG_ID { get; set; }
        //	版本名称				
        public string VER_NAME { get; set; }
        //	科别类目代码				
        public string CATY_CGY_CODE { get; set; }
        //	科别类目名称				
        public string CATY_CGY_NAME { get; set; }
        //	专科系统分类目代码				
        public string SPCY_SYS_TAXA_CODE { get; set; }
        //	专科系统分类目名称				
        public string SPCY_SYS_TAXA_NAME { get; set; }
        //	病种类型代码				
        public string DISE_TYPE_CODE { get; set; }
        //	病种类型名称				
        public string DISE_TYPE_NAME { get; set; }
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
        ////	下发标志				
        //public string ISU_FLAG { get; set; }
        ////	传输数据ID				
        //public string TRAM_DATA_ID { get; set; }
        ////	生效时间				
        //public DateTime? EFFT_TIME { get; set; }
        ////	失效时间				
        //public DateTime? INVD_TIME { get; set; }

    }
}
