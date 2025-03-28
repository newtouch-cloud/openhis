using System.Collections.Generic;
using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S21
{
    /// <summary>
    /// 根据S18门诊上传返回的补偿序号outpId进行门诊费用上传,结算后上传无效 请求报文
    /// </summary>
    [InterfaceCode("S21")]
    [XmlRoot("body")]
    public class S21RequestDTO
    {
        /// <summary>
        /// 补偿序号
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        /// 上传明细
        /// </summary>
        public List<detail> list { get; set; }
    }

    /// <summary>
    /// 明细
    /// </summary>
    public class detail
    {
        /// <summary>
        /// 门诊处方流水号(跨省)
        /// </summary>
        public string clinicRxNo { get; set; }

        /// <summary>
        ///  开方医生(跨省)
        /// </summary>
        public string doctor { get; set; }

        /// <summary>
        ///    行政编码(跨省)
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        ///  是否跨省就医患者1:是0:否(跨省)
        /// </summary>
        public string isTransProvincial { get; set; }

        /// <summary>
        ///  明细中文名
        /// </summary>
        public string detailName { get; set; }

        /// <summary>
        ///   农合编码
        /// </summary>
        public string detailCode { get; set; }

        /// <summary>
        ///  His明细序号（唯一编号
        /// </summary>
        public string hisDetailCode { get; set; }

        /// <summary>
        ///  本地编码PDA可以上传农合编码 
        /// </summary>
        public string detailHosCode { get; set; }

        /// <summary>
        ///  类别代码(见字典目录类别)
        /// </summary>
        public string typeCode { get; set; }

        /// <summary>
        /// 数量（四位小数精度）
        /// </summary>
        public decimal num { get; set; }

        /// <summary>
        /// 单价（四位小数精度）
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// 总价（四位小数精度）
        /// </summary>
        public decimal totalCost { get; set; }

        /// <summary>
        /// 用药日期(yyyy-mm-dd)
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string standard { get; set; }

        /// <summary>
        /// 剂型 
        /// </summary>
        public string formulations { get; set; }

        /// <summary>
        /// 经办医院处方流水号唯一ID贵阳市患者必填(跨省)
        /// </summary>
        public string recipeID { get; set; }

    }

    /// <summary>
    /// 门诊退费信息
    /// </summary>
    public class TFS21RequestDTO
    {
        /// <summary>
        /// S21请求报文
        /// </summary>
        public S21RequestDTO S21RequestDto { get; set; }

        /// <summary>
        /// 退款总金额
        /// </summary>
        public decimal tkzje { get; set; }

        /// <summary>
        /// 再次结算医保总金额
        /// </summary>
        public decimal ybzje { get; set; }

        /// <summary>
        /// 再次结算自立总金额
        /// </summary>
        public decimal zlzje { get; set; }
    }
}