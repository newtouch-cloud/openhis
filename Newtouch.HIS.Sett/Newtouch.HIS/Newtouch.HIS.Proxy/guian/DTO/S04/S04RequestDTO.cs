using Newtouch.HIS.Proxy.Attribute;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    /// <summary>
    /// 根据获取的家庭参合列表进行入院办理 跨省修改也是这个接口 uploadType节点控制 请求报文
    /// </summary>
    [InterfaceCode("S04")]
    [XmlRoot("body")]
    public class S04RequestDTO
    {
        /// <summary>
        /// 个人编码
        /// </summary>
        public string memberId { get; set; }
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
        /// <summary>
        /// 重大疾病申请序号
        /// </summary>
        public string bigDiseaseNo { get; set; }
        /// <summary>
        /// 转诊号
        /// </summary>
        public string isReferraNo { get; set; }
        /// <summary>
        /// 上传类别 0新增 1修改
        /// </summary>
        public string uploadType { get; set; }
        /// <summary>
        /// 家庭编号（跨省）
        /// </summary>
        public string familySysno { get; set; }
        /// <summary>
        /// 身高（跨省）
        /// </summary>
        public string Stature { get; set; }
        /// <summary>
        /// 体重（跨省）
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 第二疾病诊断（icd编码）（跨省）
        /// </summary>
        public string secondIcdNo { get; set; }
        /// <summary>
        /// 第三疾病诊断（icd编码）（跨省）
        /// </summary>
        public string threeIcdNo { get; set; }
        /// <summary>
        /// 手术编号（跨省）
        /// </summary>
        public string opsId { get; set; }
        /// <summary>
        /// 治疗方式编码（跨省）
        /// </summary>
        public string treatCode { get; set; }
        /// <summary>
        /// 就诊类型（跨省）群文件有文档
        /// </summary>
        public string cureId { get; set; }
        /// <summary>
        /// 并发症（跨省）传名称，只能传一个
        /// </summary>
        public string complication { get; set; }
        /// <summary>
        /// 转诊类型（0 正常住院 1 县外就医转诊 2 转院）（跨省）
        /// </summary>
        public string turnMode { get; set; }
        /// <summary>
        /// 转诊转院编码（县外就医转诊-该值为转诊申请单号；转院-该值为转院住院登记号）（跨省）
        /// </summary>
        public string turnCode { get; set; }
        /// <summary>
        /// 转院日期(yyyy-mm-dd hh:mm:ss)（跨省）
        /// </summary>
        public string turnDate { get; set; }
        /// <summary>
        /// 医院住院收费收据号（跨省）
        /// </summary>
        public string ticketNo { get; set; }
        /// <summary>
        /// 民政通知书号（跨省）
        /// </summary>
        public string ministerNotice { get; set; }
        /// <summary>
        /// 生育证号（跨省）
        /// </summary>
        public string procreateNotice { get; set; }
        /// <summary>
        /// 是否新生儿入院1：是0：否（跨省）
        /// </summary>
        public string isNewborn { get; set; }
        /// <summary>
        /// isNewborn=1时不可为空（跨省）
        /// </summary>
        public string newbornBirthday { get; set; }
        /// <summary>
        /// isNewborn=1时不可为空（跨省）
        /// </summary>
        public string newbornName { get; set; }
        /// <summary>
        /// isNewborn=1时不可为空1:男2:女（跨省）
        /// </summary>
        public string newbornSex { get; set; }
        /// <summary>
        /// 经办医院登记流水号唯一ID（跨省）
        /// </summary>
        public string registerID { get; set; }
        /// <summary>
        /// 住院登记流水号住院登记修改时传入（跨省）
        /// </summary>
        public string inpatientSn { get; set; }
        /// <summary>
        /// 行政编码(跨省)
        /// </summary>
        public string areaCode { get; set; }
        /// <summary>
        /// 是否跨省就医患者1:是0:否(跨省)
        /// </summary>
        public string isTransProvincial { get; set; }
        /// <summary>
        /// 重大疾病主诊断组编码
        /// </summary>
        public string majorDiseaseICD { get; set; }
        /// <summary>
        /// 重大疾病第二诊断组编码
        /// </summary>
        public string secondMajorDiseaseICD { get; set; }
        /// <summary>
        /// 重大疾病第三诊断组编码
        /// </summary>
        public string threeMajorDiseaseICD { get; set; }
        /// <summary>
        /// 第二治疗方式编码，对应第二诊断
        /// </summary>
        public string secondTreatCode { get; set; }
        /// <summary>
        /// 第三治疗方式编码，对应第三诊断
        /// </summary>
        public string threeTreatCode { get; set; }
        /// <summary>
        /// 多诊断节点
        /// </summary>
        public List<valueList> disList { get; set; }
    }

    public class valueList
    {
        /// <summary>
        /// 疾病代码（多诊断）
        /// </summary>
        public string disCode { get; set; }
    }
}