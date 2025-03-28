using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_02
{
    /// <summary>
    /// 推送草药信息 请求体
    /// </summary>
    [XmlRoot("Request")]
    public class PushMedicineInfoRequestEntity : RequestBase
    {
        /// <summary>
        /// 草药信息体
        /// </summary>
        public Medicine Medicine { get; set; }
    }

    /// <summary>
    /// 草药信息体
    /// </summary>
    public class Medicine
    {
        /// <summary>
        /// 药品编号
        /// 必填
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 药品名称
        /// 必填
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 药品编码
        /// 必填
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string specification { get; set; }

        /// <summary>
        /// 生产厂商
        /// </summary>
        public string manufacturer { get; set; }

        /// <summary>
        /// 是否启用：Y： 是 N： 否
        /// </summary>
        public string activeFlg { get; set; }

        /// <summary>
        /// 机构编码
        /// 必填
        /// </summary>
        public string orgCode { get; set; }

    }
}