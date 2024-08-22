using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// 中草药信息表
    /// </summary>
    public class G_yb_tcmherb_info_bVO
    {
        //	医疗目录编码				
        public string MED_LIST_CODG { get; set; }
        //	中草药名称				
        public string TCMHERB_NAME { get; set; }
        //	单复方标志				
        public string CPND_FLAG { get; set; }
        //	质量等级				
        public string QLT_REG { get; set; }
        //	中草药年份				
        public string TCMHERB_YEAR { get; set; }
        //	药用部位				
        public string MED_PART { get; set; }
        //	安全计量				
        public string SAFE_MTR { get; set; }
        //	常规用法				
        public string CNVL_USED { get; set; }
        //	性味				
        public string NATFLA { get; set; }
        //	归经				
        public string MERTPM { get; set; }
        //	品种				
        public string CAT { get; set; }
        //	开始日期				
        public DateTime? BEGNDATE { get; set; }
        //	结束日期				
        public DateTime? ENDDATE { get; set; }
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
        //public DateTime? OPT_TIME { get; set; }
        ////	经办机构编号				
        //public string OPTINS_NO { get; set; }
        //	版本号				
        public string VER { get; set; }
        //	版本名称				
        public string VER_NAME { get; set; }
        //	药材名称				
        public string MLMS_NAME { get; set; }
        //	功能主治				
        public string EFCC_ATD { get; set; }
        //	炮制方法				
        public string PSDG_MTD { get; set; }
        //	功效分类				
        public string ECY_TYPE { get; set; }
        //	药材种来源				
        public string MLMS_CAT_SOUC { get; set; }
        //	国家医保支付政策				
        public string NAT_HI_PAY_POL { get; set; }
        //	省级医保支付政策				
        public string PROV_HI_PAY_POL { get; set; }
        //	标准名称				
        public string STD_NAME { get; set; }
        //	标准页码				
        public string STD_PAGEN { get; set; }
        //	标准电子档案				
        public string STD_ELECACS { get; set; }
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
