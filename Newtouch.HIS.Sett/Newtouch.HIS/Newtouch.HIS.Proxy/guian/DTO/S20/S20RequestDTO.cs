using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S20
{
    /// <summary>
    /// 根据S18上传门诊返回的补偿序号outpId进行门诊信息修改 请求报文
    /// </summary>
    [InterfaceCode("S20")]
    [XmlRoot("body")]
    public class S20RequestDTO
    {
        /// <summary>
        /// 门诊补偿序号
        /// </summary>
        public string outpId { get; set; }

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
        /// 是否扣家庭账户
        /// </summary>
        public string isAccountPay { get; set; }

        /// <summary>
        /// 是统筹
        /// </summary>
        public string isOutpPay { get; set; }

        /// <summary>
        /// 是否提高（1提高0不提高）
        /// </summary>
        public string isIncrease { get; set; }

        /// <summary>
        /// 是否转诊（1转诊默认0）
        /// </summary>
        public string isReferra { get; set; }

        /// <summary>
        /// 病人类型（见字典病人类型）
        /// </summary>
        public string inpatientTypeOflocal { get; set; }

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