using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 出入库单据主信息
    /// </summary>
    public class VCrkdjEntity
    {
        /// <summary>
        /// 审核状态
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// 票单号
        /// </summary>
        public string Pdh { get; set; }

        /// <summary>
        /// 出库部门名称
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// 出库部门ID
        /// </summary>
        public string ckbmId { get; set; }

        /// <summary>
        /// 入库部门名称
        /// </summary>
        public string rkbmmc { get; set; }

        /// <summary>
        /// 入库部门ID
        /// </summary>
        public string rkbmId { get; set; }

        /// <summary>
        /// 出入库方式ID
        /// </summary>
        public string crkfsId { get; set; }

        /// <summary>
        /// 出入库方式名称
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime czsj { get; set; }

        /// <summary>
        /// 出库时间
        /// </summary>
        public string cksj { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public string rksj { get; set; }

        /// <summary>
        /// 零售总金额
        /// </summary>
        public decimal ljze { get; set; }

        /// <summary>
        /// 进价总金额
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// 进销总金额差价
        /// </summary>
        public decimal jxcj { get; set; }

        /// <summary>
        /// 出入库单据ID
        /// </summary>
        public long crkId { get; set; }

        /// <summary>
        /// 单据类型ID
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// 配送单号
        /// </summary>
        public string deliveryNo { get; set; }

    }
}
