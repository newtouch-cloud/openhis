using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09
{
    /// <summary>
    /// 提取诊断信息请求体
    /// </summary>
    [XmlRoot("Request")]
    public class PullDiagInfoRequestEntity : RequestBase
    {

        /// <summary>
        /// 诊断信息体
        /// </summary>
        public DiagInfo DiagInfo { get; set; }
    }

    /// <summary>
    /// 诊断信息体
    /// </summary>
    public class DiagInfo
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
        /// 诊断明细集合
        /// </summary>
        public List<Diagnosis> diagnosislist { get; set; }
    }

    /// <summary>
    /// 诊断项
    /// </summary>
    public class Diagnosis
    {
        /// <summary>
        /// 本地诊断代码
        /// </summary>
        public string localCode { get; set; }

        /// <summary>
        /// 诊断编码（ICD10）
        /// 必填
        /// </summary>
        public string code { get; set; }

        /// <summary>
        ///诊断类别 0：西医 1：中医
        /// 必填
        /// </summary>
        public string cate { get; set; }

        /// <summary>
        ///诊断名称
        /// 必填
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///中医诊断类型 0：疾病 1：症候； 当诊断分类为中医时必填
        /// 必填
        /// </summary>
        public string type { get; set; }

        /// <summary>
        ///中医诊断分组 此标识相同的疾病和症候诊断为一组
        /// </summary>
        public string group { get; set; }

        /// <summary>
        ///中医诊断级别 0：主要诊断 1：次要诊断
        /// </summary>
        public string level { get; set; }

        /// <summary>
        ///描述
        /// </summary>
        public string description { get; set; }
    }
}