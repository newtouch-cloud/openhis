using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Output
{
    public class Output_D005 : OutputBase
    {
        public string hiRxno { get; set; }//医保处方编号 	字符型 	30 	 	Y 	 
        public string fixmedinsCode { get; set; }//定点医疗机构编号 	字符型 	12 	 	Y 	 
        public string fixmedinsName { get; set; }//定点医疗机构名称 	字符型 	200 	 	Y 	 
        public string rxStasCodg { get; set; }//医保处方状态编码 	字符型 	3 	Y 	Y 	参考（rx_stas_cod g） 
        public string rxStasName { get; set; }//医保处方状态名称 	字符型 	20 	 	Y 	 
        public string rxUsedStasCodg { get; set; }//医保处方使用状态编码 	字符型 	3 	Y 	Y 	参考（rx_used_sta s_codg） 
        public string rxUsedStasName { get; set; }//医保处方使用状态名称 	字符型 	20 	 	Y 	 
        public string prscTime { get; set; }//开方时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string rxDrugCnt { get; set; }//药品数量（剂数） 	数值型 	16,2 	 	Y 	 
        public string rxUsedWayCodg { get; set; }//处方整剂用法编号 	字符型 	20 	Y 	 	参考药物使用途径代码(drug_medc_wa y_code) 
        public string rxUsedWayName { get; set; }//处方整剂用法名称 	字符型 	40 	 	 	 
        public string rxFrquCodg { get; set; }//处方整剂频次编号 	字符型 	20 	Y 	 	参考使用频次（used_frqu） 
        public string rxFrquName { get; set; }//处方整剂频次名称 	字符型 	40 	 	 	 
        public string rxDosunt { get; set; }//处方整剂剂量单位 	字符型 	20 	 	 	 
        public string rxDoscnt { get; set; }//处方整剂单次剂量数 	数值型 	16,2 	 	 	 
        public string rxDrordDscr { get; set; }//处方整剂医嘱说明 	字符型 	500 	 	 	 
        public string valiDays { get; set; }//处方有效天数 	数值型 	10 	 	Y 	 
        public string valiEndTime { get; set; }//有效截止时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string reptFlag { get; set; }//复用（多次）使用标志 	字符型 	3 	Y 	Y 	0-否、1-是 
        public string maxReptCnt { get; set; }//最大复用次数 	数值型 	3,0 	 	 	 
        public string reptdCnt { get; set; }//已复用次数 	数值型 	3,0 	 	 	 
        public string minInrvDays { get; set; }//使用最小间隔（天数） 	数值型 	3,0 	 	 	 
        public string rxTypeCode { get; set; }//处方类别编号 	字符型 	3 	Y 	Y 	 
        public string rxTypeName { get; set; }//处方类别名称 	字符型 	20 	 	 	 
        public string longRxFlag { get; set; }//长期处方标志 	字符型 	3 	Y 	 	0否、1-是 
        public string bizTypeCode { get; set; }//业务类型代码 	字符型 	3 	Y 	Y 	01-定点医疗机构就诊，02-互联网医院问诊 
        public string bizTypeName { get; set; }//业务类型名称 	字符型 	20 	 	 	 
        public string rxExraAttrCode { get; set; }//处方附加属性代码 	字符型 	3 	Y 	 	01-双通道处方，02-门诊统筹处方，03-其他 
        public string rxExraAttrName { get; set; }//处方附加属性名称 	字符型 	20 	 	
        public List<rxDetlList> rxDetlList { get; set; }//处方明细信息
        public rxOtpinfo rxOtpinfo { get; set; }//就诊信息
        public List<rxDiseList> rxDiseList { get; set; }//诊断信息
    }
    public class rxDetlList
    {
        public string medListCodg { get; set; }//医疗目录编码 	字符型 	50 	 	Y 	即医保目录编码 
        public string fixmedinsHilistId { get; set; }//定点医药机构目录编号 	字符型 	30 	 	 	即院内药品编码 
        public string hospPrepFlag { get; set; }//院内制剂标志 	字符型 	3 	Y 	 	 
        public string rxItemTypeCode { get; set; }//处方项目分类代码 	字符型 	30 	Y 	 	 
        public string rxItemTypeName { get; set; }//处方项目分类名称 	字符型 	100 	 	 	 
        public string tcmdrugTypeName { get; set; }//中药类别名称 	字符型 	20 	 	 	 
        public string tcmdrugTypeCode { get; set; }//中药类别代码 	字符型 	30 	Y 	 	 
        public string tcmherbFoote { get; set; }//草药脚注 	字符型 	200 	 	 	 
        public string mednTypeCode { get; set; }//药物类型代码 	字符型 	100 	Y 	 	 
        public string mednTypeName { get; set; }//药物类型 	字符型 	100 	 	 	 
        public string mainMedcFlag { get; set; }//主要用药标志 	字符型 	30 	 	 	 
        public string urgtFlag { get; set; }//加急标志 	字符型 	30 	 	 	 
        public string basMednFlag { get; set; }//基本药物标志 	字符型 	3 	Y 	 	 
        public string impDrugFlag { get; set; }//是否进口药品 	字符型 	3 	Y 	 	0-否、1-是 
        public string drugProdname { get; set; }//药品商品名 	字符型 	255 	 	Y 	 
        public string drugGenname { get; set; }//药品通用名 	字符型 	500 	 	Y 	 
        public string drugDosform { get; set; }//药品剂型 	字符型 	30 	 	Y 	 
        public string drugSpec { get; set; }//药品规格 	字符型 	40 	 	Y 	 
        public string prdrName { get; set; }//生厂厂家 	字符型 	100 	 	 	 
        public string medcWayCodg { get; set; }//用药途径代码 	字符型 	10 	 	Y 	 
        public string medcWayDscr { get; set; }//用药途径描述 	字符型 	100 	 	Y 	 
        public string medcBegntime { get; set; }//用药开始时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string medcEndtime { get; set; }//用药结束时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string medcDays { get; set; }//用药天数 	数值型 	8,2 	 	Y 	 
        public string drugCnt { get; set; }//药品总用药量 （取药或处方流转时药品医保结算使用的数量） 	数值型 	16,4 	 	Y 	 
        public string drugDosunt { get; set; }//药品总用药量单位（即发药计价单位，取药或处方流转时药品医保结算使用的单位；如“片“或”盒“） 	字符型 	20 	 	 	 
        public string sinDoscnt { get; set; }//单次用量 	数值型 	16,4 	 	 	 
        public string sinDosunt { get; set; }//单次剂量单位 	字符型 	20 	 	 	 
        public string usedFrquCodg { get; set; }//使用频次编码 	字符型 	10 	 	 	 
        public string usedFrquName { get; set; }//使用频次名称 	字符型 	30 	 	 	 
        public string hospApprFlag { get; set; }//医院审批标志 	字符型 	3 	Y 	 	 
        public string takeDrugFlag { get; set; }//取药标志位 	字符型 	3 	Y 	Y 	 
        public string otcFlag { get; set; }//是否OTC药品 	字符型 	3 	Y 	 	0-处方药品（默认）、1-OTC药品 
        public string selfPayRea { get; set; }//自费原因类型 	字符型 	6 	Y 	 	 
        public string realDscr { get; set; }//自费原因描述 	字符型 	1000 	 	 	 
        public string drugTotlcnt { get; set; }//所需药品库存数量 	数值型  	16 	 	 	 
        public string drugTotlcntEmp { get; set; }//所需药品库存单位 	字符型 	40 	 	 	  	 
    }
    public class rxOtpinfo
    {
        public string medType { get; set; }//医疗类别 	字符型 	6 	Y 	Y 	参考医疗类别（med_type） 
        public string iptOtpNo { get; set; }//门诊/住院号 	字符型 	30 	 	Y 	 
        public string otpIptFlag { get; set; }//门诊住院标志 	字符型 	3 	 	 	1-门诊，2-住院 
        public string patnName { get; set; }//患者姓名 	字符型 	40 	 	Y 	 
        public string patnAge { get; set; }//年龄 	数值型 	4,1 	 	Y 	 
        public string patnHgt { get; set; }//患者身高 	数值型 	6,2 	 	 	 
        public string patnWt { get; set; }//患者体重 	数值型 	6,2 	 	 	 
        public string gend { get; set; }//性别 	字符型 	6 	Y 	Y 	 
        public string gesoVal { get; set; }//妊娠(孕周) 	数值型 	2 	 	 	 
        public string nwbFlag { get; set; }//新生儿标志 	字符型 	3 	Y 	 	0-否、1-是 
        public string nwbAge { get; set; }//新生儿日、月龄 	字符型 	20 	 	 	 
        public string suckPrdFlag { get; set; }//哺乳期标志 	数值型 	3 	Y 	 	0-否、1-是 
        public string algsHis { get; set; }//过敏史 	字符型 	1000 	 	 	 
        public string insutype { get; set; }//险种类型 	字符型 	6 	Y 	Y 	 
        public string prscDeptName { get; set; }//开方科室名称 	字符型 	50 	 	Y 	 
        public string prscDrName { get; set; }//开方医师姓名 	字符型 	50 	 	Y 	 
        public string pharName { get; set; }//药师姓名 	字符型 	50 	 	Y 	 
        public string pharChkTime { get; set; }//医疗机构药师审方时间 	日期时间型	 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string mdtrtTime { get; set; }//就诊时间 	日期时间型	 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string diseCodg { get; set; }//病种编码 	字符型 	30 	 	 	按照标准编码填写：按病种结算病种目录代码(bydise_setl_lis t_code)、 门诊慢特病病种目录代码(opsp_dise_cod) 
        public string diseName { get; set; }//病种名称 	字符型 	500 	 	 	 
        public string spDiseFlag { get; set; }//是否特殊病种 	字符型 	1 	 	Y 	 
        public string maindiagCode { get; set; }//主诊断代码 	字符型 	30 	 	Y 	 
        public string maindiagName { get; set; }//主诊断名称 	字符型 	100 	 	Y 	 
        public string diseCondDscr { get; set; }//疾病病情描述 	字符型 	2000 	 	 	 
        public string fstdiagFlag { get; set; }//是否初诊 	字符型 	3 	 	 	0-否、1-是 
    }
    public class rxDiseList
    {
        public string diagType { get; set; }//诊断类别 	字符型 	3 	Y 	Y 	 
        public string maindiagFlag { get; set; }//主诊断标志 	字符型 	3 	Y 	Y 	 
        public string diagSrtNo { get; set; }//诊断排序号 	数值型 	2 	 	Y 	 
        public string diagCode { get; set; }//诊断代码 	字符型 	30 	 	Y 	 
        public string diagName { get; set; }//诊断名称 	字符型 	100 	 	Y 	 
        public string diagDept { get; set; }//诊断科室 	字符型 	50 	 	Y 	 
        public string diagDeptCode { get; set; }//诊断科室代码 	字符型 	20 	 	Y 	 
        public string diagDrNo { get; set; }//诊断医生编码 	字符型 	30 	 	Y 	 
        public string diagDrName { get; set; }//诊断医生姓名 	字符型 	50 	 	Y 	 
        public string diagTime { get; set; }//诊断时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string tcmDiseCode { get; set; }//中医病名代码 	字符型 	30 	 	 	 
        public string tcmDiseName { get; set; }//中医病名名称 	字符型 	300 	 	 	 
        public string tcmsympCode { get; set; }//中医症候代码 	字符型 	30 	 	 	 
        public string tcmsymp { get; set; }//中医症候 	字符型 	300 	 	 

    }
}
