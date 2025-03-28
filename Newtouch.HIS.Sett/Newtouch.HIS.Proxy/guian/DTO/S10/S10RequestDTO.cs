using Newtouch.HIS.Proxy.Attribute;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S10")]
    [XmlRoot("body")]
    public class S10RequestDTO
    {
        /// <summary>
        /// 补偿序号
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// 开方医生（跨省
        /// </summary>
        public string doctor { get; set; }
        /// <summary>
        /// 医疗证卡号（跨省）
        /// </summary>
        public string bookNo { get; set; }
        /// <summary>
        /// 患者姓名（跨省）
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 家庭编号（跨省）
        /// </summary>
        public string familyNo { get; set; }
        /// <summary>
        /// 个人编号（跨省）
        /// </summary>
        public string memberNo { get; set; }
        /// <summary>
        /// 本次上传明细条（跨省）
        /// </summary>
        public string rows { get; set; }
        /// <summary>
        /// 行政编码(跨省)
        /// </summary>
        public string areaCode { get; set; }
        /// <summary>
        /// 是否跨省就医患者1:是0:否(跨省)
        /// </summary>
        public string isTransProvincial { get; set; }

        [XmlArrayItem("detail")]
        public List<S10detail> list { get; set; }
    }

    public class S10detail
    {
        /// <summary>
        /// 明细中文名
        /// </summary>
        public string detailName { get; set; }
        /// <summary>
        /// 农合编码
        /// </summary>
        public string detailCode { get; set; }
        /// <summary>
        /// His明细序号（唯一编号）
        /// </summary>
        public string hisDetailCode { get; set; }
        /// <summary>
        /// 项目编号医院编码
        /// </summary>
        public string detailHosCode { get; set; }
        /// <summary>
        /// 类别代码(见字典目录类别)跨省结算传00
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
        /// 经办医院处方流水号唯一ID 贵阳市患者必填（跨省）
        /// </summary>
        public string recipeID { get; set; }
    }
}