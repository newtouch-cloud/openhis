using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [XmlRoot("list")]
    public class S10ResponseDTO
    {
        [XmlArrayItem("item")]
        public List<S10item> list { get; set; }
    }

    public class S10item
    {
        /// <summary>
        /// 住院处方流水号
        /// </summary>
        public decimal? detailNo { get; set; }
        /// <summary>
        /// His明细序号
        /// </summary>
        public string hisDetailCode { get; set; }
        /// <summary>
        /// 明细中文名
        /// </summary>
        public string detailName { get; set; }
        /// <summary>
        /// 药品可报比例
        /// </summary>
        public string enableRatio { get; set; }
        /// <summary>
        /// 药品可报金额
        /// </summary>
        public string enableMoney { get; set; }
        /// <summary>
        /// 是否为基本药品0不是1是
        /// </summary>
        public string essentialMedicine { get; set; }
        /// <summary>
        /// 是否为中药 0 不是  1 是
        /// </summary>
        public string chineseMedicine { get; set; }
        /// <summary>
        /// 1：成功 0：失败
        /// </summary>
        public string success { get; set; }
        /// <summary>
        /// 失败消息
        /// </summary>
        public string message { get; set; }
    }
}