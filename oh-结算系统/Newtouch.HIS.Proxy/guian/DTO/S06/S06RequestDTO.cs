using Newtouch.HIS.Proxy.Attribute;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S06")]
    [XmlRoot("body")]
    public class S06RequestDTO
    {
       
        /// <summary>
        /// 补偿序号
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string inpatientNo { get; set; }
        /// <summary>
        /// 入院日期(yyyy-mm-dd)
        /// </summary>
        public string admissionDate { get; set; }
        /// <summary>
        /// 入院科室（见字典科室）
        /// </summary>
        public string admissionDepartments { get; set; }
        /// <summary>
        /// 经治医生
        /// </summary>
        public string treatingPhysician { get; set; }
        /// <summary>
        /// 入院状态(见字典入出院状态)
        /// </summary>
        public string admissionStatus { get; set; }
        /// <summary>
        /// 疾病代码(接口S29下载获取)
        /// </summary>
        public string diseaseCode { get; set; }
        /// <summary>
        /// 初步诊断
        /// </summary>
        public string initialDiagnosis { get; set; }
        /// <summary>
        /// 手术代码(接口S31下载获取)
        /// </summary>
        public string surgeryCode { get; set; }
        /// <summary>
        /// 病区
        /// </summary>
        public string wardArea { get; set; }
        /// <summary>
        /// 病房号
        /// </summary>
        public string wardNo { get; set; }
        /// <summary>
        /// 床位号
        /// </summary>
        public string berthNo { get; set; }
        /// <summary>
        /// 证件是否齐全0不齐全1齐全
        /// </summary>
        public string isIncrease { get; set; }
        /// <summary>
        /// 是否转诊1转诊默认0
        /// </summary>
        public string isReferra { get; set; }
        /// <summary>
        /// 病人类型（见字典病人类型）
        /// </summary>
        public string inpatientTypeOflocal { get; set; }
        /// <summary>
        /// 保险金额
        /// </summary>
        public string bxAccount { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string bankCardNo { get; set; }
        /// <summary>
        /// 银行卡号开户人姓
        /// </summary>
        public string accountHolder { get; set; }
        /// <summary>
        /// 患者与开户人关系
        /// </summary>
        public string holderRelation { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}