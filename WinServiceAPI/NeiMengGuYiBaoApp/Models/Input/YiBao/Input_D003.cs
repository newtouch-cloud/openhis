using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_D003 : InputBase
    {
        public string rxTraceCode { get; set; }//	处方追溯码 	字符型 	20 	 	Y 	有效时间和处方有效时间保持一致，上传时每张处方只能使用一次 
        public string hiRxno { get; set; }//医保处方编号 	字符型 	30 	 	Y 	 
        public string mdtrtId { get; set; }//医保就诊ID 	字符型 	30 	 	Y 	参保病人信息字段（注：医保门诊挂号时返回） 
        public string patnName { get; set; }//患者姓名 	字符型 	40 	 	Y 	 
        public string psnCertType { get; set; }//人员证件类型 	字符型 	6 	Y 	Y 	 
        public string certno { get; set; }//证件号码 	字符型 	50 	 	Y 	 
        public string fixmedinsName { get; set; }//	定点医疗机构名称 	字符型 	200 	 	Y 	 
        public string fixmedinsCode { get; set; }//	定点医疗机构编号 	字符型 	20 	 	Y 	 
        public string drCode { get; set; }//	开方医保医师代码 	字符型 	20 	 	Y 	国家医保医师代码 
        public string prscDrName { get; set; }//	开方医师姓名 	字符型 	50 	 	Y 	 
        public string pharDeptName { get; set; }//	审方药师科室名称 	字符型 	50 	 	Y 	 
        public string pharDeptCode { get; set; }//	审方药师科室编号 	字符型 	30 	 	Y 	与医药机构服务的科室管理：【3401科室信息上传、3401A批量科室信息上传】中上传的
        public string hosp_dept_codg { get; set; }//医院科室编码字段保持一致 
        public string pharProfttlCodg { get; set; }//	审方药师职称编码 	字符型 	20 	Y 	 	参照审方药师职称编码（phar_pro_tech_duty） 
        public string pharProfttlName { get; set; }//	审方药师职称名称 	字符型 	20 	 	 	 
        public string pharCode { get; set; }//	审方医保药师代码 	字符型 	20 	 	Y 	国家医保定点医疗机构药学、技术人员代码（如 HY110000000001） 
        public string pharCertType { get; set; }//审方药师证件类型 	字符型 	6 	Y 	 	参照人员证件类型 (psn_cert_type) 
        public string pharCertno { get; set; }//审方药师证件号码 	字符型 	50 	 	 	 
        public string pharName { get; set; }//审方药师姓名 	字符型 	50 	 	Y 	 
        public string pharPracCertNo { get; set; }//	审方药师执业资格证号 	字符型 	50 	 	 	 
        public string pharChkTime { get; set; }//	医疗机构药师审方时间 	日期时间型 	 	 	Y 	yyyy-MM-dd HH:mm:ss 
        public string rxFile { get; set; }//处方原件 	大文本 	 	 	Y 	医保电子签名后的处方文件base64字符(PDF 或OFD格式) 
        public string signDigest { get; set; }//处方信息签名值 	字符型 	4000 	 	Y 	医保电子签名后处方信息的签名结果 
        public string extras { get; set; }//扩展字段 	JSON 	4000 	 	 	（预留字段，当前未使用），JSON序列化成字符串后长度不能超过4000 
    }

}
