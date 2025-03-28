using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07
{
    /// <summary>
    /// 提取电子病历响应体
    /// </summary>
    [XmlRoot("Request")]
    public class PullRecordRequestEntity : RequestBase
    {
        /// <summary>
        /// 病历信息体
        /// </summary>
        public Record Record { get; set; }
    }

    /// <summary>
    /// 病历信息体
    /// </summary>
    public class Record
    {
        /// <summary>
        /// 就诊流水号
        /// 必填
        /// </summary>
        public string serialNo { get; set; }

        /// <summary>
        /// 机构编码
        /// 必填
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// 患者编号
        /// 必填
        /// </summary>
        public string patiId { get; set; }

        /// <summary>
        /// 患者姓名
        /// 必填
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 患者年龄
        /// 必填
        /// </summary>
        public string age { get; set; }

        /// <summary>
        /// 患者性别
        /// 必填
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// 就诊日期-YYYYMMDD
        /// 必填
        /// </summary>
        public string clinicDate { get; set; }

        /// <summary>
        /// 医生编号（用户名）
        /// 必填
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// 医生名称
        /// 必填
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        public string priDepict { get; set; }

        /// <summary>
        /// localDisHis
        /// </summary>
        public string localDisHis { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        public string allergicHis { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        public string anamnesis { get; set; }

        /// <summary>
        /// 个人史
        /// </summary>
        public string individualHis { get; set; }

        /// <summary>
        /// 月经婚育史
        /// </summary>
        public string mcObstetrical { get; set; }

        /// <summary>
        /// 家族史
        /// </summary>
        public string fhx { get; set; }

        /// <summary>
        /// 望诊
        /// </summary>
        public string inspection { get; set; }

        /// <summary>
        /// 闻诊
        /// </summary>
        public string smelling { get; set; }

        /// <summary>
        /// 切诊
        /// </summary>
        public string palpation { get; set; }

        /// <summary>
        /// 辨证要点
        /// </summary>
        public string syndrome { get; set; }

        /// <summary>
        /// 体格检查
        /// </summary>
        public string psyCheck { get; set; }

        /// <summary>
        /// 医生建议
        /// </summary>
        public string suggession { get; set; }

        /// <summary>
        /// 医嘱编号
        /// </summary>
        public string recordId { get; set; }
    }
}