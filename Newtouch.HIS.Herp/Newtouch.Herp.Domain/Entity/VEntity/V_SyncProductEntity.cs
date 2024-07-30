
namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 物资同步
    /// </summary>
    public class VSyncProductEntity
    {
        /// <summary>
        /// rel_productWarehouse 表主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 物资类别
        /// </summary>
        public string wzlb { get; set; }

        /// <summary>
        /// 物资ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 最小单位
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// 部门单位
        /// </summary>
        public string bmdw { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// 控制标志  1：有效  0：无效（被控制）
        /// </summary>
        public string zt { get; set; }
    }
}
