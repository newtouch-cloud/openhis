using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// 自制剂信息表
    /// </summary>
    public class G_yb_selfprep_info_bVO
    {
        //	医疗目录编码				
        public string MED_LIST_CODG { get; set; }
        //	药品商品名				
        public string DRUG_PRODNAME { get; set; }
        //	别名				
        public string ALIS { get; set; }
        //	英文名称				
        public string ENG_NAME { get; set; }
        //	剂型				
        public string DOSFORM { get; set; }
        //	剂型名称				
        public string DOSFORM_NAME { get; set; }
        //	注册剂型				
        public string REG_DOSFORM { get; set; }
        //	成分				
        public string ING { get; set; }
        //	功能主治				
        public string EFCC_ATD { get; set; }
        //	性状				
        public string CHRT { get; set; }
        //	药品规格				
        public string DRUG_SPEC { get; set; }
        //	药品规格代码				
        public string DRUG_SPEC_CODE { get; set; }
        //	注册规格				
        public string REG_SPEC { get; set; }
        //	注册规格代码				
        public string REG_SPEC_CODE { get; set; }
        //	给药途径				
        public string RUTE { get; set; }
        //	贮藏				
        public string STOG { get; set; }
        //	使用频次				
        public string USED_FRQU { get; set; }
        //	每次用量				
        public string EACH_DOS { get; set; }
        //	药品类别				
        public string DRUG_TYPE { get; set; }
        //	药品类别名称				
        public string DRUG_TYPE_NAME { get; set; }
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
        //	说明书				
        public string MANL { get; set; }
        //	包装数量				
        public string PAC_CNT { get; set; }
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
        //	最小制剂单位名称				
        public string MIN_PREPUNT_NAME { get; set; }
        //	药品有效期				
        public string DRUG_EXPY { get; set; }
        //	最小计价单位				
        public string MIN_PRCUNT { get; set; }
        //	不良反应				
        public string DEFS { get; set; }
        //	注意事项				
        public string MNAN { get; set; }
        //	禁忌				
        public string TABO { get; set; }
        //	生产企业代码				
        public string PRODENTP_CODE { get; set; }
        //	生产企业名称				
        public string PRODENTP_NAME { get; set; }
        //	生产企业地址				
        public string PRODENTP_ADDR { get; set; }
        //	特殊限价药品标志				
        public string SP_LMTPRIC_DRUG_FLAG { get; set; }
        //	批准文号				
        public string APRVNO { get; set; }
        //	批准文号开始日期				
        public DateTime? APRVNO_BEGNDATE { get; set; }
        //	批准文号结束日期				
        public DateTime? APRVNO_ENDDATE { get; set; }
        //	药品注册证编号				
        public string DRUG_REGNO { get; set; }
        //	药品注册证号开始日期				
        public DateTime? DRUG_REGCERT_BEGNDATE { get; set; }
        //	药品注册证号结束日期				
        public DateTime? DRUG_REGCERT_ENDDATE { get; set; }
        //	转换比				
        public int? CONVRAT { get; set; }
        //	限制使用范围				
        public string LMT_USESCP { get; set; }
        //	最小包装单位名称				
        public string MINPACUNT_NAME { get; set; }
        //	注册名称				
        public string REG_NAME { get; set; }
        //	分包装厂家				
        public string SUBPCK_FCTY { get; set; }
        //	市场状态				
        public string MKT_STAS { get; set; }
        //	药品注册批件电子档案				
        public string DRUG_REG_APPVLETR_ELECACS { get; set; }
        //	药品补充申请批件电子档案				
        public string SPLM_APPY_APPVLETR_FILE { get; set; }
        //	国家医保药品目录编号				
        public string NAT_HI_DRUGLIST_NO { get; set; }
        //	国家医保药品目录备注				
        public string NAT_HI_DRUGLIST_MEMO { get; set; }
        //	增值税调整药品标志				
        public string ADVALTAX_ADJM_DRUG_FLAG { get; set; }
        //	增值税调整药品名称				
        public string ADVALTAX_ADJM_DRUG_NAME { get; set; }
        //	上市药品目录集药品				
        public string LSTD_DRUGLIST_DRUG { get; set; }
        //	卫健委药品编码				
        public string NHC_DRUG_CODG { get; set; }
        //	备注				
        public string MEMO { get; set; }
        //	有效标志				
        public string VALI_FLAG { get; set; }
        //	开始时间				
        public DateTime? BEGNTIME { get; set; }
        //	结束时间				
        public DateTime? ENDTIME { get; set; }
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
        //	版本名称				
        public string VER_NAME { get; set; }
        //	自制剂许可证号				
        public string SELFPREP_PMTNO { get; set; }
        //	儿童用药				
        public string CHLD_MEDC { get; set; }
        //	老年患者用药				
        public string ELD_PATN_MEDC { get; set; }
        //	医疗机构联系人姓名				
        public string MEDINS_CONER_NAME { get; set; }
        //	医疗机构联系人电话				
        public string MEDINS_CONER_TEL { get; set; }
        //	自制剂许可证电子档案				
        public string SELFPREP_PMTNO_ELECACS { get; set; }
        ////	所在地区				
        //public string REGN { get; set; }
        ////	下发标志				
        //public string ISU_FLAG { get; set; }
        ////	传输数据ID				
        //public string TRAM_DATA_ID { get; set; }
        ////	生效时间				
        //public DateTime? EFFT_TIME { get; set; }
        ////	失效时间				
        //public DateTime? INVD_TIME { get; set; }
        ////	医院制剂申请人单位名称				
        //public string HOSP_PREP_APPYER_EMP_NAME { get; set; }
        ////	医院制剂申请人单位地址				
        //public string HOSP_PREP_APPYER_EMP_ADDR { get; set; }

    }
}
