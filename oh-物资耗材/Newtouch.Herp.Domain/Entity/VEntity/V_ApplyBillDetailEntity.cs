namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// 申领单明细
    /// </summary>
    public class VApplyBillDetailEntity
    {
        /// <summary>
        /// 申领单ID
        /// </summary>
        public long sldId { get; set; }

        /// <summary>
        /// 申领单号
        /// </summary>
        public string sldh { get; set; }

        /// <summary>
        /// 入库部门ID
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// 申领单明细ID
        /// </summary>
        public long sldmxId { get; set; }

        /// <summary>
        /// 物资ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// 申领数量,部门单位
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 申领数量
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// 已发数量
        /// </summary>
        public string yfslStr { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// 可用库存数量
        /// </summary>
        public int kykcsl { get; set; }

        /// <summary>
        /// 部门单位剩余数量
        /// </summary>
        public int bmdwsysl { get; set; }

        /// <summary>
        /// 现发数量，默认等于部门单位剩余数量
        /// </summary>
        public int xfsl { get; set; }

        /// <summary>
        /// 部门单位ID
        /// </summary>
        public string bmdwId { get; set; }

        /// <summary>
        /// 部门单位名称
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 最小单位名称
        /// </summary>
        public string zxdwmc { get; set; }

        /// <summary>
        /// 可用库存数量
        /// </summary>
        public string kyslStr { get; set; }

        /// <summary>
        /// 最小单位剩余数量
        /// </summary>
        public int zxdwsysl { get; set; }
    }
}