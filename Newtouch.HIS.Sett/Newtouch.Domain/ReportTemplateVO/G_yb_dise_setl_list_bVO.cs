using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// 医保按病种结算目录表
    /// </summary>
    public class G_yb_dise_setl_list_bVO
    {
        //	病种结算目录ID				
        public string DISE_SETL_LIST_ID { get; set; }
        //	按病种结算病种目录代码				
        public string BYDISE_SETL_LIST_CODE { get; set; }
        //	按病种结算病种名称				
        public string BYDISE_SETL_DISE_NAME { get; set; }
        //	限定手术操作代码				
        public string QUA_OPRN_OPRT_CODE { get; set; }
        //	限定手术操作名称				
        public string QUA_OPRN_OPRT_NAME { get; set; }
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
        //	病种内涵				
        public string DISE_CONT { get; set; }
        //	备注				
        public string MEMO { get; set; }
        //	版本名称				
        public string VER_NAME { get; set; }
        //	诊疗指南页码				
        public string TRT_GUIDE_PAGEN { get; set; }
        //	诊疗指南电子档案				
        public string TRT_GUIDE_ELECACS { get; set; }
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
