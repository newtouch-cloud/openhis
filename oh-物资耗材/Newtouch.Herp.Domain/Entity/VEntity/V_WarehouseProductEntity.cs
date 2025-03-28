namespace Newtouch.Herp.Domain.Entity.VEntity
{
    public class VWarehouseProductEntity
    {
        /// <summary>
        /// 物资ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// 控制标志
        /// </summary>
        public string kzbz { get; set; }

        /// <summary>
        /// 物资状态
        /// </summary>
        public string wzzt { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string lb { get; set; }

        /// <summary>
        /// 最小起订数  最小单位
        /// </summary>
        public int? zxqds { get; set; }

        /// <summary>
        /// 零售价  最小单位
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 最小单位名称
        /// </summary>
        public string zxdwmc { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// 是否零库存
        /// </summary>
        public string sflkc { get; set; }

        /// <summary>
        /// 是否复用
        /// </summary>
        public string sffy { get; set; }

        /// <summary>
        /// 是否跟台
        /// </summary>
        public string sfgt { get; set; }
        /// <summary>
        /// 国家医保代码
        /// </summary>
        public string gjybdm { get; set; }
    }
}
