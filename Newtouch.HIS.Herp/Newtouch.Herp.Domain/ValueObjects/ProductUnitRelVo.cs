namespace Newtouch.Herp.Domain.ValueObjects
{
    /// <summary>
    /// 物资单位关联关系
    /// </summary>
    public class ProductUnitRelVo
    {
        /// <summary>
        /// gys_contacts Id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string unitId { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string unitName { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public string keyWord { get; set; }
    }
}
