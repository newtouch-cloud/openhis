namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// 下拉列表物资信息
    /// </summary>
    public class DepartmentStockListQueryParam
    {
        /// <summary>
        /// keyword 物资名称/拼音
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 当前库房
        /// </summary>
        public string warehouseId { get; set; }

        /// <summary>
        /// 配送单号
        /// </summary>
        public string deliveryNo { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public string gysId { get; set; }

        /// <summary>
        /// 组织结构
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }
    }
}
