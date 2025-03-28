using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S18
{
    /// <summary>
    /// 根据获取的家庭参合列表进行门诊上传 跨省登记修改也是这个接口，使用uploadType 请求报文
    /// </summary>
    [InterfaceCode("S18")]
    [XmlRoot("body")]
    public class S18RequestDTO
    {

        /// <summary>
        /// 个人编码
        /// </summary>
        public string memberId { get; set; }

        /// <summary>
        /// 就诊时间(yyyy-mm-dd)
        /// </summary>
        public string inpatientDate { get; set; }

        /// <summary>
        /// 就诊科室（见字典科室）
        /// </summary>
        public string inpatientDepartments { get; set; }

        /// <summary>
        /// 经治医生
        /// </summary>
        public string treatingPhysician { get; set; }

        /// <summary>
        /// 疾病代码(接口S29下载获取)
        /// </summary>
        public string diseaseCode { get; set; }

        /// <summary>
        /// 初步诊断
        /// </summary>
        public string initialDiagnosis { get; set; }

        /// <summary>
        /// 是否提高（1提高0不提高）
        /// </summary>
        public string isIncrease { get; set; }

        /// <summary>
        /// 是否转诊（1转诊默认0）
        /// </summary>
        public string isInfusion { get; set; }

        /// <summary>
        /// 是否扣家庭账户
        /// </summary>
        public string isAccountPay { get; set; }

        /// <summary>
        /// 是否统筹
        /// </summary>
        public string isOutpPay { get; set; }

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
        /// 输液次数
        /// </summary>
        public string infusionCount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 多诊断节点
        /// </summary>
        public string disList { get; set; }

        /// <summary>
        /// 疾病代码（多诊断）
        /// </summary>
        public string disCode { get; set; }

        /// <summary>
        /// 上传类别 0新增 1修改
        /// </summary>
        public string uploadType { get; set; }

        /// <summary>
        /// HIS门诊号（跨省）需保证本医疗机构内HIS门诊号唯一
        /// </summary>
        public string hisClinicNo { get; set; }

        /// <summary>
        /// 家庭编号（跨省）
        /// </summary>
        public string familyNo { get; set; }

        /// <summary>
        /// 身高（跨省）
        /// </summary>
        public string stature { get; set; }

        /// <summary>
        /// 体重（跨省）
        /// </summary>
        public string weight { get; set; }

        /// <summary>
        /// 第二疾病诊断（跨省）
        /// </summary>
        public string secondIcdNo { get; set; }

        /// <summary>
        /// 第三疾病诊断（跨省）
        /// </summary>
        public string threeIcdNo { get; set; }

        /// <summary>
        /// 治疗方式编码（跨省）
        /// </summary>
        public string treatCode { get; set; }

        /// <summary>
        /// 就诊类型(跨省)参见附表，群文件有文件
        /// </summary>
        public string cureCode { get; set; }

        /// <summary>
        /// 来就诊状态（跨省）参见附表
        /// </summary>
        public string inHosId { get; set; }

        /// <summary>
        /// 经办医疗机构登记流水号ID（跨省）
        /// </summary>
        public string registerID { get; set; }

        /// <summary>
        /// 农合门诊登记流水号（跨省）门诊登记修改时传入
        /// </summary>
        public string ClinicNo { get; set; }

        /// <summary>
        /// 行政编码(跨省)
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// 是否跨省就医患者1:是0:否(跨省)
        /// </summary>
        public string isTransProvincial { get; set; }

        /// <summary>
        /// 重大疾病诊断编码
        /// </summary>
        public string majorDiseaseICD { get; set; }

        /// <summary>
        /// 医生级别
        /// </summary>
        public string docatorRank { get; set; }

    }
}