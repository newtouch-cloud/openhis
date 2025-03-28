using System.Collections.Generic;

namespace Newtouch.HIS.Proxy.guian.DTO.S21
{
    /// <summary>
    /// 查询当此门诊已上传费用明细 返回报文
    /// </summary>
    public class S21ResponseDTO
    {
        /// <summary>
        /// 本次完成交易的明细数据量
        /// </summary>
        public decimal num { get; set; }

        /// <summary>
        /// 明细
        /// </summary>
        public List<item> list { get; set; }
    }

    /// <summary>
    /// 项目
    /// </summary>
    public class item
    {
        /// <summary>
        /// 门诊处方流水号
        /// </summary>
        public string detailNo  { get; set; }

        /// <summary>
        /// His明细序号（唯一编号）
        /// </summary>
        public string hisDetailCode { get; set; }

        /// <summary>
        /// 明细中文名
        /// </summary>
        public string detailName { get; set; }

    }
}