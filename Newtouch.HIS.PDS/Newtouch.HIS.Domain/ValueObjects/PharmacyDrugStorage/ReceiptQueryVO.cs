using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 单据查询主记录
    /// </summary>
    public class ReceiptQueryVO
    {
        /// <summary>
        /// 单据类型
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// 单据类型名称
        /// </summary>
        public string djlxmc { get; set; }

        /// <summary>
        /// 出库部门名称
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// 入库部门名称
        /// </summary>
        public string rkbmmc { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string gysmc { get; set; }

        /// <summary>
        /// 出入库单据主表ID
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string pdh { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? czsj { get; set; }

        /// <summary>
        /// 出入库方式名称
        /// </summary>
        public string crkfsmc { get; set; }
        public string crkfscode { get; set; }
        /// <summary>
        /// 批发总额
        /// </summary>
        public decimal? pjze { get; set; }

        /// <summary>
        /// 零售总额
        /// </summary>
        public decimal? ljze { get; set; }

        /// <summary>
        /// 进价总额
        /// </summary>
        public decimal? zje { get; set; }

        /// <summary>
        /// 进销差价
        /// </summary>
        public decimal? jxcj { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime? Rksj { get; set; }

        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime? Cksj { get; set; }
        /// <summary>
        /// 进价总金额
        /// </summary>
        public decimal? jjzje { get; set; }
    }
}
