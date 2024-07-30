namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// 下拉列表物资信息
    /// </summary>
    public class DepartmentStockListQueryParamDTO
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
        /// 组织结构
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }
    }
}
