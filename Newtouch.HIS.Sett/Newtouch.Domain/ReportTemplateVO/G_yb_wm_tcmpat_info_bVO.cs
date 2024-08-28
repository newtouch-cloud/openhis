using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// 西药中成药信息表
    /// </summary>
    public class G_yb_wm_tcmpat_info_bVO
    {
        //	医疗目录编码				
        public string MED_LIST_CODG { get; set; }
        //	药品商品名				
        public string DRUG_PRODNAME { get; set; }
        //	通用名编码				
        public string GENNAME_CODG { get; set; }
        //	药品通用名				
        public string DRUG_GENNAME { get; set; }
        //	化学名称				
        public string CHEMNAME { get; set; }
        //	别名				
        public string ALIS { get; set; }
        //	英文名称				
        public string ENG_NAME { get; set; }
        //	注册名称				
        public string REG_NAME { get; set; }
        //	药品本位码				
        public string DRUGSTDCODE { get; set; }
        //	药品剂型				
        public string DRUG_DOSFORM { get; set; }
        //	药品剂型名称				
        public string DRUG_DOSFORM_NAME { get; set; }
        //	药品类别				
        public string DRUG_TYPE { get; set; }
        //	药品类别名称				
        public string DRUG_TYPE_NAME { get; set; }
        //	药品规格				
        public string DRUG_SPEC { get; set; }
        //	药品规格代码				
        public string DRUG_SPEC_CODE { get; set; }
        //	注册剂型				
        public string REG_DOSFORM { get; set; }
        //	注册规格				
        public string REG_SPEC { get; set; }
        //	注册规格代码				
        public string REG_SPEC_CODE { get; set; }
        //	每次用量				
        public string EACH_DOS { get; set; }
        //	使用频次				
        public string USED_FRQU { get; set; }
        //	酸根盐基				
        public string ACDBAS { get; set; }
        //	国家药品编号				
        public string NAT_DRUG_NO { get; set; }
        //	使用方法				
        public string USED_MTD { get; set; }
        //	中成药标志				
        public string TCMPAT_FLAG { get; set; }
        //	生产地类别				
        public string PRODPLAC_TYPE { get; set; }
        //	生产地类别名称				
        public string PRODPLAC_TYPE_NAME { get; set; }
        //	计价单位类型				
        public string PRCUNT_TYPE { get; set; }
        //	非处方药标志				
        public string OTC_FLAG { get; set; }
        //	非处方药标志名称				
        public string OTC_FLAG_NAME { get; set; }
        //	包装材质				
        public string PACMATL { get; set; }
        //	包装材质名称				
        public string PACMATL_NAME { get; set; }
        //	包装规格				
        public string PACSPEC { get; set; }
        //	包装数量				
        public string PAC_CNT { get; set; }
        //	功能主治				
        public string EFCC_ATD { get; set; }
        //	给药途径				
        public string RUTE { get; set; }
        //	说明书				
        public string MANL { get; set; }
        //	开始日期				
        public string BEGNDATE { get; set; }
        //	结束日期				
        public string ENDDATE { get; set; }
        //	最小使用单位				
        public string MIN_USEUNT { get; set; }
        //	最小销售单位				
        public string MIN_SALUNT { get; set; }
        //	最小计量单位				
        public string MIN_UNT { get; set; }
        //	最小包装数量				
        public int? MIN_PAC_CNT { get; set; }
        //	最小包装单位				
        public string MIN_PACUNT { get; set; }
        //	最小制剂单位				
        public string MIN_PREPUNT { get; set; }
        //	最小包装单位名称				
        public string MINPACUNT_NAME { get; set; }
        //	最小制剂单位名称				
        public string MIN_PREPUNT_NAME { get; set; }
        //	转换比				
        public int? CONVRAT { get; set; }
        //	药品有效期				
        public string DRUG_EXPY { get; set; }
        //	最小计价单位				
        public string MIN_PRCUNT { get; set; }
        //	五笔助记码				
        public string WUBI { get; set; }
        //	拼音助记码				
        public string PINYIN { get; set; }
        //	分包装厂家				
        public string SUBPCK_FCTY { get; set; }
        //	生产企业代码				
        public string PRODENTP_CODE { get; set; }
        //	生产企业名称				
        public string PRODENTP_NAME { get; set; }
        //	特殊限价药品标志				
        public string SP_LMTPRIC_DRUG_FLAG { get; set; }
        //	特殊药品标志				
        public string SP_DRUG_FLAG { get; set; }
        //	限制使用范围				
        public string LMT_USESCP { get; set; }
        //	限制使用标志				
        public string LMT_USED_FLAG { get; set; }
        //	药品注册证编号				
        public string DRUG_REGNO { get; set; }
        //	药品注册证号开始日期				
        public DateTime? DRUG_REGCERT_BEGNDATE { get; set; }
        //	药品注册证号结束日期				
        public DateTime? DRUG_REGCERT_ENDDATE { get; set; }
        //	批准文号				
        public string APRVNO { get; set; }
        //	批准文号开始日期				
        public DateTime? APRVNO_BEGNDATE { get; set; }
        //	批准文号结束日期				
        public DateTime? APRVNO_ENDDATE { get; set; }
        //	市场状态				
        public string MKT_STAS { get; set; }
        //	市场状态名称				
        public string MKT_STAS_NAME { get; set; }
        //	药品注册批件电子档案				
        public string DRUG_REG_APPVLETR_ELECACS { get; set; }
        //	药品补充申请批件电子档案				
        public string SPLM_APPY_APPVLETR_FILE { get; set; }
        //	国家医保药品目录备注				
        public string NAT_HI_DRUGLIST_MEMO { get; set; }
        //	基本药物标志名称				
        public string BAS_MEDN_FLAG_NAME { get; set; }
        //	基本药物标志				
        public string BAS_MEDN_FLAG { get; set; }
        //	增值税调整药品标志				
        public string ADVALTAX_ADJM_DRUG_FLAG { get; set; }
        //	增值税调整药品名称				
        public string ADVALTAX_ADJM_DRUG_NAME { get; set; }
        //	上市药品目录集药品				
        public string LSTD_DRUGLIST_DRUG { get; set; }
        //	医保谈判药品标志				
        public string HI_NEGO_DRUG_FLAG { get; set; }
        //	医保谈判药品名称				
        public string HI_NEGO_DRUG_NAME { get; set; }
        //	卫健委药品编码				
        public string NHC_DRUG_CODG { get; set; }
        //	备注				
        public string MEMO { get; set; }
        //	有效标志				
        public string VALI_FLAG { get; set; }
        //	数据唯一记录号				
        public string RID { get; set; }
        //	数据创建时间				
        public DateTime? CRTE_TIME { get; set; }
        //	数据更新时间				
        public DateTime? UPDT_TIME { get; set; }
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
        //	儿童用药				
        public string CHLD_MEDC { get; set; }
        //	公司名称				
        public string CO_NAME { get; set; }
        //	仿制药一致性评价药品				
        public string CONSEVAL_DRUG { get; set; }
        //	经销企业				
        public string DSTR { get; set; }
        //	经销企业联系人				
        public string DSTR_CONER { get; set; }
        //	经销企业授权书电子档案				
        public string DSTR_AUTH_FILE_ELECACS { get; set; }
        //	国家医保药品目录剂型				
        public string NAT_HI_DRUGLIST_DOSFORM { get; set; }
        //	国家医保药品目录甲乙类标识				
        public string NAT_HI_DRUGLIST_CHRGITM_LV { get; set; }
        ////	上市许可证持有人				
        //public string LSTD_LIC_HOLDER { get; set; }
        ////	下发标志				
        //public string ISU_FLAG { get; set; }
        ////	传输数据ID				
        //public string TRAM_DATA_ID { get; set; }
        ////	生效时间				
        //public DateTime? EFFT_TIME { get; set; }
        ////	失效时间				
        //public DateTime? INVD_TIME { get; set; }

    }
    public class G_yb_mluCommon_Info: G_yb_wm_tcmpat_info_bVO
    {
        #region 1301
        public string TCMHERB_NAME { get; set; }
        public string MED_PART { get; set; }
        public string CNVL_USED { get; set; }
        public string NATFLA { get; set; }
        public string CAT { get; set; }
        public string MLMS_NAME { get; set; }
        public string PSDG_MTD { get; set; }
        public string ECY_TYPE { get; set; }
        public string MLMS_CAT_SOUC { get; set; }
        #endregion

        #region 1302
        public string DOSFORM { get; set; }
        public string BEGNTIME { get; set; }
        public string ENDTIME { get; set; }
        #endregion

        #region 1305
        public string PRCUNT { get; set; }
        public string PRCUNT_NAME { get; set; }
        public string TRT_ITEM_DSCR { get; set; }
        public string TRT_EXCT_CONT { get; set; }
        public string SERVITEM_TYPE { get; set; }
        public string SERVITEM_NAME { get; set; }
        #endregion

        #region 1306
        public string HI_GENNAME { get; set; }
        public string SPEC { get; set; }
        public string MCS_MATL { get; set; }
        #endregion

        #region 1307
        public string DIAG_LIST_ID { get; set; }
        public string CPR { get; set; }
        public string CPR_CODE_SCP { get; set; }
        public string CPR_NAME { get; set; }
        public string SEC_CODE_SCP { get; set; }
        public string SEC_NAME { get; set; }
        public string CGY_CODE { get; set; }
        public string CGY_NAME { get; set; }
        public string SOR_CODE { get; set; }
        public string SOR_NAME { get; set; }
        public string DIAG_CODE { get; set; }
        public string DIAG_NAME { get; set; }
        public string USED_STD { get; set; }
        #endregion

        #region 1308
        public string OPRN_STD_LIST_ID { get; set; }
        public string OPRN_OPRT_CODE { get; set; }
        public string OPRN_OPRT_NAME { get; set; }
        public string DTLS_CODE { get; set; }
        public string DTLS_NAME { get; set; }
        #endregion

        #region 1309
        public string OPSP_DISE_CODE { get; set; }
        public string OPSP_DISE_MAJCLS_NAME { get; set; }
        public string OPSP_DISE_SUBD_CLSS_NAME { get; set; }
        public string ADMDVS { get; set; }
        public string OPSP_DISE_NAME { get; set; }
        public string OPSP_DISE_MAJCLS_CODE { get; set; }
        #endregion

        #region 1310
        public string DISE_SETL_LIST_ID { get; set; }
        public string BYDISE_SETL_LIST_CODE { get; set; }
        public string BYDISE_SETL_DISE_NAME { get; set; }
        public string QUA_OPRN_OPRT_CODE { get; set; }
        public string QUA_OPRN_OPRT_NAME { get; set; }

        #endregion
        #region 1311
        public string DAYSRG_TRT_LIST_ID { get; set; }
        public string DAYSRG_DISE_LIST_CODE { get; set; }

        public string DAYSRG_DISE_NAME { get; set; }
        #endregion

        #region 1313
        public string TMOR_MPY_ID { get; set; }
        public string TMOR_CELL_TYPE_CODE { get; set; }

        public string TMOR_CELL_TYPE { get; set; }
        public string MPY_TYPE_CODE { get; set; }
        public string MPY_TYPE { get; set; }
        #endregion

        #region 1314
        public string TCM_DIAG_ID { get; set; }
        public string CATY_CGY_CODE { get; set; }
        public string CATY_CGY_NAME { get; set; }
        public string SPCY_SYS_TAXA_CODE { get; set; }
        public string SPCY_SYS_TAXA_NAME { get; set; }
        public string DISE_TYPE_CODE { get; set; }
        public string DISE_TYPE_NAME { get; set; }
        #endregion

        #region 1315
        public string TCMSYMP_ID { get; set; }
        public string SDS_CGY_CODE { get; set; }
        public string SDS_CGY_NAME { get; set; }
        public string SDS_ATTR_CODE { get; set; }
        public string SDS_ATTR { get; set; }
        public string SDS_TYPE_CODE { get; set; }
        public string SDS_TYPE_NAME { get; set; }
        #endregion

        #region 1320
        public string ZYPFKL_CODE { get; set; }
        public string ZYPFKL_NAME { get; set; }
        public string DDYC { get; set; }
        public string GG { get; set; }
        public string ZYPFKLZXBZ { get; set; }
        public string BLFYJCXX { get; set; }
        public string ZYYP_CODE { get; set; }
        public string ZYYP_NAME { get; set; }
        public string YC_NAME { get; set; }
        public string GXFL { get; set; }
        public string GNZZ { get; set; }
        public string CGYF { get; set; }
        public string SJGXSJ { get; set; }
        public string CJJG_CODE { get; set; }
        public string YBQH { get; set; }

        #endregion


        #region 1321
        public string YLML_CODE { get; set; }
        public string YLFWXM_NAME { get; set; }
        public string YLFWXMSC { get; set; }
        public string JGGC { get; set; }
        public string JJDW { get; set; }
        public string JJSM { get; set; }
        public string ZLXMSM { get; set; }
        public string FYLXKJ { get; set; }
        public string EFFECTIVE_TIME { get; set; }
        public string EXPIRATION_TIME { get; set; }
        #endregion
    }
}
