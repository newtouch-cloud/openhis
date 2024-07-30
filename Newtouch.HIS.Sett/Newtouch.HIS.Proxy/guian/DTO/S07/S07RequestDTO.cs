using Newtouch.HIS.Proxy.Attribute;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S07")]
    [XmlRoot("body")]
    public class S07RequestDTO
    {
        /// <summary>
        /// 补偿序号
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// 出院日期(yyyy-mm-dd)
        /// </summary>
        public string dischargeDate { get; set; }
        /// <summary>
        /// 出院科室(见字典科室)
        /// </summary>
        public string dischargeDepartments { get; set; }
        /// <summary>
        /// 出院状态(见字典入出院状态)
        /// </summary>
        public string dischargeStatus { get; set; }
        /// <summary>
        /// 农合疾病出院诊断（icd编码）(跨省)
        /// </summary>
        public string icdAllNo { get; set; }
        /// <summary>
        /// 第二疾病诊断（icd编码）(跨省)
        /// </summary>
        public string secondIcdNo { get; set; }
        /// <summary>
        /// 第三疾病诊断（icd编码）(跨省)
        /// </summary>
        public string threeIcdNo { get; set; }
        /// <summary>
        /// 第四疾病诊断（icd编码）(跨省)
        /// </summary>
        public string fourNo { get; set; }
        /// <summary>
        /// 第五疾病诊断（icd编码）(跨省)
        /// </summary>
        public string fiveNo { get; set; }
        /// <summary>
        /// 治疗方式编码(跨省)
        /// </summary>
        public string treatCode { get; set; }
        /// <summary>
        /// 第二治疗方式编码(跨省)
        /// </summary>
        public string secondTreatCode { get; set; }
        /// <summary>
        /// 第三治疗方式编码(跨省)
        /// </summary>
        public string threeTreatCode { get; set; }
        /// <summary>
        /// 第四治疗方式编码(跨省)
        /// </summary>
        public string fourTreatCode { get; set; }
        /// <summary>
        /// 第五治疗方式编码(跨省)
        /// </summary>
        public string fiveTreatCode { get; set; }
        /// <summary>
        /// 转诊类型（0 正常住院 1 县外就医转诊 2 转院）(跨省)
        /// </summary>
        public string turnMode { get; set; }
        /// <summary>
        /// 转诊转院编码（县外就医转诊-该值为转诊申请单号；转院-该值为转院住院登记号）(跨省)
        /// </summary>
        public string turnCode { get; set; }
        /// <summary>
        /// 转院日期( yyyy-mm-dd hh:mm:ss)(跨省)
        /// </summary>
        public string turnDate { get; set; }
        /// <summary>
        /// HIS系统中病人总费用(跨省)
        /// </summary>
        public string hisTotal { get; set; }
        /// <summary>
        /// 行政编码(跨省)
        /// </summary>
        public string areaCode { get; set; }
        /// <summary>
        /// 是否跨省就医患者1:是0:否(跨省)
        /// </summary>
        public string isTransProvincial { get; set; }
        /// <summary>
        /// 重大疾病主诊断编码
        /// </summary>
        public string majorDiseaseICD { get; set; }
        /// <summary>
        /// 重大疾病第二诊断编码
        /// </summary>
        public string secondMajorDiseaseICD { get; set; }
        /// <summary>
        /// 重大疾病第三诊断编码
        /// </summary>
        public string threeMajorDiseaseICD { get; set; }
        /// <summary>
        /// 重大疾病第四诊断编码
        /// </summary>
        public string fourMajorDiseaseICD { get; set; }
        /// <summary>
        /// 重大疾病第五诊断编码
        /// </summary>
        public string fiveMajorDiseaseICD { get; set; }
    }
}