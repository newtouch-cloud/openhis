using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 单据查询明细
    /// </summary>
    public class ReceiptQueryDetailVO
    {
        /// <summary>
        /// 出入库单据明细
        /// </summary>
        public string crkmxId { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        ///发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 药品大类代码
        /// </summary>
        public string ypdlCode { get; set; }

        /// <summary>
        /// 药品类别名称
        /// </summary>
        public string yplbmc { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        ///生产厂家
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 数量 申请数量
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// 数量 带单位
        /// </summary>
        public string slanddw { get; set; }

        /// <summary>
        /// 批发总额
        /// </summary>
        public decimal? pjze { get; set; }

        /// <summary>
        ///零售总额
        /// </summary>
        public decimal? ljze { get; set; }

        /// <summary>
        /// 进价总额
        /// </summary>
        public decimal? zje { get; set; }

        /// <summary>
        ///扣率
        /// </summary>
        public decimal? kl { get; set; }

        /// <summary>
        ///进销差价
        /// </summary>
        public decimal? jxcj { get; set; }

        ///// <summary>
        ///// 账册号
        ///// </summary>
        //public string zch { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 进价
        /// </summary>
        public decimal? jj { get; set; }

        /// <summary>
        /// 进价单位单价
        /// </summary>
        public string jjdwdj { get; set; }

        /// <summary>
        /// 退货原因
        /// </summary>
        public string thyy { get; set; }
    }
}
