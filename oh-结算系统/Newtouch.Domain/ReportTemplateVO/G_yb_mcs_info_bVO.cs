using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// 耗材信息表
    /// </summary>
    public class G_yb_mcs_info_bVO
    {
        //	医疗目录编码				
        public string MED_LIST_CODG { get; set; }
        //	耗材名称				
        public string MCS_NAME { get; set; }
        //	医疗器械唯一标识码				
        public string UDI { get; set; }
        //	医保通用名代码				
        public string HI_GENNAME_CODE { get; set; }
        //	医保通用名				
        public string HI_GENNAME { get; set; }
        //	产品型号				
        public string PROD_MOL { get; set; }
        //	规格代码				
        public string SPEC_CODE { get; set; }
        //	规格				
        public string SPEC { get; set; }
        //	耗材分类				
        public string MCS_TYPE { get; set; }
        //	规格型号				
        public string SPEC_MOL { get; set; }
        //	材质代码				
        public string MATL_CODE { get; set; }
        //	耗材材质				
        public string MCS_MATL { get; set; }
        //	包装规格				
        public string PACSPEC { get; set; }
        //	包装数量				
        public string PAC_CNT { get; set; }
        //	产品包装材质				
        public string PROD_PACMATL { get; set; }
        //	包装单位				
        public string PACUNT { get; set; }
        //	产品转换比				
        public string PROD_CONVRAT { get; set; }
        //	最小使用单位				
        public string MIN_USEUNT { get; set; }
        //	生产地类别				
        public string PRODPLAC_TYPE { get; set; }
        //	生产地类别名称				
        public string PRODPLAC_TYPE_NAME { get; set; }
        //	产品标准				
        public string PRODSTRD { get; set; }
        //	产品有效期				
        public string PRODEXPY { get; set; }
        //	性能结构与组成				
        public string PERF_STRU_COMP { get; set; }
        //	适用范围				
        public string APB_SCP { get; set; }
        //	产品使用方法				
        public string PROD_ISTR { get; set; }
        //	产品图片编号				
        public string PROD_IMG_NO { get; set; }
        //	产品质量标准				
        public string PROD_QLT_STD { get; set; }
        //	说明书				
        public string MANL { get; set; }
        //	其他证明材料				
        public string OTH_CERT_MATL { get; set; }
        //	专机专用标志				
        public string SPED_FLAG { get; set; }
        //	专机名称				
        public string SPED_NAME { get; set; }
        //	组套名称				
        public string COMB_NAME { get; set; }
        //	机套标志				
        public string CASE_FLAG { get; set; }
        //	限制使用标志				
        public string LMT_USED_FLAG { get; set; }
        //	医保限用范围				
        public string HI_LMT_SCP { get; set; }
        //	最小销售单位				
        public string MIN_SALUNT { get; set; }
        //	高值耗材标志				
        public string HIGHVAL_MCS_FLAG { get; set; }
        //	医用材料分类代码				
        public string MATL_TYPE_CODE { get; set; }
        //	植入材料和人体器官标志				
        public string IMPT_MATL_HMORGN_FLAG { get; set; }
        //	灭菌标志				
        public string STLZ_FLAG { get; set; }
        //	灭菌标志名称				
        public string STLZ_FLAG_NAME { get; set; }
        //	植入或介入类标志				
        public string IMPT_ITVT_CLSS_FLAG { get; set; }
        //	植入或介入类名称				
        public string IMPT_ITVT_CLSS_NAME { get; set; }
        //	一次性使用标志				
        public string DSPO_USED_FLAG { get; set; }
        //	一次性使用标志名称				
        public string DSPO_USED_FLAG_NAME { get; set; }
        //	注册备案人名称				
        public string REGER_NAME { get; set; }
        //	开始日期				
        public DateTime? BEGNDATE { get; set; }
        //	结束日期				
        public DateTime? ENDDATE { get; set; }
        //	医疗器械管理类别				
        public string MED_EQU_MGT_TYPE { get; set; }
        //	医疗器械管理类别名称				
        public string MED_EQU_MGT_TYPE_NAME { get; set; }
        //	注册备案号				
        public string REG_FIL_NO { get; set; }
        //	注册备案产品名称				
        public string REG_FIL_PROD_NAME { get; set; }
        //	结构及组成				
        public string STRU_COMP { get; set; }
        //	其他内容				
        public string OTH_CONT { get; set; }
        //	批准日期				
        public DateTime? APRV_DATE { get; set; }
        //	注册备案人住所				
        public string REGER_ADDR { get; set; }
        //	注册证有效期开始时间				
        public DateTime? REGCERT_EXPY_BEGNTIME { get; set; }
        //	注册证有效期结束时间				
        public DateTime? REGCERT_EXPY_ENDTIME { get; set; }
        //	生产企业代码				
        public string PRODENTP_CODE { get; set; }
        //	生产企业名称				
        public string PRODENTP_NAME { get; set; }
        //	生产地址				
        public string MANU_ADDR { get; set; }
        //	代理人企业				
        public string AGNT_ENTP { get; set; }
        //	代理人企业地址				
        public string AGNT_ENTP_ADDR { get; set; }
        //	生产国或地区				
        public string MANU_NAT_REGN { get; set; }
        //	售后服务机构				
        public string AFTSAL_SERINS { get; set; }
        //	注册或备案证电子档案				
        public string REG_FIL_ELEC_FILE { get; set; }
        //	产品影像				
        public string PROD_IMG { get; set; }
        //	有效标志				
        public string VALI_FLAG { get; set; }
        //	数据唯一记录号				
        public string RID { get; set; }
       
        //	版本号				
        public string VER { get; set; }
        //	版本名称				
        public string VER_NAME { get; set; }
        

    }
}
