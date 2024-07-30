using System.Collections.Generic;
using System.Xml.Serialization;
using FrameworkBase.MultiOrg.Domain.ValueObjects;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08
{
    /// <summary>
    /// 提取处方请求体
    /// </summary>
    [XmlRoot("Request")]
    public class PullPrescriptionRequestEntity : RequestBase
    {
        /// <summary>
        /// 处方信息体
        /// </summary>
        public Prescription Prescription { get; set; }
    }

    /// <summary>
    /// 处方信息体
    /// </summary>
    public class Prescription
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
        /// 直接选方的方子名称
        /// </summary>
        public string preName { get; set; }

        /// <summary>
        /// 药方信息集合
        /// </summary>
        public List<DrugData> drugDataList { get; set; }
    }

    /// <summary>
    /// 药方信息集合
    /// </summary>
    public class DrugData
    {

        /// <summary>
        /// 药品名称
        /// 必填
        /// </summary>
        public string drugName { get; set; }

        /// <summary>
        /// 药品编码
        /// 必填
        /// </summary>
        public string drugCode { get; set; }

        /// <summary>
        /// 药品单位
        /// </summary>
        public string drugUnit { get; set; }

        /// <summary>
        /// 数量
        /// 必填
        /// </summary>
        public string drugQuantity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string note { get; set; }
    }

    /// <summary>
    /// 药方扩展信息
    /// </summary>
    public class DrugDataEx : SfxmYpSelectResultVO
    {
        /// <summary>
        /// 药品名称
        /// 必填
        /// </summary>
        public string drugName { get; set; }

        /// <summary>
        /// 药品编码
        /// 必填
        /// </summary>
        public string drugCode { get; set; }

        /// <summary>
        /// 药品单位
        /// </summary>
        public string drugUnit { get; set; }

        /// <summary>
        /// 数量
        /// 必填
        /// </summary>
        public string drugQuantity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string note { get; set; }
    }
}