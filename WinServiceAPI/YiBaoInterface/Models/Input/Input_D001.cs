using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_D001 : InputBase
    {
        public string mdtrtCertType { get; set; }//	就诊凭证类型 	字符型 	3 	Y 	Y 	01-电子凭证令牌、02-身份证号、03-社会保障卡号 
        public string mdtrtCertNo { get; set; }//就诊凭证编号 	字符型 	50 	 	 	就诊凭证类型为“01”时填写电子凭证令牌，为“02” 时填写身份证号，为“03” 时填写社会保障卡卡号（注：就诊凭证类型“03” 时必填） 
        public string cardSn { get; set; }//卡识别码 	字符型 	32 	 	 	就诊凭证类型为“03”时必填 
        public string bizTypeCode { get; set; }//业务类型代码 	字符型 	3 	 	Y 	01-定点医疗机构就诊，02-互联网医院问诊 
        public string rxExraAttrCode { get; set; }// 	处方附加属性代码 	字符型 	3 	Y 	 	01-双通道处方，02-门诊统筹处方，03-其他
        public string ecToken { get; set; }//电子凭证令牌 	字符型 	64 	 	 	使用医保电子凭证就诊时必填 
        public string authNo { get; set; }//电子凭证线上身份核验流水号 	字符型 	100 	 	Y 	线上场景互联网医院问诊时使用，就诊凭证类型：01且必填 
        public string insuPlcNo { get; set; }//参保地编号 	字符型 	6 	 	 	默认取电子凭证返回的参保地或就诊登记时参保地信息；患者有多个参保地信息、省外参保人时必传 
        public string mdtrtareaNo { get; set; }//就医地编号 	字符型 	6 	 	 	默认取就诊登记时就医地信息；医疗机构有多个定点协议统筹区、省外参保人时必传 
        public string hospRxno { get; set; }//定点医疗机构处方编号 	字符型 	40 	 	Y 	院内内部处方号，单笔处方不可重复 
        public string initRxno { get; set; }//续方的原处方编号 	字符型 	40 	 	 	 
        public string rxTypeCode { get; set; }//处方类别代码 	字符型 	3 	Y 	Y 	参考处方类别代码（rx_type_cod e） 
        public string prscTime { get; set; }//开方时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string rxDrugCnt { get; set; }//药品类目数（剂数） 	数值型 	16,4 	 	Y 	西药、中成药时为药品的类目数量，中药饮片时为草药的总剂数 
        public string rxUsedWayCodg { get; set; }//处方整剂用法编号 	字符型 	20 	Y 	 （注：rxTypeCode为中药饮片处方必填），参考药物使用-途径代码(drug_medc_wa y_code) 
        public string rxUsedWayName { get; set; }//处方整剂用法名称 	字符型 	40 	 	 	（注：rxTypeCode为中药饮片处方必填） 
        public string rxFrquCodg { get; set; }//处方整剂频次编号 	字符型 	20 	Y 	 	（注：rxTypeCode为中药饮片处方必填），参考使用频次（used_frqu） 
        public string rxFrquName { get; set; }//处方整剂频次名称 	字符型 	40 	 	 	（注：rxTypeCode为中药饮片处方必填） 
        public string rxDosunt { get; set; }//处方整剂剂量单位 	字符型 	20 	 	 	（注：rxTypeCode为中药饮片处方必填） 
        public string rxDoscnt { get; set; }//处方整剂单次剂量数 	数值型 	16,2 	 	 	（注：rxTypeCode为中药饮片处方必填） 
        public string rxDrordDscr { get; set; }//处方整剂医嘱说明 	字符型 	400 	 	 	（注：rxTypeCode为中药饮片处方时使用） 
        public string valiDays { get; set; }//处方有效天数 	数值型 	10 	 	Y 	处方取药的时限（注：处方须在“有效天数”内完成取药，过期后无法使用） 
        public string valiEndTime { get; set; }//有效截止时间 	日期时间型 	 	 	Y 	开方时间+处方有效天数=有效截止时间 
        public string reptFlag { get; set; }//复用（多次）使用标志 	字符型 	3 	Y 	 	0-否、1-是 （预留字段，当前未使用） 
        public string maxReptCnt { get; set; }//最大使用次数 	数值型 	3,0 	 	 	（预留字段，当前未使用） 
        public string minInrvDays { get; set; }//使用最小间隔（天数） 	数值型 	3,0 	 	 	（预留字段，当前未使用） 
        public string rxCotnFlag { get; set; }//  续方标志 	字符型 	3 	Y 	 	0否、1-是 默认否；（预留字段，当前未使用） 
        public string longRxFlag { get; set; }//  长期处方标志 	字符型 	3 	Y 	 	0-否、1-是，默认为否；  
        /// <summary>
        /// 
        /// </summary>
        public List<rxdrugdetail> rxdrugdetail { get; set; }
        public mdtrtinfoV0 mdtrtinfo { get; set; }
        public List<diseinfoVO> diseinfo { get; set; }
    }
    public class rxdrugdetail
    {
        public string medListCodg { get; set; }//  医疗目录编码 	字符型 	50 	 	Y 	即医保药品编码（注：仅作为医保药品代码标识和记录，可按通用名开方和取药） 
        public string fixmedinsHilistId { get; set; }//	定点医药机构目录编号 	字符型 	30 	 	 	即院内药品编码 
        public string hospPrepFlag { get; set; }//  医疗机构制剂标志 	字符型 	3 	Y 	 	0-否、1-是，默认否 
        public string rxItemTypeCode { get; set; }//	处方项目分类代码 	字符型 	30 	Y 	Y 	11:西药,12:中成药,13:中药饮片，参考处方项目分类代码（rx_item_type_code） 
        public string rxItemTypeName { get; set; }//  处方项目分类名称 	字符型 	100 	 	 	 
        public string tcmdrugTypeCode { get; set; }// 中药类别代码 	字符型 	30 	Y 	 	参考中药类别代码（tcmdrug_type_code），中药处方必填，中药饮片固定传3 
        public string tcmdrugTypeName { get; set; }// 中药类别名称 	字符型 	20 	 	 	 
        public string tcmherbFoote { get; set; }//	草药脚注 	字符型 	200 	 	 	 
        public string mednTypeCode { get; set; }// 药物类型代码 	字符型 	100 	Y 	 	参考药物类型代码（medn_type_co de），（注：可按院内内部的药品类型分类）					
        public string mednTypeName { get; set; }// 药物类型 	字符型 	100 	 	 	 
        public string mainMedcFlag { get; set; }// 主要用药标志 	字符型 	3 	Y 	 	0-否、1-是 
        public string urgtFlag { get; set; }// 加急标志 	字符型 	3 	Y 	 	0-否、1-是 
        public string basMednFlag { get; set; }// 基本药物标志 	字符型 	3 	Y 	 	0-否、1-是 
        public string impDrugFlag { get; set; }// 是否进口药品 	字符型 	3 	Y 	 	0-否、1-是 
        public string otcFlag { get; set; }// 是否OTC药品 	字符型 	3 	Y 	 	0-处方药品（默认）、1-OTC药品 
        public string drugGenname { get; set; }// 药品通用名 	字符型 	100 	 	Y 	 
        public string drugDosform { get; set; }// 药品剂型 	字符型 	30 	 	Y 	 
        public string drugSpec { get; set; }// 药品规格 	字符型 	40 	 	Y 	 
        public string drugProdname { get; set; }// 药品商品名 	字符型 	255 	 	 	非必填，可按通用名开方 
        public string prdrName { get; set; }//生厂厂家 	字符型 	100 	 	 	非必填，可按通用名开方 
        public string medcWayCodg { get; set; }//用药途径代码 	字符型 	10 	Y 	Y 	rx_item_type_code为西药、中成药时必填，中药饮片使用字段 rxUsedWayCodg ，参考药物使用-途径代码(drug_medc_way_code)（注：可使用院内内部代码） 
        public string medcWayDscr { get; set; }// 用药途径描述 	字符型 	100 	 	Y 	rx_item_type_code为西药、中成药时必填，中药饮片使用字段rxUsedWayName  
        public string medcBegntime { get; set; }// 用药开始时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string medcEndtime { get; set; }// 用药结束时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string medcDays { get; set; }// 用药天数 	数值型 	8,2 	 	Y 	 
        public string sinDosunt { get; set; }//单次剂量单位 （即开方单位或剂量单位，如“mg“） 	字符型 	20 	 	Y 	rx_item_type_c ode为西药、中成药时必填，中药饮片使用字段rxDosunt 
        public string sinDoscnt { get; set; }//单次用量 	数值型 	16,4 	 	Y 	rx_item_type_code为西药、中成药时必填，中药饮片使用字段rxDoscnt 
        public string usedFrquCodg { get; set; }// 使用频次编码 	字符型 	10 	Y 	Y 	rx_item_type_c ode为西药、中成药时必填，中药饮片使用字段 rxFrquCodg,参考使用频次（used_frqu）处方项目分类代码（注：可使用院内内部代码） 
        public string usedFrquName { get; set; }// 使用频次名称 	字符型 	30 	 	Y 	rx_item_type_code为西药、中成药时必填, 中药饮片使用字段rxFrquName  
        public string drugDosunt { get; set; }//	药品总用药量单位（即发药计价单位，取药或处方流转时药品医保结算使用的单位；如“片“或”盒“） 	字符型 	20 	 	Y 	（注：拆零时使用最小制剂单位如“片”，非拆零时使用库存包装单位，如“盒 “，须统一使用国家医保药品目录的“最小制剂单位”或“最小包装单位”） 
        public string drugCnt { get; set; }//药品总用药量 （取药或处方流转时药品医保结算使用的数量） 	数值型 	16,4 	 	Y 	根据单次剂量及单位、频次等和 drugDosunt按院内“药品单位转换系数“换算 
        public string drugPric { get; set; }//药品单价 	数值型 	16,6 	 	 	非必填，院内价格按drugDosunt 计价 
        public string drugSumamt { get; set; }//药品总金额 	数值型 	16,2 	 	 	非必填，院内总金额=drugCnt× 药品单价 
        public string hospApprFlag { get; set; }//医院审批标志 	字符型 	3 	Y 	Y 	参照字典：（hosp_appr_fl ag） 医院审批标志，配合目录的限制使用标志使用： a). 当目录限制使用标志为 “是”时: 1)医院审批标志为“0”或 “2”时，明细按照自费处理； 2)医院审批标志为“1”时，明细按纳入报销处理。 b). 当目录限制使用标志为“否”时: 1)医院审批标志为“0”或 “1”时，明细按照实际情况处理； 2)医院审批标志为“2”时，明细按照自费处理。 
        public string selfPayRea { get; set; }//自费原因类型 	字符型 	6 	Y 	 	医院审批标志 hospApprFlag值为2（自费）时必填，值参考字典self_pay_rea 
        public string realDscr { get; set; }//自费原因描述 	字符型 	1000 	N 	 	self_pay_rea自费原因类型为6（其他原因）时必填 
        public string extras { get; set; }//扩展数据 	对象型 	 	 	 	地方业务扩展信息，处方结算核验时可传递予地方核心业务系统 
    }
    public class mdtrtinfoV0
    {
        public string fixmedinsName { get; set; }
        public string fixmedinsCode { get; set; }
        public string mdtrtId { get; set; }
        public string medType { get; set; }
        public string iptOtpNo { get; set; }
        public string otpIptFlag { get; set; }
        public string psnNo { get; set; }
        public string patnName { get; set; }
        public string psnCertType { get; set; }
        public string certno { get; set; }
        public string patnAge { get; set; }
        public string patnHgt { get; set; }
        public string patnWt { get; set; }
        public string gend { get; set; }
        public string birctrlType { get; set; }
        public string birctrlMatnDate { get; set; }
        public string matnType { get; set; }
        public string gesoVal { get; set; }
        public string nwbFlag { get; set; }
        public string nwbAge { get; set; }
        public string suckPrdFlag { get; set; }
        public string algsHis { get; set; }
        public string prscDeptName { get; set; }
        public string prscDeptCode { get; set; }
        public string drCode { get; set; }
        public string prscDrName { get; set; }
        public string prscDrCertType { get; set; }
        public string prscDrCertno { get; set; }
        public string drProfttlCodg { get; set; }
        public string drProfttlName { get; set; }
        public string drDeptCode { get; set; }
        public string drDeptName { get; set; }
        public string caty { get; set; }
        public string mdtrtTime { get; set; }
        public string diseCodg { get; set; }
        public string diseName { get; set; }
        public string spDiseFlag { get; set; }
        public string maindiagCode { get; set; }
        public string maindiagName { get; set; }
        public string diseCondDscr { get; set; }
        public string hiFeesetlType { get; set; }
        public string hiFeesetlName { get; set; }
        public string rgstFee { get; set; }
        public string medfeeSumamt { get; set; }
        public string fstdiagFlag { get; set; }
        public string extras { get; set; }
    }
    public class diseinfoVO
    {
        public string diagType { get; set; }
        public string maindiagFlag { get; set; }
        public string diagSrtNo { get; set; }
        public string diagCode { get; set; }
        public string diagName { get; set; }
        public string diagDept { get; set; }
        public string diagDeptCode { get; set; }
        public string diagDrNo { get; set; }
        public string diagDrName { get; set; }
        public string diagTime { get; set; }
        public string tcmDiseCode { get; set; }
        public string tcmDiseName { get; set; }
        public string tcmsympCode { get; set; }
        public string tcmsymp { get; set; }
    }
}
