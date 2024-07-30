using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// 出院结算金额支付相关
    /// </summary>
    public class InpatientSettFeeRelatedDTO
    {
        /// <summary>
        /// 结算总金额（dj*sl之和）
        /// </summary>
        public decimal? zje { get; set; }
        /// <summary>
        /// 原支付应收
        /// </summary>
        public decimal? orglxjzfys { get; set; }

        /// <summary>
        /// 支付应收（原支付应收打折后）
        /// </summary>
        public decimal? xjzfys { get; set; }
        /// <summary>
        /// 实收款
        /// </summary>
        public decimal? ssk { get; set; }
        public string zffs1 { get; set; }
        public decimal? zfje1 { get; set; }
        public string zffs2 { get; set; }
        public decimal? zfje2 { get; set; }
        /// <summary>
        /// 找零
        /// </summary>
        public decimal? zhaoling { get; set; }
        /// <summary>
        /// 抵用交易流水号，重庆医保账户抵用
        /// </summary>
        public string dyjylsh { get; set; }
        public string ver { get; set; }
        /// <summary>
        /// 医保交易流水号
        /// </summary>
        public string ybjslsh { get; set; }

        #region new
        /// <summary>
        /// 预交金支付金额
        /// </summary>
        public decimal? yjjzfje { get; set; }
        /// <summary>
        /// 预交金可退余额
        /// </summary>
        public decimal? yjjtye { get; set; }
        /// <summary>
        /// 窗口实收金额
        /// </summary>
        public decimal? djjess { get; set; }
        /// <summary>
        /// 窗口实收支付方式
        /// </summary>
        public string djjesszffs { get; set; }

        /// <summary>
        /// 折扣比例
        /// </summary>
        public decimal? zkbl { get; set; }
        public decimal jmje { get; set; }

        /// <summary>
        /// 电子凭证码
        /// </summary>
        public string ecToken { get; set; }

        public List<PatZfList> patZflist { get; set; }
        public decimal? xjwc { get; set; }
        #endregion
    }
}
