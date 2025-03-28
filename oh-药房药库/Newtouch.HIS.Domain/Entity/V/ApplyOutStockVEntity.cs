namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// 申领出库明细
    /// </summary>
    public class ApplyOutStockVEntity
    {
        /// <summary>
        /// 申领部门 入库部门
        /// </summary>
        public string slbm { get; set; }

        /// <summary>
        /// 申领单明细ID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// 申领单ID
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// 申领单号
        /// </summary>
        public string sldh { get; set; }

        /// <summary>
        /// 先发数 最小单位
        /// </summary>
        public int xfsl { get; set; }

        /// <summary>
        /// 剩余待发数量 初次加载与xfsl相同 最小单位
        /// </summary>
        public int sysl { get; set; }

        /// <summary>
        /// 最小单位
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// 转化因子 目前为最小单位转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// 现发数 最小单位
        /// </summary>
        public int zxdwxfsl { get; set; }

        /// <summary>
        /// 包装数
        /// </summary>
        public int bzs { get; set; }

        /// <summary>
        /// 已发数量  带单位
        /// </summary>
        public string yfslStr { get; set; }

        /// <summary>
        /// 申领数量 带单位
        /// </summary>
        public string slslStr { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 可用库存 带单位
        /// </summary>
        public string kyslStr { get; set; }

        /// <summary>
        /// 可用库存 最小单位
        /// </summary>
        public int kykc { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// 批发单位单价
        /// </summary>
        public string pfjdwdj { get; set; }

        /// <summary>
        /// 零售单位单价
        /// </summary>
        public string lsjdwdj { get; set; }

        /// <summary>
        /// 批发总额
        /// </summary>
        public decimal pjze { get; set; }

        /// <summary>
        /// 零售总额
        /// </summary>
        public decimal lsze { get; set; }

        /// <summary>
        /// 最小单位进价
        /// </summary>
        public decimal? zxdwjj { get; set; }

        /// <summary>
        /// 最小单位批发价
        /// </summary>
        public decimal zxdwpfj { get; set; }

        /// <summary>
        /// 最小单位零售价
        /// </summary>
        public decimal zxdwlsj { get; set; }
    }
}
