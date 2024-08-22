using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{

    public class detail
    {
        /// <summary>
        /// 项目明细序号
        /// </summary>
        public string detailId { get; set; }
        /// <summary>
        /// 补偿序号
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// 农合编码
        /// </summary>
        public string detailCode { get; set; }
        /// <summary>
        /// 类别代码(见字典目录类别)
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
        /// 补偿比例
        /// </summary>
        public decimal scale { get; set; }
        /// <summary>
        /// 医保范围
        /// </summary>
        public string scope { get; set; }
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
    }
    
}